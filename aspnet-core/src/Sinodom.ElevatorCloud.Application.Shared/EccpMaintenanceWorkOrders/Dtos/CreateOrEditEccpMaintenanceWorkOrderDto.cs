// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkOrderDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance work order dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkOrderDto : FullAuditedEntityDto<int?>
    {
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

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        [StringLength(
            EccpMaintenanceWorkOrderConsts.MaxRemarkLength,
            MinimumLength = EccpMaintenanceWorkOrderConsts.MinRemarkLength)]
        public string Remark { get; set; }
    }
}