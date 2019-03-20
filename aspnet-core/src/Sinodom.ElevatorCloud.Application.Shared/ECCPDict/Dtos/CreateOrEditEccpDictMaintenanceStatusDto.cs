// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictMaintenanceStatusDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict maintenance status dto.
    /// </summary>
    public class CreateOrEditEccpDictMaintenanceStatusDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            EccpDictMaintenanceStatusConsts.MaxNameLength,
            MinimumLength = EccpDictMaintenanceStatusConsts.MinNameLength)]
        public string Name { get; set; }
    }
}