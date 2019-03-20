// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkManager.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Domain.Repositories;
    using Abp.Runtime.Session;

    /// <summary>
    /// The eccp maintenance work manager.
    /// </summary>
    public class EccpMaintenanceWorkManager
    {
        /// <summary>
        /// The eccp maintenance work log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkLog, long> _eccpMaintenanceWorkLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkManager"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkLogRepository">
        /// The eccp maintenance work log repository.
        /// </param>
        public EccpMaintenanceWorkManager(IRepository<EccpMaintenanceWorkLog, long> eccpMaintenanceWorkLogRepository)
        {
            this._eccpMaintenanceWorkLogRepository = eccpMaintenanceWorkLogRepository;
            this.AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// Gets or sets the abp session.
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="eccpMaintenanceWorkLog">
        /// The eccp maintenance work log.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task Create(EccpMaintenanceWorkLog eccpMaintenanceWorkLog)
        {
            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceWorkLog.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._eccpMaintenanceWorkLogRepository.InsertAsync(eccpMaintenanceWorkLog);
        }

        /// <summary>
        /// The get all async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<EccpMaintenanceWorkLog>> GetAllAsync()
        {
            return await this._eccpMaintenanceWorkLogRepository.GetAllListAsync();
        }
    }
}