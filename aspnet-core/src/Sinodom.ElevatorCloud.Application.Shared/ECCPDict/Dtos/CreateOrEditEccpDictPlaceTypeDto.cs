// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictPlaceTypeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict place type dto.
    /// </summary>
    public class CreateOrEditEccpDictPlaceTypeDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(EccpDictPlaceTypeConsts.MaxNameLength, MinimumLength = EccpDictPlaceTypeConsts.MinNameLength)]
        public string Name { get; set; }
    }
}