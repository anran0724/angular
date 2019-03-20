// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTemplatesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp maintenance templates excel exporter.
    /// </summary>
    public class EccpMaintenanceTemplatesExcelExporter : EpPlusExcelExporterBase, IEccpMaintenanceTemplatesExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTemplatesExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public EccpMaintenanceTemplatesExcelExporter(
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpMaintenanceTemplates">
        /// The eccp maintenance templates.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpMaintenanceTemplateForView> eccpMaintenanceTemplates)
        {
            return this.CreateExcelPackage(
                "EccpMaintenanceTemplates.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpMaintenanceTemplates"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("TempName"),
                            this.L("TempAllow"),
                            this.L("TempDeny"),
                            this.L("TempNodeCount"),
                            this.L("EccpDictMaintenanceType") + this.L("Name"),
                            this.L("EccpDictElevatorType") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpMaintenanceTemplates,
                            _ => _.EccpMaintenanceTemplate.TempName,
                            _ => _.EccpMaintenanceTemplate.TempAllow,
                            _ => _.EccpMaintenanceTemplate.TempDeny,
                            _ => _.EccpMaintenanceTemplate.TempNodeCount,
                            _ => _.EccpDictMaintenanceTypeName,
                            _ => _.EccpDictElevatorTypeName);
                    });
        }
    }
}