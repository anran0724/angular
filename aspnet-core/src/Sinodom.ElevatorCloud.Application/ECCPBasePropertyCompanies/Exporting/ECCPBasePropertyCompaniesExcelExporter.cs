// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBasePropertyCompaniesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base property companies excel exporter.
    /// </summary>
    public class ECCPBasePropertyCompaniesExcelExporter : EpPlusExcelExporterBase,
                                                          IECCPBasePropertyCompaniesExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBasePropertyCompaniesExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public ECCPBasePropertyCompaniesExcelExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBasePropertyCompanies">
        /// The e ccp base property companies.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetECCPBasePropertyCompanyForView> eccpBasePropertyCompanies)
        {
            return this.CreateExcelPackage(
                "ECCPBasePropertyCompanies.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("ECCPBasePropertyCompanies"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("Name"),
                            this.L("Addresse"),
                            this.L("Longitude"),
                            this.L("Latitude"),
                            this.L("Telephone"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpBasePropertyCompanies,
                            _ => _.ECCPBasePropertyCompany.Name,
                            _ => _.ECCPBasePropertyCompany.Addresse,
                            _ => _.ECCPBasePropertyCompany.Longitude,
                            _ => _.ECCPBasePropertyCompany.Latitude,
                            _ => _.ECCPBasePropertyCompany.Telephone,
                            _ => _.ProvinceName,
                            _ => _.CityName,
                            _ => _.DistrictName,
                            _ => _.StreetName);
                    });
        }
    }
}