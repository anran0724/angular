// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceCompanyChangeLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance company change log dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceCompanyChangeLogDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        [Required]
        [StringLength(
            EccpMaintenanceCompanyChangeLogConsts.MaxFieldNameLength,
            MinimumLength = EccpMaintenanceCompanyChangeLogConsts.MinFieldNameLength)]
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the maintenance company id.
        /// </summary>
        public int MaintenanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        [Required]
        [StringLength(
            EccpMaintenanceCompanyChangeLogConsts.MaxNewValueLength,
            MinimumLength = EccpMaintenanceCompanyChangeLogConsts.MinNewValueLength)]
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        [Required]
        [StringLength(
            EccpMaintenanceCompanyChangeLogConsts.MaxOldValueLength,
            MinimumLength = EccpMaintenanceCompanyChangeLogConsts.MinOldValueLength)]
        public string OldValue { get; set; }
    }
}