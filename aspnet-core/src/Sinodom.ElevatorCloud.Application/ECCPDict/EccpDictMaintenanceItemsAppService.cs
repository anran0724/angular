// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceItemsAppService.cs" company="Sinodom">
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
    ///     The eccp dict maintenance items app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems)]
    public class EccpDictMaintenanceItemsAppService : ElevatorCloudAppServiceBase, IEccpDictMaintenanceItemsAppService
    {
        /// <summary>
        ///     The _eccp dict maintenance item repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceItem> _eccpDictMaintenanceItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceItemsAppService"/> class.
        /// </summary>
        /// <param name="eccpDictMaintenanceItemRepository">
        /// The eccp dict maintenance item repository.
        /// </param>
        public EccpDictMaintenanceItemsAppService(
            IRepository<EccpDictMaintenanceItem> eccpDictMaintenanceItemRepository)
        {
            this._eccpDictMaintenanceItemRepository = eccpDictMaintenanceItemRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictMaintenanceItemDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictMaintenanceItemRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictMaintenanceItemForView>> GetAll(
            GetAllEccpDictMaintenanceItemsInput input)
        {
            var filteredEccpDictMaintenanceItems = this._eccpDictMaintenanceItemRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter) || e.TermCode.Contains(input.Filter)
                                                       || e.TermDesc.Contains(input.Filter)).WhereIf(
                    !string.IsNullOrWhiteSpace(input.TermCodeFilter),
                    e => e.TermCode.ToLower() == input.TermCodeFilter.ToLower().Trim());

            var query = from o in filteredEccpDictMaintenanceItems
                        select new 
                                   {
                                       EccpDictMaintenanceItem = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictMaintenanceItems = new List<GetEccpDictMaintenanceItemForView>();
                
            query.OrderBy(input.Sorting ?? "eccpDictMaintenanceItem.id asc").PageBy(input).MapTo(eccpDictMaintenanceItems);

            return new PagedResultDto<GetEccpDictMaintenanceItemForView>(totalCount, eccpDictMaintenanceItems);
        }

        /// <summary>
        /// The get eccp dict maintenance item for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Edit)]
        public async Task<GetEccpDictMaintenanceItemForEditOutput> GetEccpDictMaintenanceItemForEdit(EntityDto input)
        {
            var eccpDictMaintenanceItem = await this._eccpDictMaintenanceItemRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictMaintenanceItemForEditOutput
                             {
                                 EccpDictMaintenanceItem =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictMaintenanceItemDto>(
                                         eccpDictMaintenanceItem)
                             };

            return output;
        }

        /// <summary>
        ///     获取全部维保项目
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task<EccpDictMaintenanceItemTemplateNodeDto[]> GetMaintenanceItemTemplateNodeAll()
        {
            return await this._eccpDictMaintenanceItemRepository.GetAll().Select(
                       r => new EccpDictMaintenanceItemTemplateNodeDto
                                {
                                    DictMaintenanceItemID = r.Id,
                                    Name = r.Name,
                                    DisOrder = r.DisOrder,
                                    IsAssigned = false
                                }).ToArrayAsync();
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Create)]
        private async Task Create(CreateOrEditEccpDictMaintenanceItemDto input)
        {
            var eccpDictMaintenanceItem = this.ObjectMapper.Map<EccpDictMaintenanceItem>(input);

            await this._eccpDictMaintenanceItemRepository.InsertAsync(eccpDictMaintenanceItem);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Edit)]
        private async Task Update(CreateOrEditEccpDictMaintenanceItemDto input)
        {
            if (input.Id != null)
            {
                var eccpDictMaintenanceItem =
                    await this._eccpDictMaintenanceItemRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictMaintenanceItem);
            }
        }
    }
}