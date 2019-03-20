// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTempWorkOrdersExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Exporting
{
    using System.Collections.Generic;

    using Abp.Runtime.Session;
    using Abp.Timing.Timezone;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp maintenance temp work orders excel exporter.
    /// </summary>
    public class EccpMaintenanceTempWorkOrdersExcelExporter : EpPlusExcelExporterBase,
                                                              IEccpMaintenanceTempWorkOrdersExcelExporter
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
        /// Initializes a new instance of the <see cref="EccpMaintenanceTempWorkOrdersExcelExporter"/> class.
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
        public EccpMaintenanceTempWorkOrdersExcelExporter(
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
        /// <param name="eccpMaintenanceTempWorkOrders">
        /// The eccp maintenance temp work orders.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpMaintenanceTempWorkOrderForView> eccpMaintenanceTempWorkOrders)
        {
            return this.CreateExcelPackage(
                "EccpMaintenanceTempWorkOrders.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpMaintenanceTempWorkOrders"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("Title"),
                            this.L("CheckState"),
                            this.L("CompletionTime"),
                            this.L("ECCPBaseMaintenanceCompany") + this.L("Name"),
                            this.L("User") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpMaintenanceTempWorkOrders,
                            _ => _.EccpMaintenanceTempWorkOrder.Title,
                            _ => _.EccpMaintenanceTempWorkOrder.CheckState,
                            _ => this._timeZoneConverter.Convert(
                                _.EccpMaintenanceTempWorkOrder.CompletionTime,
                                this._abpSession.TenantId,
                                this._abpSession.GetUserId()),
                            _ => _.ECCPBaseMaintenanceCompanyName,
                            _ => _.UserName);

                        var completionTimeColumn = sheet.Column(3);
                        completionTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                        completionTimeColumn.AutoFit();
                    });
        }
    }
}