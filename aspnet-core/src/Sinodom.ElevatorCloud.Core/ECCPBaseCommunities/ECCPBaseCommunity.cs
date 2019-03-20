
using Sinodom.ElevatorCloud.ECCPBaseAreas;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.ECCPBaseCommunities
{
	[Table("ECCPBaseCommunities")]
    public class ECCPBaseCommunity : FullAuditedEntity<long> 
    {



		[Required]
		[StringLength(ECCPBaseCommunityConsts.MaxNameLength, MinimumLength = ECCPBaseCommunityConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		[Required]
		[StringLength(ECCPBaseCommunityConsts.MaxAddressLength, MinimumLength = ECCPBaseCommunityConsts.MinAddressLength)]
		public virtual string Address { get; set; }
		
		[Required]
		[StringLength(ECCPBaseCommunityConsts.MaxLongitudeLength, MinimumLength = ECCPBaseCommunityConsts.MinLongitudeLength)]
		public virtual string Longitude { get; set; }
		
		[Required]
		[StringLength(ECCPBaseCommunityConsts.MaxLatitudeLength, MinimumLength = ECCPBaseCommunityConsts.MinLatitudeLength)]
		public virtual string Latitude { get; set; }
		

		public virtual int? ProvinceId { get; set; }
		public ECCPBaseArea Province { get; set; }
		
		public virtual int? CityId { get; set; }
		public ECCPBaseArea City { get; set; }
		
		public virtual int? DistrictId { get; set; }
		public ECCPBaseArea District { get; set; }
		
		public virtual int? StreetId { get; set; }
		public ECCPBaseArea Street { get; set; }
		
    }
}