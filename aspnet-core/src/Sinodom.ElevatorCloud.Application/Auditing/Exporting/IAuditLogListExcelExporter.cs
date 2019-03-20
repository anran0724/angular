using System.Collections.Generic;
using Sinodom.ElevatorCloud.Auditing.Dto;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
