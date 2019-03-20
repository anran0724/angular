// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpDictMaintenanceTypeForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    /// <summary>
    /// The get eccp dict maintenance type for view.
    /// </summary>
    public class GetEccpDictMaintenanceTypeForView
    {
        /// <summary>
        /// Gets or sets the eccp dict maintenance type.
        /// </summary>
        public GetAllEccpDictMaintenanceTypeDto EccpDictMaintenanceType { get; set; }

        /// <summary>
        ///     ά������ģ��ID
        /// </summary>
        public int MaintenanceTemplateId { get; set; }

        /// <summary>
        ///     ά������ģ������
        /// </summary>
        public string MaintenanceTemplateName { get; set; }

        /// <summary>
        ///     ά������ID
        /// </summary>
        public int TypeId { get; set; }
    }
}