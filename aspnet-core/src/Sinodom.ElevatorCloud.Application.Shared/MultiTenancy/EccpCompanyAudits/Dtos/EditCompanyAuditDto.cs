namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos
{
    using Abp.Application.Services.Dto;

    public class EditCompanyAuditDto : FullAuditedEntityDto<int>
    {
        public string Remarks { get; set; }

        public bool CheckState { get; set; }
    }
}