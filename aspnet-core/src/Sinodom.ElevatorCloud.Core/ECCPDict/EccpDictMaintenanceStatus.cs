

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictMaintenanceStatuses")]
    public class EccpDictMaintenanceStatus : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictMaintenanceStatusConsts.MaxNameLength, MinimumLength = EccpDictMaintenanceStatusConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}