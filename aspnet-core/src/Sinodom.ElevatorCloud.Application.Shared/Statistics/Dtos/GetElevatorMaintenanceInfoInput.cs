using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetElevatorMaintenanceInfoInput : PagedAndSortedResultRequestDto
    {
        public int? MaintenanceStatusId { get; set; }
        public int? MaintenanceTypeId { get; set; }
        public string CertificateNum { get; set; }
        public string ElevatorNum { get; set; }
    }
}
