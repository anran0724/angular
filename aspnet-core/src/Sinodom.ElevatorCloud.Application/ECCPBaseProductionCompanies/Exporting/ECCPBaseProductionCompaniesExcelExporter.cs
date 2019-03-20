// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseProductionCompaniesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base production companies excel exporter.
    /// </summary>
    public class ECCPBaseProductionCompaniesExcelExporter : EpPlusExcelExporterBase,
                                                            IECCPBaseProductionCompaniesExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseProductionCompaniesExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public ECCPBaseProductionCompaniesExcelExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseProductionCompanies">
        /// The e ccp base production companies.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetECCPBaseProductionCompanyForView> eccpBaseProductionCompanies)
        {
            return this.CreateExcelPackage(
                "ECCPBaseProductionCompanies.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("ECCPBaseProductionCompanies"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("Name"),
                            this.L("Addresse"),
                            this.L("Telephone"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpBaseProductionCompanies,
                            _ => _.ECCPBaseProductionCompany.Name,
                            _ => _.ECCPBaseProductionCompany.Addresse,
                            _ => _.ECCPBaseProductionCompany.Telephone,
                            _ => _.ProvinceName,
                            _ => _.CityName,
                            _ => _.DistrictName,
                            _ => _.StreetName);
                    });
        }
    }
}