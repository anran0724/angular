// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrdersExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Sinodom.ElevatorCloud.DataExporting.Word.Npoi;

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
    public class EccpMaintenanceWorkOrdersWordExporter : NpoiWordExporterBase,
        IEccpMaintenanceWorkOrdersWordExporter
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
        public EccpMaintenanceWorkOrdersWordExporter(
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
        /// <param name="eccpDictMaintenanceItems">
        /// The eccp dict maintenance items.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(byte[] bytes)
        {
            return this.CreateWordPackage(
                "EccpDictMaintenanceItems.docx", bytes);
        }
    }
}