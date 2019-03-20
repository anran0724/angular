// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance work order dto.
    /// </summary>
    public class EccpMaintenanceWorkOrderDto : EntityDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether is closed.
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is passed.
        /// </summary>
        public bool IsPassed { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the maintenance plan id.
        /// </summary>
        public int MaintenancePlanId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance status id.
        /// </summary>
        public int MaintenanceStatusId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance type id.
        /// </summary>
        public int MaintenanceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the plan check date.
        /// </summary>
        public DateTime PlanCheckDate { get; set; }
        public DateTime? ComplateDate { get; set; }
    }
}