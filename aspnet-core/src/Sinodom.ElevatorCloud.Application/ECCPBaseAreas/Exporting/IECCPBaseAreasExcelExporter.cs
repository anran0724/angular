// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseAreasExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAreas.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;

    /// <summary>
    /// The ECCPBaseAreasExcelExporter interface.
    /// </summary>
    public interface IECCPBaseAreasExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseAreas">
        /// The eccp base areas.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetECCPBaseAreaForView> eccpBaseAreas);
    }
}