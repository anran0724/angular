using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Abp;
using Abp.Collections.Extensions;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Extensions;
using Abp.Events.Bus.Entities;
using Abp.Extensions;
using Abp.IdentityServer4;
using Abp.Timing;
using Abp.Zero.EntityFrameworkCore;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Sinodom.ElevatorCloud.Authorization.Roles;
using Sinodom.ElevatorCloud.Authorization.Users;
using Sinodom.ElevatorCloud.Chat;
using Sinodom.ElevatorCloud.EccpAppVersions;
using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
using Sinodom.ElevatorCloud.ECCPBaseAreas;
using Sinodom.ElevatorCloud.ECCPBaseCommunities;
using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
using Sinodom.ElevatorCloud.EccpBaseElevatorLabels;
using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
using Sinodom.ElevatorCloud.EccpBaseSaicUnits;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplateNodes;
using Sinodom.ElevatorCloud.EccpMaintenanceTemplates;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrderActionLogs;
using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders;
using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
using Sinodom.ElevatorCloud.EccpMaintenanceWorks;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Extensions;
using Sinodom.ElevatorCloud.Friendships;
using Sinodom.ElevatorCloud.LanFlows;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.MultiTenancy.Accounting;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;
using Sinodom.ElevatorCloud.Storage;

namespace Sinodom.ElevatorCloud.EntityFrameworkCore
{
    using Sinodom.ElevatorCloud.EccpMaintenanceTransfers;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;
    using Sinodom.ElevatorCloud.MultiTenancy.UserExtensions;

    public class ElevatorCloudDbContext : AbpZeroDbContext<Tenant, Role, User, ElevatorCloudDbContext>, IAbpPersistedGrantDbContext
    {
        public IEccpAbpSessionExtensions AbpSession { get; set; }
        public virtual DbSet<EccpMaintenanceWorkFlow_Refuse_Link> EccpMaintenanceWorkFlow_Refuse_Links { get; set; }

        public virtual DbSet<EccpCompanyUserChangeLog> EccpCompanyUserChangeLogs { get; set; }

        public virtual DbSet<EccpBaseSaicUnit> EccpBaseSaicUnits { get; set; }

        public virtual DbSet<UserPathHistory> UserPathHistories { get; set; }

        public virtual DbSet<LanFlowInstanceOperationHistory> LanFlowInstanceOperationHistories { get; set; }

        public virtual DbSet<LanFlowStatusAction> LanFlowStatusActions { get; set; }

        public virtual DbSet<LanFlowScheme> LanFlowSchemes { get; set; }

        public virtual DbSet<EccpAppVersion> EccpAppVersions { get; set; }

        public virtual DbSet<EccpMaintenanceWorkFlow_Item_Link> EccpMaintenanceWorkFlow_Item_Links { get; set; }

        public virtual DbSet<EccpBaseElevatorLabelBindLog> EccpBaseElevatorLabelBindLogs { get; set; }

        public virtual DbSet<EccpBaseElevatorLabel> EccpBaseElevatorLabels { get; set; }

        public virtual DbSet<EccpDictLabelStatus> EccpDictLabelStatuses { get; set; }

        public virtual DbSet<EccpMaintenanceTroubledWorkOrder> EccpMaintenanceTroubledWorkOrders { get; set; }

        public virtual DbSet<EccpMaintenanceWorkLog> EccpMaintenanceWorkLogs { get; set; }

        public virtual DbSet<EccpMaintenanceTemplateNode_DictMaintenanceItem_Link> EccpMaintenanceTemplateNode_DictMaintenanceItem_Links { get; set; }

        public virtual DbSet<EccpDictMaintenanceItem> EccpDictMaintenanceItems { get; set; }

        public virtual DbSet<EccpMaintenanceWorkOrderEvaluation> EccpMaintenanceWorkOrderEvaluations { get; set; }

        public virtual DbSet<EccpMaintenanceTempWorkOrderTransferAuditLog> EccpMaintenanceTempWorkOrderTransferAuditLogs { get; set; }

        public virtual DbSet<EccpMaintenanceTempWorkOrderTransfer> EccpMaintenanceTempWorkOrderTransfers { get; set; }

        public virtual DbSet<EccpMaintenanceWorkOrderTransferAuditLog> EccpMaintenanceWorkOrderTransferAuditLogs { get; set; }

        public virtual DbSet<EccpMaintenanceWorkOrderTransfer> EccpMaintenanceWorkOrderTransfers { get; set; }

        public virtual DbSet<ElevatorClaimLog> ElevatorClaimLogs { get; set; }

