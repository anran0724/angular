using System;
using System.Text;
using Abp.Dependency;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Reflection.Extensions;
using Sinodom.ElevatorCloud.Url;

namespace Sinodom.ElevatorCloud.Emailing
{
    public class EmailTemplateProvider : IEmailTemplateProvider, ISingletonDependency
    {
        private readonly IWebUrlService _webUrlService;
        private string _defaultTemplate;

        protected readonly object SyncObj = new object();


        public EmailTemplateProvider(
            IWebUrlService webUrlService)
        {
            _webUrlService = webUrlService;
        }

        public string GetDefaultTemplate(int? tenantId)
        {
            if (!string.IsNullOrEmpty(_defaultTemplate))
            {
                return _defaultTemplate;
            }

            lock (SyncObj)
            {
                using (var stream = typeof(EmailTemplateProvider).GetAssembly().GetManifestResourceStream("Sinodom.ElevatorCloud.Emailing.EmailTemplates.default.html"))
                {
                    var bytes = stream.GetAllBytes();
                    var template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
                    template = template.Replace("{THIS_YEAR}", DateTime.Now.Year.ToString());
                    _defaultTemplate = template.Replace("{EMAIL_LOGO_URL}", GetTenantLogoUrl(tenantId));
                    return _defaultTemplate;
                }
            }
        }

        private string GetTenantLogoUrl(int? tenantId)
        {
            if (!tenantId.HasValue)
            {
                return _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "TenantCustomization/GetTenantLogo?skin=light";
            }

            return _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "TenantCustomization/GetTenantLogo?skin=light&tenantId=" + tenantId.Value;
        }
    }
}
