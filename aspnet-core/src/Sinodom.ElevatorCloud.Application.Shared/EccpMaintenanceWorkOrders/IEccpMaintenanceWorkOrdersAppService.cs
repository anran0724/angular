// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceWorkOrdersAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    using GetAllForLookupTableInput = Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos.GetAllForLookupTableInput;

    /// <summary>
    ///     The EccpMaintenanceWorkOrdersAppService interface.
    /// </summary>
    public interface IEccpMaintenanceWorkOrdersAppService : IApplicationService
    {
        /// <summary>
        /// The close work order.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CloseWorkOrder(EntityDto input);

        /// <summary>
        /// The completion work rearrange work order.
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrderId">
        /// The eccp maintenance work order id.
        /// </param>
        /// <param name="completionTime">
        /// The completion time.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int CompletionWorkRearrangeWorkOrder(int eccpMaintenanceWorkOrderId, DateTime completionTime);

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkOrderDto input);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(EntityDto input);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceWorkOrderForView>> GetAll(GetAllEccpMaintenanceWorkOrdersInput input);

        Task<PagedResultDto<GetEccpMaintenanceWorkOrderForView>> GetAllByElevatorId(GetAllByElevatorIdEccpMaintenanceWorkOrdersInput input);

        /// <summary>
        /// The get all completed work order.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceWorkOrderForView>> GetAllCompletedWorkOrder(
            GetAllEccpMaintenanceWorkOrdersInput input);

        /// <summary>
        /// The get all eccp dict maintenance status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictMaintenanceStatusLookupTableDto>> GetAllEccpDictMaintenanceStatusForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp dict maintenance type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictMaintenanceTypeLookupTableDto>> GetAllEccpDictMaintenanceTypeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp maintenance plan for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpMaintenancePlanLookupTableDto>> GetAllEccpMaintenancePlanForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all pending processing.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceUsersWorkOrder>> GetAllPendingProcessing(
            GetAllEccpMaintenanceWorkOrdersInput input);

        /// <summary>
        /// The get all work order evaluation by work order id.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceWorkOrderEvaluationForView>> GetAllWorkOrderEvaluationByWorkOrderId(
            GetAllByWorkOrderIdEccpMaintenanceWorkOrderEvaluationsInput input);

        /// <summary>
        ///     The get app index data.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<GetAppIndexDataDto> GetAppIndexData();

        /// <summary>
        /// The get eccp dict maintenance item.
        /// </summary>
        /// <param name="workOrderId">
        /// The work order id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<List<GetEccpDictMaintenanceItemPrintForView>> GetEccpDictMaintenanceItem(int workOrderId);

        /// <summary>
        /// The get eccp dict maintenance item to excel.
        /// </summary>
        /// <param name="workOrderId">
        /// The work order id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpDictMaintenanceItemToExcel(int workOrderId);

        /// <summary>
        /// The get eccp dict maintenance item to word.
        /// </summary>
        /// <param name="workOrderId">
        /// The work order id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpDictMaintenanceItemToWord(int workOrderId);

        /// <summary>
        /// The get eccp maintenance work order for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceWorkOrderForEditOutput> GetEccpMaintenanceWorkOrderForEdit(EntityDto input);

        /// <summary>
        /// The get eccp maintenance work orders to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpMaintenanceWorkOrdersToExcel(GetAllEccpMaintenanceWorkOrdersForExcelInput input);

        /// <summary>
        /// The get period all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceWorkOrderForView>> GetPeriodAll(
            GetAllEccpMaintenanceWorkOrdersInput input);

        /// <summary>
        /// The overdue treatment of maintenance work order.
        /// </summary>
        void GetOverdueTreatmentOfMaintenanceWorkOrder();

        /// <summary>
        /// The plan deleting rearrange work order.
        /// </summary>
        /// <param name="maintenancePlanId">
        /// The maintenance plan id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int PlanDeletingRearrangeWorkOrder(long maintenancePlanId);

        /// <summary>
        /// The plan modification rearrange work order.
        /// </summary>
        /// <param name="maintenancePlanId">
        /// The maintenance plan id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        Task<int> PlanModificationRearrangeWorkOrder(long maintenancePlanId);

        Task<GetEccpBaseElevatorsInfoDto> GetEccpBaseElevatorsInfoByPlanId(int planId, int workOrderId);
        Task<List<GetEccpMaintenanceWorkFlowsDto>> GetAllEccpMaintenanceWorkFlowsByWorkOrderId(int workOrderId);
    }
}