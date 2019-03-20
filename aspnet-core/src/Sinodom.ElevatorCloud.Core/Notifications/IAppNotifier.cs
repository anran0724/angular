using System;
using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.MultiTenancy;

namespace Sinodom.ElevatorCloud.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task GdprDataPrepared(UserIdentifier user, Guid binaryObjectId);
        void AuditSuccess(UserIdentifier user);
        void AuditFailure(UserIdentifier user);
        Task AuditSuccessAsync(UserIdentifier user);
        Task AuditFailureAsync(UserIdentifier user);
        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
