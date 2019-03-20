// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpDictMaintenanceItemDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp dict maintenance item dto.
    /// </summary>
    public class CreateOrEditEccpDictMaintenanceItemDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the dis order.
        /// </summary>
        public int DisOrder { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [StringLength(
            EccpDictMaintenanceItemConsts.MaxNameLength,
            MinimumLength = EccpDictMaintenanceItemConsts.MinNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the term code.
        /// </summary>
        [StringLength(
            EccpDictMaintenanceItemConsts.MaxTermCodeLength,
            MinimumLength = EccpDictMaintenanceItemConsts.MinTermCodeLength)]
        public string TermCode { get; set; }

        /// <summary>
        /// Gets or sets the term desc.
        /// </summary>
        [StringLength(
            EccpDictMaintenanceItemConsts.MaxTermDescLength,
            MinimumLength = EccpDictMaintenanceItemConsts.MinTermDescLength)]
        public string TermDesc { get; set; }
    }
}