        public virtual DbSet<EccpElevatorChangeLog> EccpElevatorChangeLogs { get; set; }

        public virtual DbSet<EccpMaintenanceCompanyChangeLog> EccpMaintenanceCompanyChangeLogs { get; set; }

        public virtual DbSet<EccpPropertyCompanyChangeLog> EccpPropertyCompanyChangeLogs { get; set; }

        public virtual DbSet<EccpMaintenanceCompanyAuditLog> EccpMaintenanceCompanyAuditLogs { get; set; }

        public virtual DbSet<EccpPropertyCompanyAuditLog> EccpPropertyCompanyAuditLogs { get; set; }

        public virtual DbSet<EccpCompanyUserAuditLog> EccpCompanyUserAuditLogs { get; set; }

        public virtual DbSet<EccpDictTempWorkOrderType> EccpDictTempWorkOrderTypes { get; set; }

        public virtual DbSet<EccpMaintenanceTempWorkOrderActionLog> EccpMaintenanceTempWorkOrderActionLogs { get; set; }

        public virtual DbSet<EccpMaintenanceTempWorkOrder> EccpMaintenanceTempWorkOrders { get; set; }

        public virtual DbSet<EccpElevatorQrCodeBindLog> EccpElevatorQrCodeBindLogs { get; set; }

        public virtual DbSet<EccpMaintenancePlanCustomRule> EccpMaintenancePlanCustomRules { get; set; }

        public virtual DbSet<EccpCompanyUserExtension> EccpCompanyUserExtensions { get; set; }

        public virtual DbSet<EccpPropertyCompanyExtension> EccpPropertyCompanyExtensions { get; set; }

        public virtual DbSet<EccpMaintenanceCompanyExtension> EccpMaintenanceCompanyExtensions { get; set; }

        public virtual DbSet<EccpMaintenanceWorkFlow> EccpMaintenanceWorkFlows { get; set; }

        public virtual DbSet<EccpDictMaintenanceWorkFlowStatus> EccpDictMaintenanceWorkFlowStatuses { get; set; }

        public virtual DbSet<EccpMaintenanceWork> EccpMaintenanceWorks { get; set; }

        public virtual DbSet<EccpMaintenanceTemplateNode> EccpMaintenanceTemplateNodes { get; set; }

        public virtual DbSet<EccpDictNodeType> EccpDictNodeTypes { get; set; }

        public virtual DbSet<EccpMaintenanceWorkOrder> EccpMaintenanceWorkOrders { get; set; }

        public virtual DbSet<EccpDictMaintenanceStatus> EccpDictMaintenanceStatuses { get; set; }

        public virtual DbSet<EccpElevatorQrCode> EccpElevatorQrCodes { get; set; }

        public virtual DbSet<EccpMaintenancePlan_Template_Link> EccpMaintenancePlan_Template_Links { get; set; }

        public virtual DbSet<EccpMaintenancePlan_PropertyUser_Link> EccpMaintenancePlan_PropertyUser_Links { get; set; }

        public virtual DbSet<EccpMaintenancePlan_MaintenanceUser_Link> EccpMaintenancePlan_MaintenanceUser_Links { get; set; }

        public virtual DbSet<EccpMaintenanceWorkOrder_PropertyUser_Link> EccpMaintenanceWorkOrder_PropertyUser_Links { get; set; }

        public virtual DbSet<EccpMaintenanceWorkOrder_MaintenanceUser_Link> EccpMaintenanceWorkOrder_MaintenanceUser_Links { get; set; }

        public virtual DbSet<EccpMaintenanceTemplate> EccpMaintenanceTemplates { get; set; }

        public virtual DbSet<EccpMaintenancePlan> EccpMaintenancePlans { get; set; }

        public virtual DbSet<EccpBaseElevatorSubsidiaryInfo> EccpBaseElevatorSubsidiaryInfos { get; set; }

        public virtual DbSet<EccpBaseElevator> EccpBaseElevators { get; set; }

        public virtual DbSet<EccpDictPlaceType> EccpDictPlaceTypes { get; set; }

        public virtual DbSet<EccpMaintenanceContract> EccpMaintenanceContracts { get; set; }

        public virtual DbSet<EccpMaintenanceContract_Elevator_Link> EccpMaintenanceContract_Elevator_Links { get; set; }

        public virtual DbSet<EccpDictMaintenanceType> EccpDictMaintenanceTypes { get; set; }

        public virtual DbSet<EccpDictWorkOrderType> EccpDictWorkOrderTypes { get; set; }

