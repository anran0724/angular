using System.Collections.Generic;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}
