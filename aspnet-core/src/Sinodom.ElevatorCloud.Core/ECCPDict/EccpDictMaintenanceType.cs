

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictMaintenanceTypes")]
    public class EccpDictMaintenanceType : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictMaintenanceTypeConsts.MaxNameLength, MinimumLength = EccpDictMaintenanceTypeConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}