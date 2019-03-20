using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.LanFlows.Dtos;
using System.Linq.Dynamic.Core;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;


namespace Sinodom.ElevatorCloud.LanFlows
{   
    /// <summary>
    /// 
    /// </summary>
    public class EccpFlowInstancesAppService : ElevatorCloudAppServiceBase, IEccpFlowInstancesAppService
    {

        /// <summary>
        /// 业务逻辑说明
        /// </summary>
        private readonly IRepository<LanFlowScheme> _lanFlowSchemeRepository;
   
        /// <summary>
        ///  维保工单
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        ///  维保计划
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan> _eccpMaintenancePlanRepository;

        /// <summary>
        ///  电梯
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorsRepository;

        /// <summary>
        /// 工单类型
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceType> _eccpDictMaintenanceTypeRepository;

        /// <summary>
        /// 工单类型
        /// </summary>
        private readonly IRepository<LanFlowStatusAction> _lanFlowStatusActionsRepository;
        private readonly IRepository<ECCPEdition> _eccpEditionRepository;
        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;

        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;
        private readonly LanFlowManager _lanFlowManager;

        public EccpFlowInstancesAppService(IRepository<LanFlowScheme> lanFlowSchemeRepository,           
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository, 
            IRepository<LanFlowStatusAction> lanFlowStatusActionsRepository,           
            IRepository<EccpMaintenancePlan> eccpMaintenancePlanRepository,          
            IRepository<EccpDictMaintenanceType> eccpDictMaintenanceTypeRepository, 
            IRepository<ECCPEdition> eccpEditionRepository,
            IRepository<ECCPEditionsType> eccpEditionsTypeRepository, 
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorsRepository,
            LanFlowManager lanFlowManager, IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository)
        {
            _lanFlowSchemeRepository = lanFlowSchemeRepository;
            _lanFlowStatusActionsRepository = lanFlowStatusActionsRepository;
            _eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;       
            _eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;             
            _eccpDictMaintenanceTypeRepository = eccpDictMaintenanceTypeRepository;
            _eccpEditionRepository = eccpEditionRepository;
            _eccpEditionsTypeRepository = eccpEditionsTypeRepository;
            _eccpBaseElevatorsRepository = eccpBaseElevatorsRepository;
            _lanFlowManager = lanFlowManager;
            _eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
        }

        public async  Task<object> GetFlowInstancesList(GetAllLanFlowsQueryInput input)
        {
            
            if (AbpSession.TenantId == null)
            {
                return new PagedResultDto<object>();
            }

            // 获取租户
            var tenant = await this.TenantManager.GetByIdAsync(AbpSession.TenantId.Value);
            if (tenant.EditionId == null)
            {
                return new PagedResultDto<object>();
            }

            // 通过租户版本ID获取版本信息
            var edition = await this._eccpEditionRepository.FirstOrDefaultAsync(tenant.EditionId.Value);
            if (edition?.ECCPEditionsTypeId == null)
            {
                return new PagedResultDto<object>();
            }

            
            // 根据版本类型ID获取版本类型
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);

            if (eccpEditionsType == null)
            {
                return new PagedResultDto<object>();
            }

            GetAllLanFlowsParameterInput parameterInput = new GetAllLanFlowsParameterInput()
            {
                FlowStatusActionId = input.FlowStatusActionId,
                TableName = input.TableName,
                SchemeType = input.SchemeType
            };

            if (eccpEditionsType.Name == "维保公司")
            {
                parameterInput.AuthorizeType = 1;
                parameterInput.UserRoleCode = "1";
            }
            else if (eccpEditionsType.Name == "物业公司")
            {
                parameterInput.AuthorizeType = 2;
                parameterInput.UserRoleCode = "2";
            }
            
            var lanFlowScheme = await _lanFlowSchemeRepository.FirstOrDefaultAsync(w =>
                w.TableName == input.TableName &&
                w.SchemeType == input.SchemeType &&
                (w.AuthorizeType == 0 || w.AuthorizeType == parameterInput.AuthorizeType));
            if (lanFlowScheme == null)
            {
                return new PagedResultDto<object>();
            }

            if (input.TableName== "EccpMaintenanceWorkOrders" && input.SchemeType == "MaintenanceStatusId")
            {
                var reData = await MaintenanceWorkOrdersSign(parameterInput);
                return reData;
            }

            return new PagedResultDto<object>();

        }



