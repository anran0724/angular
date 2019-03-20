
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts
{
	[Table("EccpMaintenanceContracts")]
    public class EccpMaintenanceContract : FullAuditedEntity<long> , IMustHaveTenant
    {
		public int TenantId { get; set; }

        public virtual Guid? ContractPictureId { get; set; }

        [StringLength(EccpMaintenanceContractConsts.MaxContractPictureDescLength, MinimumLength = EccpMaintenanceContractConsts.MinContractPictureDescLength)]
        public string ContractPictureDesc { get; set; }

        [StringLength(EccpMaintenanceContractConsts.MaxStopContractRemarksLength, MinimumLength = EccpMaintenanceContractConsts.MinStopContractRemarksLength)]
        public string StopContractRemarks { get; set; }

        public virtual DateTime StartDate { get; set; }
		
		public virtual DateTime EndDate { get; set; }
		
        public virtual bool IsStop { get; set; }

        public virtual DateTime? StopDate { get; set; }

        public virtual int MaintenanceCompanyId { get; set; }
		public ECCPBaseMaintenanceCompany MaintenanceCompany { get; set; }
		
		public virtual int PropertyCompanyId { get; set; }
		public ECCPBasePropertyCompany PropertyCompany { get; set; }
		
    }
}