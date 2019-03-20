
namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    using System;
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp maintenance work orders input.
    /// </summary>
    public class GetAllByElevatorIdEccpMaintenanceWorkOrdersInput : PagedAndSortedResultRequestDto
    {

        public string Filter { get; set; }

        /// <summary>
        /// 电梯Id
        /// </summary>
        public string ElevatorIdFilter { get; set; }
        /// <summary>
        /// 维保状态
        /// </summary>
        public string EccpDictMaintenanceStatusNameFilter { get; set; }
        /// <summary>
        /// 维保类型
        /// </summary>
        public string EccpDictMaintenanceTypeNameFilter { get; set; }
        /// <summary>
        /// 维保公司名字
        /// </summary>
        public string MaintenanceCompanyNameFilter { get; set; }

    }
}