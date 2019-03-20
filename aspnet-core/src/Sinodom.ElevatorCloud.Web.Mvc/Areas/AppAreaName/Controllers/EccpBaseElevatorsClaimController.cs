// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorsClaimController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System;

    using Abp.AspNetCore.Mvc.Authorization;
    using Abp.Domain.Repositories;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevators;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base elevators claim controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpElevator_ElevatorClaims)]
    public class EccpBaseElevatorsClaimController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _edition repository.
        /// </summary>
        private readonly IRepository<ECCPEdition, int> _editionRepository;

        /// <summary>
        /// The _tenant repository.
        /// </summary>
        private readonly IRepository<Tenant, int> _tenantRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorsClaimController"/> class.
        /// </summary>
        /// <param name="tenantRepository">
        /// The tenant repository.
        /// </param>
        /// <param name="editionRepository">
        /// The edition repository.
        /// </param>
        public EccpBaseElevatorsClaimController(
            IRepository<Tenant, int> tenantRepository,
            IRepository<ECCPEdition, int> editionRepository)
        {
            this._tenantRepository = tenantRepository;
            this._editionRepository = editionRepository;
        }

        /// <summary>
        /// The claim modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult ClaimModal(Guid? id)
        {
            if (this.AbpSession.TenantId.HasValue)
            {
                var tenant = this._tenantRepository.Get(this.AbpSession.TenantId.Value);

                if (tenant.EditionId != null)
                {
                    var edition = this._editionRepository.Get(tenant.EditionId.Value);

                    return this.PartialView("_ClaimModal", edition);
                }
            }

            return null;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            var model = new EccpBaseElevatorsViewModel { FilterText = string.Empty };

            return this.View(model);
        }
    }
}