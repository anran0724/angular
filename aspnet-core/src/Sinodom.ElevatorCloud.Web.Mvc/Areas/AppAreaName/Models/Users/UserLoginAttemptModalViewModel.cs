using System.Collections.Generic;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}
