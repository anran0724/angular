// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictWorkOrderTypeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict work order type dto.
    /// </summary>
    public class CreateOrEditEccpDictWorkOrderTypeDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            EccpDictWorkOrderTypeConsts.MaxNameLength,
            MinimumLength = EccpDictWorkOrderTypeConsts.MinNameLength)]
        public string Name { get; set; }
    }
}