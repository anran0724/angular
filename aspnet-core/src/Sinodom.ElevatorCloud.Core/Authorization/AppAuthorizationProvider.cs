using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Sinodom.ElevatorCloud.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var userPathHistories = pages.CreateChildPermission(AppPermissions.Pages_UserPathHistories, L("UserPathHistories"), multiTenancySides: MultiTenancySides.Tenant);
            userPathHistories.CreateChildPermission(AppPermissions.Pages_UserPathHistories_Create, L("CreateNewUserPathHistory"), multiTenancySides: MultiTenancySides.Tenant);
            userPathHistories.CreateChildPermission(AppPermissions.Pages_UserPathHistories_Edit, L("EditUserPathHistory"), multiTenancySides: MultiTenancySides.Tenant);
            userPathHistories.CreateChildPermission(AppPermissions.Pages_UserPathHistories_Delete, L("DeleteUserPathHistory"), multiTenancySides: MultiTenancySides.Tenant);



            var lanFlowInstanceOperationHistories = pages.CreateChildPermission(AppPermissions.Pages_LanFlowInstanceOperationHistories, L("LanFlowInstanceOperationHistories"), multiTenancySides: MultiTenancySides.Host);
            lanFlowInstanceOperationHistories.CreateChildPermission(AppPermissions.Pages_LanFlowInstanceOperationHistories_Create, L("CreateNewLanFlowInstanceOperationHistory"), multiTenancySides: MultiTenancySides.Host);
            lanFlowInstanceOperationHistories.CreateChildPermission(AppPermissions.Pages_LanFlowInstanceOperationHistories_Edit, L("EditLanFlowInstanceOperationHistory"), multiTenancySides: MultiTenancySides.Host);
            lanFlowInstanceOperationHistories.CreateChildPermission(AppPermissions.Pages_LanFlowInstanceOperationHistories_Delete, L("DeleteLanFlowInstanceOperationHistory"), multiTenancySides: MultiTenancySides.Host);



            var lanFlowInstances = pages.CreateChildPermission(AppPermissions.Pages_LanFlowInstances, L("LanFlowInstances"), multiTenancySides: MultiTenancySides.Host);
            lanFlowInstances.CreateChildPermission(AppPermissions.Pages_LanFlowInstances_Create, L("CreateNewLanFlowInstance"), multiTenancySides: MultiTenancySides.Host);
            lanFlowInstances.CreateChildPermission(AppPermissions.Pages_LanFlowInstances_Edit, L("EditLanFlowInstance"), multiTenancySides: MultiTenancySides.Host);
            lanFlowInstances.CreateChildPermission(AppPermissions.Pages_LanFlowInstances_Delete, L("DeleteLanFlowInstance"), multiTenancySides: MultiTenancySides.Host);



            var eccpElevator = pages.CreateChildPermission(AppPermissions.Pages_EccpElevator, L("EccpElevator"));

            var eccpBaseElevators = eccpElevator.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevators, L("EccpBaseElevators"));
            eccpBaseElevators.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create, L("CreateNewEccpBaseElevator"));
            eccpBaseElevators.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit, L("EditEccpBaseElevator"));
            eccpBaseElevators.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevators_Delete, L("DeleteEccpBaseElevator"));
            eccpBaseElevators.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevators_ViewEvaluations, L("ViewEvaluationsEccpBaseElevator"));
            eccpBaseElevators.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevators_ViewMaintenanceWorkOrders, L("ViewMaintenanceWorkOrderEccpBaseElevator"));

            var eccpElevatorQrCodes = eccpElevator.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes, L("EccpElevatorQrCodes"));
            eccpElevatorQrCodes.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Create, L("CreateNewEccpElevatorQrCode"));
            eccpElevatorQrCodes.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Edit, L("EditEccpElevatorQrCode"));
            eccpElevatorQrCodes.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Delete, L("DeleteEccpElevatorQrCode"));

            var eccpBaseElevatorLabels = eccpElevator.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels, L("EccpBaseElevatorLabels"));
            eccpBaseElevatorLabels.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Create, L("CreateNewEccpBaseElevatorLabel"));
            eccpBaseElevatorLabels.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Edit, L("EditEccpBaseElevatorLabel"));
            eccpBaseElevatorLabels.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Delete, L("DeleteEccpBaseElevatorLabel"));
            eccpBaseElevatorLabels.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_DiscontinueUse, L("DiscontinueUse"));

            eccpElevator.CreateChildPermission(AppPermissions.Pages_EccpElevator_ElevatorClaims, L("EccpBaseElevatorsClaim"));


            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var eccpBaseSaicUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpBaseSaicUnits, L("EccpBaseSaicUnits"));
            eccpBaseSaicUnits.CreateChildPermission(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Create, L("CreateNewEccpBaseSaicUnit"));
            eccpBaseSaicUnits.CreateChildPermission(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Edit, L("EditEccpBaseSaicUnit"));
            eccpBaseSaicUnits.CreateChildPermission(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Delete, L("DeleteEccpBaseSaicUnit"));



            var lanFlowStatusActions = administration.CreateChildPermission(AppPermissions.Pages_Administration_LanFlowStatusActions, L("LanFlowStatusActions"), multiTenancySides: MultiTenancySides.Host);
            lanFlowStatusActions.CreateChildPermission(AppPermissions.Pages_Administration_LanFlowStatusActions_Create, L("CreateNewLanFlowStatusAction"), multiTenancySides: MultiTenancySides.Host);
            lanFlowStatusActions.CreateChildPermission(AppPermissions.Pages_Administration_LanFlowStatusActions_Edit, L("EditLanFlowStatusAction"), multiTenancySides: MultiTenancySides.Host);
            lanFlowStatusActions.CreateChildPermission(AppPermissions.Pages_Administration_LanFlowStatusActions_Delete, L("DeleteLanFlowStatusAction"), multiTenancySides: MultiTenancySides.Host);



            var lanFlowSchemes = administration.CreateChildPermission(AppPermissions.Pages_Administration_LanFlowSchemes, L("LanFlowSchemes"), multiTenancySides: MultiTenancySides.Host);
            lanFlowSchemes.CreateChildPermission(AppPermissions.Pages_Administration_LanFlowSchemes_Create, L("CreateNewLanFlowScheme"), multiTenancySides: MultiTenancySides.Host);
            lanFlowSchemes.CreateChildPermission(AppPermissions.Pages_Administration_LanFlowSchemes_Edit, L("EditLanFlowScheme"), multiTenancySides: MultiTenancySides.Host);
            lanFlowSchemes.CreateChildPermission(AppPermissions.Pages_Administration_LanFlowSchemes_Delete, L("DeleteLanFlowScheme"), multiTenancySides: MultiTenancySides.Host);



            var eccpAppVersions = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpAppVersions, L("EccpAppVersions"), multiTenancySides: MultiTenancySides.Host);
            eccpAppVersions.CreateChildPermission(AppPermissions.Pages_Administration_EccpAppVersions_Create, L("CreateNewEccpAppVersion"), multiTenancySides: MultiTenancySides.Host);
            eccpAppVersions.CreateChildPermission(AppPermissions.Pages_Administration_EccpAppVersions_Edit, L("EditEccpAppVersion"), multiTenancySides: MultiTenancySides.Host);
            eccpAppVersions.CreateChildPermission(AppPermissions.Pages_Administration_EccpAppVersions_Delete, L("DeleteEccpAppVersion"), multiTenancySides: MultiTenancySides.Host);




            var eccpMaintenance = pages.CreateChildPermission(AppPermissions.Pages_EccpMaintenance, L("EccpMaintenance"));

            var eccpMaintenanceWorkOrders = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders, L("EccpMaintenanceWorkOrders"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Create, L("CreateNewEccpMaintenanceWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Edit, L("EditEccpMaintenanceWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_Delete, L("DeleteEccpMaintenanceWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_CloseWorkOrder, L("CloseEccpMaintenanceWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrders_ViewEvaluations, L("ViewEvaluations"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceWorkOrderTransfers = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers, L("EccpMaintenanceWorkOrderTransfers"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkOrderTransfers.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers_Audit, L("AuditEccpMaintenanceWorkOrderTransfer"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenancePlans = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans, L("EccpMaintenancePlans"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenancePlans.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Create, L("CreateNewEccpMaintenancePlan"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenancePlans.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Edit, L("EditEccpMaintenancePlan"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenancePlans.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_Delete, L("DeleteEccpMaintenancePlan"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenancePlans.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_ClosePlan, L("CloseEccpMaintenancePlan"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenancePlans.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePlans_MaintenanceWorkOrders, L("MaintenanceWorkOrders"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceTempWorkOrders = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders, L("EccpMaintenanceTempWorkOrders"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTempWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Create, L("CreateNewEccpMaintenanceTempWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTempWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Edit, L("EditEccpMaintenanceTempWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTempWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrders_Delete, L("DeleteEccpMaintenanceTempWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceTroubledWorkOrders = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders, L("EccpMaintenanceTroubledWorkOrders"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTroubledWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Create, L("CreateNewEccpMaintenanceTroubledWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTroubledWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit, L("EditEccpMaintenanceTroubledWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTroubledWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Delete, L("DeleteEccpMaintenanceTroubledWorkOrder"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTroubledWorkOrders.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Audit, L("Audit"), multiTenancySides: MultiTenancySides.Tenant);

            eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenancePeriodWorkOrders, L("EccpMaintenancePeriodWorkOrders"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceReportGeneration = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceReportGeneration, L("EccpMaintenanceReportGeneration"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceReportGeneration.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Print, L("PrintEccpMaintenanceReportGeneration"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceReportGeneration.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceReportGeneration_Excel, L("ExcelEccpMaintenanceReportGeneration"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceWorkLogs = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs, L("EccpMaintenanceWorkLogs"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkLogs.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Create, L("CreateNewEccpMaintenanceWorkLog"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkLogs.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Edit, L("EditEccpMaintenanceWorkLog"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkLogs.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkLogs_Delete, L("DeleteEccpMaintenanceWorkLog"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceWorkOrderEvaluations = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations, L("EccpMaintenanceWorkOrderEvaluations"), multiTenancySides: MultiTenancySides.Host);
            eccpMaintenanceWorkOrderEvaluations.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Create, L("CreateNewEccpMaintenanceWorkOrderEvaluation"), multiTenancySides: MultiTenancySides.Host);
            eccpMaintenanceWorkOrderEvaluations.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Edit, L("EditEccpMaintenanceWorkOrderEvaluation"), multiTenancySides: MultiTenancySides.Host);
            eccpMaintenanceWorkOrderEvaluations.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Delete, L("DeleteEccpMaintenanceWorkOrderEvaluation"), multiTenancySides: MultiTenancySides.Host);

            var eccpMaintenanceTempWorkOrderActionLogs = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs, L("EccpMaintenanceTempWorkOrderActionLogs"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTempWorkOrderActionLogs.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Create, L("CreateNewEccpMaintenanceTempWorkOrderActionLog"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTempWorkOrderActionLogs.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Edit, L("EditEccpMaintenanceTempWorkOrderActionLog"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceTempWorkOrderActionLogs.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTempWorkOrderActionLogs_Delete, L("DeleteEccpMaintenanceTempWorkOrderActionLog"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceWorkFlows = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows, L("EccpMaintenanceWorkFlows"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkFlows.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Create, L("CreateNewEccpMaintenanceWorkFlow"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkFlows.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Edit, L("EditEccpMaintenanceWorkFlow"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorkFlows.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkFlows_Delete, L("DeleteEccpMaintenanceWorkFlow"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceWorks = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks, L("EccpMaintenanceWorks"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorks.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Create, L("CreateNewEccpMaintenanceWork"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorks.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Edit, L("EditEccpMaintenanceWork"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceWorks.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorks_Delete, L("DeleteEccpMaintenanceWork"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceTemplates = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates, L("EccpMaintenanceTemplates"));
            eccpMaintenanceTemplates.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Create, L("CreateNewEccpMaintenanceTemplate"));
            eccpMaintenanceTemplates.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Edit, L("EditEccpMaintenanceTemplate"));
            eccpMaintenanceTemplates.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplates_Delete, L("DeleteEccpMaintenanceTemplate"));

            var eccpMaintenanceTemplateNodes = eccpMaintenance.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes, L("EccpMaintenanceTemplateNodes"));
            eccpMaintenanceTemplateNodes.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Create, L("CreateNewEccpMaintenanceTemplateNode"));
            eccpMaintenanceTemplateNodes.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Edit, L("EditEccpMaintenanceTemplateNode"));
            eccpMaintenanceTemplateNodes.CreateChildPermission(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTemplateNodes_Delete, L("DeleteEccpMaintenanceTemplateNode"));






            var eccpMaintenanceCompanyChangeLogs = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs, L("EccpMaintenanceCompanyChangeLogs"), multiTenancySides: MultiTenancySides.Host);
            eccpMaintenanceCompanyChangeLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Create, L("CreateNewEccpMaintenanceCompanyChangeLog"), multiTenancySides: MultiTenancySides.Host);
            eccpMaintenanceCompanyChangeLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Edit, L("EditEccpMaintenanceCompanyChangeLog"), multiTenancySides: MultiTenancySides.Host);
            eccpMaintenanceCompanyChangeLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceCompanyChangeLogs_Delete, L("DeleteEccpMaintenanceCompanyChangeLog"), multiTenancySides: MultiTenancySides.Host);



            var eccpPropertyCompanyChangeLogs = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs, L("EccpPropertyCompanyChangeLogs"), multiTenancySides: MultiTenancySides.Host);
            eccpPropertyCompanyChangeLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Create, L("CreateNewEccpPropertyCompanyChangeLog"), multiTenancySides: MultiTenancySides.Host);
            eccpPropertyCompanyChangeLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Edit, L("EditEccpPropertyCompanyChangeLog"), multiTenancySides: MultiTenancySides.Host);
            eccpPropertyCompanyChangeLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpPropertyCompanyChangeLogs_Delete, L("DeleteEccpPropertyCompanyChangeLog"), multiTenancySides: MultiTenancySides.Host);



            var eccpCompanyAudits = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpCompanyAudits, L("EccpCompanyAudits"), multiTenancySides: MultiTenancySides.Host);
            eccpCompanyAudits.CreateChildPermission(AppPermissions.Pages_Administration_EccpCompanyAudits_Edit, L("EditEccpCompanyAudit"), multiTenancySides: MultiTenancySides.Host);

            var eccpCompanyUserAuditLogs = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs, L("EccpCompanyUserAuditLogs"), multiTenancySides: MultiTenancySides.Host);
            eccpCompanyUserAuditLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Create, L("CreateNewEccpCompanyUserAuditLog"), multiTenancySides: MultiTenancySides.Host);
            eccpCompanyUserAuditLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Edit, L("EditEccpCompanyUserAuditLog"), multiTenancySides: MultiTenancySides.Host);
            eccpCompanyUserAuditLogs.CreateChildPermission(AppPermissions.Pages_Administration_EccpCompanyUserAuditLogs_Delete, L("DeleteEccpCompanyUserAuditLog"), multiTenancySides: MultiTenancySides.Host);




            var eccpMaintenanceContracts = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceContracts, L("EccpMaintenanceContracts"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceContracts.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceContracts_Create, L("CreateNewEccpMaintenanceContract"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceContracts.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceContracts_Edit, L("EditEccpMaintenanceContract"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceContracts.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceContracts_Delete, L("DeleteEccpMaintenanceContract"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceContracts.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceContracts_StopContract, L("StopContractEccpMaintenanceContract"), multiTenancySides: MultiTenancySides.Tenant);

            var eccpMaintenanceContractsStop = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop, L("EccpMaintenanceContractsStop"), multiTenancySides: MultiTenancySides.Tenant);
            eccpMaintenanceContractsStop.CreateChildPermission(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop_RecoveryContract, L("RecoveryMaintenanceContract"), multiTenancySides: MultiTenancySides.Tenant);



            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));



            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

            var eccpCompanyUserExtensions = administration.CreateChildPermission(AppPermissions.Pages_Administration_EccpCompanyUserExtensions, L("EccpCompanyUserExtensions"));
            eccpCompanyUserExtensions.CreateChildPermission(AppPermissions.Pages_Administration_EccpCompanyUserExtensions_EditState, L("EditState"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            var elevatorClaimLogs = eccpElevator.CreateChildPermission(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs, L("ElevatorClaimLogs"), multiTenancySides: MultiTenancySides.Tenant);
            elevatorClaimLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Create, L("CreateNewElevatorClaimLog"), multiTenancySides: MultiTenancySides.Tenant);
            elevatorClaimLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Edit, L("EditElevatorClaimLog"), multiTenancySides: MultiTenancySides.Tenant);
            elevatorClaimLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_ElevatorClaimLogs_Delete, L("DeleteElevatorClaimLog"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var eccpBaseElevatorLabelBindLogs = eccpElevator.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs, L("EccpBaseElevatorLabelBindLogs"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorLabelBindLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Create, L("CreateNewEccpBaseElevatorLabelBindLog"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorLabelBindLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Edit, L("EditEccpBaseElevatorLabelBindLog"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorLabelBindLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabelBindLogs_Delete, L("DeleteEccpBaseElevatorLabelBindLog"), multiTenancySides: MultiTenancySides.Host);

            var eccpElevatorChangeLogs = eccpElevator.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs, L("EccpElevatorChangeLogs"), multiTenancySides: MultiTenancySides.Host);
            eccpElevatorChangeLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Create, L("CreateNewEccpElevatorChangeLog"), multiTenancySides: MultiTenancySides.Host);
            eccpElevatorChangeLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Edit, L("EditEccpElevatorChangeLog"), multiTenancySides: MultiTenancySides.Host);
            eccpElevatorChangeLogs.CreateChildPermission(AppPermissions.Pages_EccpElevator_EccpElevatorChangeLogs_Delete, L("DeleteEccpElevatorChangeLog"), multiTenancySides: MultiTenancySides.Host);

            var eccpDict = pages.CreateChildPermission(AppPermissions.Pages_EccpDict, L("EccpDict"));

            var eccpDictElevatorTypes = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes, L("EccpDictElevatorTypes"), multiTenancySides: MultiTenancySides.Host);
            eccpDictElevatorTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Create, L("CreateNewEccpDictElevatorType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictElevatorTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Edit, L("EditEccpDictElevatorType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictElevatorTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Delete, L("DeleteEccpDictElevatorType"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictLabelStatuses = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses, L("EccpDictLabelStatuses"), multiTenancySides: MultiTenancySides.Host);
            eccpDictLabelStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Create, L("CreateNewEccpDictLabelStatus"), multiTenancySides: MultiTenancySides.Host);
            eccpDictLabelStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Edit, L("EditEccpDictLabelStatus"), multiTenancySides: MultiTenancySides.Host);
            eccpDictLabelStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Delete, L("DeleteEccpDictLabelStatus"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictMaintenanceItems = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems, L("EccpDictMaintenanceItems"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceItems.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Create, L("CreateNewEccpDictMaintenanceItem"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceItems.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Edit, L("EditEccpDictMaintenanceItem"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceItems.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Delete, L("DeleteEccpDictMaintenanceItem"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictTempWorkOrderTypes = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes, L("EccpDictTempWorkOrderTypes"), multiTenancySides: MultiTenancySides.Host);
            eccpDictTempWorkOrderTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Create, L("CreateNewEccpDictTempWorkOrderType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictTempWorkOrderTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Edit, L("EditEccpDictTempWorkOrderType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictTempWorkOrderTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Delete, L("DeleteEccpDictTempWorkOrderType"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictMaintenanceWorkFlowStatuses = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses, L("EccpDictMaintenanceWorkFlowStatuses"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceWorkFlowStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Create, L("CreateNewEccpDictMaintenanceWorkFlowStatus"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceWorkFlowStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Edit, L("EditEccpDictMaintenanceWorkFlowStatus"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceWorkFlowStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Delete, L("DeleteEccpDictMaintenanceWorkFlowStatus"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictNodeTypes = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictNodeTypes, L("EccpDictNodeTypes"), multiTenancySides: MultiTenancySides.Host);
            eccpDictNodeTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Create, L("CreateNewEccpDictNodeType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictNodeTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Edit, L("EditEccpDictNodeType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictNodeTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Delete, L("DeleteEccpDictNodeType"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictMaintenanceStatuses = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses, L("EccpDictMaintenanceStatuses"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Create, L("CreateNewEccpDictMaintenanceStatus"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Edit, L("EditEccpDictMaintenanceStatus"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Delete, L("DeleteEccpDictMaintenanceStatus"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictPlaceTypes = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes, L("EccpDictPlaceTypes"), multiTenancySides: MultiTenancySides.Host);
            eccpDictPlaceTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Create, L("CreateNewEccpDictPlaceType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictPlaceTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Edit, L("EditEccpDictPlaceType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictPlaceTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Delete, L("DeleteEccpDictPlaceType"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictMaintenanceTypes = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes, L("EccpDictMaintenanceTypes"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Create, L("CreateNewEccpDictMaintenanceType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Edit, L("EditEccpDictMaintenanceType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictMaintenanceTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Delete, L("DeleteEccpDictMaintenanceType"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictWorkOrderTypes = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes, L("EccpDictWorkOrderTypes"), multiTenancySides: MultiTenancySides.Host);
            eccpDictWorkOrderTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Create, L("CreateNewEccpDictWorkOrderType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictWorkOrderTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Edit, L("EditEccpDictWorkOrderType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictWorkOrderTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Delete, L("DeleteEccpDictWorkOrderType"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictElevatorStatuses = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses, L("ECCPDictElevatorStatuses"), multiTenancySides: MultiTenancySides.Host);
            eccpDictElevatorStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Create, L("CreateNewECCPDictElevatorStatus"), multiTenancySides: MultiTenancySides.Host);
            eccpDictElevatorStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Edit, L("EditECCPDictElevatorStatus"), multiTenancySides: MultiTenancySides.Host);
            eccpDictElevatorStatuses.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Delete, L("DeleteECCPDictElevatorStatus"), multiTenancySides: MultiTenancySides.Host);

            var eccpDictEditionsTypes = eccpDict.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpEditionsTypes, L("ECCPEditionsTypes"), multiTenancySides: MultiTenancySides.Host);
            eccpDictEditionsTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpEditionsTypes_Create, L("CreateNewECCPEditionsType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictEditionsTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpEditionsTypes_Edit, L("EditECCPEditionsType"), multiTenancySides: MultiTenancySides.Host);
            eccpDictEditionsTypes.CreateChildPermission(AppPermissions.Pages_EccpDict_EccpEditionsTypes_Delete, L("DeleteECCPEditionsType"), multiTenancySides: MultiTenancySides.Host);



            var eccpBase = pages.CreateChildPermission(AppPermissions.Pages_EccpBase, L("EccpBase"));

            var eccpBaseElevatorModels = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels, L("EccpBaseElevatorModels"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorModels.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Create, L("CreateNewEccpBaseElevatorModel"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorModels.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Edit, L("EditEccpBaseElevatorModel"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorModels.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Delete, L("DeleteEccpBaseElevatorModel"), multiTenancySides: MultiTenancySides.Host);

            var eccpBaseElevatorBrands = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands, L("EccpBaseElevatorBrands"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorBrands.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Create, L("CreateNewEccpBaseElevatorBrand"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorBrands.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Edit, L("EditEccpBaseElevatorBrand"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseElevatorBrands.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Delete, L("DeleteEccpBaseElevatorBrand"), multiTenancySides: MultiTenancySides.Host);

            var eccpBaseAnnualInspectionUnits = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits, L("ECCPBaseAnnualInspectionUnits"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseAnnualInspectionUnits.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Create, L("CreateNewECCPBaseAnnualInspectionUnit"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseAnnualInspectionUnits.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Edit, L("EditECCPBaseAnnualInspectionUnit"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseAnnualInspectionUnits.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Delete, L("DeleteECCPBaseAnnualInspectionUnit"), multiTenancySides: MultiTenancySides.Host);

            var eccpBaseRegisterCompanies = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies, L("ECCPBaseRegisterCompanies"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseRegisterCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Create, L("CreateNewECCPBaseRegisterCompany"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseRegisterCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Edit, L("EditECCPBaseRegisterCompany"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseRegisterCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Delete, L("DeleteECCPBaseRegisterCompany"), multiTenancySides: MultiTenancySides.Host);

            var eccpBaseProductionCompanies = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies, L("ECCPBaseProductionCompanies"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseProductionCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Create, L("CreateNewECCPBaseProductionCompany"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseProductionCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Edit, L("EditECCPBaseProductionCompany"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseProductionCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Delete, L("DeleteECCPBaseProductionCompany"), multiTenancySides: MultiTenancySides.Host);

            var eccpBaseMaintenanceCompanies = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies, L("ECCPBaseMaintenanceCompanies"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseMaintenanceCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Create, L("CreateNewECCPBaseMaintenanceCompany"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseMaintenanceCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Edit, L("EditECCPBaseMaintenanceCompany"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseMaintenanceCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Delete, L("DeleteECCPBaseMaintenanceCompany"), multiTenancySides: MultiTenancySides.Host);

            var eccpBasePropertyCompanies = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies, L("ECCPBasePropertyCompanies"), multiTenancySides: MultiTenancySides.Host);
            eccpBasePropertyCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Create, L("CreateNewECCPBasePropertyCompany"), multiTenancySides: MultiTenancySides.Host);
            eccpBasePropertyCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Edit, L("EditECCPBasePropertyCompany"), multiTenancySides: MultiTenancySides.Host);
            eccpBasePropertyCompanies.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Delete, L("DeleteECCPBasePropertyCompany"), multiTenancySides: MultiTenancySides.Host);

            var eccpBaseCommunities = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseCommunities, L("ECCPBaseCommunities"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseCommunities.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseCommunities_Create, L("CreateNewECCPBaseCommunity"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseCommunities.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseCommunities_Edit, L("EditECCPBaseCommunity"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseCommunities.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseCommunities_Delete, L("DeleteECCPBaseCommunity"), multiTenancySides: MultiTenancySides.Host);

            var eccpBaseAreas = eccpBase.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseAreas, L("ECCPBaseAreas"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseAreas.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseAreas_Create, L("CreateNewECCPBaseArea"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseAreas.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseAreas_Edit, L("EditECCPBaseArea"), multiTenancySides: MultiTenancySides.Host);
            eccpBaseAreas.CreateChildPermission(AppPermissions.Pages_EccpBase_EccpBaseAreas_Delete, L("DeleteECCPBaseArea"), multiTenancySides: MultiTenancySides.Host);




            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ElevatorCloudConsts.LocalizationSourceName);
        }
    }
}
