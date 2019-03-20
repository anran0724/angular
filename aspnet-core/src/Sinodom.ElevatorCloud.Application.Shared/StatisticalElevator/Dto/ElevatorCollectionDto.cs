using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class ElevatorCollectionDto
    {
        //电梯编号 
        public Guid? ElevatorId { get; set; }

        //电梯20位码
        public string CertificateNum { get; set; }

        // 维保状态
        public string MaintenanceStatus { get; set; }

        // 上次维保时间
        public string LastMaintenanceTime { get; set; }

        // 维保到期时间
        public string MaintenanceMaturityTime { get; set; }

        // 维保人员
        public string MaintenanceUsers { get; set; }

        public List<string> MaintenanceUsersList { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string InstallationAddress { get; set; }

        public int? WorkOrderId { get; set; }

        public string ElevatorNum { get; set; }
    }
}
