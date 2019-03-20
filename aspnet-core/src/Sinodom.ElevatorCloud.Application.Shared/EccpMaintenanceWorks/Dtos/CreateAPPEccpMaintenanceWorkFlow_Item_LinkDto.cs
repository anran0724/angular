// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create app eccp maintenance work flow_ item_ link dto.
    /// </summary>
    public class CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto : FullAuditedEntityDto<Guid?>
    {
        /// <summary>
        /// Gets or sets the dict maintenance item id.
        /// </summary>
        public int DictMaintenanceItemId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is qualified.
        /// </summary>
        public bool IsQualified { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        public string Remark { get; set; }
    }
}