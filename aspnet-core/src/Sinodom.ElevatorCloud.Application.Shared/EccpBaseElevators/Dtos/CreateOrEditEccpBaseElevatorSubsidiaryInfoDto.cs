// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorSubsidiaryInfoDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base elevator subsidiary info dto.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorSubsidiaryInfoDto : FullAuditedEntityDto<Guid?>
    {
        /// <summary>
        /// Gets or sets the custom num.
        /// </summary>
        [StringLength(
            EccpBaseElevatorSubsidiaryInfoConsts.MaxCustomNumLength,
            MinimumLength = EccpBaseElevatorSubsidiaryInfoConsts.MinCustomNumLength)]
        public virtual string CustomNum { get; set; }

        /// <summary>
        /// Gets or sets the deadweight.
        /// </summary>
        public virtual double? Deadweight { get; set; }

        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public virtual Guid? ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the floor number.
        /// </summary>
        public virtual int? FloorNumber { get; set; }

        /// <summary>
        /// Gets or sets the gate number.
        /// </summary>
        public virtual int? GateNumber { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing license number.
        /// </summary>      
        public virtual string ManufacturingLicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the rated speed.
        /// </summary>
        public virtual double? RatedSpeed { get; set; }

        /// <summary>
        /// Gets or sets the tenant id.
        /// </summary>
        public int? TenantId { get; set; }
    }
}