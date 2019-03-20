// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpElevatorQrCodesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;

    /// <summary>
    /// The EccpElevatorQrCodesExcelExporter interface.
    /// </summary>
    public interface IEccpElevatorQrCodesExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpElevatorQrCodes">
        /// The eccp elevator qr codes.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetEccpElevatorQrCodeForView> eccpElevatorQrCodes);
    }
}