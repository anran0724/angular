using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetElevatorDetailedInfoDto
    {
        public Guid ElevatorId { get; set; }
        public int WorkOrderId { get; set; }
        public int PlanId { get; set; }
        public int MaintenanceStatusId { get; set; }
        public string MaintenanceStatusName { get; set; }
        public DateTime PlanCheckDate { get; set; }
        public DateTime? ComplateDate { get; set; }
        public DateTime? LastMaintenanceTime { get; set; }
        public string MaintenanceTypeName { get; set; }
        public string InstallationAddress { get; set; }
        public List<GetMaintenanceUserInfosDto> MaintenanceUserInfos { get; set; }
        public List<GetEquipmentFaultInfosDto> EquipmentFaultInfos { get; set; }
        public List<GetAdviceInfoDto> AdviceInfos { get; set; }
        public GetCameraInformationDto CameraInformation { get; set; }
        public string Remarks { get; set; }
        public string EquipmentUrl { get; set; }
        public int? SyncElevatorId { get; set; }
    }
}