        public virtual DbSet<EccpBaseElevatorModel> EccpBaseElevatorModels { get; set; }

        public virtual DbSet<EccpDictElevatorType> EccpDictElevatorTypes { get; set; }

        public virtual DbSet<EccpBaseElevatorBrand> EccpBaseElevatorBrands { get; set; }

        public virtual DbSet<ECCPBaseAnnualInspectionUnit> ECCPBaseAnnualInspectionUnits { get; set; }

        public virtual DbSet<ECCPBaseRegisterCompany> ECCPBaseRegisterCompanies { get; set; }

        public virtual DbSet<ECCPBaseProductionCompany> ECCPBaseProductionCompanies { get; set; }

        public virtual DbSet<ECCPDictElevatorStatus> ECCPDictElevatorStatuses { get; set; }

        public virtual DbSet<ECCPBaseMaintenanceCompany> ECCPBaseMaintenanceCompanies { get; set; }

        public virtual DbSet<ECCPBasePropertyCompany> ECCPBasePropertyCompanies { get; set; }

        public virtual DbSet<ECCPBaseCommunity> ECCPBaseCommunities { get; set; }

        public virtual DbSet<ECCPBaseArea> ECCPBaseAreas { get; set; }

        public virtual DbSet<ECCPEditionsType> ECCPEditionsTypes { get; set; }

        public virtual DbSet<ECCPEditionPermission> ECCPEditionPermissions { get; set; }

        public virtual DbSet<ECCPEdition> EccpEditions { get; set; }

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        protected virtual int? CurrentProvinceId => AbpSession.EditionTypeName == "政府" ? AbpSession.ProvinceId : null;
        protected virtual int? CurrenCityId => AbpSession.EditionTypeName == "政府" ? AbpSession.CityId : null;
        protected virtual int? CurrentDistrictId => AbpSession.EditionTypeName == "政府" ? AbpSession.DistrictId : null;
        protected virtual int? CurrentStreetId => AbpSession.EditionTypeName == "政府" ? AbpSession.StreetId : null;

        protected virtual bool IsMustHaveProvinceFilterEnabled => CurrentProvinceId != null;

        protected virtual bool IsMustHaveCityFilterEnabled => CurrenCityId != null;
        protected virtual bool IsMustHaveDistrictFilterEnabled => CurrentDistrictId != null;
        protected virtual bool IsMustHaveStreetFilterEnabled => CurrentStreetId != null;
        protected virtual int? CurrentTenantId => GetCurrentTenantIdOrNull();
        protected virtual bool IsSoftDeleteFilterEnabled => CurrentUnitOfWorkProvider?.Current?.IsFilterEnabled(AbpDataFilters.SoftDelete) == true;

        protected virtual bool IsMayHaveTenantFilterEnabled => CurrentUnitOfWorkProvider?.Current?.IsFilterEnabled(AbpDataFilters.MayHaveTenant) == true;

        protected virtual bool IsMustHaveTenantFilterEnabled => CurrentTenantId != null && CurrentUnitOfWorkProvider?.Current?.IsFilterEnabled(AbpDataFilters.MustHaveTenant) == true;

