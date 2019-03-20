// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAnnualInspectionUnitsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base annual inspection units excel exporter.
    /// </summary>
    public class ECCPBaseAnnualInspectionUnitsExcelExporter : EpPlusExcelExporterBase,
                                                              IECCPBaseAnnualInspectionUnitsExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseAnnualInspectionUnitsExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public ECCPBaseAnnualInspectionUnitsExcelExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseAnnualInspectionUnits">
        /// The e ccp base annual inspection units.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetECCPBaseAnnualInspectionUnitForView> eccpBaseAnnualInspectionUnits)
        {
            return this.CreateExcelPackage(
                "ECCPBaseAnnualInspectionUnits.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("ECCPBaseAnnualInspectionUnits"));
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
                            eccpBaseAnnualInspectionUnits,
                            _ => _.ECCPBaseAnnualInspectionUnit.Name,
                            _ => _.ECCPBaseAnnualInspectionUnit.Addresse,
                            _ => _.ECCPBaseAnnualInspectionUnit.Telephone,
                            _ => _.ProvinceName,
                            _ => _.CityName,
                            _ => _.DistrictName,
                            _ => _.StreetName);
                    });
        }
    }
}