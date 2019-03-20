using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Statistics.Dtos;

namespace Sinodom.ElevatorCloud.Statistics
{
    public interface IStatisticsAppService : IApplicationService
    {
        Task<GetAppMaintenanceStatisticsDto> GetAppMaintenanceStatistics();

        Task<PagedResultDto<GetElevatorMaintenanceInfoDto>> GetElevatorMaintenanceInfoList(
            GetElevatorMaintenanceInfoInput input);
        Task UpdateAppOnlineHeartbeat(UpdateAppOnlineHeartbeatDto input);
        Task<GetElevatorDetailedInfoDto> GetElevatorDetailedInfoById(Guid elevatorId, int? workOrderId);

        Task<PagedResultDto<GetElevatorMaintenanceHistorysDto>> GetElevatorMaintenanceHistoryList(
            GetElevatorMaintenanceHistorysInput input);

        Task<GetElevatorArchivesInfoDto> GetEccpBaseElevatorInfo(EntityDto<Guid> input);
        Task<GetAppIndexMapMaintenanceStatisticsDto> GetAppIndexMapMaintenanceStatistics();
        Task<List<GetCompanyDataDto>> GetCompanyDataList(GetCompanyDataInput input);
        Task<GetPropertyCompaniesMaintenanceStatisticsDto> GetPropertyCompaniesMaintenanceStatistics();
        Task<List<GetMaintenanceCompaniesDto>> GetMaintenanceCompaniesList();
        GetTaskListDto GetTaskList();
        GetAdviceListDto GetAdviceList();
        GetEquipmentListDto GetEquipmentList();
        Task<List<GetAreasDto>> GetAreas(int areaId);

        Task<GetMaintenanceTempWorkOrdersElevatorListDto> GetMaintenanceTempWorkOrdersElevatorList();
        GetJsonMessageDto Troubleshooting(GetTroubleshootingInput input);
        GetJsonMessageDto HandlingComplaints(GetHandlingComplaintsInput input);
    }
}
