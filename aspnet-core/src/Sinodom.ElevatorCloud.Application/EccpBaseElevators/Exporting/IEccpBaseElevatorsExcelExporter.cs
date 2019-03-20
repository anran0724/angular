// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpBaseElevatorsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    ///     The EccpBaseElevatorsExcelExporter interface.
    /// </summary>
    public interface IEccpBaseElevatorsExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseElevators">
        /// The eccp base elevators.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetEccpBaseElevatorForView> eccpBaseElevators);
    }
}