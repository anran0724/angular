// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceStatusesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    ///     The eccp dict maintenance statuses app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses)]
    public class EccpDictMaintenanceStatusesAppService : ElevatorCloudAppServiceBase,
                                                         IEccpDictMaintenanceStatusesAppService
    {
        /// <summary>
        ///     The _eccp dict maintenance status repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceStatus> _eccpDictMaintenanceStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceStatusesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictMaintenanceStatusRepository">
        /// The eccp dict maintenance status repository.
        /// </param>
        public EccpDictMaintenanceStatusesAppService(
            IRepository<EccpDictMaintenanceStatus> eccpDictMaintenanceStatusRepository)
        {
            this._eccpDictMaintenanceStatusRepository = eccpDictMaintenanceStatusRepository;
        }

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task CreateOrEdit(CreateOrEditEccpDictMaintenanceStatusDto input)
        {
            if (input.Id == null)
            {
                await this.Create(input);
            }
            else
            {
                await this.Update(input);
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictMaintenanceStatusRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpDictMaintenanceStatusForView>> GetAll(
            GetAllEccpDictMaintenanceStatusesInput input)
        {
            var filteredEccpDictMaintenanceStatuses = this._eccpDictMaintenanceStatusRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictMaintenanceStatuses
                        select new 
                                   {
                                       EccpDictMaintenanceStatus = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictMaintenanceStatuses = new List<GetEccpDictMaintenanceStatusForView>();
                
            query.OrderBy(input.Sorting ?? "eccpDictMaintenanceStatus.id asc").PageBy(input).MapTo(eccpDictMaintenanceStatuses);

            return new PagedResultDto<GetEccpDictMaintenanceStatusForView>(totalCount, eccpDictMaintenanceStatuses);
        }

        /// <summary>
        /// The get eccp dict maintenance status for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Edit)]
        public async Task<GetEccpDictMaintenanceStatusForEditOutput> GetEccpDictMaintenanceStatusForEdit(
            EntityDto input)
        {
            var eccpDictMaintenanceStatus =
                await this._eccpDictMaintenanceStatusRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictMaintenanceStatusForEditOutput
                             {
                                 EccpDictMaintenanceStatus =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictMaintenanceStatusDto>(
                                         eccpDictMaintenanceStatus)
                             };

            return output;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Create)]
        private async Task Create(CreateOrEditEccpDictMaintenanceStatusDto input)
        {
            var eccpDictMaintenanceStatus = this.ObjectMapper.Map<EccpDictMaintenanceStatus>(input);

            await this._eccpDictMaintenanceStatusRepository.InsertAsync(eccpDictMaintenanceStatus);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceStatuses_Edit)]
        private async Task Update(CreateOrEditEccpDictMaintenanceStatusDto input)
        {
            if (input.Id != null)
            {
                var eccpDictMaintenanceStatus =
                    await this._eccpDictMaintenanceStatusRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictMaintenanceStatus);
            }
        }
    }
}