// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpElevatorQrCodeForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos
{
    /// <summary>
    /// The get eccp elevator qr code for view.
    /// </summary>
    public class GetEccpElevatorQrCodeForView
    {
        /// <summary>
        /// Gets or sets the bytess.
        /// </summary>
        public byte[] Bytess { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp elevator qr code.
        /// </summary>
        public EccpElevatorQrCodeDto EccpElevatorQrCode { get; set; }
    }
}