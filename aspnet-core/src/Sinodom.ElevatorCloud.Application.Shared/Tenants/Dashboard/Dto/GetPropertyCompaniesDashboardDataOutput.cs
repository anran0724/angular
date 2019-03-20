using Sinodom.ElevatorCloud.Statistics.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Tenants.Dashboard.Dto
{
    public  class GetPropertyCompaniesDashboardDataOutput
    {
         public GetPropertyCompaniesMaintenanceStatisticsDto PropertyCompaniesMaintenanceStatistics { get; set; }

        public List<GetMaintenanceCompaniesDto> MaintenanceCompaniesList { get; set; }
    }
}
