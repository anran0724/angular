using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetEquipmentFaultInfosDto
    {
        public int LiftId { get; set; }
        public string FaultCondition { get; set; }
        public DateTime? TimeOfFailure { get; set; }
        public DateTime? FailureEndTime { get; set; }
    }
}
