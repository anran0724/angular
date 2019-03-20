// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpElevatorQrCodeViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpElevatorQrCodes
{
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;

    /// <summary>
    /// The create or edit eccp elevator qr code view model.
    /// </summary>
    public class CreateOrEditEccpElevatorQrCodeViewModel
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

        /// <summary>
        /// The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpElevatorQrCode.Id.HasValue;
    }
}