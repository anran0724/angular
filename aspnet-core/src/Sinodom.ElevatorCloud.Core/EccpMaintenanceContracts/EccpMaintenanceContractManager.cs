// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceContractManager.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts
{
    using System;
    using System.Collections.Generic;

    using Abp.Domain.Repositories;
    using Abp.Domain.Services;
    using Abp.Domain.Uow;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;

    /// <summary>
    /// The eccp maintenance contract manager.
    /// </summary>
    public class EccpMaintenanceContractManager : IDomainService
    {
        /// <summary>
        /// The _eccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        /// The _eccp maintenance contract elevator link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLinkRepository;

        /// <summary>
        /// The _eccp maintenance contract repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;

        /// <summary>
        /// The _eccp maintenance plans manager.
        /// </summary>
        private readonly EccpMaintenancePlansManager _eccpMaintenancePlansManager;

        /// <summary>
        /// The _unit of work manager.
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceContractManager"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceContractRepository">
        /// The eccp maintenance contract repository.
        /// </param>
        /// <param name="eccpMaintenanceContractElevatorLinkRepository">
        /// The eccp maintenance contract elevator link repository.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompanyRepository">
        /// The eccp base maintenance company repository.
        /// </param>
        /// <param name="eccpMaintenancePlansManager">
        /// The eccp maintenance plans manager.
        /// </param>
        /// <param name="unitOfWorkManager">
        /// The unit of work manager.
        /// </param>
        public EccpMaintenanceContractManager(
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLinkRepository,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository,
            EccpMaintenancePlansManager eccpMaintenancePlansManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpMaintenanceContractElevatorLinkRepository = eccpMaintenanceContractElevatorLinkRepository;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpMaintenancePlansManager = eccpMaintenancePlansManager;
            this._unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// The create maintenance contract.
        /// 创建维保合同
        /// </summary>
        /// <param name="elevatorIds">
        /// The elevator ids.
        /// 电梯ID集合
        /// </param>
        /// <param name="maintenanceCompanyId">
        /// The maintenance company id.
        /// 维保单位ID
        /// </param>
        /// <param name="propertyCompanyId">
        /// The property company id.
        /// 使用单位ID
        /// </param>
        /// <returns>
        /// The<see cref="int"/>.
        /// </returns>
        public int CreateMaintenanceContract(List<Guid> elevatorIds, int maintenanceCompanyId, int propertyCompanyId)
        {
            if (this._unitOfWorkManager.Current == null)
            {
                return 1;
            }

            using (this._unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var maintenanceCompany =
                    this._eccpBaseMaintenanceCompanyRepository.FirstOrDefault(maintenanceCompanyId);

                int result;
                if (maintenanceCompany != null && maintenanceCompany.TenantId.HasValue)
                {
                    // 根据维保单位和使用单位查询合同，如果存在合同，向这个合同下添加电梯，如果不存在合同创建合同添加电梯
                    var maintenanceContract = this._eccpMaintenanceContractRepository.FirstOrDefault(
                        e => e.MaintenanceCompanyId == maintenanceCompany.Id
                             && e.PropertyCompanyId == propertyCompanyId);
                    if (maintenanceContract != null)
                    {
                        foreach (var elevatorId in elevatorIds)
                        {
                            // 查询此电梯是否存在于别的合同下，存在则修改
                            var maintenanceContractElevatorLink =
                                this._eccpMaintenanceContractElevatorLinkRepository.FirstOrDefault(
                                    e => e.ElevatorId == elevatorId);
                            if (maintenanceContractElevatorLink != null)
                            {
                                if (maintenanceContractElevatorLink.MaintenanceContractId != maintenanceContract.Id)
                                {
                                    // 关闭维保计划
                                    this._eccpMaintenancePlansManager.ClosePlan(elevatorId);

                                    maintenanceContractElevatorLink.MaintenanceContractId = maintenanceContract.Id;
                                    this._eccpMaintenanceContractElevatorLinkRepository.Update(
                                        maintenanceContractElevatorLink);
                                }
                            }
                            else
                            {
                                // 添加合同电梯
                                maintenanceContractElevatorLink = new EccpMaintenanceContract_Elevator_Link
                                                                        {
                                                                            MaintenanceContractId =
                                                                                maintenanceContract.Id,
                                                                            ElevatorId = elevatorId
                                                                        };
                                this._eccpMaintenanceContractElevatorLinkRepository.Insert(
                                    maintenanceContractElevatorLink);
                            }
                        }

                        result = 1;
                    }
                    else
                    {
                        // 添加合同
                        maintenanceContract = new EccpMaintenanceContract
                                                  {
                                                      TenantId = maintenanceCompany.TenantId.Value,
                                                      StartDate = DateTime.Now,
                                                      EndDate = DateTime.Now.AddYears(1),
                                                      MaintenanceCompanyId = maintenanceCompany.Id,
                                                      PropertyCompanyId = propertyCompanyId
                                                  };
                        var maintenanceContractId =
                            this._eccpMaintenanceContractRepository.InsertAndGetId(maintenanceContract);

                        // 添加合同电梯
                        foreach (var elevatorId in elevatorIds)
                        {
                            var maintenanceContractElevatorLink = new EccpMaintenanceContract_Elevator_Link
                                                                        {
                                                                            MaintenanceContractId =
                                                                                maintenanceContractId,
                                                                            ElevatorId = elevatorId
                                                                        };
                            this._eccpMaintenanceContractElevatorLinkRepository.Insert(
                                maintenanceContractElevatorLink);
                        }

                        result = 1;
                    }
                }
                else
                {
                    result = -1;
                }

                return result;
            }
        }
    }
}