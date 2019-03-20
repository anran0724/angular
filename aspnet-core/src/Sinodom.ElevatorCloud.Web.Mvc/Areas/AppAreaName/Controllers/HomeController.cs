using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Web.Controllers;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy;

    [Area("AppAreaName")]
    [AbpMvcAuthorize]
    public class HomeController : ElevatorCloudControllerBase
    {
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;

        public HomeController(TenantManager tenantManager, EditionManager editionManager)
        {
            this._tenantManager = tenantManager;
            this._editionManager = editionManager;
        }

        public async Task<ActionResult> Index()
        {
            if (AbpSession.MultiTenancySide == MultiTenancySides.Host)
            {
                if (await IsGrantedAsync(AppPermissions.Pages_Administration_Host_Dashboard))
                {
                    return RedirectToAction("Index", "HostDashboard");
                }

                if (await IsGrantedAsync(AppPermissions.Pages_Tenants))
                {
                    return RedirectToAction("Index", "Tenants");
                }
            }
            else
            {
                if (await IsGrantedAsync(AppPermissions.Pages_Tenant_Dashboard))
                {
                    var tenant = await this._tenantManager.GetByIdAsync(this.AbpSession.TenantId.GetValueOrDefault(0));
                    if (tenant != null)
                    {
                        var edition = (SubscribableEdition)await this._editionManager.GetByIdAsync(tenant.EditionId.GetValueOrDefault(0));
                        if (edition != null)
                        {
                            var editionType = (await this._editionManager.GetAllEditionTypeAsync()).Find(e => e.Id == edition.ECCPEditionsTypeId);
                            if (editionType.Name == "物业公司")
                            {
                                return this.RedirectToAction("PropertyCompaniesIndex", "Dashboard");
                            }
                            else if (editionType.Name == "政府")
                            {
                                return this.RedirectToAction("Index", "Welcome");
                            }
                        }
                    }

                    return this.RedirectToAction("Index", "Dashboard");
                }
            }

            //Default page if no permission to the pages above
            return this.RedirectToAction("Index", "Welcome");
        }
    }
}
