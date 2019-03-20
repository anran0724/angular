using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetMaintenanceTempWorkOrdersElevatorListDto
    {
        public int CountNum { get; set; }
        public List<GetMaintenanceTempWorkOrdersElevatorsDto> MaintenanceTempWorkOrdersElevators { get; set; }
    }
}
