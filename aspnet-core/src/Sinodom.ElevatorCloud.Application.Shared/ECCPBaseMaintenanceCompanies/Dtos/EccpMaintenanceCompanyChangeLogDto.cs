// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceCompanyChangeLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp maintenance company change log dto.
    /// </summary>
    public class EccpMaintenanceCompanyChangeLogDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the maintenance company id.
        /// </summary>
        public int MaintenanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        public string OldValue { get; set; }
    }
}