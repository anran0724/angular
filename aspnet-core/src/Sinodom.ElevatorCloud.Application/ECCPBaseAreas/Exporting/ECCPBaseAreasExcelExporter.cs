// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAreasExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAreas.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base areas excel exporter.
    /// </summary>
    public class ECCPBaseAreasExcelExporter : EpPlusExcelExporterBase, IECCPBaseAreasExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseAreasExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public ECCPBaseAreasExcelExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseAreas">
        /// The eccp base areas.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetECCPBaseAreaForView> eccpBaseAreas)
        {
            return this.CreateExcelPackage(
                "ECCPBaseAreas.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("ECCPBaseAreas"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(sheet, this.L("ParentId"), this.L("Code"), this.L("Name"), this.L("Level"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpBaseAreas,
                            _ => _.ECCPBaseArea.ParentId,
                            _ => _.ECCPBaseArea.Code,
                            _ => _.ECCPBaseArea.Name,
                            _ => _.ECCPBaseArea.Level);
                    });
        }
    }
}