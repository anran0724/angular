

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictMaintenanceItems")]
    public class EccpDictMaintenanceItem : FullAuditedEntity 
    {



		[StringLength(EccpDictMaintenanceItemConsts.MaxNameLength, MinimumLength = EccpDictMaintenanceItemConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		[StringLength(EccpDictMaintenanceItemConsts.MaxTermCodeLength, MinimumLength = EccpDictMaintenanceItemConsts.MinTermCodeLength)]
		public virtual string TermCode { get; set; }
		
		public virtual int DisOrder { get; set; }
		
		[StringLength(EccpDictMaintenanceItemConsts.MaxTermDescLength, MinimumLength = EccpDictMaintenanceItemConsts.MinTermDescLength)]
		public virtual string TermDesc { get; set; }
		

    }
}