using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Authorization.Permissions;
using Sinodom.ElevatorCloud.Authorization.Permissions.Dto;
using Sinodom.ElevatorCloud.Authorization.Roles.Dto;

namespace Sinodom.ElevatorCloud.Authorization.Roles
{
    using Abp.Domain.Repositories;

    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy;

    /// <summary>
    /// Application service that is used by 'role management' page.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_Roles)]
    public class RoleAppService : ElevatorCloudAppServiceBase, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly TenantManager _tenantManager;
        private readonly IRepository<ECCPEditionPermission> _eccpEditionPermissionRepository;

        public RoleAppService(RoleManager roleManager, TenantManager tenantManager, IRepository<ECCPEditionPermission> eccpEditionPermissionRepository)
        {
            _roleManager = roleManager;
            _tenantManager = tenantManager;
            _eccpEditionPermissionRepository = eccpEditionPermissionRepository;
        }

        public async Task<ListResultDto<RoleListDto>> GetRoles(GetRolesInput input)
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Create, AppPermissions.Pages_Administration_Roles_Edit)]
        public async Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input)
        {
            List<FlatPermissionDto> flatPermissionDtos = new List<FlatPermissionDto>();

            if (!this.AbpSession.TenantId.HasValue)
            {
                var permissions = this.PermissionManager.GetAllPermissions();
                flatPermissionDtos = this.ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList();
            }
            else
            {
                var tenant = await _tenantManager.GetByIdAsync(AbpSession.TenantId.Value);
                if (tenant.EditionId.HasValue)
                {
                    var permissions = this.PermissionManager.GetAllPermissions();
                    var editionGrantedPermissions = await this._eccpEditionPermissionRepository.GetAllListAsync(e => e.EditionId == tenant.EditionId.Value && e.IsGranted);
                    var editionPermissions = new List<Permission>();
                    foreach (Permission permission in permissions)
                    {
                        if (editionGrantedPermissions.Any(e => e.Name == permission.Name))
                        {
                            editionPermissions.Add(permission);
                        }
                    }

                    flatPermissionDtos = this.ObjectMapper.Map<List<FlatPermissionDto>>(editionPermissions).OrderBy(p => p.DisplayName).ToList();
                }
            }

            var grantedPermissions = new Permission[0];
            RoleEditDto roleEditDto;

            if (input.Id.HasValue) //Editing existing role?
            {
                var role = await _roleManager.GetRoleByIdAsync(input.Id.Value);
                grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                roleEditDto = ObjectMapper.Map<RoleEditDto>(role);
            }
            else
            {
                roleEditDto = new RoleEditDto();
            }

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = flatPermissionDtos,
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        public async Task CreateOrUpdateRole(CreateOrUpdateRoleInput input)
        {
            if (input.Role.Id.HasValue)
            {
                await UpdateRoleAsync(input);
            }
            else
            {
                await CreateRoleAsync(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Delete)]
        public async Task DeleteRole(EntityDto input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            var users = await UserManager.GetUsersInRoleAsync(role.Name);
            foreach (var user in users)
            {
                CheckErrors(await UserManager.RemoveFromRoleAsync(user, role.Name));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Edit)]
        protected virtual async Task UpdateRoleAsync(CreateOrUpdateRoleInput input)
        {
            Debug.Assert(input.Role.Id != null, "input.Role.Id should be set.");

            var role = await _roleManager.GetRoleByIdAsync(input.Role.Id.Value);
            role.DisplayName = input.Role.DisplayName;
            role.IsDefault = input.Role.IsDefault;

            await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Create)]
        protected virtual async Task CreateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var role = new Role(AbpSession.TenantId, input.Role.DisplayName) { IsDefault = input.Role.IsDefault };
            CheckErrors(await _roleManager.CreateAsync(role));
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the role.
            await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
        }

        private async Task UpdateGrantedPermissionsAsync(Role role, List<string> grantedPermissionNames)
        {
            var grantedPermissions = PermissionManager.GetPermissionsFromNamesByValidating(grantedPermissionNames);
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }

        public async Task RolePermissionAssignment()
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var roles = _roleManager.Roles.Where(e => e.Name != StaticRoleNames.Tenants.Admin && e.Name != StaticRoleNames.Tenants.User);
                foreach (var role in roles)
                {
                    var i = (await _roleManager.GetGrantedPermissionsAsync(role)).Count;
                    if (i <= 0)
                    {
                        List<string> grantedPermissionNames = new List<string>();
                        switch (role.Name)
                        {
                            case StaticRoleNames.UseTenants.UseInfoManage:
                                //维保报告生成
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel);
                                break;
                            case StaticRoleNames.UseTenants.UseManage:
                                //维保工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                                //维保计划
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_ClosePlan);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                                //临期工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                                //临时工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                                //维保报告生成
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel);
                                break;
                            case StaticRoleNames.UseTenants.UseLeader:
                                //维保合同
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts_StopContract);
                                break;
                            case StaticRoleNames.UseTenants.UseUser:
                                //维保工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                                //临期工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                                //临时工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                                break;
                            case StaticRoleNames.MaintenanceTenants.MainManage:
                                //维保合同
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_Administration_EccpMaintenanceContracts_StopContract);
                                //维保工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                                //维保计划
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_ClosePlan);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                                //临期工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                                //临时工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                                //维保报告生成
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel);
                                //维保工单转接
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers_Audit);
                                //问题工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Audit);
                                //维保工作
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorks);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorks_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorks_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorks_Delete);
                                //维保工作流程
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkFlows);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Delete);
                                break;
                            case StaticRoleNames.MaintenanceTenants.MainInfoManage:
                                //维保报告生成
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel);
                                //维保工单转接
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers_Audit);
                                //问题工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Audit);
                                break;
                            case StaticRoleNames.MaintenanceTenants.MaintPrincipal:
                                //维保工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                                //维保计划
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_ClosePlan);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders);
                                //临期工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                                //临时工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                                //计划模板
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplates);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplates_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplates_Delete);
                                //模板节点
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Delete);
                                break;
                            case StaticRoleNames.MaintenanceTenants.MainUser:
                                //维保工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations);
                                //临期工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders);
                                //临时工单
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete);
                                //计划模板
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplates);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplates_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplates_Delete);
                                //模板节点
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit);
                                grantedPermissionNames.Add(AppPermissions
                                    .Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Delete);
                                break;
                        }
                        await UpdateGrantedPermissionsAsync(role, grantedPermissionNames);
                    }
                }
            }
        }
    }
}
