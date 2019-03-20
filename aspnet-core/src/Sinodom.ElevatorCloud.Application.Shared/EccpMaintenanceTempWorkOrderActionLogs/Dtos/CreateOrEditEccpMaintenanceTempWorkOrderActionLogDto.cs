// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance temp work order action log dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto : FullAuditedEntityDto<Guid?>
    {
        /// <summary>
        /// Gets or sets the check state.
        /// </summary>
        public int CheckState { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(
            EccpMaintenanceTempWorkOrderActionLogConsts.MaxRemarksLength,
            MinimumLength = EccpMaintenanceTempWorkOrderActionLogConsts.MinRemarksLength)]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the temp work order id.
        /// </summary>
        public Guid TempWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public long UserId { get; set; }
    }
}