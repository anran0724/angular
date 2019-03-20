// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpElevatorQrCodeBindLogView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos
{
    /// <summary>
    /// The get eccp elevator qr code bind log view.
    /// </summary>
    public class GetEccpElevatorQrCodeBindLogView
    {
        /// <summary>
        /// Gets or sets the newaename.
        /// </summary>
        public string newaename { get; set; }

        /// <summary>
        /// Gets or sets the new certificate num.
        /// </summary>
        public string newCertificateNum { get; set; }

        /// <summary>
        /// Gets or sets the oleaename.
        /// </summary>
        public string oleaename { get; set; }

        /// <summary>
        /// Gets or sets the ole certificate num.
        /// </summary>
        public string oleCertificateNum { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        public string Remark { get; set; }
    }
}