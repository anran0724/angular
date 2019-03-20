// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictLabelStatusDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict label status dto.
    /// </summary>
    public class CreateOrEditEccpDictLabelStatusDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(EccpDictLabelStatusConsts.MaxNameLength, MinimumLength = EccpDictLabelStatusConsts.MinNameLength)]
        public string Name { get; set; }
    }
}