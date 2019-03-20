using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetMaintenanceCompaniesDto
    {
        /// <summary>
        /// 维保公司ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 维保公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 维保电梯总数
        /// </summary>
        public int ElevatorNum { get; set; }
        /// <summary>
        /// 临期维保数量
        /// </summary>
        public int NumberOfTemporaryMaintenance { get; set; }
        /// <summary>
        /// 超期次数
        /// </summary>
        public int OverdueNum { get; set; }
        /// <summary>
        /// 完成维保次数
        /// </summary>
        public int NumberOfMaintenanceCompleted { get; set; }
    }
}
