// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorBrandsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base elevator brands excel exporter.
    /// </summary>
    public class EccpBaseElevatorBrandsExcelExporter : EpPlusExcelExporterBase, IEccpBaseElevatorBrandsExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorBrandsExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public EccpBaseElevatorBrandsExcelExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseElevatorBrands">
        /// The eccp base elevator brands.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpBaseElevatorBrandForView> eccpBaseElevatorBrands)
        {
            return this.CreateExcelPackage(
                "EccpBaseElevatorBrands.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpBaseElevatorBrands"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(sheet, this.L("Name"), this.L("ECCPBaseProductionCompany") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpBaseElevatorBrands,
                            _ => _.EccpBaseElevatorBrand.Name,
                            _ => _.ECCPBaseProductionCompanyName);
                    });
        }
    }
}