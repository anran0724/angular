

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.Editions
{
	[Table("ECCPEditionsTypes")]
    public class ECCPEditionsType : FullAuditedEntity 
    {



		[Required]
		[StringLength(ECCPEditionsTypeConsts.MaxNameLength, MinimumLength = ECCPEditionsTypeConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}