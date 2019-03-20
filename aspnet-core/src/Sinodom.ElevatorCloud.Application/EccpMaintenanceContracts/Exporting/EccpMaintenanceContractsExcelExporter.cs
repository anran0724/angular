// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceContractsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Exporting
{
    using System.Collections.Generic;

    using Abp.Runtime.Session;
    using Abp.Timing.Timezone;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp maintenance contracts excel exporter.
    /// </summary>
    public class EccpMaintenanceContractsExcelExporter : EpPlusExcelExporterBase, IEccpMaintenanceContractsExcelExporter
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
        /// Initializes a new instance of the <see cref="EccpMaintenanceContractsExcelExporter"/> class.
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
        public EccpMaintenanceContractsExcelExporter(
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
        /// <param name="eccpMaintenanceContracts">
        /// The eccp maintenance contracts.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpMaintenanceContractForView> eccpMaintenanceContracts)
        {
            return this.CreateExcelPackage(
                "EccpMaintenanceContracts.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpMaintenanceContracts"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("StartDate"),
                            this.L("EndDate"),
                            this.L("ECCPBaseMaintenanceCompany") + this.L("Name"),
                            this.L("ECCPBasePropertyCompany") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpMaintenanceContracts,
                            _ => this._timeZoneConverter.Convert(
                                _.EccpMaintenanceContract.StartDate,
                                this._abpSession.TenantId,
                                this._abpSession.GetUserId()),
                            _ => this._timeZoneConverter.Convert(
                                _.EccpMaintenanceContract.EndDate,
                                this._abpSession.TenantId,
                                this._abpSession.GetUserId()),
                            _ => _.ECCPBaseMaintenanceCompanyName,
                            _ => _.ECCPBasePropertyCompanyName);

                        var startDateColumn = sheet.Column(1);
                        startDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                        startDateColumn.AutoFit();
                        var endDateColumn = sheet.Column(2);
                        endDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                        endDateColumn.AutoFit();
                    });
        }
    }
}