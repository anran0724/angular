// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceWorkOrderTransfersAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers.Dtos;

    /// <summary>
    /// The EccpMaintenanceWorkOrderTransfersAppService interface.
    /// </summary>
    public interface IEccpMaintenanceWorkOrderTransfersAppService : IApplicationService
    {
        /// <summary>
        /// The audit maintenance work order transfer.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int AuditMaintenanceWorkOrderTransfer(EccpMaintenanceWorkOrderTransfersForAuditOutput input);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetAllEccpMaintenanceWorkOrderTransferForView>> GetAll(
            GetAllEccpMaintenanceWorkOrderTransfersInput input);

        /// <summary>
        /// The maintenance temp work order transfer.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int MaintenanceTempWorkOrderTransfer(EccpMaintenanceWorkOrderTransfersForAuditOutput input);

        /// <summary>
        /// The maintenance work order transfer.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int MaintenanceWorkOrderTransfer(EccpMaintenanceWorkOrderTransfersForAuditOutput input);

        /// <summary>
        /// 维保工单转接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> ApplyWorkOrderTransfer(ApplyEccpMaintenanceWorkOrderTransferDto input);

        /// <summary>
        /// 临时工单转接申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> ApplyTempWorkOrderTransfer(ApplyEccpMaintenanceTempWorkOrderTransferDto input);
    }
}