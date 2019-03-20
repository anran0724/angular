// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictNodeTypeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict node type dto.
    /// </summary>
    public class CreateOrEditEccpDictNodeTypeDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(EccpDictNodeTypeConsts.MaxNameLength, MinimumLength = EccpDictNodeTypeConsts.MinNameLength)]
        public string Name { get; set; }
    }
}