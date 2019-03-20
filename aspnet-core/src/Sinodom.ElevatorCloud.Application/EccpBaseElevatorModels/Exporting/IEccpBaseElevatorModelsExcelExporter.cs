// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpBaseElevatorModelsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos;

    /// <summary>
    ///     The EccpBaseElevatorModelsExcelExporter interface.
    /// </summary>
    public interface IEccpBaseElevatorModelsExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseElevatorModels">
        /// The eccp base elevator models.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetEccpBaseElevatorModelForView> eccpBaseElevatorModels);
    }
}