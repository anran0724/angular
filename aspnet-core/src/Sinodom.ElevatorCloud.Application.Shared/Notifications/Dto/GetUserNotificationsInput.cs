using Abp.Notifications;
using Sinodom.ElevatorCloud.Dto;

namespace Sinodom.ElevatorCloud.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}
