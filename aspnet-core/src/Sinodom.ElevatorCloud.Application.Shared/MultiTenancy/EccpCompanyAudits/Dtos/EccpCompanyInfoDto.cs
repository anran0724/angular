namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos
{
    using Abp.Application.Services.Dto;

    public class EccpCompanyInfoDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string Addresse { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Telephone { get; set; }


        public int? ProvinceId { get; set; }

        public int? CityId { get; set; }

        public int? DistrictId { get; set; }

        public int? StreetId { get; set; }
    }
}