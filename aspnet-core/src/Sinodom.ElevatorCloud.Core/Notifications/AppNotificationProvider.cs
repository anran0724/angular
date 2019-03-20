using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;
using Sinodom.ElevatorCloud.Authorization;

namespace Sinodom.ElevatorCloud.Notifications
{
    public class AppNotificationProvider : NotificationProvider
    {
        public override void SetNotifications(INotificationDefinitionContext context)
        {
            context.Manager.Add(
                new NotificationDefinition(
                    AppNotificationNames.NewUserRegistered,
                    displayName: L("NewUserRegisteredNotificationDefinition"),
                    permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Users)
                    )
                );

            context.Manager.Add(
                new NotificationDefinition(
                    AppNotificationNames.NewTenantRegistered,
                    displayName: L("NewTenantRegisteredNotificationDefinition"),
                    permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                    )
                );
            context.Manager.Add(
               new NotificationDefinition(
                   AppNotificationNames.AuditSuccess,
                   displayName: L("AuditSuccessMessage"),
                   permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Users)
                   )
               );
            context.Manager.Add(
              new NotificationDefinition(
                  AppNotificationNames.AuditFailure,
                  displayName: L("AuditFailureMessage"),
                  permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Users)
                  )
              );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ElevatorCloudConsts.LocalizationSourceName);
        }
    }
}
