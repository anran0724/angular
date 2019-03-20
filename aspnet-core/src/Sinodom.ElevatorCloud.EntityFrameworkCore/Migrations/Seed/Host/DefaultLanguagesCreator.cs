using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.Migrations.Seed.Host
{
    public class DefaultLanguagesCreator
    {
        public static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

        private readonly ElevatorCloudDbContext _context;

        private static List<ApplicationLanguage> GetInitialLanguages()
        {
            var tenantId = ElevatorCloudConsts.MultiTenancyEnabled ? null : (int?)1;
            return new List<ApplicationLanguage>
            {
                new ApplicationLanguage(tenantId, "en", "English", "famfamfam-flags us"),
                new ApplicationLanguage(tenantId, "ar", "???????", "famfamfam-flags sa"),
                new ApplicationLanguage(tenantId, "de", "Deutsch", "famfamfam-flags de"),
                new ApplicationLanguage(tenantId, "it", "Italiano", "famfamfam-flags it"),
                new ApplicationLanguage(tenantId, "fr", "Fran?ais", "famfamfam-flags fr"),
                new ApplicationLanguage(tenantId, "pt-BR", "Portugu¨ºs (Brasil)", "famfamfam-flags br"),
                new ApplicationLanguage(tenantId, "tr", "T¨¹rk?e", "famfamfam-flags tr"),
                new ApplicationLanguage(tenantId, "ru", "P§å§ã§ã§Ü§Ú§Û", "famfamfam-flags ru"),
                new ApplicationLanguage(tenantId, "zh-Hans", "¼òÌåÖÐÎÄ", "famfamfam-flags cn"),
                new ApplicationLanguage(tenantId, "es-MX", "Espa?ol (M¨¦xico)", "famfamfam-flags mx"),
                new ApplicationLanguage(tenantId, "es", "Espa?ol (Spanish)", "famfamfam-flags es"),
                new ApplicationLanguage(tenantId, "vi", "Ti?ng Vi?t", "famfamfam-flags vn")
            };
        }

        public DefaultLanguagesCreator(ElevatorCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.IgnoreQueryFilters().Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);

            _context.SaveChanges();
        }
    }
}
