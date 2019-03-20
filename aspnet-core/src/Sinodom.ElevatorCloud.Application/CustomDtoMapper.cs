using Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos;
using Sinodom.ElevatorCloud.EccpBaseSaicUnits;
using Sinodom.ElevatorCloud.LanFlows.Dtos;
using Sinodom.ElevatorCloud.LanFlows;
using Sinodom.ElevatorCloud.EccpAppVersions.Dtos;
using Sinodom.ElevatorCloud.EccpAppVersions;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using Sinodom.ElevatorCloud.Auditing.Dto;
using Sinodom.ElevatorCloud.Authorization.Accounts.Dto;
using Sinodom.ElevatorCloud.Authorization.Permissions.Dto;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Roles.Dto;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Authorization.Users.Dto;
using Sinodom.ElevatorCloud.Authorization.Users.Profile.Dto;
using Sinodom.ElevatorCloud.Chat;
using Sinodom.ElevatorCloud.Chat.Dto;
using Sinodom.ElevatorCloud.Common.Dto;
using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;
using Sinodom.ElevatorCloud.ECCPBaseAreas;
using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;
using Sinodom.ElevatorCloud.ECCPBaseCommunities;
using Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos;
using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos;
using Sinodom.ElevatorCloud.EccpBaseElevatorLabels;
using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;
using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;
using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Dtos;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;
using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EccpDict.Dtos;
using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceTransfers;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;
using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Editions.Dto;
using Sinodom.ElevatorCloud.Editions.Dtos;
using Sinodom.ElevatorCloud.Friendships;
using Sinodom.ElevatorCloud.Friendships.Cache;
using Sinodom.ElevatorCloud.Friendships.Dto;
using Sinodom.ElevatorCloud.Localization.Dto;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos;
using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos;
using Sinodom.ElevatorCloud.MultiTenancy.HostDashboard.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;
using Sinodom.ElevatorCloud.Notifications.Dto;
using Sinodom.ElevatorCloud.Organizations.Dto;
using Sinodom.ElevatorCloud.Sessions.Dto;
using Sinodom.ElevatorCloud.Statistics.Dtos;

