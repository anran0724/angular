// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpElevatorQrCodeForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos
{
    /// <summary>
    /// The get eccp elevator qr code for edit output.
    /// </summary>
    public class GetEccpElevatorQrCodeForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp base elevator certificate num.
        /// </summary>
        public string EccpBaseElevatorCertificateNum { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp elevator qr code.
        /// </summary>
        public CreateOrEditEccpElevatorQrCodeDto EccpElevatorQrCode { get; set; }
    }
}