
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
        /// ����Id
        /// </summary>
        public string ElevatorIdFilter { get; set; }
        /// <summary>
        /// ά��״̬
        /// </summary>
        public string EccpDictMaintenanceStatusNameFilter { get; set; }
        /// <summary>
        /// ά������
        /// </summary>
        public string EccpDictMaintenanceTypeNameFilter { get; set; }
        /// <summary>
        /// ά����˾����
        /// </summary>
        public string MaintenanceCompanyNameFilter { get; set; }

    }
}