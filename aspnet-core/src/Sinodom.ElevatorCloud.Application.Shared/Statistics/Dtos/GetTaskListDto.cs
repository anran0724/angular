using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetTaskListDto
    {
        public int CountNum { get; set; }
        public List<GetTaskInfoDto> TaskInfo { get; set; }
    }
}
