using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetEquipmentInfoDto
    {
        public int ID { get; set; }
        public string FaultCondition { get; set; }
        public DateTime? TimeOfFailure { get; set; }
    }
}
