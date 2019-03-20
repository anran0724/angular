// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictLabelStatusesAppService.cs" company="Sinodom">
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
    ///     The eccp dict label statuses app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses)]
    public class EccpDictLabelStatusesAppService : ElevatorCloudAppServiceBase, IEccpDictLabelStatusesAppService
    {
        /// <summary>
        ///     The _eccp dict label status repository.
        /// </summary>
        private readonly IRepository<EccpDictLabelStatus> _eccpDictLabelStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictLabelStatusesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictLabelStatusRepository">
        /// The eccp dict label status repository.
        /// </param>
        public EccpDictLabelStatusesAppService(IRepository<EccpDictLabelStatus> eccpDictLabelStatusRepository)
        {
            this._eccpDictLabelStatusRepository = eccpDictLabelStatusRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictLabelStatusDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictLabelStatusRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictLabelStatusForView>> GetAll(GetAllEccpDictLabelStatusesInput input)
        {
            var filteredEccpDictLabelStatuses = this._eccpDictLabelStatusRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictLabelStatuses
                        select new 
                                   {
                                       EccpDictLabelStatus = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictLabelStatuses = new List<GetEccpDictLabelStatusForView>();
                
            query.OrderBy(input.Sorting ?? "eccpDictLabelStatus.id asc").PageBy(input).MapTo(eccpDictLabelStatuses);

            return new PagedResultDto<GetEccpDictLabelStatusForView>(totalCount, eccpDictLabelStatuses);
        }

        /// <summary>
        /// The get eccp dict label status for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Edit)]
        public async Task<GetEccpDictLabelStatusForEditOutput> GetEccpDictLabelStatusForEdit(EntityDto input)
        {
            var eccpDictLabelStatus = await this._eccpDictLabelStatusRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictLabelStatusForEditOutput
                             {
                                 EccpDictLabelStatus =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictLabelStatusDto>(eccpDictLabelStatus)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Create)]
        private async Task Create(CreateOrEditEccpDictLabelStatusDto input)
        {
            var eccpDictLabelStatus = this.ObjectMapper.Map<EccpDictLabelStatus>(input);

            await this._eccpDictLabelStatusRepository.InsertAsync(eccpDictLabelStatus);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictLabelStatuses_Edit)]
        private async Task Update(CreateOrEditEccpDictLabelStatusDto input)
        {
            if (input.Id != null)
            {
                var eccpDictLabelStatus = await this._eccpDictLabelStatusRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictLabelStatus);
            }
        }
    }
}