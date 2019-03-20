using Abp.Domain.Entities;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sinodom.ElevatorCloud.Authorization.Users.Dto
{
    public class EccpCompanyUserExtensionEditDto 
    {
        //[Required]
        //[StringLength(
        //    EccpCompanyUserExtensionConsts.MaxIdCardLength,
        //    MinimumLength = EccpCompanyUserExtensionConsts.MinIdCardLength)]
        public virtual string IdCard { get; set; }

        //[Required]
        //[StringLength(
        //    EccpCompanyUserExtensionConsts.MaxMobileLength,
        //    MinimumLength = EccpCompanyUserExtensionConsts.MinMobileLength)]
        public virtual string Mobile { get; set; }

        public virtual long UserId { get; set; }       

        public virtual Guid? SignPictureId { get; set; }

        public virtual Guid? CertificateFrontPictureId { get; set; }

        public virtual Guid? CertificateBackPictureId { get; set; }

        public virtual DateTime? ExpirationDate { get; set; }

    }
}
