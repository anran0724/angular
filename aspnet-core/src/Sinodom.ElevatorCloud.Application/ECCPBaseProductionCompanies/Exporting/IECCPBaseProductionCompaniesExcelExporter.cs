// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseProductionCompaniesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Dtos;

    /// <summary>
    ///     The ECCPBaseProductionCompaniesExcelExporter interface.
    /// </summary>
    public interface IECCPBaseProductionCompaniesExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseProductionCompanies">
        /// The e ccp base production companies.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetECCPBaseProductionCompanyForView> eccpBaseProductionCompanies);
    }
}