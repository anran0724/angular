using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
   public class GetAreasDto
    {
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public long? CommunityId { get; set; }
        public string Name { get; set; }
        public int ElevatorNum { get; set; }
        public string LongitudeAndLatitude { get; set; }
    }
}
