// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPDictElevatorStatusDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp dict elevator status dto.
    /// </summary>
    public class ECCPDictElevatorStatusDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the color style.
        /// </summary>
        public string ColorStyle { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}