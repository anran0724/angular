using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Editions;
using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.SubscriptionManagement;
using Sinodom.ElevatorCloud.Web.Controllers;
using Sinodom.ElevatorCloud.Web.Session;
using PaymentViewModel = Sinodom.ElevatorCloud.Web.Models.Payment.PaymentViewModel;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement)]
    public class SubscriptionManagementController : ElevatorCloudControllerBase
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IPaymentAppService _paymentAppService;

        public SubscriptionManagementController(
            IPerRequestSessionCache sessionCache,
            IPaymentAppService paymentAppService
        )
        {
            _sessionCache = sessionCache;
            _paymentAppService = paymentAppService;
        }

        public async Task<ActionResult> Index()
        {
            var loginInfo = await _sessionCache.GetCurrentLoginInformationsAsync();
            var model = new SubscriptionDashboardViewModel
            {
                LoginInformations = loginInfo
            };

            return View(model);
        }

        public async Task<ActionResult> Payment(int? upgradeEditionId, EditionPaymentType editionPaymentType)
        {
            var paymentInfo = await _paymentAppService.GetPaymentInfo(new PaymentInfoInput { UpgradeEditionId = upgradeEditionId });

            return View("~/Views/Payment/Index.cshtml", new PaymentViewModel
            {
                Edition = paymentInfo.Edition,
                AdditionalPrice = paymentInfo.AdditionalPrice,
                EditionPaymentType = editionPaymentType
            });
        }

        [HttpPost]
        public async Task<ActionResult> PaymentResult(PaymentResultViewModel model)
        {
            var data = Request.Form.ToDictionary(q => q.Key, q => string.Join(",", q.Value));
            var executePaymentDto = ObjectMapper.Map<ExecutePaymentDto>(model);
            executePaymentDto.AdditionalData = data;

            await _paymentAppService.ExecutePayment(executePaymentDto);

            return RedirectToAction("Index");
        }
    }
}
