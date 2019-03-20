// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseMaintenanceCompaniesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;

    /// <summary>
    ///     The ECCPBaseMaintenanceCompaniesExcelExporter interface.
    /// </summary>
    public interface IECCPBaseMaintenanceCompaniesExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseMaintenanceCompanies">
        /// The e ccp base maintenance companies.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetECCPBaseMaintenanceCompanyForView> eccpBaseMaintenanceCompanies);
    }
}