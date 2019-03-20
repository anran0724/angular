using System;
using System.Threading.Tasks;
using Abp;
using Abp.Localization;
using Abp.Notifications;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.MultiTenancy;

namespace Sinodom.ElevatorCloud.Notifications
{
    public class AppNotifier : ElevatorCloudDomainServiceBase, IAppNotifier
    {
        private readonly INotificationPublisher _notificationPublisher;

        public AppNotifier(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public async Task WelcomeToTheApplicationAsync(User user)
        {
            await _notificationPublisher.PublishAsync(
                AppNotificationNames.WelcomeToTheApplication,
                new MessageNotificationData(L("WelcomeToTheApplicationNotificationMessage")),
                severity: NotificationSeverity.Success,
                userIds: new[] { user.ToUserIdentifier() }
                );
        }

        public async Task NewUserRegisteredAsync(User user)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "NewUserRegisteredNotificationMessage",
                    ElevatorCloudConsts.LocalizationSourceName
                    )
                );

            notificationData["userName"] = user.UserName;
            notificationData["emailAddress"] = user.EmailAddress;

            await _notificationPublisher.PublishAsync(AppNotificationNames.NewUserRegistered, notificationData, tenantIds: new[] { user.TenantId });
        }

        public async Task NewTenantRegisteredAsync(Tenant tenant)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "NewTenantRegisteredNotificationMessage",
                    ElevatorCloudConsts.LocalizationSourceName
                    )
                );

            notificationData["tenancyName"] = tenant.TenancyName;
            await _notificationPublisher.PublishAsync(AppNotificationNames.NewTenantRegistered, notificationData);
        }

        public async Task GdprDataPrepared(UserIdentifier user, Guid binaryObjectId)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "GdprDataPreparedNotificationMessage",
                    ElevatorCloudConsts.LocalizationSourceName
                )
            );

            notificationData["binaryObjectId"] = binaryObjectId;

            await _notificationPublisher.PublishAsync(AppNotificationNames.GdprDataPrepared, notificationData, userIds: new[] { user });
        }
        /// <summary>
        /// 同步的审核通过消息提醒
        /// </summary>
        /// <param name="user"></param>
        public  void AuditSuccess(UserIdentifier user)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "AuditSuccess",
                    ElevatorCloudConsts.LocalizationSourceName
                )
            );
             _notificationPublisher.Publish(AppNotificationNames.AuditSuccess, notificationData, userIds: new[] { user });
        }
        /// <summary>
        /// 同步的审核不通过消息提醒
        /// </summary>
        /// <param name="user"></param>
        public void  AuditFailure(UserIdentifier user)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "AuditFailure",
                    ElevatorCloudConsts.LocalizationSourceName
                )
            );
             _notificationPublisher.Publish(AppNotificationNames.AuditFailure, notificationData, userIds: new[] { user });
        }
        /// <summary>
        /// 异步的审核通过消息提醒
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AuditSuccessAsync(UserIdentifier user)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "AuditSuccess",
                    ElevatorCloudConsts.LocalizationSourceName
                )
            );
            await _notificationPublisher.PublishAsync(AppNotificationNames.AuditSuccess, notificationData, userIds: new[] { user });
        }
        /// <summary>
        /// 异步的审核不通过消息提醒
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AuditFailureAsync(UserIdentifier user)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "AuditFailure",
                    ElevatorCloudConsts.LocalizationSourceName
                )
            );
            await _notificationPublisher.PublishAsync(AppNotificationNames.AuditFailure, notificationData, userIds: new[] { user });
        }
        //This is for test purposes
        public async Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info)
        {
            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: severity,
                userIds: new[] { user }
                );
        }
    }
}
