﻿using System;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.MaintenanceWorkOrderTransfers
{
    public class AuditMaintenanceWorkOrderTransfersViewModel
    {
        /// <summary>
        /// 工单简介
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 工单简介
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public string OrderTypeName { get; set; }
        /// <summary>
        /// 工单创建时间
        /// </summary>
        public DateTime OrderCreationTime { get; set; }
        /// <summary>
        /// 申请转接人姓名
        /// </summary>
        public string ApplicationTransferName { get; set; }
        /// <summary>
        /// 被转接人姓名
        /// </summary>
        public string TransferUserName { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// 工单类别
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 申请转接时间
        /// </summary>
        public DateTime ApplicationTransferCreationTime { get; set; }
    }
}