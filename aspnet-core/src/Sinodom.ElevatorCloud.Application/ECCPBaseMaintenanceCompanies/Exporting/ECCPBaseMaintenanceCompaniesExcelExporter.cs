// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseMaintenanceCompaniesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base maintenance companies excel exporter.
    /// </summary>
    public class ECCPBaseMaintenanceCompaniesExcelExporter : EpPlusExcelExporterBase,
                                                             IECCPBaseMaintenanceCompaniesExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseMaintenanceCompaniesExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public ECCPBaseMaintenanceCompaniesExcelExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseMaintenanceCompanies">
        /// The e ccp base maintenance companies.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetECCPBaseMaintenanceCompanyForView> eccpBaseMaintenanceCompanies)
        {
            return this.CreateExcelPackage(
                "ECCPBaseMaintenanceCompanies.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("ECCPBaseMaintenanceCompanies"));
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
                            eccpBaseMaintenanceCompanies,
                            _ => _.ECCPBaseMaintenanceCompany.Name,
                            _ => _.ECCPBaseMaintenanceCompany.Addresse,
                            _ => _.ECCPBaseMaintenanceCompany.Longitude,
                            _ => _.ECCPBaseMaintenanceCompany.Latitude,
                            _ => _.ECCPBaseMaintenanceCompany.Telephone,
                            _ => _.ProvinceName,
                            _ => _.CityName,
                            _ => _.DistrictName,
                            _ => _.StreetName);
                    });
        }
    }
}