// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorQrCodeBindLogTableViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpElevatorQrCodes
{
    using System;

    /// <summary>
    /// The eccp elevator qr code bind log table view model.
    /// </summary>
    public class EccpElevatorQrCodeBindLogTableViewModel
    {
        /// <summary>
        /// Gets or sets the new elevator id.
        /// </summary>
        public Guid? NewElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the new qr code id.
        /// </summary>
        public Guid? NewQrCodeId { get; set; }

        /// <summary>
        /// Gets or sets the old elevator id.
        /// </summary>
        public Guid? OldElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the old qr code id.
        /// </summary>
        public Guid? OldQrCodeId { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        public virtual string Remark { get; set; }
    }
}