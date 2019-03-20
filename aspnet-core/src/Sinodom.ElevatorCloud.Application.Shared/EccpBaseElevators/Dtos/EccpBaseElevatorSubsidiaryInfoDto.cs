// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorSubsidiaryInfoDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp base elevator subsidiary info dto.
    /// </summary>
    public class EccpBaseElevatorSubsidiaryInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// Gets or sets the custom num.
        /// </summary>
        public string CustomNum { get; set; }

        /// <summary>
        /// Gets or sets the deadweight.
        /// </summary>
        public double? Deadweight { get; set; }

        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid? ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the floor number.
        /// </summary>
        public int? FloorNumber { get; set; }

        /// <summary>
        /// Gets or sets the gate number.
        /// </summary>
        public int? GateNumber { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing license number.
        /// </summary>
        public string ManufacturingLicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the rated speed.
        /// </summary>
        public double? RatedSpeed { get; set; }
    }
}