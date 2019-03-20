// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTroubledWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance troubled work order dto.
    /// </summary>
    public class EccpMaintenanceTroubledWorkOrderDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the is audit.
        /// </summary>
        public int? IsAudit { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work order id.
        /// </summary>
        public int MaintenanceWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the troubled desc.
        /// </summary>
        public string TroubledDesc { get; set; }

        /// <summary>
        /// Gets or sets the work order status name.
        /// </summary>
        public string WorkOrderStatusName { get; set; }
    }
}