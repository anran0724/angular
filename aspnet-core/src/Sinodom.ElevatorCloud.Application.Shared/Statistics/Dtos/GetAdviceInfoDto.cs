using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetAdviceInfoDto
    {
        public int ID { get; set; }
        public string ContactPhone { get; set; }
        public string ContactName { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public string AdviceStatusName { get; set; }
    }
}
