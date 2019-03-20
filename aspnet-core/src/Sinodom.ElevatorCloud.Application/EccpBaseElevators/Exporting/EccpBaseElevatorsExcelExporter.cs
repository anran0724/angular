// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Exporting
{
    using System.Collections.Generic;

    using Abp.Runtime.Session;
    using Abp.Timing.Timezone;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base elevators excel exporter.
    /// </summary>
    public class EccpBaseElevatorsExcelExporter : EpPlusExcelExporterBase, IEccpBaseElevatorsExcelExporter
    {
        /// <summary>
        ///     The abp session.
        /// </summary>
        private readonly IAbpSession _abpSession;

        /// <summary>
        ///     The time zone converter.
        /// </summary>
        private readonly ITimeZoneConverter _timeZoneConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorsExcelExporter"/> class.
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
        public EccpBaseElevatorsExcelExporter(
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
        /// <param name="eccpBaseElevators">
        /// The eccp base elevators.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpBaseElevatorForView> eccpBaseElevators)
        {
            return this.CreateExcelPackage(
                "EccpBaseElevators.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpBaseElevators"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("Name"),
                            this.L("CertificateNum"),
                            this.L("MachineNum"),
                            this.L("InstallationAddress"),
                            this.L("InstallationDatetime"),
                            this.L("Longitude"),
                            this.L("Latitude"),
                            this.L("EccpDictPlaceType") + this.L("Name"),
                            this.L("EccpDictElevatorType") + this.L("Name"),
                            this.L("ECCPDictElevatorStatus") + this.L("Name"),
                            this.L("ECCPBaseCommunity") + this.L("Name"),
                            this.L("ECCPBaseMaintenanceCompany") + this.L("Name"),
                            this.L("ECCPBaseAnnualInspectionUnit") + this.L("Name"),
                            this.L("ECCPBaseRegisterCompany") + this.L("Name"),
                            this.L("ECCPBaseProductionCompany") + this.L("Name"),
                            this.L("EccpBaseElevatorBrand") + this.L("Name"),
                            this.L("EccpBaseElevatorModel") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpBaseElevators,
                            _ => _.EccpBaseElevator.Name,
                            _ => _.EccpBaseElevator.CertificateNum,
                            _ => _.EccpBaseElevator.MachineNum,
                            _ => _.EccpBaseElevator.InstallationAddress,
                            _ => this._timeZoneConverter.Convert(
                                _.EccpBaseElevator.InstallationDatetime,
                                this._abpSession.TenantId,
                                this._abpSession.GetUserId()),
                            _ => _.EccpBaseElevator.Longitude,
                            _ => _.EccpBaseElevator.Latitude,
                            _ => _.EccpDictPlaceTypeName,
                            _ => _.EccpDictElevatorTypeName,
                            _ => _.ECCPDictElevatorStatusName,
                            _ => _.ECCPBaseCommunityName,
                            _ => _.ECCPBaseMaintenanceCompanyName,
                            _ => _.ECCPBaseAnnualInspectionUnitName,
                            _ => _.ECCPBaseRegisterCompanyName,
                            _ => _.ECCPBaseProductionCompanyName,
                            _ => _.EccpBaseElevatorBrandName,
                            _ => _.EccpBaseElevatorModelName,
                            _ => _.ProvinceName,
                            _ => _.CityName,
                            _ => _.DistrictName,
                            _ => _.StreetName);

                        var installationDatetimeColumn = sheet.Column(5);
                        installationDatetimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                        installationDatetimeColumn.AutoFit();
                    });
        }
    }
}