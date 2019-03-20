namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos
{
    using System;

    public class GetEccpMaintenanceTempWorkOrderForAppView
    {
        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        public DateTime? CreationTime { get; set; }


        public string MaintenanceUserName { get; set; }
        /// <summary>
        ///     Gets or sets the eccp dict temp work order types name.
        /// </summary>
        public string EccpDictTempWorkOrderTypesName { get; set; }
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }
}