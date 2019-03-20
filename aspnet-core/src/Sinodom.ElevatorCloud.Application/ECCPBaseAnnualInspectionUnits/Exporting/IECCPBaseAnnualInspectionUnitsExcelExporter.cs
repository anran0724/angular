// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseAnnualInspectionUnitsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;

    /// <summary>
    /// The IeccpBaseAnnualInspectionUnitsExcelExporter interface.
    /// </summary>
    public interface IECCPBaseAnnualInspectionUnitsExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseAnnualInspectionUnits">
        /// The e ccp base annual inspection units.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetECCPBaseAnnualInspectionUnitForView> eccpBaseAnnualInspectionUnits);
    }
}