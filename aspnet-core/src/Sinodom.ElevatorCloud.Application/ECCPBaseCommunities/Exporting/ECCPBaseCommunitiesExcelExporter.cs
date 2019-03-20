// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseCommunitiesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseCommunities.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base communities excel exporter.
    /// </summary>
    public class ECCPBaseCommunitiesExcelExporter : EpPlusExcelExporterBase, IECCPBaseCommunitiesExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseCommunitiesExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public ECCPBaseCommunitiesExcelExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseCommunities">
        /// The eccp base communities.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetECCPBaseCommunityForView> eccpBaseCommunities)
        {
            return this.CreateExcelPackage(
                "ECCPBaseCommunities.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("ECCPBaseCommunities"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(
                            sheet,
                            this.L("Name"),
                            this.L("Address"),
                            this.L("Longitude"),
                            this.L("Latitude"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"),
                            this.L("ECCPBaseArea") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpBaseCommunities,
                            _ => _.ECCPBaseCommunity.Name,
                            _ => _.ECCPBaseCommunity.Address,
                            _ => _.ECCPBaseCommunity.Longitude,
                            _ => _.ECCPBaseCommunity.Latitude,
                            _ => _.ProvinceName,
                            _ => _.CityName,
                            _ => _.DistrictName,
                            _ => _.StreetName);
                    });
        }
    }
}