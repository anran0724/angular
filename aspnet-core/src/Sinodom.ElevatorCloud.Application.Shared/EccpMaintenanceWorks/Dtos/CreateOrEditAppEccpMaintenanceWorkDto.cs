// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditAppEccpMaintenanceWorkDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit app eccp maintenance work dto.
    /// </summary>
    public class CreateOrEditAppEccpMaintenanceWorkDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the maintenance type id.
        /// </summary>
        public int MaintenanceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work order id.
        /// </summary>
        public int MaintenanceWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        public string Remark { get; set; }

        // public int? TenantId { get; set; }
    }
}