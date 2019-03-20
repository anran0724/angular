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

            public const string MainManageDisplayName = "ά����λ������";

            public const string MainInfoManage = "MainInfoManage";

            public const string MainInfoManageDisplayName = "ά����λ��Ϣ����Ա";

            public const string MaintPrincipal = "MaintPrincipal";

            public const string MaintPrincipalDisplayName = "ά��������";

            public const string MainUser = "MainUser";

            public const string MainUserDisplayName = "ά����λ��Ա";
        }

        public static class UseTenants
        {
            public const string UseInfoManage = "UseInfoManage";

            public const string UseInfoManageDisplayName = "ʹ�õ�λ��Ϣ����Ա";

            public const string UseManage = "UseManage";

            public const string UseManageDisplayName = "ʹ�õ�λ��ȫ����Ա";

            public const string UseLeader = "UseLeader";

            public const string UseLeaderDisplayName = "ʹ�õ�λ������";

            public const string UseUser = "UseUser";

            public const string UseUserDisplayName = "ʹ�õ�λ��Ա";
        }
    }
}
