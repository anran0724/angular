using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class PersonnelDto
    {
        //人员编号
        public long UserId { get; set; }

        //维保公司名字
        public string MaintenanceCompaniesName { get; set; }

        //手机号
        public string PhoneNumber { get; set; }

        //维保人员名字
        public string UserName { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }
    }
}
