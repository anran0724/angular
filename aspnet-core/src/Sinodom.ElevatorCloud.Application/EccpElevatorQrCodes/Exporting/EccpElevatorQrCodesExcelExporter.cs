// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorQrCodesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Exporting
{
    using System.Collections.Generic;

    using Abp.Runtime.Session;
    using Abp.Timing.Timezone;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp elevator qr codes excel exporter.
    /// </summary>
    public class EccpElevatorQrCodesExcelExporter : EpPlusExcelExporterBase, IEccpElevatorQrCodesExcelExporter
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
        /// Initializes a new instance of the <see cref="EccpElevatorQrCodesExcelExporter"/> class.
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
        public EccpElevatorQrCodesExcelExporter(
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
        /// <param name="eccpElevatorQrCodes">
        /// The eccp elevator qr codes.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpElevatorQrCodeForView> eccpElevatorQrCodes)
        {
            return this.CreateExcelPackage(
                "EccpElevatorQrCodes.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpElevatorQrCodes"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("AreaName"),
                            this.L("ElevatorNum"),
                            this.L("ImgPicture"),
                            this.L("IsInstall"),
                            this.L("IsGrant"),
                            this.L("InstallDateTime"),
                            this.L("GrantDateTime"),
                            this.L("EccpBaseElevator") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpElevatorQrCodes,
                            _ => _.EccpElevatorQrCode.AreaName,
                            _ => _.EccpElevatorQrCode.ElevatorNum,
                            _ => _.EccpElevatorQrCode.ImgPicture,
                            _ => _.EccpElevatorQrCode.IsInstall,
                            _ => _.EccpElevatorQrCode.IsGrant,
                            _ => this._timeZoneConverter.Convert(
                                _.EccpElevatorQrCode.InstallDateTime,
                                this._abpSession.TenantId,
                                this._abpSession.GetUserId()),
                            _ => this._timeZoneConverter.Convert(
                                _.EccpElevatorQrCode.GrantDateTime,
                                this._abpSession.TenantId,
                                this._abpSession.GetUserId()),
                            _ => _.EccpBaseElevatorName);

                        var installDateTimeColumn = sheet.Column(6);
                        installDateTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                        installDateTimeColumn.AutoFit();
                        var grantDateTimeColumn = sheet.Column(7);
                        grantDateTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                        grantDateTimeColumn.AutoFit();
                    });
        }
    }
}