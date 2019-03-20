using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.Dto
{
    using System;

    public class RegisterTenantInput
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string AdminPassword { get; set; }

        [DisableAuditing]
        public string CaptchaResponse { get; set; }

        public SubscriptionStartType SubscriptionStartType { get; set; }

        public SubscriptionPaymentGatewayType? Gateway { get; set; }

        public int? EditionId { get; set; }

        public string PaymentId { get; set; }

        public string LegalPerson { get; set; }

        public string Mobile { get; set; }

        public bool IsMember { get; set; }

        public Guid? BusinessLicenseId { get; set; }

        public Guid? AptitudePhotoId { get; set; }

        [MaxLength(400)]
        public string BusinessLicenseIdFileToken { get; set; }

        [MaxLength(400)]
        public string AptitudePhotoIdFileToken { get; set; }
    }
}
