using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetCameraInformationDto
    {
        public string AccessToken { get; set; }
        public string EquipmentSerialNumber { get; set; }
        public int? ChannelNumber { get; set; }
    }
}
