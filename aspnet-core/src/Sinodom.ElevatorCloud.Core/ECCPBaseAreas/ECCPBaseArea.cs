

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.ECCPBaseAreas
{
	[Table("ECCPBaseAreas")]
    public class ECCPBaseArea : FullAuditedEntity 
    {



		public virtual int ParentId { get; set; }
		
		[StringLength(ECCPBaseAreaConsts.MaxCodeLength, MinimumLength = ECCPBaseAreaConsts.MinCodeLength)]
		public virtual string Code { get; set; }
		
		[Required]
		[StringLength(ECCPBaseAreaConsts.MaxNameLength, MinimumLength = ECCPBaseAreaConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		public virtual int Level { get; set; }
		
		[Required]
		[StringLength(ECCPBaseAreaConsts.MaxPathLength, MinimumLength = ECCPBaseAreaConsts.MinPathLength)]
		public virtual string Path { get; set; }

        [StringLength(ECCPBaseAreaConsts.MaxAreaCodeLength, MinimumLength = ECCPBaseAreaConsts.MinAreaCodeLength)]
        public virtual string AreaCode { get; set; }

        public virtual int? AreaTypeCode { get; set; }
    }
}