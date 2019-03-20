// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictPlaceTypeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp dict place type dto.
    /// </summary>
    public class EccpDictPlaceTypeDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}