namespace Sinodom.ElevatorCloud
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
           configuration.CreateMap<CreateOrEditEccpBaseSaicUnitDto, EccpBaseSaicUnit>();
            configuration.CreateMap<EditEccpBaseSaicUnitDto, EccpBaseSaicUnit>();
            configuration.CreateMap<EccpBaseSaicUnit, EccpBaseSaicUnitDto>();
           configuration.CreateMap<CreateOrEditUserPathHistoryDto, UserPathHistory>();
           configuration.CreateMap<UserPathHistory, UserPathHistoryDto>();
           configuration.CreateMap<CreateOrEditLanFlowInstanceOperationHistoryDto, LanFlowInstanceOperationHistory>();
           configuration.CreateMap<LanFlowInstanceOperationHistory, LanFlowInstanceOperationHistoryDto>();
           configuration.CreateMap<CreateOrEditLanFlowStatusActionDto, LanFlowStatusAction>();
           configuration.CreateMap<LanFlowStatusAction, LanFlowStatusActionDto>();
           configuration.CreateMap<CreateOrEditLanFlowSchemeDto, LanFlowScheme>();
           configuration.CreateMap<LanFlowScheme, LanFlowSchemeDto>();
            configuration.CreateMap<UploadCreateOrEditEccpAppVersionDto, EccpAppVersion>();
            configuration.CreateMap<CreateOrEditEccpAppVersionDto, EccpAppVersion>();
           configuration.CreateMap<EccpAppVersion, EccpAppVersionDto>();
            configuration.CreateMap<ApplyEccpMaintenanceWorkOrderTransferDto, EccpMaintenanceWorkOrderTransfer>();
            configuration.CreateMap<ApplyEccpMaintenanceTempWorkOrderTransferDto, EccpMaintenanceTempWorkOrderTransfer>();
            configuration.CreateMap<CreateOrEditEccpBaseElevatorLabelBindLogDto, EccpBaseElevatorLabelBindLog>();
            configuration.CreateMap<EccpBaseElevatorLabelBindLog, EccpBaseElevatorLabelBindLogDto>();
            configuration.CreateMap<CreateOrEditEccpBaseElevatorLabelDto, EccpBaseElevatorLabel>();
            configuration.CreateMap<EccpBaseElevatorLabel, EccpBaseElevatorLabelDto>();
            configuration.CreateMap<CreateOrEditEccpDictLabelStatusDto, EccpDictLabelStatus>();
            configuration.CreateMap<EccpDictLabelStatus, EccpDictLabelStatusDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceTroubledWorkOrderDto, EccpMaintenanceTroubledWorkOrder>();
            configuration.CreateMap<EccpMaintenanceTroubledWorkOrder, EccpMaintenanceTroubledWorkOrderDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceWorkLogDto, EccpMaintenanceWorkLog>();
            configuration.CreateMap<EccpMaintenanceWorkLog, EccpMaintenanceWorkLogDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceTroubledWorkOrderDto, EccpMaintenanceTroubledWorkOrder>();
            configuration.CreateMap<AuditEccpMaintenanceTroubledWorkOrderDto, EccpMaintenanceTroubledWorkOrder>(); configuration.CreateMap<EccpMaintenanceTroubledWorkOrder, EccpMaintenanceTroubledWorkOrderDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceWorkLogDto, EccpMaintenanceWorkLog>();
            configuration.CreateMap<EccpMaintenanceWorkLog, EccpMaintenanceWorkLogDto>();
            configuration.CreateMap<CreateOrEditEccpDictMaintenanceItemDto, EccpDictMaintenanceItem>();
            configuration.CreateMap<EccpDictMaintenanceItem, EccpDictMaintenanceItemDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceWorkOrderEvaluationDto, EccpMaintenanceWorkOrderEvaluation>();
            configuration.CreateMap<EccpMaintenanceWorkOrderEvaluation, EccpMaintenanceWorkOrderEvaluationDto>();
            configuration.CreateMap<CreateOrEditElevatorClaimLogDto, ElevatorClaimLog>();
            configuration.CreateMap<ElevatorClaimLog, ElevatorClaimLogDto>();
            configuration.CreateMap<CreateOrEditEccpElevatorChangeLogDto, EccpElevatorChangeLog>();
            configuration.CreateMap<EccpElevatorChangeLog, EccpElevatorChangeLogDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceCompanyChangeLogDto, EccpMaintenanceCompanyChangeLog>();
            configuration.CreateMap<EccpMaintenanceCompanyChangeLog, EccpMaintenanceCompanyChangeLogDto>();
            configuration.CreateMap<CreateOrEditEccpPropertyCompanyChangeLogDto, EccpPropertyCompanyChangeLog>();
            configuration.CreateMap<EccpPropertyCompanyChangeLog, EccpPropertyCompanyChangeLogDto>();
            configuration.CreateMap<EccpMaintenanceCompanyAuditLog, EccpCompanyAuditLogDto>();
            configuration.CreateMap<EccpPropertyCompanyAuditLog, EccpCompanyAuditLogDto>();
            configuration.CreateMap<ECCPBasePropertyCompany, EccpCompanyInfoDto>();
            configuration.CreateMap<ECCPBaseMaintenanceCompany, EccpCompanyInfoDto>();
            configuration.CreateMap<EccpPropertyCompanyExtension, EccpCompanyInfoExtensionDto>();
            configuration.CreateMap<EccpMaintenanceCompanyExtension, EccpCompanyInfoExtensionDto>();
            configuration.CreateMap<CreateOrEditEccpCompanyUserAuditLogDto, EccpCompanyUserAuditLog>();
            configuration.CreateMap<EccpCompanyUserAuditLog, EccpCompanyUserAuditLogDto>();
            configuration.CreateMap<CreateOrEditEccpDictTempWorkOrderTypeDto, EccpDictTempWorkOrderType>();
            configuration.CreateMap<EccpDictTempWorkOrderType, EccpDictTempWorkOrderTypeDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceTempWorkOrderActionLogDto, EccpMaintenanceTempWorkOrderActionLog>();
            configuration.CreateMap<EccpMaintenanceTempWorkOrderActionLog, EccpMaintenanceTempWorkOrderActionLogDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceTempWorkOrderDto, EccpMaintenanceTempWorkOrder>();
            configuration.CreateMap<EccpMaintenanceTempWorkOrder, EccpMaintenanceTempWorkOrderDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceWorkFlowDto, EccpMaintenanceWorkFlow>();

            configuration.CreateMap<EccpMaintenanceWorkFlow, CreateOrEditEccpMaintenanceWorkFlowDto>().ForMember(d => d.workFlowItems, opt => opt.Ignore());


            configuration.CreateMap<EccpMaintenanceWorkFlow, EccpMaintenanceWorkFlowDto>();
            configuration.CreateMap<CreateOrEditEccpDictMaintenanceWorkFlowStatusDto, EccpDictMaintenanceWorkFlowStatus>();
            configuration.CreateMap<EccpDictMaintenanceWorkFlowStatus, EccpDictMaintenanceWorkFlowStatusDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceWorkDto, EccpMaintenanceWork>();
            configuration.CreateMap<CreateOrEditAppEccpMaintenanceWorkDto, EccpMaintenanceWork>();
            configuration.CreateMap<EccpMaintenanceWork, EccpMaintenanceWorkDto>();

            configuration.CreateMap<CreateOrEditEccpMaintenanceTemplateNodeDto, EccpMaintenanceTemplateNode>();

            configuration.CreateMap<EccpMaintenanceTemplateNode, CreateOrEditEccpMaintenanceTemplateNodeDto>().ForMember(m => m.AssignedItemIds, o => o.Ignore());

            configuration.CreateMap<EccpMaintenanceTemplateNode, EccpMaintenanceTemplateNodeDto>();
            configuration.CreateMap<CreateOrEditEccpDictNodeTypeDto, EccpDictNodeType>();
            configuration.CreateMap<EccpDictNodeType, EccpDictNodeTypeDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceWorkOrderDto, EccpMaintenanceWorkOrder>();
            configuration.CreateMap<EccpMaintenanceWorkOrder, EccpMaintenanceWorkOrderDto>();

            configuration.CreateMap<CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto, EccpMaintenanceWorkFlow_Item_Link>();
            configuration.CreateMap<EccpMaintenanceWorkFlow_Item_Link, CreateAPPEccpMaintenanceWorkFlow_Item_LinkDto>();

            configuration.CreateMap<CreateOrEditEccpDictMaintenanceStatusDto, EccpDictMaintenanceStatus>();
            configuration.CreateMap<EccpDictMaintenanceStatus, EccpDictMaintenanceStatusDto>();
            configuration.CreateMap<CreateOrEditEccpElevatorQrCodeDto, EccpElevatorQrCode>();
            configuration.CreateMap<EccpElevatorQrCode, EccpElevatorQrCodeDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceTemplateDto, EccpMaintenanceTemplate>();
            configuration.CreateMap<EccpMaintenanceTemplate, EccpMaintenanceTemplateDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenancePlanDto, EccpMaintenancePlan>();
            configuration.CreateMap<CreateOrEditEccpMaintenancePlanInfoDto, EccpMaintenancePlan>();
            configuration.CreateMap<CreateOrEditEccpMaintenancePlanInfoDto, EccpMaintenancePlanCustomRule>();
            configuration.CreateMap<EccpMaintenancePlan, EccpMaintenancePlanDto>();
            configuration.CreateMap<CreateOrEditEccpBaseElevatorDto, EccpBaseElevator>();
            configuration.CreateMap<EccpBaseElevator, CreateOrEditEccpBaseElevatorDto>().ForMember(m => m.createOrEditEccpBaseElevatorSubsidiaryInfoDto, o => o.Ignore());
            configuration.CreateMap<EccpBaseElevator, EccpBaseElevatorDto>();
            configuration.CreateMap<EccpBaseElevatorDto, EccpBaseElevator>();
            configuration.CreateMap<EccpBaseElevator, GetEccpBaseElevatorsInfoDto>();
            configuration.CreateMap<EccpBaseElevatorDto, CreateOrEditEccpBaseElevatorDto>();
            configuration.CreateMap<EccpBaseElevatorDto, GetEccpBaseElevatorDto>();
            configuration.CreateMap<EccpBaseElevatorSubsidiaryInfo, GetEccpBaseElevatorSubsidiaryInfoDto>();

            configuration.CreateMap<CreateOrEditEccpBaseElevatorSubsidiaryInfoDto, EccpBaseElevatorSubsidiaryInfo>();

            configuration.CreateMap<EccpBaseElevatorSubsidiaryInfo, CreateOrEditEccpBaseElevatorSubsidiaryInfoDto>();
            configuration.CreateMap<EccpBaseElevatorSubsidiaryInfo, EccpBaseElevatorSubsidiaryInfoDto>();

            configuration.CreateMap<CreateOrEditEccpDictPlaceTypeDto, EccpDictPlaceType>();
            configuration.CreateMap<EccpDictPlaceType, EccpDictPlaceTypeDto>();
            configuration.CreateMap<CreateOrEditEccpMaintenanceContractDto, EccpMaintenanceContract>();
            configuration.CreateMap<EccpMaintenanceContract, EccpMaintenanceContractDto>();
            configuration.CreateMap<CreateOrEditEccpDictMaintenanceTypeDto, EccpDictMaintenanceType>();
            configuration.CreateMap<EccpDictMaintenanceType, EccpDictMaintenanceTypeDto>();
            configuration.CreateMap<CreateOrEditEccpDictWorkOrderTypeDto, EccpDictWorkOrderType>();
            configuration.CreateMap<EccpDictWorkOrderType, EccpDictWorkOrderTypeDto>();
            configuration.CreateMap<CreateOrEditEccpBaseElevatorModelDto, EccpBaseElevatorModel>();
            configuration.CreateMap<EccpBaseElevatorModel, EccpBaseElevatorModelDto>();
            configuration.CreateMap<CreateOrEditEccpDictElevatorTypeDto, EccpDictElevatorType>();
            configuration.CreateMap<EccpDictElevatorType, EccpDictElevatorTypeDto>();
            configuration.CreateMap<CreateOrEditEccpBaseElevatorBrandDto, EccpBaseElevatorBrand>();
            configuration.CreateMap<EccpBaseElevatorBrand, EccpBaseElevatorBrandDto>();
            configuration.CreateMap<CreateOrEditECCPBaseAnnualInspectionUnitDto, ECCPBaseAnnualInspectionUnit>();
            configuration.CreateMap<ECCPBaseAnnualInspectionUnit, ECCPBaseAnnualInspectionUnitDto>();
            configuration.CreateMap<CreateOrEditECCPBaseRegisterCompanyDto, ECCPBaseRegisterCompany>();
            configuration.CreateMap<ECCPBaseRegisterCompany, ECCPBaseRegisterCompanyDto>();
            configuration.CreateMap<CreateOrEditECCPBaseProductionCompanyDto, ECCPBaseProductionCompany>();
            configuration.CreateMap<ECCPBaseProductionCompany, ECCPBaseProductionCompanyDto>();
            configuration.CreateMap<CreateOrEditECCPDictElevatorStatusDto, ECCPDictElevatorStatus>();
            configuration.CreateMap<ECCPDictElevatorStatus, ECCPDictElevatorStatusDto>();
            configuration.CreateMap<CreateOrEditECCPBaseMaintenanceCompanyDto, ECCPBaseMaintenanceCompany>();
            configuration.CreateMap<ECCPBaseMaintenanceCompany, ECCPBaseMaintenanceCompanyDto>();
            configuration.CreateMap<CreateOrEditECCPBasePropertyCompanyDto, ECCPBasePropertyCompany>();

            // ͬDataSynchronous begin
            configuration.CreateMap<DeptDataSynchronousEntity, ECCPBasePropertyCompany>();
            configuration.CreateMap<DeptDataSynchronousEntity, ECCPBaseMaintenanceCompany>();
            configuration.CreateMap<DeptDataSynchronousEntity, EccpPropertyCompanyExtension>();
            configuration.CreateMap<DeptDataSynchronousEntity, EccpMaintenanceCompanyExtension>();
            configuration.CreateMap<UserDataSynchronousEntity, User>();
            configuration.CreateMap<UserDataSynchronousEntity, EccpCompanyUserExtension>();
            configuration.CreateMap<ElevatorsDataSynchronousEntity, EccpBaseElevator>();
            configuration.CreateMap<ElevatorsDataSynchronousEntity, EccpBaseElevatorSubsidiaryInfo>();
            configuration.CreateMap<ElevatorsDataSynchronousEntity, EccpElevatorQrCode>();

            // ͬDataSynchronous end
            configuration.CreateMap<ECCPBasePropertyCompany, ECCPBasePropertyCompanyDto>();
            configuration.CreateMap<CreateOrEditECCPBaseCommunityDto, ECCPBaseCommunity>();
            configuration.CreateMap<ECCPBaseCommunity, ECCPBaseCommunityDto>();
            configuration.CreateMap<CreateOrEditECCPBaseAreaDto, ECCPBaseArea>();
            configuration.CreateMap<ECCPBaseArea, ECCPBaseAreaDto>();
            configuration.CreateMap<CreateOrEditECCPEditionsTypeDto, ECCPEditionsType>();
            configuration.CreateMap<ECCPEditionsType, ECCPEditionsTypeDto>();
            configuration.CreateMap<UploadCreateOrEditEccpMaintenanceContractDto, EccpMaintenanceContract>();
            configuration.CreateMap<UploadRecoveryContractEccpMaintenanceContractDto, EccpMaintenanceContract>();
            configuration.CreateMap<StopContractEccpMaintenanceContractDto, EccpMaintenanceContract>();
            // Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            // Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            // Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            // Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            // Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<SubscribableEdition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionSelectDto>();
            configuration.CreateMap<Permission, FlatEditionPermissionDto>();


            // Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            // Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            // Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            // Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            // User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<EccpCompanyUserExtension, EccpCompanyUserExtensionEditDto>();
            configuration.CreateMap<EccpCompanyUserExtensionEditDto, EccpCompanyUserExtension>();
            configuration.CreateMap<UpdateAppOnlineHeartbeatDto, EccpCompanyUserExtension>();
            configuration.CreateMap<UpdateAppOnlineHeartbeatDto, UserPathHistory>();
            // AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();

            // Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            // OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
            configuration.CreateMap<SyncDeptInput, ECCPBaseMaintenanceCompany>().ForMember(d => d.Id, opt => opt.Ignore());

            configuration.CreateMap<SyncDeptInput, ECCPBasePropertyCompany>().ForMember(d => d.Id, opt => opt.Ignore());

            configuration.CreateMap<SyncAreaInput, ECCPBaseArea>().ForMember(d => d.Id, opt => opt.Ignore());
        }
    }
}
