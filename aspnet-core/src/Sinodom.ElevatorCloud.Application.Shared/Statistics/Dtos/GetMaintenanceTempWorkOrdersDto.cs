using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetMaintenanceTempWorkOrdersDto
    {
        public Guid TempWorkOrderId { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string Title { get; set; }
        public string Describe { get; set; }
        public string TempWorkOrderTypeName { get; set; }
    }
}
