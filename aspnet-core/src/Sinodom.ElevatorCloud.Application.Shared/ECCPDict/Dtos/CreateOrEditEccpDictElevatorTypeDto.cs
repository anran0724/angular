// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictElevatorTypeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict elevator type dto.
    /// </summary>
    public class CreateOrEditEccpDictElevatorTypeDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            EccpDictElevatorTypeConsts.MaxNameLength,
            MinimumLength = EccpDictElevatorTypeConsts.MinNameLength)]
        public string Name { get; set; }
    }
}