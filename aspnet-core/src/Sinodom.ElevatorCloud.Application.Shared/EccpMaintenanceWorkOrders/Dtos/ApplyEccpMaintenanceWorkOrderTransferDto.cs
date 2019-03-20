// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplyEccpMaintenanceWorkOrderTransferDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The apply eccp maintenance work order transfer dto.
    /// </summary>
    public class ApplyEccpMaintenanceWorkOrderTransferDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the maintenance work order id.
        /// </summary>
        public int MaintenanceWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the transfer user id.
        /// </summary>
        public int TransferUserId { get; set; }
    }
}