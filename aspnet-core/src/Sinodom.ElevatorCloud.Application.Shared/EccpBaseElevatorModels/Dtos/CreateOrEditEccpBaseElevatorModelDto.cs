// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorModelDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base elevator model dto.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorModelDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the elevator brand id.
        /// </summary>
        public int ElevatorBrandId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorModelConsts.MaxNameLength,
            MinimumLength = EccpBaseElevatorModelConsts.MinNameLength)]
        public string Name { get; set; }
    }
}