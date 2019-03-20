
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using Sinodom.ElevatorCloud.Authorization.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders
{
    using Sinodom.ElevatorCloud.EccpDict;

    [Table("EccpMaintenanceTempWorkOrders")]
    public class EccpMaintenanceTempWorkOrder : CreationAuditedEntity<Guid> , IMustHaveTenant
    {
		public int TenantId { get; set; }


		[Required]
		[StringLength(EccpMaintenanceTempWorkOrderConsts.MaxTitleLength, MinimumLength = EccpMaintenanceTempWorkOrderConsts.MinTitleLength)]
		public virtual string Title { get; set; }
		
		[StringLength(EccpMaintenanceTempWorkOrderConsts.MaxDescribeLength, MinimumLength = EccpMaintenanceTempWorkOrderConsts.MinDescribeLength)]
		public virtual string Describe { get; set; }
		
		public virtual int CheckState { get; set; }

        public virtual int Priority { get; set; }

        public virtual DateTime? CompletionTime { get; set; }
		

		public virtual int MaintenanceCompanyId { get; set; }
		public ECCPBaseMaintenanceCompany MaintenanceCompany { get; set; }


        public virtual int TempWorkOrderTypeId { get; set; }
        public EccpDictTempWorkOrderType TempWorkOrderType { get; set; }

        public virtual long? UserId { get; set; }
		public User User { get; set; }
        public virtual Guid ElevatorId { get; set; }

    }
}