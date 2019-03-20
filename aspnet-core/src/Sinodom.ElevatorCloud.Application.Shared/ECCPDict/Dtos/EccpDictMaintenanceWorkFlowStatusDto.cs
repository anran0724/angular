// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceWorkFlowStatusDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp dict maintenance work flow status dto.
    /// </summary>
    public class EccpDictMaintenanceWorkFlowStatusDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}