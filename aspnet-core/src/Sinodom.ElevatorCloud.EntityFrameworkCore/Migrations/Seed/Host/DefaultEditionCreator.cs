using System.Linq;
using Abp.Application.Features;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Features;

namespace Sinodom.ElevatorCloud.Migrations.Seed.Host
{
    using System.Collections.Generic;

    public class DefaultEditionCreator
    {
        private readonly ElevatorCloudDbContext _context;

        public DefaultEditionCreator(ElevatorCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var defaultEditionTypes = new List<string> { "政府", "维保公司", "物业公司" };
            foreach (string defaultEditionType in defaultEditionTypes)
            {
                if (this._context.ECCPEditionsTypes.Any(e => e.Name == defaultEditionType) == false)
                {
                    this._context.ECCPEditionsTypes.Add(new ECCPEditionsType { Name = defaultEditionType });
                    this._context.SaveChanges();
                }
            }

            var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition == null)
            {
                var defaultEditionType = this._context.ECCPEditionsTypes.FirstOrDefault();

                defaultEdition = new SubscribableEdition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName, ECCPEditionsTypeId = defaultEditionType.Id};
                _context.Editions.Add(defaultEdition);
                _context.SaveChanges();

                /* Add desired features to the standard edition, if wanted... */
            }

            if (defaultEdition.Id > 0)
            {
                CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.ChatFeature, true);
                CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.TenantToTenantChatFeature, true);
                CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.TenantToHostChatFeature, true);
            }
            
            var defaultMaintenanceEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultMaintenanceEditionName);
            if (defaultMaintenanceEdition == null)
            {
                var defaultMaintenanceEditionType = this._context.ECCPEditionsTypes.FirstOrDefault(e => e.Name == "维保公司");

                defaultMaintenanceEdition = new SubscribableEdition { Name = EditionManager.DefaultMaintenanceEditionName, DisplayName = EditionManager.DefaultMaintenanceEditionName, ECCPEditionsTypeId = defaultMaintenanceEditionType.Id };
                _context.Editions.Add(defaultMaintenanceEdition);
                _context.SaveChanges();

                /* Add desired features to the standard edition, if wanted... */
            }

            if (defaultMaintenanceEdition.Id > 0)
            {
                CreateFeatureIfNotExists(defaultMaintenanceEdition.Id, AppFeatures.ChatFeature, true);
                CreateFeatureIfNotExists(defaultMaintenanceEdition.Id, AppFeatures.TenantToTenantChatFeature, true);
                CreateFeatureIfNotExists(defaultMaintenanceEdition.Id, AppFeatures.TenantToHostChatFeature, true);
            }

            var defaultPropertyEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultPropertyEditionName);
            if (defaultPropertyEdition == null)
            {
                var defaultPropertyEditionType = this._context.ECCPEditionsTypes.FirstOrDefault(e => e.Name == "物业公司");

                defaultPropertyEdition = new SubscribableEdition { Name = EditionManager.DefaultPropertyEditionName, DisplayName = EditionManager.DefaultPropertyEditionName, ECCPEditionsTypeId = defaultPropertyEditionType.Id };
                _context.Editions.Add(defaultPropertyEdition);
                _context.SaveChanges();

                /* Add desired features to the standard edition, if wanted... */
            }

            if (defaultPropertyEdition.Id > 0)
            {
                CreateFeatureIfNotExists(defaultPropertyEdition.Id, AppFeatures.ChatFeature, true);
                CreateFeatureIfNotExists(defaultPropertyEdition.Id, AppFeatures.TenantToTenantChatFeature, true);
                CreateFeatureIfNotExists(defaultPropertyEdition.Id, AppFeatures.TenantToHostChatFeature, true);
            }
        }

        private void CreateFeatureIfNotExists(int editionId, string featureName, bool isEnabled)
        {
            var defaultEditionChatFeature = _context.EditionFeatureSettings.IgnoreQueryFilters()
                                                        .FirstOrDefault(ef => ef.EditionId == editionId && ef.Name == featureName);

            if (defaultEditionChatFeature == null)
            {
                _context.EditionFeatureSettings.Add(new EditionFeatureSetting
                {
                    Name = featureName,
                    Value = isEnabled.ToString().ToLower(),
                    EditionId = editionId
                });
            }
        }
    }
}
