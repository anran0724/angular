// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrdersExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Exporting
{
    using System.Collections.Generic;

    using Abp.Runtime.Session;
    using Abp.Timing.Timezone;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp maintenance work orders excel exporter.
    /// </summary>
    public class EccpMaintenanceWorkOrdersExcelExporter : EpPlusExcelExporterBase,
                                                          IEccpMaintenanceWorkOrdersExcelExporter
    {
        /// <summary>
        /// The _abp session.
        /// </summary>
        private readonly IAbpSession _abpSession;

        /// <summary>
        /// The _time zone converter.
        /// </summary>
        private readonly ITimeZoneConverter _timeZoneConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrdersExcelExporter"/> class.
        /// </summary>
        /// <param name="timeZoneConverter">
        /// The time zone converter.
        /// </param>
        /// <param name="abpSession">
        /// The abp session.
        /// </param>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public EccpMaintenanceWorkOrdersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
            this._timeZoneConverter = timeZoneConverter;
            this._abpSession = abpSession;
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrders">
        /// The eccp maintenance work orders.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpMaintenanceWorkOrderForView> eccpMaintenanceWorkOrders)
        {
            return this.CreateExcelPackage(
                "EccpMaintenanceWorkOrders.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpMaintenanceWorkOrders"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("IsPassed"),
                            this.L("Longitude"),
                            this.L("Latitude"),
                            this.L("PlanCheckDate"),
                            this.L("EccpMaintenancePlan") + this.L("PollingPeriod"),
                            this.L("EccpDictMaintenanceType") + this.L("Name"),
                            this.L("EccpDictMaintenanceStatus") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpMaintenanceWorkOrders,
                            _ => _.EccpMaintenanceWorkOrder.IsPassed,
                            _ => _.EccpMaintenanceWorkOrder.Longitude,
                            _ => _.EccpMaintenanceWorkOrder.Latitude,
                            _ => this._timeZoneConverter.Convert(
                                _.EccpMaintenanceWorkOrder.PlanCheckDate,
                                this._abpSession.TenantId,
                                this._abpSession.GetUserId()),
                            _ => _.EccpMaintenancePlanPollingPeriod,
                            _ => _.EccpDictMaintenanceTypeName,
                            _ => _.EccpDictMaintenanceStatusName);

                        var planCheckDateColumn = sheet.Column(4);
                        planCheckDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                        planCheckDateColumn.AutoFit();
                    });
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpDictMaintenanceItems">
        /// The eccp dict maintenance items.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpDictMaintenanceItemForView> eccpDictMaintenanceItems)
        {
            return this.CreateExcelPackage(
                "EccpDictMaintenanceItems.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpDictMaintenanceItems"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(sheet, this.L("Name"), this.L("TermDesc"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpDictMaintenanceItems,
                            _ => _.EccpDictMaintenanceItem.Name,
                            _ => _.EccpDictMaintenanceItem.TermDesc);
                    });
        }
    }
}