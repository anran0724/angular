using System.Collections.Generic;
using Sinodom.ElevatorCloud.Chat.Dto;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(List<ChatMessageExportDto> messages);
    }
}
