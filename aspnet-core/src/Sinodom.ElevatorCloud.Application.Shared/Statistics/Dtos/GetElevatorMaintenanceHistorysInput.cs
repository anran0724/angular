using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetElevatorMaintenanceHistorysInput: PagedAndSortedResultRequestDto
    {
        public Guid ElevatorId { get; set; }
    }
}
