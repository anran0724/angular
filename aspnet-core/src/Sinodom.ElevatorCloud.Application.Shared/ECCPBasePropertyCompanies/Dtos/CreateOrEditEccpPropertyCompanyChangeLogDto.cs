// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpPropertyCompanyChangeLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp property company change log dto.
    /// </summary>
    public class CreateOrEditEccpPropertyCompanyChangeLogDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        [Required]
        [StringLength(
            EccpPropertyCompanyChangeLogConsts.MaxFieldNameLength,
            MinimumLength = EccpPropertyCompanyChangeLogConsts.MinFieldNameLength)]
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        [Required]
        [StringLength(
            EccpPropertyCompanyChangeLogConsts.MaxNewValueLength,
            MinimumLength = EccpPropertyCompanyChangeLogConsts.MinNewValueLength)]
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        [Required]
        [StringLength(
            EccpPropertyCompanyChangeLogConsts.MaxOldValueLength,
            MinimumLength = EccpPropertyCompanyChangeLogConsts.MinOldValueLength)]
        public string OldValue { get; set; }

        /// <summary>
        /// Gets or sets the property company id.
        /// </summary>
        public int PropertyCompanyId { get; set; }
    }
}