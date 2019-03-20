// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorBrandDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp base elevator brand dto.
    /// </summary>
    public class EccpBaseElevatorBrandDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the production company id.
        /// </summary>
        public long ProductionCompanyId { get; set; }
    }
}