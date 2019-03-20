// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceTempWorkOrdersAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders
{
    using System;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;

    /// <summary>
    /// The EccpMaintenanceTempWorkOrdersAppService interface.
    /// </summary>
    public interface IEccpMaintenanceTempWorkOrdersAppService : IApplicationService
    {
        
        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpMaintenanceTempWorkOrderDto input);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(EntityDto<Guid> input);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceTempWorkOrderForView>> GetAll(
            GetAllEccpMaintenanceTempWorkOrdersInput input);

        Task<PagedResultDto<EccpBaseElevatorLookupTableDto>> GetAllEccpBaseElevatorForLookupTable(
            GetAllForLookupTableInput input);
        /// <summary>
        /// The get all eccp dict temp work order type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<ECCPDictTempWorkOrderTypeLookupTableDto>> GetAllECCPDictTempWorkOrderTypeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<UserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp maintenance temp work order.
        /// </summary>
        ///   <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceTempWorkOrderForAppView>> GetEccpMaintenanceTempWorkOrder(GetAllEccpMaintenanceTempWorkOrdersInput input);

        /// <summary>
        /// The get eccp maintenance temp work order for details.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceTempWorkOrderForDetails> GetEccpMaintenanceTempWorkOrderForDetails(EntityDto<Guid> input);

        /// <summary>
        /// The get eccp maintenance temp work order for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceTempWorkOrderForEditOutput> GetEccpMaintenanceTempWorkOrderForEdit(EntityDto<Guid> input);

        /// <summary>
        /// The get eccp maintenance temp work orders to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpMaintenanceTempWorkOrdersToExcel(GetAllEccpMaintenanceTempWorkOrdersForExcelInput input);

        /// <summary>
        /// The management eccp maintenance temp work order.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task ManagementEccpMaintenanceTempWorkOrder(HandleEccpMaintenanceTempWorkOrderDto input);

        /// <summary>
        /// 维保工单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetEccpMaintenanceTempWorkOrderDetails> GetEccpMaintenanceTempWorkOrderDetails(Guid id);
    }
}