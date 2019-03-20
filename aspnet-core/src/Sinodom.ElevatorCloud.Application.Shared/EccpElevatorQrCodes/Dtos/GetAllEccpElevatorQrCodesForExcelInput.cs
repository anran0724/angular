// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpElevatorQrCodesForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos
{
    using System;

    /// <summary>
    /// The get all eccp elevator qr codes for excel input.
    /// </summary>
    public class GetAllEccpElevatorQrCodesForExcelInput
    {
        /// <summary>
        /// Gets or sets the area name filter.
        /// </summary>
        public string AreaNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator name filter.
        /// </summary>
        public string EccpBaseElevatorNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the elevator num filter.
        /// </summary>
        public string ElevatorNumFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the is grant filter.
        /// </summary>
        public int IsGrantFilter { get; set; }

        /// <summary>
        /// Gets or sets the is install filter.
        /// </summary>
        public int IsInstallFilter { get; set; }

        /// <summary>
        /// Gets or sets the max grant date time filter.
        /// </summary>
        public DateTime? MaxGrantDateTimeFilter { get; set; }

        /// <summary>
        /// Gets or sets the max install date time filter.
        /// </summary>
        public DateTime? MaxInstallDateTimeFilter { get; set; }

        /// <summary>
        /// Gets or sets the min grant date time filter.
        /// </summary>
        public DateTime? MinGrantDateTimeFilter { get; set; }

        /// <summary>
        /// Gets or sets the min install date time filter.
        /// </summary>
        public DateTime? MinInstallDateTimeFilter { get; set; }
    }
}