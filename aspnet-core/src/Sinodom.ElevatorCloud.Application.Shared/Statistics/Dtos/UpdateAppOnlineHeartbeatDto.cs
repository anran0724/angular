using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class UpdateAppOnlineHeartbeatDto
    {
        public float? Longitude { get; set; }

        public float? Latitude { get; set; }
        public bool IsOnline { get; set; }

        public DateTime? Heartbeat { get; set; }
        public string PhoneId { get; set; }
        public int? PositionProvinceId { get; set; }
        public int? PositionCityId { get; set; }
        public int? PositionDistrictId { get; set; }
        public int? PositionStreetId { get; set; }
    }
}
