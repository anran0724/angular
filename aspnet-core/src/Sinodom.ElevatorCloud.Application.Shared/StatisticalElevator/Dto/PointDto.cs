using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class PointDto
    {
        public int status { get; set; }

        public Result result;
    }

    public class Result
    {
        public Location location;

        public int precise { get; set; }
        public int confidence { get; set; }
        public int comprehension { get; set; }
        public string level { get; set; }
    }
    public class Location
    {
        public string lng { get; set; }

        public string lat { get; set; }
    }
    
}
