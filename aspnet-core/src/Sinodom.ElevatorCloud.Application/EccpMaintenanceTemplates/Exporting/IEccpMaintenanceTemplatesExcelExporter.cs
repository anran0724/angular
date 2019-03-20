// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceTemplatesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos;

    /// <summary>
    /// The EccpMaintenanceTemplatesExcelExporter interface.
    /// </summary>
    public interface IEccpMaintenanceTemplatesExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpMaintenanceTemplates">
        /// The eccp maintenance templates.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetEccpMaintenanceTemplateForView> eccpMaintenanceTemplates);
    }
}