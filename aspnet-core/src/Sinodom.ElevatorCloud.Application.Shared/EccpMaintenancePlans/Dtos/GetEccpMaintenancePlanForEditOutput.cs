// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenancePlanForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using System.Collections.Generic;

    /// <summary>
    /// The get eccp maintenance plan for edit output.
    /// </summary>
    public class GetEccpMaintenancePlanForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp base elevator name.
        /// </summary>
        public string EccpBaseElevatorName { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance plan.
        /// </summary>
        public CreateOrEditEccpMaintenancePlanDto EccpMaintenancePlan { get; set; }

        /// <summary>
        ///     ����ID
        /// </summary>
        public string ElevatorIds { get; set; }

        /// <summary>
        ///     ��������
        /// </summary>
        public string ElevatorNames { get; set; }

        /// <summary>
        /// Gets or sets the half year polling period.
        /// </summary>
        public int HalfYearPollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the maintenance types.
        /// </summary>
        public List<GetEccpDictMaintenanceTypeForView> MaintenanceTypes { get; set; }

        /// <summary>
        ///     ά���û�ID
        /// </summary>
        public string MaintenanceUserIds { get; set; }

        /// <summary>
        ///     ά���û�����
        /// </summary>
        public string MaintenanceUserNames { get; set; }

        /// <summary>
        ///     ʹ���û�ID
        /// </summary>
        public string PropertyUserIds { get; set; }

        /// <summary>
        ///     ʹ���û�����
        /// </summary>
        public string PropertyUserNames { get; set; }

        /// <summary>
        /// Gets or sets the quarter polling period.
        /// </summary>
        public int QuarterPollingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the year polling period.
        /// </summary>
        public int YearPollingPeriod { get; set; }

        public int EccpBaseElevatorNum { get; set; }
        public bool IsClose { get; set; }

    }
}