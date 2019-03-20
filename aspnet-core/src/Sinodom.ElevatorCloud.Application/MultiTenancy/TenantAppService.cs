using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Security;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Editions.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;
using Sinodom.ElevatorCloud.Url;

namespace Sinodom.ElevatorCloud.MultiTenancy
{
    [AbpAuthorize(AppPermissions.Pages_Tenants)]
    public class TenantAppService : ElevatorCloudAppServiceBase, ITenantAppService
    {
        public IAppUrlService AppUrlService { get; set; }

        public TenantAppService()
        {
            AppUrlService = NullAppUrlService.Instance;
        }

        public async Task<PagedResultDto<TenantListDto>> GetTenants(GetTenantsInput input)
        {
            var query = TenantManager.Tenants
                .Include(t => t.Edition)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), t => t.Name.Contains(input.Filter) || t.TenancyName.Contains(input.Filter))
                .WhereIf(input.CreationDateStart.HasValue, t => t.CreationTime >= input.CreationDateStart.Value)
                .WhereIf(input.CreationDateEnd.HasValue, t => t.CreationTime <= input.CreationDateEnd.Value)
                .WhereIf(input.SubscriptionEndDateStart.HasValue, t => t.SubscriptionEndDateUtc >= input.SubscriptionEndDateStart.Value.ToUniversalTime())
                .WhereIf(input.SubscriptionEndDateEnd.HasValue, t => t.SubscriptionEndDateUtc <= input.SubscriptionEndDateEnd.Value.ToUniversalTime())
                .WhereIf(input.EditionIdSpecified, t => t.EditionId == input.EditionId);

            var tenantCount = await query.CountAsync();
            var tenants = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<TenantListDto>(
                tenantCount,
                ObjectMapper.Map<List<TenantListDto>>(tenants)
                );
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Create)]
        [UnitOfWork(IsDisabled = true)]
        public async Task CreateTenant(CreateTenantInput input)
        {
            await TenantManager.CreateWithAdminUserAsync(
                input.TenancyName,
                input.Name,
                input.AdminPassword,
                input.AdminEmailAddress,
                input.ConnectionString,
                input.IsActive,
                input.EditionId,
                input.ShouldChangePasswordOnNextLogin,
                input.SendActivationEmail,
                input.SubscriptionEndDateUtc?.ToUniversalTime(),
                input.IsInTrialPeriod,
                AppUrlService.CreateEmailActivationUrlFormat(input.TenancyName),
                null,
                null,
                string.Empty,
                string.Empty,
                false);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task<TenantEditDto> GetTenantForEdit(EntityDto input)
        {
            var tenantEditDto = ObjectMapper.Map<TenantEditDto>(await TenantManager.GetByIdAsync(input.Id));
            tenantEditDto.ConnectionString = SimpleStringCipher.Instance.Decrypt(tenantEditDto.ConnectionString);
            return tenantEditDto;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task UpdateTenant(TenantEditDto input)
        {
            await TenantManager.CheckEditionAsync(input.EditionId, input.IsInTrialPeriod);

            input.ConnectionString = SimpleStringCipher.Instance.Encrypt(input.ConnectionString);
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            ObjectMapper.Map(input, tenant);
            tenant.SubscriptionEndDateUtc = tenant.SubscriptionEndDateUtc?.ToUniversalTime();

            await TenantManager.UpdateAsync(tenant);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Delete)]
        public async Task DeleteTenant(EntityDto input)
        {
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            await TenantManager.DeleteAsync(tenant);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task<GetTenantFeaturesEditOutput> GetTenantFeaturesForEdit(EntityDto input)
        {
            var features = FeatureManager.GetAll()
                .Where(f => f.Scope.HasFlag(FeatureScopes.Tenant));
            var featureValues = await TenantManager.GetFeatureValuesAsync(input.Id);

            return new GetTenantFeaturesEditOutput
            {
                Features = ObjectMapper.Map<List<FlatFeatureDto>>(features).OrderBy(f => f.DisplayName).ToList(),
                FeatureValues = featureValues.Select(fv => new NameValueDto(fv)).ToList()
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task UpdateTenantFeatures(UpdateTenantFeaturesInput input)
        {
            await TenantManager.SetFeatureValuesAsync(input.Id, input.FeatureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task ResetTenantSpecificFeatures(EntityDto input)
        {
            await TenantManager.ResetAllFeaturesAsync(input.Id);
        }

        public async Task UnlockTenantAdmin(EntityDto input)
        {
            using (CurrentUnitOfWork.SetTenantId(input.Id))
            {
                var tenantAdmin = await UserManager.FindByNameAsync(AbpUserBase.AdminUserName);
                if (tenantAdmin != null)
                {
                    tenantAdmin.Unlock();
                }
            }
        }

        [AbpAllowAnonymous]
        public async Task<PagedResultDto<TenantNameListDto>> GetTenantNames(string input)
        {
            if (input.Length <= 2)
            {
                return new PagedResultDto<TenantNameListDto>(0, null);
            }

            var query = this.TenantManager.Tenants
                .WhereIf(!input.IsNullOrWhiteSpace(), t => t.Name.Contains(input) || t.TenancyName.Contains(input));

            var tenantCount = await query.CountAsync();
            var tenants = await query.OrderBy(e => e.TenancyName).PageBy(0, 10).ToListAsync();

            return new PagedResultDto<TenantNameListDto>(
                tenantCount,
                this.ObjectMapper.Map<List<TenantNameListDto>>(tenants));
        }
    }
}
