// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceTemplateForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos
{
    /// <summary>
    /// The get eccp maintenance template for view.
    /// </summary>
    public class GetEccpMaintenanceTemplateForView
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
        public EccpMaintenanceTemplateDto EccpMaintenanceTemplate { get; set; }
    }
}