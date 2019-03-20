// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplateDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance template dto.
    /// </summary>
    public class EccpMaintenanceTemplateDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the elevator type id.
        /// </summary>
        public int ElevatorTypeId { get; set; }

        /// <summary>
        /// Gets or sets the maintenance type id.
        /// </summary>
        public int MaintenanceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the temp allow.
        /// </summary>
        public string TempAllow { get; set; }

        /// <summary>
        /// Gets or sets the temp deny.
        /// </summary>
        public string TempDeny { get; set; }

        /// <summary>
        /// Gets or sets the temp name.
        /// </summary>
        public string TempName { get; set; }

        /// <summary>
        /// Gets or sets the temp node count.
        /// </summary>
        public int TempNodeCount { get; set; }
    }
}