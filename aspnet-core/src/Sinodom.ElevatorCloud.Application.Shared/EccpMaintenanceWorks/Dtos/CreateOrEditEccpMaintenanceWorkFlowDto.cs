// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceWorkFlowDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance work flow dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceWorkFlowDto : FullAuditedEntityDto<Guid?>
    {
        /// <summary>
        /// Gets or sets the action code value.
        /// </summary>
        [StringLength(
            EccpMaintenanceWorkFlowConsts.MaxActionCodeValueLength,
            MinimumLength = EccpMaintenanceWorkFlowConsts.MinActionCodeValueLength)]
        public string ActionCodeValue { get; set; }

        /// <summary>
        /// Gets or sets the dict maintenance work flow status id.
        /// </summary>
        public int DictMaintenanceWorkFlowStatusId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance template node id.
        /// </summary>
        public int MaintenanceTemplateNodeId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance work id.
        /// </summary>
        public int MaintenanceWorkId { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        [StringLength(
            EccpMaintenanceWorkFlowConsts.MaxRemarkLength,
            MinimumLength = EccpMaintenanceWorkFlowConsts.MinRemarkLength)]
        public string Remark { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public Guid? RefusePictureId { get; set; }
        /// <summary>
        /// Gets or sets the work flow items.
        /// </summary>
        public List<CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto> workFlowItems { get; set; }
    }
}