        public async Task<bool> UpdateFlowInstances(UpdateFlowStatusAction input)
        {
            UpdateLanFlowInput lanFlow = new UpdateLanFlowInput();
            input.MapTo(lanFlow);
            bool ret = await _lanFlowManager.UpdateFlowsManagement(lanFlow);
            return ret;

        }





        public async Task<PagedResultDto<EccpMaintenanceWorkOrderFlowDto>> MaintenanceWorkOrdersSign(GetAllLanFlowsParameterInput input)
        {
            if (input.FlowStatusActionId < 1)
            {
                return new PagedResultDto<EccpMaintenanceWorkOrderFlowDto>();
            }

            var lanFlowStatusActions =   _lanFlowStatusActionsRepository.GetAll().Where(w =>
                w.IsEndProcess != true      
                && input.FlowStatusActionId==w.Id
                && (w.UserRoleCode == "0" || w.UserRoleCode == input.UserRoleCode)
                );

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpMaintenancePlans = _eccpMaintenancePlanRepository.GetAll();
                var eccpDictMaintenanceTypes = _eccpDictMaintenanceTypeRepository.GetAll();
                var eccpBaseElevators = _eccpBaseElevatorsRepository.GetAll();

                var eccpMaintenanceWorkOrders = _eccpMaintenanceWorkOrderRepository.GetAll();
                if (input.AuthorizeType==1)
                {
                    eccpMaintenanceWorkOrders =
                        eccpMaintenanceWorkOrders.Where(w => w.TenantId == AbpSession.TenantId.Value);
                }
                else if (input.AuthorizeType == 2)
                {
                    var basePropertyCompany = _eccpBasePropertyCompanyRepository.GetAll()
                        .FirstOrDefaultAsync(w => w.TenantId == AbpSession.TenantId.Value);
                    if (basePropertyCompany == null)
                    {
                        return new PagedResultDto<EccpMaintenanceWorkOrderFlowDto>();
                    }

                    eccpBaseElevators =
                        eccpBaseElevators.Where(w => w.ECCPBasePropertyCompanyId == basePropertyCompany.Id);
                }

                var query = from eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrders
                            join lanFlowStatusAction in lanFlowStatusActions
                                on eccpMaintenanceWorkOrder.MaintenanceStatusId equals lanFlowStatusAction.Id
                            join eccpMaintenancePlan in eccpMaintenancePlans
                                on eccpMaintenanceWorkOrder.MaintenancePlanId equals eccpMaintenancePlan.Id
                            join eccpBaseElevator in eccpBaseElevators
                                on eccpMaintenancePlan.ElevatorId equals eccpBaseElevator.Id
                            join eccpDictMaintenanceType in eccpDictMaintenanceTypes
                                on eccpMaintenanceWorkOrder.MaintenanceTypeId equals eccpDictMaintenanceType.Id
                            select new EccpMaintenanceWorkOrderFlowDto
                            {
                                WorkOrderId = eccpMaintenanceWorkOrder.Id,
                                CertificateNum = eccpBaseElevator.CertificateNum,
                                InstallationAddress = eccpBaseElevator.InstallationAddress,
                                MaintenanceTypeName = eccpDictMaintenanceType.Name,
                                MaintenanceCompletionTime = eccpMaintenanceWorkOrder.ComplateDate,
                            };


                var totalCount = await query.CountAsync();

                var eccpMaintenanceWorkOrderFlows = new List<EccpMaintenanceWorkOrderFlowDto>();

                query.OrderBy(input.Sorting ?? "WorkOrderId asc").PageBy(input)
                    .MapTo(eccpMaintenanceWorkOrderFlows);


                var reData = new PagedResultDto<EccpMaintenanceWorkOrderFlowDto>(totalCount, eccpMaintenanceWorkOrderFlows);
                LanFlowInput lanFlowInput = new LanFlowInput()
                {
                    FlowStatusActionsId = input.FlowStatusActionId.Value,
                    UserRoleCode = input.UserRoleCode
                };
                if (reData.Items.Count == 0)
                {
                    return reData;
                }
                List<LanFlowStatusAction> flowStates = await _lanFlowManager.FlowsManagement(lanFlowInput);
                foreach (var item in reData.Items)
                {
                    item.LanFlowStates = flowStates.Select(s => new FlowStatusAction
                    {
                        FlowStatusActionsId = s.Id,
                        ActionCode = s.ActionCode,
                        ActionName = s.ActionName,
                        TableName = input.TableName,
                        SchemeType = input.SchemeType,
                    }).ToList();
                }
                return reData;
            }

           

        }


    }
}