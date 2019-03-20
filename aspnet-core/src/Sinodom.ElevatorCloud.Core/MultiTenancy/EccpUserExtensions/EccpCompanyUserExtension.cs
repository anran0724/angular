namespace Sinodom.ElevatorCloud.MultiTenancy.UserExtensions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.Authorization.Users;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities;
    using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions;

    [Table("EccpCompanyUserExtensions")]
    public class EccpCompanyUserExtension : FullAuditedEntity
    {
        [Required]
        [StringLength(
            EccpCompanyUserExtensionConsts.MaxIdCardLength,
            MinimumLength = EccpCompanyUserExtensionConsts.MinIdCardLength)]
        public virtual string IdCard { get; set; }

        [Required]
        [StringLength(
            EccpCompanyUserExtensionConsts.MaxMobileLength,
            MinimumLength = EccpCompanyUserExtensionConsts.MinMobileLength)]
        public virtual string Mobile { get; set; }

        public virtual long UserId { get; set; }

        public User User { get; set; }

        public virtual Guid? SignPictureId { get; set; }

        public virtual Guid? CertificateFrontPictureId { get; set; }

        public virtual Guid? CertificateBackPictureId { get; set; }

        public virtual DateTime? ExpirationDate { get; set; }

        public virtual int CheckState { get; set; }

        public virtual int? SyncUserId { get; set; }

        public virtual float? Longitude { get; set; }

        public virtual float? Latitude { get; set; }

        public virtual bool IsOnline { get; set; }

        public virtual DateTime? Heartbeat { get; set; }

        public virtual int? PositionProvinceId { get; set; }

        public ECCPBaseArea PositionProvince { get; set; }

        public virtual int? PositionCityId { get; set; }

        public ECCPBaseArea PositionCity { get; set; }

        public virtual int? PositionDistrictId { get; set; }

        public ECCPBaseArea PositionDistrict { get; set; }

        public virtual int? PositionStreetId { get; set; }

        public ECCPBaseArea PositionStreet { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community id.
        /// </summary>
        public virtual long? PositionCommunityId { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community.
        /// </summary>
        public ECCPBaseCommunity PositionCommunity { get; set; }

        [StringLength(
            EccpCompanyUserExtensionConsts.MaxLastLoginDeviceCodeLength,
            MinimumLength = EccpCompanyUserExtensionConsts.MinLastLoginDeviceCodeLength)]
        public virtual string LastLoginDeviceCode { get; set; }
    }
}