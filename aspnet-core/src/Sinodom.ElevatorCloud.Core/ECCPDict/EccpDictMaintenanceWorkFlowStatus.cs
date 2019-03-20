

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictMaintenanceWorkFlowStatuses")]
    public class EccpDictMaintenanceWorkFlowStatus : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictMaintenanceWorkFlowStatusConsts.MaxNameLength, MinimumLength = EccpDictMaintenanceWorkFlowStatusConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}