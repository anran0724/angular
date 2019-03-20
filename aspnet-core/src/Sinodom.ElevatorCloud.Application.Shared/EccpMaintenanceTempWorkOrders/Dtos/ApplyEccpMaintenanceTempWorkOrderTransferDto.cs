// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplyEccpMaintenanceTempWorkOrderTransferDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The apply eccp maintenance temp work order transfer dto.
    /// </summary>
    public class ApplyEccpMaintenanceTempWorkOrderTransferDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the maintenance temp work order id.
        /// </summary>
        public Guid MaintenanceTempWorkOrderId { get; set; }

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