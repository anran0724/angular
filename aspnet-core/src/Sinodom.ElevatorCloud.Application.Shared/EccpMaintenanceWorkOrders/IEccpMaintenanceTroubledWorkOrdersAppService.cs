// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceTroubledWorkOrdersAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The EccpMaintenanceTroubledWorkOrdersAppService interface.
    /// </summary>
    public interface IEccpMaintenanceTroubledWorkOrdersAppService : IApplicationService
    {
        /// <summary>
        /// The apply troubled work order.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> ApplyTroubledWorkOrder(CreateOrEditEccpMaintenanceTroubledWorkOrderDto input);

        /// <summary>
        /// The audit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Audit(AuditEccpMaintenanceTroubledWorkOrderDto input);

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpMaintenanceTroubledWorkOrderDto input);

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
        Task<PagedResultDto<GetEccpMaintenanceTroubledWorkOrderForView>> GetAll(
            GetAllEccpMaintenanceTroubledWorkOrdersInput input);

        /// <summary>
        /// The get all eccp maintenance work order for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpMaintenanceWorkOrderLookupTableDto>> GetAllEccpMaintenanceWorkOrderForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp maintenance troubled work order for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceTroubledWorkOrderForEditOutput> GetEccpMaintenanceTroubledWorkOrderForEdit(
            EntityDto input);
    }
}