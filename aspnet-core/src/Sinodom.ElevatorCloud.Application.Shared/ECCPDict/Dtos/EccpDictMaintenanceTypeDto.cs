// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceTypeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp dict maintenance type dto.
    /// </summary>
    public class EccpDictMaintenanceTypeDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}