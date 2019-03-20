
using Sinodom.ElevatorCloud.EccpBaseElevators;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpBaseElevators
{
	[Table("ElevatorClaimLogs")]
    public class ElevatorClaimLog : FullAuditedEntity<long> , IMustHaveTenant
    {
		public int TenantId { get; set; }



		public virtual Guid ElevatorId { get; set; }
		public EccpBaseElevator Elevator { get; set; }
		
    }
}