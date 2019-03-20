// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictMaintenanceWorkFlowStatusDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict maintenance work flow status dto.
    /// </summary>
    public class CreateOrEditEccpDictMaintenanceWorkFlowStatusDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(
            EccpDictMaintenanceWorkFlowStatusConsts.MaxNameLength,
            MinimumLength = EccpDictMaintenanceWorkFlowStatusConsts.MinNameLength)]
        public string Name { get; set; }
    }
}