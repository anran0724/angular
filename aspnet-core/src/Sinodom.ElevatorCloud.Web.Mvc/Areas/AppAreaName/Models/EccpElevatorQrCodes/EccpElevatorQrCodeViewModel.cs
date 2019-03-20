// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorQrCodeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpElevatorQrCodes
{
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;

    /// <summary>
    /// The eccp elevator qr code view model.
    /// </summary>
    public class EccpElevatorQrCodeViewModel : GetEccpElevatorQrCodeForView
    {
        /// <summary>
        /// Gets or sets the is grant name.
        /// </summary>
        public string IsGrantName { get; set; }

        /// <summary>
        /// Gets or sets the is install name.
        /// </summary>
        public string IsInstallName { get; set; }
    }
}