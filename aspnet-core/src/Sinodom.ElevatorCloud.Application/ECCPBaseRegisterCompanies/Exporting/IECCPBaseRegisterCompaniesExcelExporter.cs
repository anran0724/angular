// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseRegisterCompaniesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos;

    /// <summary>
    /// The ECCPBaseRegisterCompaniesExcelExporter interface.
    /// </summary>
    public interface IECCPBaseRegisterCompaniesExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseRegisterCompanies">
        /// The e ccp base register companies.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetECCPBaseRegisterCompanyForView> eccpBaseRegisterCompanies);
    }
}