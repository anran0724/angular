// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBasePropertyCompaniesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;

    /// <summary>
    ///     The ECCPBasePropertyCompaniesExcelExporter interface.
    /// </summary>
    public interface IECCPBasePropertyCompaniesExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBasePropertyCompanies">
        /// The e ccp base property companies.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetECCPBasePropertyCompanyForView> eccpBasePropertyCompanies);
    }
}