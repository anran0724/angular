// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorBrandDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base elevator brand dto.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorBrandDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            EccpBaseElevatorBrandConsts.MaxNameLength,
            MinimumLength = EccpBaseElevatorBrandConsts.MinNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the production company id.
        /// </summary>
        public long ProductionCompanyId { get; set; }
    }
}