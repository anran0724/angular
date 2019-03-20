using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
   public class GetMaintenanceUserInfosDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
    }
}
