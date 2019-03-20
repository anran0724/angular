// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPDictElevatorStatusDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict elevator status dto.
    /// </summary>
    public class CreateOrEditECCPDictElevatorStatusDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the color style.
        /// </summary>
        [StringLength(
            ECCPDictElevatorStatusConsts.MaxColorStyleLength,
            MinimumLength = ECCPDictElevatorStatusConsts.MinColorStyleLength)]
        public string ColorStyle { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            ECCPDictElevatorStatusConsts.MaxNameLength,
            MinimumLength = ECCPDictElevatorStatusConsts.MinNameLength)]
        public string Name { get; set; }
    }
}