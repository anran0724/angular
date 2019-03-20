namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos
{
    using Abp.Application.Services.Dto;

    public class EccpCompanyAuditLogDto : EntityDto
    {
        public bool CheckState { get; set; }

        public string Remarks { get; set; }

        public int CompanyId { get; set; }
    }
}