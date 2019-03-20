// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpElevatorQrCodeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp elevator qr code dto.
    /// </summary>
    public class CreateOrEditEccpElevatorQrCodeDto : FullAuditedEntityDto<Guid?>
    {
        /// <summary>
        /// Gets or sets the area name.
        /// </summary>
        [Required]
        [StringLength(
            EccpElevatorQrCodeConsts.MaxAreaNameLength,
            MinimumLength = EccpElevatorQrCodeConsts.MinAreaNameLength)]
        public string AreaName { get; set; }

        /// <summary>
        /// Gets or sets the elevator id.
        /// </summary>
        public Guid? ElevatorId { get; set; }

        /// <summary>
        /// Gets or sets the elevator num.
        /// </summary>
        [Required]
        [StringLength(
            EccpElevatorQrCodeConsts.MaxElevatorNumLength,
            MinimumLength = EccpElevatorQrCodeConsts.MinElevatorNumLength)]
        public string ElevatorNum { get; set; }

        /// <summary>
        /// Gets or sets the grant date time.
        /// </summary>
        public DateTime? GrantDateTime { get; set; }

        /// <summary>
        /// Gets or sets the img picture.
        /// </summary>
        [StringLength(
            EccpElevatorQrCodeConsts.MaxImgPictureLength,
            MinimumLength = EccpElevatorQrCodeConsts.MinImgPictureLength)]
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