// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseRegisterCompaniesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp base register companies excel exporter.
    /// </summary>
    public class ECCPBaseRegisterCompaniesExcelExporter : EpPlusExcelExporterBase,
                                                          IECCPBaseRegisterCompaniesExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseRegisterCompaniesExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public ECCPBaseRegisterCompaniesExcelExporter(
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseRegisterCompanies">
        /// The e ccp base register companies.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetECCPBaseRegisterCompanyForView> eccpBaseRegisterCompanies)
        {
            return this.CreateExcelPackage(
                "ECCPBaseRegisterCompanies.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("ECCPBaseRegisterCompanies"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("Name"),
                            this.L("Addresse"),
                            this.L("Telephone"),
                            this.L("ProvinceName"),
                            this.L("CityName"),
                            this.L("DistrictName"),
                            this.L("StreetName"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpBaseRegisterCompanies,
                            _ => _.ECCPBaseRegisterCompany.Name,
                            _ => _.ECCPBaseRegisterCompany.Addresse,
                            _ => _.ECCPBaseRegisterCompany.Telephone,
                            _ => _.ProvinceName,
                            _ => _.CityName,
                            _ => _.DistrictName,
                            _ => _.StreetName);
                    });
        }
    }
}