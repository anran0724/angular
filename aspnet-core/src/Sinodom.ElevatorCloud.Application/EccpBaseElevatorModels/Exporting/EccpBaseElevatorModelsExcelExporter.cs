// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorModelsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.DataExporting.Excel.EpPlus;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp base elevator models excel exporter.
    /// </summary>
    public class EccpBaseElevatorModelsExcelExporter : EpPlusExcelExporterBase, IEccpBaseElevatorModelsExcelExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorModelsExcelExporter"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public EccpBaseElevatorModelsExcelExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseElevatorModels">
        /// The eccp base elevator models.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        public FileDto ExportToFile(List<GetEccpBaseElevatorModelForView> eccpBaseElevatorModels)
        {
            return this.CreateExcelPackage(
                "EccpBaseElevatorModels.xlsx",
                excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add(this.L("EccpBaseElevatorModels"));
                        sheet.OutLineApplyStyle = true;

                        this.AddHeader(sheet, this.L("Name"), this.L("EccpBaseElevatorBrand") + this.L("Name"));

                        this.AddObjects(
                            sheet,
                            2,
                            eccpBaseElevatorModels,
                            _ => _.EccpBaseElevatorModel.Name,
                            _ => _.EccpBaseElevatorBrandName);
                    });
        }
    }
}