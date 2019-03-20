// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPDictElevatorStatusesAppService.cs" company="Sinodom">
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
    ///     The eccp dict elevator statuses app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses)]
    public class ECCPDictElevatorStatusesAppService : ElevatorCloudAppServiceBase, IECCPDictElevatorStatusesAppService
    {
        /// <summary>
        ///     The _eccp dict elevator status repository.
        /// </summary>
        private readonly IRepository<ECCPDictElevatorStatus> _eccpDictElevatorStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPDictElevatorStatusesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictElevatorStatusRepository">
        /// The eccp dict elevator status repository.
        /// </param>
        public ECCPDictElevatorStatusesAppService(IRepository<ECCPDictElevatorStatus> eccpDictElevatorStatusRepository)
        {
            this._eccpDictElevatorStatusRepository = eccpDictElevatorStatusRepository;
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
        public async Task CreateOrEdit(CreateOrEditECCPDictElevatorStatusDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictElevatorStatusRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetECCPDictElevatorStatusForView>> GetAll(
            GetAllECCPDictElevatorStatusesInput input)
        {
            var filteredEccpDictElevatorStatuses = this._eccpDictElevatorStatusRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter) || e.ColorStyle.Contains(input.Filter));

            var query = from o in filteredEccpDictElevatorStatuses
                        select new 
                                   {
                                       ECCPDictElevatorStatus = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictElevatorStatuses = new List<GetECCPDictElevatorStatusForView>();

            query.OrderBy(input.Sorting ?? "eCCPDictElevatorStatus.id asc").PageBy(input).MapTo(eccpDictElevatorStatuses);

            return new PagedResultDto<GetECCPDictElevatorStatusForView>(totalCount, eccpDictElevatorStatuses);
        }

        /// <summary>
        /// The get eccp dict elevator status for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Edit)]
        public async Task<GetECCPDictElevatorStatusForEditOutput> GetECCPDictElevatorStatusForEdit(EntityDto input)
        {
            var eccpDictElevatorStatus = await this._eccpDictElevatorStatusRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetECCPDictElevatorStatusForEditOutput
                             {
                                 ECCPDictElevatorStatus =
                                     this.ObjectMapper.Map<CreateOrEditECCPDictElevatorStatusDto>(
                                         eccpDictElevatorStatus)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Create)]
        private async Task Create(CreateOrEditECCPDictElevatorStatusDto input)
        {
            var eccpDictElevatorStatus = this.ObjectMapper.Map<ECCPDictElevatorStatus>(input);

            await this._eccpDictElevatorStatusRepository.InsertAsync(eccpDictElevatorStatus);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorStatuses_Edit)]
        private async Task Update(CreateOrEditECCPDictElevatorStatusDto input)
        {
            if (input.Id != null)
            {
                var eccpDictElevatorStatus =
                    await this._eccpDictElevatorStatusRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictElevatorStatus);
            }
        }
    }
}