namespace Sinodom.ElevatorCloud.Authorization.Roles
{
    public static class StaticRoleNames
    {
        public static class Host
        {
            public const string Admin = "Admin";
        }

        public static class Tenants
        {
            public const string Admin = "Admin";

            public const string User = "User";
        }

        public static class MaintenanceTenants
        {
            public const string MainManage = "MainManage";

            public const string MainManageDisplayName = "维保单位负责人";

            public const string MainInfoManage = "MainInfoManage";

            public const string MainInfoManageDisplayName = "维保单位信息管理员";

            public const string MaintPrincipal = "MaintPrincipal";

            public const string MaintPrincipalDisplayName = "维保负责人";

            public const string MainUser = "MainUser";

            public const string MainUserDisplayName = "维保单位人员";
        }

        public static class UseTenants
        {
            public const string UseInfoManage = "UseInfoManage";

            public const string UseInfoManageDisplayName = "使用单位信息管理员";

            public const string UseManage = "UseManage";

            public const string UseManageDisplayName = "使用单位安全管理员";

            public const string UseLeader = "UseLeader";

            public const string UseLeaderDisplayName = "使用单位负责人";

            public const string UseUser = "UseUser";

            public const string UseUserDisplayName = "使用单位人员";
        }
    }
}
