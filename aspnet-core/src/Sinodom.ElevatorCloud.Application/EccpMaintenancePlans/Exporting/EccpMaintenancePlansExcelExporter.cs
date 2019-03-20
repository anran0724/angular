// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenancePlansExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp maintenance plans excel exporter.
    /// </summary>
    public class EccpMaintenancePlansExcelExporter : EpPlusExcelExporterBase, IEccpMaintenancePlansExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenancePlansExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public EccpMaintenancePlansExcelExporter(
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpMaintenancePlans">
        /// The eccp maintenance plans.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpMaintenancePlanForView> eccpMaintenancePlans)
        {
            return this.CreateExcelPackage(
                "EccpMaintenancePlans.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpMaintenancePlans"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("PollingPeriod"),
                            this.L("RemindHour"),
                            this.L("EccpBaseElevatorNum"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpMaintenancePlans,
                            _ => _.EccpMaintenancePlan.PollingPeriod,
                            _ => _.EccpMaintenancePlan.RemindHour,
                            _ => _.EccpBaseElevatorNum);
                    });
        }
    }
}