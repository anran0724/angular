namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos
{
    public class GetEccpCompanyAuditForView
    {
        public EccpCompanyInfoDto EccpCompanyInfo { get; set; }

        public EccpCompanyInfoExtensionDto EccpCompanyInfoExtension { get; set; }

        public int Id { get; set; }

        public string CheckStateName { get; set; }

        public string ProvinceName { get; set; }

        public string CityName { get; set; }

        public string DistrictName { get; set; }

        public string StreetName { get; set; }

        public string EditionTypeName { get; set; }
    }
}