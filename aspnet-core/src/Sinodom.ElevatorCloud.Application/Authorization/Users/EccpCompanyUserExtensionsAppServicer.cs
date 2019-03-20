
namespace Sinodom.ElevatorCloud.Authorization.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.Authorization.Users;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;
    using Sinodom.ElevatorCloud.Authorization.Roles;
    using Sinodom.ElevatorCloud.Authorization.Users.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions;
    using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos;
    using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;

    /// <summary>
    /// The eccp company user extensions app servicer.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserExtensions)]
    public class EccpCompanyUserExtensionsAppServicer : ElevatorCloudAppServiceBase,
                                                        IEccpCompanyUserExtensionsAppServicer
    {
        /// <summary>
        /// The _eccp base maintenance company.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompany;

        /// <summary>
        /// The _eccp base property company.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompany;

        /// <summary>
        /// The _eccp company user audit log.
        /// </summary>
        private readonly IRepository<EccpCompanyUserAuditLog, int> _eccpCompanyUserAuditLog;

        /// <summary>
        /// The _eccp company user extension.
        /// </summary>
        private readonly IRepository<EccpCompanyUserExtension, int> _eccpCompanyUserExtension;

        /// <summary>
        /// The _eccp editions type.
        /// </summary>
        private readonly IRepository<ECCPEditionsType, int> _eccpEditionsType;
        /// <summary>
        /// The _eccp editions type.
        /// </summary>
        private readonly IRepository<UserRole, long> _userRole;
        /// <summary>
        /// The _eccp editions type.
        /// </summary>
        private readonly IRepository<Role, int> _role;



        /// <summary>
        /// Initializes a new instance of the <see cref="EccpCompanyUserExtensionsAppServicer"/> class.
        /// </summary>
        /// <param name="eccpCompanyUserExtension">
        /// The eccp company user extension.
        /// </param>
        /// <param name="eccpBasePropertyCompany">
        /// The eccp base property company.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompany">
        /// The eccp base maintenance company.
        /// </param>
        /// <param name="eccpEditionsType">
        /// The eccp editions type.
        /// </param>
        /// <param name="eccpCompanyUserAuditLog">
        /// The eccp company user audit log.
        /// </param>
        public EccpCompanyUserExtensionsAppServicer(
            IRepository<EccpCompanyUserExtension, int> eccpCompanyUserExtension,
            IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompany,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompany,
            IRepository<ECCPEditionsType, int> eccpEditionsType,
            IRepository<EccpCompanyUserAuditLog, int> eccpCompanyUserAuditLog, IRepository<UserRole, long> userRole, IRepository<Role, int> role)
        {
            this._eccpCompanyUserExtension = eccpCompanyUserExtension;
            this._eccpBasePropertyCompany = eccpBasePropertyCompany;
            this._eccpBaseMaintenanceCompany = eccpBaseMaintenanceCompany;
            this._eccpEditionsType = eccpEditionsType;
            this._eccpCompanyUserAuditLog = eccpCompanyUserAuditLog;
            this._userRole = userRole;
            this._role = role;
        }

        /// <summary>
        /// The get users.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<AssociationUseListDto>> GetUsers(GetAssociationUsersInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var propertyUsers = from user in this.UserManager.Users
                                    join compayUser in this._eccpCompanyUserExtension.GetAll() on user.Id equals
                                        compayUser.UserId
                                    join propertyCompany in this._eccpBasePropertyCompany.GetAll() on user.TenantId
                                        equals propertyCompany.TenantId
                                    where compayUser.CheckState == input.CheckState
                                    select new AssociationUseListDto
                                    {
                                        Id = user.Id,
                                        Name = user.Name,
                                        Surname = user.Surname,
                                        UserName = user.UserName,
                                        EmailAddress = user.EmailAddress,
                                        CompanyName = propertyCompany.Name,
                                        Mobile = compayUser.Mobile,
                                        CheckState = compayUser.CheckState,
                                        IsActive = user.IsActive,
                                        CreationTime = user.CreationTime,
                                        CompanyType = 1
                                    };

                propertyUsers = propertyUsers.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u => u.Name.Contains(input.Filter) || u.Surname.Contains(input.Filter)
                                                       || u.UserName.Contains(input.Filter)
                                                       || u.EmailAddress.Contains(input.Filter)
                                                       || u.CompanyName.Contains(input.Filter)
                                                       || u.Mobile.Contains(input.Filter));

                var eccpBaseMaintenanceCompany = this._eccpBaseMaintenanceCompany.GetAll();

                var eccpCompanyUserExtension = this._eccpCompanyUserExtension.GetAll();

                var maintenanceUsers = from user in this.UserManager.Users
                                       join compayUser in eccpCompanyUserExtension on user.Id equals compayUser.UserId
                                       join maintenanceCompany in eccpBaseMaintenanceCompany on user.TenantId equals
                                           maintenanceCompany.TenantId
                                       where compayUser.CheckState == input.CheckState
                                       select new AssociationUseListDto
                                       {
                                           Id = user.Id,
                                           Name = user.Name,
                                           Surname = user.Surname,
                                           UserName = user.UserName,
                                           EmailAddress = user.EmailAddress,
                                           CompanyName = maintenanceCompany.Name,
                                           Mobile = compayUser.Mobile,
                                           CheckState = compayUser.CheckState,
                                           IsActive = user.IsActive,
                                           CreationTime = user.CreationTime,
                                           CompanyType = 2
                                       };

                maintenanceUsers = maintenanceUsers.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u => u.Name.Contains(input.Filter) || u.Surname.Contains(input.Filter)
                                                       || u.UserName.Contains(input.Filter)
                                                       || u.EmailAddress.Contains(input.Filter)
                                                       || u.CompanyName.Contains(input.Filter)
                                                       || u.Mobile.Contains(input.Filter));

                var associationUseList = propertyUsers.Union(maintenanceUsers);

                var associationUseCount = await associationUseList.CountAsync();

                var associationUserList = await associationUseList
                                              .OrderByDescending(e => e.Id).PageBy(input).ToListAsync();
                return new PagedResultDto<AssociationUseListDto>(associationUseCount, associationUserList);
            }
        }
        /// <summary>
        /// 工单转接获取人员
        /// </summary>
        /// <returns></returns>
        public  List<WorkOrderUserListDto> GetWorkOrderUsers()
        {
            var Users = from user in this.UserManager.Users                       
                        join userRole in this._userRole.GetAll() on user.Id equals userRole.UserId
                        join role in this._role.GetAll() on userRole.RoleId equals role.Id
                        where (role.Name == "MainUser" || role.Name == "MaintPrincipal") && user.TenantId == AbpSession.TenantId  &&     user.Id  !=  AbpSession.UserId
                        select new WorkOrderUserListDto
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            Name = user.Name
                        };
            return Users.ToList();
        }
        /// <summary>
        /// 修改审核状态
        /// </summary>
        /// <param name="UserId">
        /// </param>
        /// <param name="CheckState">
        /// </param>
        /// <param name="Remarks">
        /// The Remarks.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyUserExtensions_EditState)]
        public async Task UpdateCompanyUserCheckStateAsync(int UserId, int CheckState, string Remarks)
        {
            var model = await this._eccpCompanyUserExtension.FirstOrDefaultAsync(m => m.UserId == UserId);
            model.CheckState = CheckState;

            await this._eccpCompanyUserExtension.UpdateAsync(model);

            var logEntity = new CreateOrEditEccpCompanyUserAuditLogDto
                                {
                                    UserId = UserId, CheckState = CheckState == 1 ? true : false, Remarks = Remarks
                                };

            var eccpCompanyUserAuditLog = this.ObjectMapper.Map<EccpCompanyUserAuditLog>(logEntity);

            await this._eccpCompanyUserAuditLog.InsertAsync(eccpCompanyUserAuditLog);
        }
    }
}