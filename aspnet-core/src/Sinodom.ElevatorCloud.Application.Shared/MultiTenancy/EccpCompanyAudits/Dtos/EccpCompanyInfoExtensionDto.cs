namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    public class EccpCompanyInfoExtensionDto : EntityDto<int>
    {
        public  string LegalPerson { get; set; }

        public string Mobile { get; set; }

        public Guid? BusinessLicenseId { get; set; }

        public Guid? AptitudePhotoId { get; set; }

        public bool IsMember { get; set; }
    }
}