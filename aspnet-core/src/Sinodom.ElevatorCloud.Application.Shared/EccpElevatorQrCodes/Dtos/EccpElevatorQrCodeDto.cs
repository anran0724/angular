// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorQrCodeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp elevator qr code dto.
    /// </summary>
    public class EccpElevatorQrCodeDto : EntityDto<Guid>
    {
        /// <summary>
        /// Gets or sets the area name.
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the elevator num.
        /// </summary>
        public string ElevatorNum { get; set; }

        /// <summary>
        /// Gets or sets the grant date time.
        /// </summary>
        public DateTime? GrantDateTime { get; set; }

        /// <summary>
        /// Gets or sets the img picture.
        /// </summary>
        public string ImgPicture { get; set; }

        /// <summary>
        /// Gets or sets the install date time.
        /// </summary>
        public DateTime? InstallDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is grant.
        /// </summary>
        public bool IsGrant { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is install.
        /// </summary>
        public bool IsInstall { get; set; }
    }
}