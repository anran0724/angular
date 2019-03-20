namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos
{
    public class GetEccpCompanyAuditLogForView
    {
        public int Id { get; set; }

        public string CheckStateName { get; set; }

        public string Remarks { get; set; }
    }
}