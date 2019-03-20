using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Layout;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Startup;
using Sinodom.ElevatorCloud.Web.Views;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameMenu
{
    using Abp.Domain.Repositories;

    using Sinodom.ElevatorCloud.Editions;

    public class AppAreaNameMenuViewComponent : ElevatorCloudViewComponent
    {
        private readonly INavigationManager _navigationManager;
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;
        private readonly TenantManager _tenantManager;
        private readonly IRepository<ECCPEditionPermission> _eccpEditionPermissionRepository;

        public AppAreaNameMenuViewComponent(
            INavigationManager navigationManager,
            IUserNavigationManager userNavigationManager,
            IAbpSession abpSession,
            TenantManager tenantManager,
            IRepository<ECCPEditionPermission> eccpEditionPermissionRepository)
        {
            _navigationManager = navigationManager;
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
            _tenantManager = tenantManager;
            _eccpEditionPermissionRepository = eccpEditionPermissionRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isLeftMenuUsed, string currentPageName = null)
        {
            var model = new MenuViewModel
            {
                Menu = await _userNavigationManager.GetMenuAsync(AppAreaNameNavigationProvider.MenuName, _abpSession.ToUserIdentifier()),
                CurrentPageName = currentPageName
            };

            if (AbpSession.TenantId == null)
            {
                return GetView(model, isLeftMenuUsed);
            }

            var tenant = await _tenantManager.GetByIdAsync(AbpSession.TenantId.Value);
            if (tenant.EditionId.HasValue)
            {
                var allMenus = this._navigationManager.Menus[AppAreaNameNavigationProvider.MenuName].Items;

                var permissions = await this._eccpEditionPermissionRepository.GetAllListAsync(e => e.EditionId == tenant.EditionId.Value && e.IsGranted);

                model.Menu.Items = await this.CheckEditionMenuItems(permissions, allMenus, model.Menu.Items);

                return GetView(model, isLeftMenuUsed);
            }

            var subscriptionManagement = FindMenuItemOrNull(model.Menu.Items, AppAreaNamePageNames.Tenant.SubscriptionManagement);
            if (subscriptionManagement != null)
            {
                subscriptionManagement.IsVisible = false;
            }

            return GetView(model, isLeftMenuUsed);
        }

        public UserMenuItem FindMenuItemOrNull(IList<UserMenuItem> userMenuItems, string name)
        {
            if (userMenuItems == null)
            {
                return null;
            }

            foreach (var menuItem in userMenuItems)
            {
                if (menuItem.Name == name)
                {
                    return menuItem;
                }

                var found = FindMenuItemOrNull(menuItem.Items, name);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        private IViewComponentResult GetView(MenuViewModel model, bool isLeftMenuUsed)
        {
            return View(isLeftMenuUsed ? "Default" : "Top", model);
        }

        private async Task<IList<UserMenuItem>> CheckEditionMenuItems(List<ECCPEditionPermission> permissions, IList<MenuItemDefinition> menuItemDefinitions, IList<UserMenuItem> editionMenuItems)
        {
            foreach (UserMenuItem editionMenuItem in editionMenuItems)
            {
                var menu = menuItemDefinitions.FirstOrDefault(e => e.Name == editionMenuItem.Name);
                if (menu != null)
                {
                    if (!string.IsNullOrEmpty(menu.RequiredPermissionName) && permissions.All(e => e.Name != menu.RequiredPermissionName))
                    {
                        editionMenuItem.IsVisible = false;
                    }

                    if (editionMenuItem.Items.Count > 0)
                    {
                        editionMenuItem.Items = await this.CheckEditionMenuItems(permissions, menu.Items, editionMenuItem.Items);
                    }
                }
            }

            return editionMenuItems;
        }
    }
}
