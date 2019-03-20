// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorQrCodesController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AspNetCore.Mvc.Authorization;
    using Abp.AspNetZeroCore.Net;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;
    using Sinodom.ElevatorCloud.Storage;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpElevatorQrCodes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp elevator qr codes controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes)]
    public class EccpElevatorQrCodesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _binary object manager.
        /// </summary>
        private readonly IBinaryObjectManager _binaryObjectManager;

        /// <summary>
        /// The _eccp elevator qr codes app service.
        /// </summary>
        private readonly IEccpElevatorQrCodesAppService _eccpElevatorQrCodesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpElevatorQrCodesController"/> class.
        /// </summary>
        /// <param name="eccpElevatorQrCodesAppService">
        /// The eccp elevator qr codes app service.
        /// </param>
        /// <param name="binaryObjectManager">
        /// The binary object manager.
        /// </param>
        public EccpElevatorQrCodesController(
            IEccpElevatorQrCodesAppService eccpElevatorQrCodesAppService,
            IBinaryObjectManager binaryObjectManager)
        {
            this._eccpElevatorQrCodesAppService = eccpElevatorQrCodesAppService;
            this._binaryObjectManager = binaryObjectManager;
        }

        /// <summary>
        /// The binding.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PartialViewResult> Binding(Guid? id)
        {
            GetEccpElevatorQrCodeForEditOutput getEccpElevatorQrCodeForEditOutput;

            if (id.HasValue)
            {
                getEccpElevatorQrCodeForEditOutput =
                    await this._eccpElevatorQrCodesAppService.GetEccpElevatorQrCodeForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpElevatorQrCodeForEditOutput = new GetEccpElevatorQrCodeForEditOutput
                                                         {
                                                             EccpElevatorQrCode =
                                                                 new CreateOrEditEccpElevatorQrCodeDto()
                                                         };
            }

            var viewModel = new CreateOrEditEccpElevatorQrCodeViewModel
                                {
                                    EccpElevatorQrCode = getEccpElevatorQrCodeForEditOutput.EccpElevatorQrCode,
                                    EccpBaseElevatorName = getEccpElevatorQrCodeForEditOutput.EccpBaseElevatorName
                                };

            return this.PartialView("_Binding", viewModel);
        }

        /// <summary>
        /// The create or edit modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Create,
            AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetEccpElevatorQrCodeForEditOutput getEccpElevatorQrCodeForEditOutput;

            if (id.HasValue)
            {
                getEccpElevatorQrCodeForEditOutput =
                    await this._eccpElevatorQrCodesAppService.GetEccpElevatorQrCodeForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpElevatorQrCodeForEditOutput =
                    new GetEccpElevatorQrCodeForEditOutput
                        {
                            EccpElevatorQrCode = new CreateOrEditEccpElevatorQrCodeDto()
                        };
            }

            var viewModel = new CreateOrEditEccpElevatorQrCodeViewModel
                                {
                                    EccpElevatorQrCode = getEccpElevatorQrCodeForEditOutput.EccpElevatorQrCode,
                                    EccpBaseElevatorName = getEccpElevatorQrCodeForEditOutput.EccpBaseElevatorName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The create or edit modals.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PartialViewResult> CreateOrEditModals(Guid? id)
        {
            GetEccpElevatorQrCodeForEditOutput getEccpElevatorQrCodeForEditOutput;

            if (id.HasValue)
            {
                getEccpElevatorQrCodeForEditOutput =
                    await this._eccpElevatorQrCodesAppService.GetEccpElevatorQrCodeForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpElevatorQrCodeForEditOutput = new GetEccpElevatorQrCodeForEditOutput
                                                         {
                                                             EccpElevatorQrCode =
                                                                 new CreateOrEditEccpElevatorQrCodeDto()
                                                         };
            }

            var viewModel = new CreateOrEditEccpElevatorQrCodeViewModel
                                {
                                    EccpElevatorQrCode = getEccpElevatorQrCodeForEditOutput.EccpElevatorQrCode,
                                    EccpBaseElevatorName = getEccpElevatorQrCodeForEditOutput.EccpBaseElevatorName
                                };

            return this.PartialView("_CreateOrEditModals", viewModel);
        }

        /// <summary>
        /// The eccp base elevator lookup table modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Create,
            AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Edit)]
        public PartialViewResult EccpBaseElevatorLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new EccpBaseElevatorLookupTableViewModel
                                {
                                    Id = id.ToString(), DisplayName = displayName, FilterText = string.Empty
                                };
            return this.PartialView("_EccpBaseElevatorLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp elevator qr code bind logs.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="newElevatorId">
        /// The new elevator id.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Create,
            AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Edit)]
        public PartialViewResult EccpElevatorQrCodeBindLogs(Guid? id, Guid? newElevatorId)
        {
            var viewModel = new EccpElevatorQrCodeBindLogTableViewModel
                                {
                                    NewElevatorId = newElevatorId, NewQrCodeId = id, Remark = string.Empty
                                };
            return this.PartialView("_EccpElevatorQrCodeBindLogs", viewModel);
        }

        /// <summary>
        /// The get profile picture by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileResult> GetProfilePictureById(Guid id)
        {
            var file = await this._binaryObjectManager.GetOrNullAsync(id);
            return this.File(file.Bytes, MimeTypeNames.ImageJpeg);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpElevatorQrCodesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The modify.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PartialViewResult> Modify(Guid? id)
        {
            GetEccpElevatorQrCodeForEditOutput getEccpElevatorQrCodeForEditOutput;

            if (id.HasValue)
            {
                getEccpElevatorQrCodeForEditOutput =
                    await this._eccpElevatorQrCodesAppService.GetEccpElevatorQrCodeForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpElevatorQrCodeForEditOutput = new GetEccpElevatorQrCodeForEditOutput
                                                         {
                                                             EccpElevatorQrCode =
                                                                 new CreateOrEditEccpElevatorQrCodeDto()
                                                         };
            }

            var viewModel = new CreateOrEditEccpElevatorQrCodeViewModel
                                {
                                    EccpElevatorQrCode = getEccpElevatorQrCodeForEditOutput.EccpElevatorQrCode,
                                    EccpBaseElevatorName = getEccpElevatorQrCodeForEditOutput.EccpBaseElevatorName,
                                    EccpBaseElevatorCertificateNum = getEccpElevatorQrCodeForEditOutput
                                        .EccpBaseElevatorCertificateNum
                                };

            return this.PartialView("_Modify", viewModel);
        }

        /// <summary>
        /// The modify eccp.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PartialViewResult> ModifyEccp(Guid? id)
        {
            GetEccpElevatorQrCodeForEditOutput getEccpElevatorQrCodeForEditOutput;

            if (id.HasValue)
            {
                getEccpElevatorQrCodeForEditOutput =
                    await this._eccpElevatorQrCodesAppService.GetEccpElevatorQrCodeForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpElevatorQrCodeForEditOutput = new GetEccpElevatorQrCodeForEditOutput
                                                         {
                                                             EccpElevatorQrCode =
                                                                 new CreateOrEditEccpElevatorQrCodeDto()
                                                         };
            }

            var viewModel = new CreateOrEditEccpElevatorQrCodeViewModel
                                {
                                    EccpElevatorQrCode = getEccpElevatorQrCodeForEditOutput.EccpElevatorQrCode,
                                    EccpBaseElevatorName = getEccpElevatorQrCodeForEditOutput.EccpBaseElevatorName
                                };

            return this.PartialView("_ModifyEccp", viewModel);
        }

        /// <summary>
        /// The modify qr code.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PartialViewResult> ModifyQrCode(Guid? id)
        {
            GetEccpElevatorQrCodeForEditOutput getEccpElevatorQrCodeForEditOutput;

            if (id.HasValue)
            {
                getEccpElevatorQrCodeForEditOutput =
                    await this._eccpElevatorQrCodesAppService.GetEccpElevatorQrCodeForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpElevatorQrCodeForEditOutput = new GetEccpElevatorQrCodeForEditOutput
                                                         {
                                                             EccpElevatorQrCode =
                                                                 new CreateOrEditEccpElevatorQrCodeDto()
                                                         };
            }

            var viewModel = new CreateOrEditEccpElevatorQrCodeViewModel
                                {
                                    EccpElevatorQrCode = getEccpElevatorQrCodeForEditOutput.EccpElevatorQrCode,
                                    EccpBaseElevatorName = getEccpElevatorQrCodeForEditOutput.EccpBaseElevatorName,
                                    EccpBaseElevatorCertificateNum = getEccpElevatorQrCodeForEditOutput
                                        .EccpBaseElevatorCertificateNum
                                };

            return this.PartialView("_ModifyQRCode", viewModel);
        }

        /// <summary>
        /// The view eccp elevator qr code modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpElevatorQrCodeModal(GetEccpElevatorQrCodeForView data)
        {
            var model = new EccpElevatorQrCodeViewModel
                            {
                                EccpElevatorQrCode = data.EccpElevatorQrCode,
                                IsInstallName = data.EccpElevatorQrCode.IsInstall ? "已安装" : "未安装",
                                IsGrantName = data.EccpElevatorQrCode.IsGrant ? "已发放" : "未发放",
                                EccpBaseElevatorName = data.EccpBaseElevatorName
                            };

            return this.PartialView("_ViewEccpElevatorQrCodeModal", model);
        }
    }
}