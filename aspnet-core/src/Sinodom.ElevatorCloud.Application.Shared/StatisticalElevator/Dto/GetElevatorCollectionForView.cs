using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class GetElevatorCollectionForView
    {
        public int ElevatorNumber { get; set; }

        public List<ElevatorCollectionDto> ElevatorCollection { get; set; }        
    }
}
