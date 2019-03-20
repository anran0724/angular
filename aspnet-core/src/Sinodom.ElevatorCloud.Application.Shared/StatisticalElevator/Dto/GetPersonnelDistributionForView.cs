using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class GetPersonnelDistributionForView
    {
        public int PersonnelNumber { get; set; }        

        public List<PersonnelDto> PersonnelCollection;
    }
}
