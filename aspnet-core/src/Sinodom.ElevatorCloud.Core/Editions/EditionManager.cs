using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace Sinodom.ElevatorCloud.Editions
{
    public class EditionManager : AbpEditionManager
    {
        public const string DefaultEditionName = "Standard";
        public const string DefaultMaintenanceEditionName = "维保公司";
        public const string DefaultPropertyEditionName = "使用单位";

        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;

        public EditionManager(
            IRepository<Edition> editionRepository,
            IAbpZeroFeatureValueStore featureValueStore,
            IRepository<ECCPEditionsType> eccpEditionsTypeRepository)
            : base(
                editionRepository,
                featureValueStore
            )
        {
            this._eccpEditionsTypeRepository = eccpEditionsTypeRepository;
        }

        public async Task<List<Edition>> GetAllAsync()
        {
            return await EditionRepository.GetAllListAsync();
        }

        /// <summary>
        /// The get all edition type async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<ECCPEditionsType>> GetAllEditionTypeAsync()
        {
            return await this._eccpEditionsTypeRepository.GetAllListAsync();
        }
    }
}
