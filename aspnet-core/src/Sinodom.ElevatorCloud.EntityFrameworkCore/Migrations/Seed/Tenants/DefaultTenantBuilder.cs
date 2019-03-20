using System.Linq;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.Migrations.Seed.Tenants
{
    using System;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;

    public class DefaultTenantBuilder
    {
        private readonly ElevatorCloudDbContext _context;

        public DefaultTenantBuilder(ElevatorCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
            CreateDefaultMaintenanceTenant();
            CreateDefaultPropertyTenant();
        }

        private void CreateDefaultTenant()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == MultiTenancy.Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new MultiTenancy.Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }
        }

        private void CreateDefaultMaintenanceTenant()
        {
            //Default Maintenance tenant

            var defaultMaintenanceTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == MultiTenancy.Tenant.DefaultMaintenanceTenantName);
            if (defaultMaintenanceTenant == null)
            {
                defaultMaintenanceTenant = new MultiTenancy.Tenant(MultiTenancy.Tenant.DefaultMaintenanceTenantName, MultiTenancy.Tenant.DefaultMaintenanceTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultMaintenanceEditionName);
                if (defaultEdition != null)
                {
                    defaultMaintenanceTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultMaintenanceTenant);
                _context.SaveChanges();

                var maintenanceCompany = new ECCPBaseMaintenanceCompany
                                             {
                                                 Name = "默认维保公司",
                                                 Addresse = "Addresse",
                                                 Longitude = "Longitude",
                                                 Latitude = "Latitude",
                                                 Telephone = "Telephone",
                                                 OrgOrganizationalCode = "111",
                                                 TenantId = defaultMaintenanceTenant.Id
                                             };

                _context.ECCPBaseMaintenanceCompanies.Add(maintenanceCompany);

                this._context.EccpMaintenanceCompanyExtensions.Add(
                    new EccpMaintenanceCompanyExtension
                    {
                            AptitudePhotoId = Guid.NewGuid(),
                            BusinessLicenseId = Guid.NewGuid(),
                            IsMember = true,
                            LegalPerson = "维保单位企业法人",
                            Mobile = "15852525255",
                            MaintenanceCompany = maintenanceCompany
                    });
            }
        }

        private void CreateDefaultPropertyTenant()
        {
            //Default Property tenant

            var defaultPropertyTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == MultiTenancy.Tenant.DefaultPropertyTenantName);
            if (defaultPropertyTenant == null)
            {
                defaultPropertyTenant = new MultiTenancy.Tenant(MultiTenancy.Tenant.DefaultPropertyTenantName, MultiTenancy.Tenant.DefaultPropertyTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultPropertyEditionName);
                if (defaultEdition != null)
                {
                    defaultPropertyTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultPropertyTenant);
                _context.SaveChanges();

                var propertyCompany = new ECCPBasePropertyCompany
                                          {
                                              Name = "默认使用单位",
                                              Addresse = "Addresse",
                                              Longitude = "Longitude",
                                              Latitude = "Latitude",
                                              Telephone = "Telephone",
                                              OrgOrganizationalCode = "888",
                                              TenantId = defaultPropertyTenant.Id
                                          };

                _context.ECCPBasePropertyCompanies.Add(propertyCompany);

                this._context.EccpPropertyCompanyExtensions.Add(
                    new EccpPropertyCompanyExtension
                        {
                            AptitudePhotoId = Guid.NewGuid(),
                            BusinessLicenseId = Guid.NewGuid(),
                            IsMember = true,
                            LegalPerson = "使用单位企业法人",
                            Mobile = "11123123121",
                            PropertyCompany = propertyCompany
                        });
            }
        }
    }
}
