namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Startup
{
    using Abp.Application.Navigation;
    using Abp.Localization;

    using Sinodom.ElevatorCloud.Authorization;

    public class AppAreaNameNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            MenuDefinition menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                .AddItem(
                    new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Dashboard,
                        L("Dashboard"),
                        url: "AppAreaName/HostDashboard",
                        icon: "flaticon-line-graph",
                        requiredPermissionName: AppPermissions.Pages_Administration_Host_Dashboard))
                .AddItem(
                    new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Tenants,
                        L("Tenants"),
                        url: "AppAreaName/Tenants",
                        icon: "flaticon-list-3",
                        requiredPermissionName: AppPermissions.Pages_Tenants))
                .AddItem(
                    new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Editions,
                        L("Editions"),
                        url: "AppAreaName/Editions",
                        icon: "flaticon-app",
                        requiredPermissionName: AppPermissions.Pages_Editions))
                .AddItem(
                    new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.Dashboard,
                        L("Dashboard"),
                        url: "AppAreaName/Dashboard",
                        icon: "flaticon-line-graph",
                        requiredPermissionName: AppPermissions.Pages_Tenant_Dashboard))
                .AddItem(
                    new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.EccpMaintenanceContracts,
                        L("EccpMaintenanceContracts"),
                        url: "AppAreaName/EccpMaintenanceContracts",
                        icon: "flaticon-more",
                        requiredPermissionName: AppPermissions.Pages_Administration_EccpMaintenanceContracts))
                .AddItem(
                    new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.EccpMaintenanceContractsStop,
                        L("EccpMaintenanceContractsStop"),
                        url: "AppAreaName/EccpMaintenanceContractsStop",
                        icon: "flaticon-more",
                        requiredPermissionName: AppPermissions.Pages_Administration_EccpMaintenanceContractsStop))
                .AddItem(
                    new MenuItemDefinition(
                            AppAreaNamePageNames.Common.EccpMaintenance,
                            L("EccpMaintenance"),
                            "flaticon-more")
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.EccpMaintenanceTemplates,
                                L("EccpMaintenanceTemplates"),
                                url: "AppAreaName/EccpMaintenanceTemplates",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.EccpMaintenanceTemplateNodes,
                                L("EccpMaintenanceTemplateNodes"),
                                url: "AppAreaName/EccpMaintenanceTemplateNodes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenancePlans,
                                L("EccpMaintenancePlans"),
                                url: "AppAreaName/EccpMaintenancePlans",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenanceWorkOrders,
                                L("EccpMaintenanceWorkOrders"),
                                url: "AppAreaName/EccpMaintenanceWorkOrders",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenancePeriodWorkOrders,
                                L("EccpMaintenancePeriodWorkOrders"),
                                url: "AppAreaName/EccpMaintenancePeriodWorkOrders",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenanceReportGeneration,
                                L("EccpMaintenanceReportGeneration"),
                                url: "AppAreaName/EccpMaintenanceReportGeneration",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceReportGeneration))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenanceWorkOrderTransfers,
                                L("EccpMaintenanceWorkOrderTransfers"),
                                url: "AppAreaName/EccpMaintenanceWorkOrderTransfers",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers))
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        AppAreaNamePageNames.Tenant.EccpMaintenanceWorks,
                        //        L("EccpMaintenanceWorks"),
                        //        url: "AppAreaName/EccpMaintenanceWorks",
                        //        icon: "flaticon-more",
                        //        requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks))
                        //.AddItem(
                        //    new MenuItemDefinition(
                        //        AppAreaNamePageNames.Tenant.EccpMaintenanceWorkFlows,
                        //        L("EccpMaintenanceWorkFlows"),
                        //        url: "AppAreaName/EccpMaintenanceWorkFlows",
                        //        icon: "flaticon-more",
                        //        requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenanceTempWorkOrders,
                                L("EccpMaintenanceTempWorkOrders"),
                                url: "AppAreaName/EccpMaintenanceTempWorkOrders",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenanceTempWorkOrderActionLogs,
                                L("EccpMaintenanceTempWorkOrderActionLogs"),
                                url: "AppAreaName/EccpMaintenanceTempWorkOrderActionLogs",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenanceWorkLogs,
                                L("EccpMaintenanceWorkLogs"),
                                url: "AppAreaName/EccpMaintenanceWorkLogs",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.EccpMaintenanceTroubledWorkOrders,
                                L("EccpMaintenanceTroubledWorkOrders"),
                                url: "AppAreaName/EccpMaintenanceTroubledWorkOrders",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpMaintenanceWorkOrderEvaluations,
                                L("EccpMaintenanceWorkOrderEvaluations"),
                                url: "AppAreaName/EccpMaintenanceWorkOrderEvaluations",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpMaintenanceCompanyChangeLogs,
                                L("EccpMaintenanceCompanyChangeLogs"),
                                url: "AppAreaName/EccpMaintenanceCompanyChangeLogs",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs)))
                .AddItem(
                    new MenuItemDefinition(AppAreaNamePageNames.Common.EccpElevator, L("EccpElevator"), "flaticon-more")
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.EccpBaseElevators,
                                L("EccpBaseElevators"),
                                url: "AppAreaName/EccpBaseElevators",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpElevator_EccpBaseElevators))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.EccpElevatorQrCodes,
                                L("EccpElevatorQrCodes"),
                                url: "AppAreaName/EccpElevatorQrCodes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Common.EccpBaseElevatorLabels,
                                L("EccpBaseElevatorLabels"),
                                url: "AppAreaName/EccpBaseElevatorLabels",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpBaseElevatorLabelBindLogs,
                                L("EccpBaseElevatorLabelBindLogs"),
                                url: "AppAreaName/EccpBaseElevatorLabelBindLogs",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.EccpBaseElevatorsClaim,
                                L("EccpBaseElevatorsClaim"),
                                url: "AppAreaName/EccpBaseElevatorsClaim",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpElevator_ElevatorClaims))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.ElevatorClaimLogs,
                                L("ElevatorClaimLogs"),
                                url: "AppAreaName/ElevatorClaimLogs",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpElevator_ElevatorClaimLogs))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpElevatorChangeLogs,
                                L("EccpElevatorChangeLogs"),
                                url: "AppAreaName/EccpElevatorChangeLogs",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs)))
                .AddItem(
                    new MenuItemDefinition(AppAreaNamePageNames.Host.EccpBase, L("EccpBase"), "flaticon-more")
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.ECCPBaseMaintenanceCompanies,
                                L("ECCPBaseMaintenanceCompanies"),
                                url: "AppAreaName/ECCPBaseMaintenanceCompanies",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.ECCPBasePropertyCompanies,
                                L("ECCPBasePropertyCompanies"),
                                url: "AppAreaName/ECCPBasePropertyCompanies",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.ECCPBaseAnnualInspectionUnits,
                                L("ECCPBaseAnnualInspectionUnits"),
                                url: "AppAreaName/ECCPBaseAnnualInspectionUnits",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Common.EccpBaseSaicUnits,
                                L("EccpBaseSaicUnits"),
                                url: "AppAreaName/EccpBaseSaicUnits",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_Administration_EccpBaseSaicUnits))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.ECCPBaseRegisterCompanies,
                                L("ECCPBaseRegisterCompanies"),
                                url: "AppAreaName/ECCPBaseRegisterCompanies",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.ECCPBaseProductionCompanies,
                                L("ECCPBaseProductionCompanies"),
                                url: "AppAreaName/ECCPBaseProductionCompanies",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.ECCPBaseCommunities,
                                L("ECCPBaseCommunities"),
                                url: "AppAreaName/ECCPBaseCommunities",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBaseCommunities))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.ECCPBaseAreas,
                                L("ECCPBaseAreas"),
                                url: "AppAreaName/ECCPBaseAreas",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBaseAreas))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpBaseElevatorBrands,
                                L("EccpBaseElevatorBrands"),
                                url: "AppAreaName/EccpBaseElevatorBrands",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpBaseElevatorModels,
                                L("EccpBaseElevatorModels"),
                                url: "AppAreaName/EccpBaseElevatorModels",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpBase_EccpBaseElevatorModels)))
                .AddItem(
                    new MenuItemDefinition(AppAreaNamePageNames.Host.EccpDict, L("EccpDict"), "flaticon-more")
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpDictElevatorTypes,
                                L("EccpDictElevatorTypes"),
                                url: "AppAreaName/EccpDictElevatorTypes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictElevatorTypes))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpDictPlaceTypes,
                                L("EccpDictPlaceTypes"),
                                url: "AppAreaName/EccpDictPlaceTypes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictPlaceTypes))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpDictMaintenanceTypes,
                                L("EccpDictMaintenanceTypes"),
                                url: "AppAreaName/EccpDictMaintenanceTypes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpDictWorkOrderTypes,
                                L("EccpDictWorkOrderTypes"),
                                url: "AppAreaName/EccpDictWorkOrderTypes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.ECCPDictElevatorStatuses,
                                L("ECCPDictElevatorStatuses"),
                                url: "AppAreaName/ECCPDictElevatorStatuses",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpDictMaintenanceWorkFlowStatuses,
                                L("EccpDictMaintenanceWorkFlowStatuses"),
                                url: "AppAreaName/EccpDictMaintenanceWorkFlowStatuses",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpDictNodeTypes,
                                L("EccpDictNodeTypes"),
                                url: "AppAreaName/EccpDictNodeTypes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictNodeTypes))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpDictMaintenanceStatuses,
                                L("EccpDictMaintenanceStatuses"),
                                url: "AppAreaName/EccpDictMaintenanceStatuses",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses))
                        .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Host.EccpDictMaintenanceItems,
                            L("EccpDictMaintenanceItems"),
                            url: "AppAreaName/EccpDictMaintenanceItems",
                            icon: "flaticon-more",
                            requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems))
                        .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Host.EccpDictLabelStatuses,
                            L("EccpDictLabelStatuses"),
                            url: "AppAreaName/EccpDictLabelStatuses",
                            icon: "flaticon-more",
                            requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictLabelStatuses))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpDictTempWorkOrderTypes,
                                L("EccpDictTempWorkOrderTypes"),
                                url: "AppAreaName/EccpDictTempWorkOrderTypes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpEditionsTypes,
                                L("ECCPEditionsTypes"),
                                url: "AppAreaName/ECCPEditionsTypes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_EccpDict_EccpEditionsTypes)))
                .AddItem(
                    new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Administration,
                            L("Administration"),
                            "flaticon-interface-8")
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Host.LanFlowSchemes,
                                L("LanFlowSchemes"),
                                url: "AppAreaName/LanFlowSchemes",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_Administration_LanFlowSchemes))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Host.LanFlowStatusActions,
                                L("LanFlowStatusActions"),
                                url: "AppAreaName/LanFlowStatusActions",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_Administration_LanFlowStatusActions))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpPropertyCompanyChangeLogs,
                                L("EccpPropertyCompanyChangeLogs"),
                                url: "AppAreaName/EccpPropertyCompanyChangeLogs",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpCompanyAudits,
                                L("EccpCompanyAudits"),
                                url: "AppAreaName/EccpCompanyAudits",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_Administration_EccpCompanyAudits))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpCompanyUserAuditLogs,
                                L("EccpCompanyUserAuditLogs"),
                                url: "AppAreaName/EccpCompanyUserAuditLogs",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.OrganizationUnits,
                                L("OrganizationUnits"),
                                url: "AppAreaName/OrganizationUnits",
                                icon: "flaticon-map",
                                requiredPermissionName: AppPermissions.Pages_Administration_OrganizationUnits))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.Roles,
                                L("Roles"),
                                url: "AppAreaName/Roles",
                                icon: "flaticon-suitcase",
                                requiredPermissionName: AppPermissions.Pages_Administration_Roles))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.EccpCompanyUserExtensions,
                                L("EccpCompanyUserExtensions"),
                                url: "AppAreaName/EccpCompanyUserExtensions",
                                icon: "flaticon-suitcase",
                                requiredPermissionName: AppPermissions.Pages_Administration_EccpCompanyUserExtensions))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.Users,
                                L("Users"),
                                url: "AppAreaName/Users",
                                icon: "flaticon-users",
                                requiredPermissionName: AppPermissions.Pages_Administration_Users))
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Host.EccpAppVersions,
                                L("EccpAppVersions"),
                                url: "AppAreaName/EccpAppVersions",
                                icon: "flaticon-more",
                                requiredPermissionName: AppPermissions.Pages_Administration_EccpAppVersions))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.Languages,
                                L("Languages"),
                                url: "AppAreaName/Languages",
                                icon: "flaticon-tabs",
                                requiredPermissionName: AppPermissions.Pages_Administration_Languages))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.AuditLogs,
                                L("AuditLogs"),
                                url: "AppAreaName/AuditLogs",
                                icon: "flaticon-folder-1",
                                requiredPermissionName: AppPermissions.Pages_Administration_AuditLogs))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.Maintenance,
                                L("Maintenance"),
                                url: "AppAreaName/Maintenance",
                                icon: "flaticon-lock",
                                requiredPermissionName: AppPermissions.Pages_Administration_Host_Maintenance))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.SubscriptionManagement,
                                L("Subscription"),
                                url: "AppAreaName/SubscriptionManagement",
                                icon: "flaticon-refresh",
                                requiredPermissionName: AppPermissions.Pages_Administration_Tenant_SubscriptionManagement))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Common.UiCustomization,
                                L("VisualSettings"),
                                url: "AppAreaName/UiCustomization",
                                icon: "flaticon-medical",
                                requiredPermissionName: AppPermissions.Pages_Administration_UiCustomization))
                        .AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Host.Settings,
                                L("Settings"),
                                url: "AppAreaName/HostSettings",
                                icon: "flaticon-settings",
                                requiredPermissionName: AppPermissions.Pages_Administration_Host_Settings)).AddItem(
                            new MenuItemDefinition(
                                AppAreaNamePageNames.Tenant.Settings,
                                L("Settings"),
                                url: "AppAreaName/Settings",
                                icon: "flaticon-settings",
                                requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings)));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ElevatorCloudConsts.LocalizationSourceName);
        }
    }
}