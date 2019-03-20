// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpCompanyAuditsController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AspNetCore.Mvc.Authorization;
    using Abp.Authorization;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpCompanyAudits;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp company audits controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyAudits)]
    public class EccpCompanyAuditsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp company audits app service.
        /// </summary>
        private readonly IEccpCompanyAuditsAppService _eccpCompanyAuditsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpCompanyAuditsController"/> class.
        /// </summary>
        /// <param name="eccpCompanyAuditsAppService">
        /// The eccp company audits app service.
        /// </param>
        public EccpCompanyAuditsController(IEccpCompanyAuditsAppService eccpCompanyAuditsAppService)
        {
            this._eccpCompanyAuditsAppService = eccpCompanyAuditsAppService;
        }

        /// <summary>
        /// The edit modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpCompanyAudits_Edit)]
        public async Task<PartialViewResult> EditModal(int id)
        {
            GetEccpCompanyAuditForView getEccpCompanyAuditForView;

            getEccpCompanyAuditForView =
                await this._eccpCompanyAuditsAppService.GetCompanyAuditForEdit(new EntityDto { Id = id });

            var viewModel = new EditCompanyAuditViewModel
                                {
                                    EccpCompanyInfo = getEccpCompanyAuditForView.EccpCompanyInfo,
                                    EccpCompanyInfoExtension = getEccpCompanyAuditForView.EccpCompanyInfoExtension,
                                    Id = getEccpCompanyAuditForView.Id,
                                    CheckStateName = getEccpCompanyAuditForView.CheckStateName,
                                    EditionTypeName = getEccpCompanyAuditForView.EditionTypeName,
                                    ProvinceName = getEccpCompanyAuditForView.ProvinceName,
                                    CityName = getEccpCompanyAuditForView.CityName,
                                    DistrictName = getEccpCompanyAuditForView.DistrictName,
                                    StreetName = getEccpCompanyAuditForView.StreetName
                                };

            return this.PartialView("_EditModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpCompanyAuditsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp company audit modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpCompanyAuditModal(GetEccpCompanyAuditForView data)
        {
            //var model = new EccpCompanyAuditViewModel
            //                {
            //                    EccpCompanyInfo = data.EccpCompanyInfo,
            //                    EccpCompanyInfoExtension = data.EccpCompanyInfoExtension,
            //                    Id = data.Id,
            //                    CheckStateName = data.CheckStateName,
            //                    EditionTypeName = data.EditionTypeName,
            //                    ProvinceName = data.ProvinceName,
            //                    CityName = data.CityName,
            //                    DistrictName = data.DistrictName,
            //                    StreetName = data.StreetName
            //                };



            return this.PartialView("_ViewEccpCompanyAuditModal");
        }
    }
}