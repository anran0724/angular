// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorModelDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp base elevator model dto.
    /// </summary>
    public class EccpBaseElevatorModelDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the elevator brand id.
        /// </summary>
        public int ElevatorBrandId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}