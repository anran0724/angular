// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictLabelStatusDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp dict label status dto.
    /// </summary>
    public class EccpDictLabelStatusDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}