        private static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(ElevatorCloudDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        public ElevatorCloudDbContext(DbContextOptions<ElevatorCloudDbContext> options)
            : base(options)
        {
            
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EccpBaseSaicUnit>(u => { u.HasIndex(e => new { e.TenantId }); });
            modelBuilder.Entity<UserPathHistory>(u => { u.HasIndex(e => new { e.TenantId }); });
            modelBuilder.Entity<EccpBaseElevatorLabel>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceTroubledWorkOrder>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceWorkLog>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ElevatorClaimLog>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceTempWorkOrderActionLog>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceTempWorkOrder>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceWorkFlow>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceWork>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceTemplateNode>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceWorkOrder>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpElevatorQrCode>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenancePlan_Template_Link>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenancePlan_PropertyUser_Link>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenancePlan_MaintenanceUser_Link>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceTemplate>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenancePlan>(E =>
                       {
                           E.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EccpMaintenanceContract>(E =>
            {
                E.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<BinaryObject>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { e.PaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    if (entityType.IsQueryType)
                    {
                        modelBuilder.Query<TEntity>().HasQueryFilter(filterExpression);
                    }
                    else
                    {
                        modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                    }
                }
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            if (typeof(IMayHaveTenant).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            if (typeof(IHasArea).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted || ((ISoftDelete)e).IsDeleted != IsSoftDeleteFilterEnabled;
                expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
            }

            if (typeof(IMayHaveTenant).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mayHaveTenantFilter = e => ((IMayHaveTenant)e).TenantId == CurrentTenantId || (((IMayHaveTenant)e).TenantId == CurrentTenantId) == IsMayHaveTenantFilterEnabled;
                expression = expression == null ? mayHaveTenantFilter : CombineExpressions(expression, mayHaveTenantFilter);
            }

            if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mustHaveTenantFilter = e => ((IMustHaveTenant)e).TenantId == CurrentTenantId || (((IMustHaveTenant)e).TenantId == CurrentTenantId) == IsMustHaveTenantFilterEnabled;
                expression = expression == null ? mustHaveTenantFilter : CombineExpressions(expression, mustHaveTenantFilter);
            }

            if (typeof(IHasArea).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mustHaveProvinceFilter = e => ((IHasArea)e).ProvinceId == CurrentProvinceId || (((IHasArea)e).ProvinceId == CurrentProvinceId) == IsMustHaveProvinceFilterEnabled;
                expression = expression == null ? mustHaveProvinceFilter : CombineExpressions(expression, mustHaveProvinceFilter);

                Expression<Func<TEntity, bool>> mustHaveCityFilter = e => ((IHasArea)e).CityId == CurrenCityId || (((IHasArea)e).CityId == CurrenCityId) == IsMustHaveCityFilterEnabled;
                expression = expression == null ? mustHaveCityFilter : CombineExpressions(expression, mustHaveCityFilter);

                Expression<Func<TEntity, bool>> mustHaveDistrictFilter = e => ((IHasArea)e).DistrictId == CurrentDistrictId || (((IHasArea)e).DistrictId == CurrentDistrictId) == IsMustHaveDistrictFilterEnabled;
                expression = expression == null ? mustHaveDistrictFilter : CombineExpressions(expression, mustHaveDistrictFilter);

                Expression<Func<TEntity, bool>> mustHaveStreetFilter = e => ((IHasArea)e).StreetId == CurrentStreetId || (((IHasArea)e).StreetId == CurrentStreetId) == IsMustHaveStreetFilterEnabled;
                expression = expression == null ? mustHaveStreetFilter : CombineExpressions(expression, mustHaveStreetFilter);
            }

            return expression;
        }

        public override int SaveChanges()
        {
            try
            {
                var changeReport = ApplyAbpConcepts();
                var result = base.SaveChanges();
                EntityChangeEventHelper.TriggerEvents(changeReport);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new AbpDbConcurrencyException(ex.Message, ex);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var changeReport = ApplyAbpConcepts();
                var result = await base.SaveChangesAsync(cancellationToken);
                await EntityChangeEventHelper.TriggerEventsAsync(changeReport);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new AbpDbConcurrencyException(ex.Message, ex);
            }
        }

        protected virtual EntityChangeReport ApplyAbpConcepts()
        {
            var changeReport = new EntityChangeReport();

            var userId = GetAuditUserId();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                if (entry.State != EntityState.Modified && entry.CheckOwnedEntityChange())
                {
                    Entry(entry.Entity).State = EntityState.Modified;
                }

                ApplyAbpConcepts(entry, userId, changeReport);
            }

            return changeReport;
        }

        protected virtual void ApplyAbpConcepts(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyAbpConceptsForAddedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Modified:
                    ApplyAbpConceptsForModifiedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Deleted:
                    ApplyAbpConceptsForDeletedEntity(entry, userId, changeReport);
                    break;
            }

            AddDomainEvents(changeReport.DomainEvents, entry.Entity);
        }

        protected virtual void ApplyAbpConceptsForAddedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            CheckAndSetId(entry);
            CheckAndSetMustHaveTenantIdProperty(entry.Entity);
            CheckAndSetMayHaveTenantIdProperty(entry.Entity);
            SetCreationAuditProperties(entry.Entity, userId);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Created));
        }

        protected virtual void ApplyAbpConceptsForModifiedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            SetModificationAuditProperties(entry.Entity, userId);
            if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
            {
                SetDeletionAuditProperties(entry.Entity, userId);
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
            }
            else
            {
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Updated));
            }
        }

        protected virtual void ApplyAbpConceptsForDeletedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            if (IsHardDeleteEntity(entry))
            {
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
                return;
            }

            CancelDeletionForSoftDelete(entry);
            SetDeletionAuditProperties(entry.Entity, userId);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
        }

       

        protected virtual void AddDomainEvents(List<DomainEventEntry> domainEvents, object entityAsObj)
        {
            var generatesDomainEventsEntity = entityAsObj as IGeneratesDomainEvents;
            if (generatesDomainEventsEntity == null)
            {
                return;
            }

            if (generatesDomainEventsEntity.DomainEvents.IsNullOrEmpty())
            {
                return;
            }

            domainEvents.AddRange(generatesDomainEventsEntity.DomainEvents.Select(eventData => new DomainEventEntry(entityAsObj, eventData)));
            generatesDomainEventsEntity.DomainEvents.Clear();
        }

        protected virtual void CheckAndSetId(EntityEntry entry)
        {
            //Set GUID Ids
            var entity = entry.Entity as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                var idPropertyEntry = entry.Property("Id");

                if (idPropertyEntry != null && idPropertyEntry.Metadata.ValueGenerated == ValueGenerated.Never)
                {
                    entity.Id = GuidGenerator.Create();
                }
            }
        }

        protected virtual void CheckAndSetMustHaveTenantIdProperty(object entityAsObj)
        {
            if (SuppressAutoSetTenantId)
            {
                return;
            }

            //Only set IMustHaveTenant entities
            if (!(entityAsObj is IMustHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMustHaveTenant>();

            //Don't set if it's already set
            if (entity.TenantId != 0)
            {
                return;
            }

            var currentTenantId = GetCurrentTenantIdOrNull();

            if (currentTenantId != null)
            {
                entity.TenantId = currentTenantId.Value;
            }
            else
            {
                throw new AbpException("Can not set TenantId to 0 for IMustHaveTenant entities!");
            }
        }

        protected virtual void CheckAndSetMayHaveTenantIdProperty(object entityAsObj)
        {
            if (SuppressAutoSetTenantId)
            {
                return;
            }

            //Only works for single tenant applications
            if (MultiTenancyConfig?.IsEnabled ?? false)
            {
                return;
            }

            //Only set IMayHaveTenant entities
            if (!(entityAsObj is IMayHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMayHaveTenant>();

            //Don't set if it's already set
            if (entity.TenantId != null)
            {
                return;
            }

            entity.TenantId = GetCurrentTenantIdOrNull();
        }

        protected virtual void SetCreationAuditProperties(object entityAsObj, long? userId)
        {
            EntityAuditingHelper.SetCreationAuditProperties(MultiTenancyConfig, entityAsObj, AbpSession.TenantId, userId);
        }

        protected virtual void SetModificationAuditProperties(object entityAsObj, long? userId)
        {
            EntityAuditingHelper.SetModificationAuditProperties(MultiTenancyConfig, entityAsObj, AbpSession.TenantId, userId);
        }

        protected virtual void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }

        protected virtual void SetDeletionAuditProperties(object entityAsObj, long? userId)
        {
            if (entityAsObj is IHasDeletionTime)
            {
                var entity = entityAsObj.As<IHasDeletionTime>();

                if (entity.DeletionTime == null)
                {
                    entity.DeletionTime = Clock.Now;
                }
            }

            if (entityAsObj is IDeletionAudited)
            {
                var entity = entityAsObj.As<IDeletionAudited>();

                if (entity.DeleterUserId != null)
                {
                    return;
                }

                if (userId == null)
                {
                    entity.DeleterUserId = null;
                    return;
                }

                //Special check for multi-tenant entities
                if (entity is IMayHaveTenant || entity is IMustHaveTenant)
                {
                    //Sets LastModifierUserId only if current user is in same tenant/host with the given entity
                    if ((entity is IMayHaveTenant && entity.As<IMayHaveTenant>().TenantId == AbpSession.TenantId) ||
                        (entity is IMustHaveTenant && entity.As<IMustHaveTenant>().TenantId == AbpSession.TenantId))
                    {
                        entity.DeleterUserId = userId;
                    }
                    else
                    {
                        entity.DeleterUserId = null;
                    }
                }
                else
                {
                    entity.DeleterUserId = userId;
                }
            }
        }

        protected virtual long? GetAuditUserId()
        {
            if (AbpSession.UserId.HasValue &&
                CurrentUnitOfWorkProvider != null &&
                CurrentUnitOfWorkProvider.Current != null &&
                CurrentUnitOfWorkProvider.Current.GetTenantId() == AbpSession.TenantId)
            {
                return AbpSession.UserId;
            }

            return null;
        }

        protected virtual int? GetCurrentTenantIdOrNull()
        {
            if (CurrentUnitOfWorkProvider != null &&
                CurrentUnitOfWorkProvider.Current != null)
            {
                return CurrentUnitOfWorkProvider.Current.GetTenantId();
            }

            return AbpSession.TenantId;
        }

    }
}
