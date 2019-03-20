 
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.LanFlows
{
 
    using Abp.Domain.Services;
 
    /// <summary>
    /// 
    /// </summary>
    public class LanFlowManager : IDomainService
    {

        /// <summary>
        /// 业务逻辑说明
        /// </summary>
        private readonly IRepository<LanFlowScheme> _lanFlowSchemeRepository;
       
        /// <summary>
        ///  流程状态
        /// </summary>
        private readonly IRepository<LanFlowStatusAction> _lanFlowStatusActionsRepository;
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrdersRepository;
        private readonly IRepository<LanFlowInstanceOperationHistory> _lanFlowInstanceOperationHistorysRepository;
        

        public LanFlowManager(IRepository<LanFlowScheme> lanFlowSchemeRepository,            
            IRepository<LanFlowStatusAction> lanFlowStatusActionsRepository, 
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrdersRepository,
            IRepository<LanFlowInstanceOperationHistory> lanFlowInstanceOperationHistorysRepository
            )
        {
            _lanFlowSchemeRepository = lanFlowSchemeRepository;                
            _lanFlowStatusActionsRepository = lanFlowStatusActionsRepository;
            _eccpMaintenanceWorkOrdersRepository = eccpMaintenanceWorkOrdersRepository;
            _lanFlowInstanceOperationHistorysRepository = lanFlowInstanceOperationHistorysRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<LanFlowStatusAction>> FlowsManagement(LanFlowInput input)
        {
          
   
            var lanFlowState = await _lanFlowStatusActionsRepository.FirstOrDefaultAsync(w => w.Id == input.FlowStatusActionsId);
            if (lanFlowState == null)
            {
                return null;
            }

            var lanFlowStatus = await _lanFlowStatusActionsRepository.GetAll().Where(w => w.SchemeId == lanFlowState.SchemeId
                                                                                    && w.StatusValue ==
                                                                                          lanFlowState.ArgumentValue
                                                                                    && (w.UserRoleCode == "0" ||
                                                                                        w.UserRoleCode ==
                                                                                        input.UserRoleCode)).ToListAsync();
           
            return lanFlowStatus;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateFlowsManagement(UpdateLanFlowInput input)
        {
            
            if (input.ObjIds.Count==0)
            {
                return false;
            }
            var lanFlowStatusAction = await _lanFlowStatusActionsRepository.FirstOrDefaultAsync(w => w.Id == input.FlowStatusActionId);
            if (lanFlowStatusAction == null)
            {
                return false;
            }

            var lanFlowScheme = await _lanFlowSchemeRepository.FirstOrDefaultAsync(w => w.TableName == "EccpMaintenanceWorkOrders" && w.SchemeType == "MaintenanceStatusId");
            if (lanFlowScheme ==  null)
            {
                return false;
            }
            if (input.TableName == "EccpMaintenanceWorkOrders" && input.SchemeType == "MaintenanceStatusId")
            {
                var ids = input.ObjIds.Select(int.Parse).ToArray();
                var workOrders = await _eccpMaintenanceWorkOrdersRepository.GetAll().Where(w => ids.Contains(w.Id)).ToListAsync();
              
                foreach (var work in workOrders)
                {
                    work.MaintenanceStatusId = lanFlowStatusAction.Id;
                    await _eccpMaintenanceWorkOrdersRepository.UpdateAsync(work);
                  
                    LanFlowInstanceOperationHistory flowInstanceOperationHistories = new LanFlowInstanceOperationHistory()
                    {
                        StatusValue = lanFlowStatusAction.StatusValue,
                        StatusName = lanFlowStatusAction.StatusName,
                        ActionCode = lanFlowStatusAction.ActionCode,
                        ActionValue = input.ActionValue,
                        ActionDesc = lanFlowStatusAction.ActionDesc,
                        ObjectId = work.Id.ToString(),
                        Field = "MaintenanceStatusId",
                        FlowStatusActionId= lanFlowStatusAction.Id                       
                    };
                    await _lanFlowInstanceOperationHistorysRepository.InsertAsync(flowInstanceOperationHistories);

                }
                return true;
            }


            return false;

        }

    }
}