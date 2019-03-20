// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictTempWorkOrderTypeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict temp work order type dto.
    /// </summary>
    public class CreateOrEditEccpDictTempWorkOrderTypeDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            EccpDictTempWorkOrderTypeConsts.MaxNameLength,
            MinimumLength = EccpDictTempWorkOrderTypeConsts.MinNameLength)]
        public string Name { get; set; }
    }
}