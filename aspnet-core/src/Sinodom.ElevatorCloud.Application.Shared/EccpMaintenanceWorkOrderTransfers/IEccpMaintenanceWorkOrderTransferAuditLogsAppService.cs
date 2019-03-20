// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceWorkOrderTransferAuditLogsAppService.cs" company="Sinodom">
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
    /// The EccpMaintenanceWorkOrderTransferAuditLogsAppService interface.
    /// </summary>
    public interface IEccpMaintenanceWorkOrderTransferAuditLogsAppService : IApplicationService
    {
        /// <summary>
        /// The get maintenance work order transfer audit logs.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetAllEccpMaintenanceWorkOrderTransferAuditLogForView>> GetMaintenanceWorkOrderTransferAuditLogs(GetAllEccpMaintenanceTempWorkOrderTransfersAuditLogInput input);
    }
}