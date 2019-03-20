// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceTemplateViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceTemplates
{
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos;

    /// <summary>
    /// The create or edit eccp maintenance template view model.
    /// </summary>
    public class CreateOrEditEccpMaintenanceTemplateViewModel
    {
        /// <summary>
        /// Gets or sets the eccp dict elevator type name.
        /// </summary>
        public string EccpDictElevatorTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict maintenance type name.
        /// </summary>
        public string EccpDictMaintenanceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance template.
        /// </summary>
        public CreateOrEditEccpMaintenanceTemplateDto EccpMaintenanceTemplate { get; set; }

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpMaintenanceTemplate.Id.HasValue;
    }
}