// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderTransfersForAuditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers.Dtos
{
    /// <summary>
    /// The eccp maintenance work order transfers for audit output.
    /// </summary>
    public class EccpMaintenanceWorkOrderTransfersForAuditOutput
    {
        /// <summary>
        ///     工单类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        ///     工单简介
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     是否通过
        /// </summary>
        public bool? IsApproved { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }
    }
}