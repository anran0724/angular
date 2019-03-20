// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpDictMaintenanceItemPrintForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The get eccp dict maintenance item for view.
    /// </summary>
    public class GetEccpDictMaintenanceItemPrintForView
    {
        /// <summary>
        ///     Gets or sets the dis order.
        /// </summary>
        public int DisOrder { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the maintenance complate date.
        /// </summary>
        public DateTime? MaintenanceComplateDate { get; set; }

        /// <summary>
        /// Gets or sets the maintenance status.
        /// 维保人员认可0，否则1，无此项目部位2
        /// </summary>
        public int MaintenanceStatus { get; set; }

        /// <summary>
        /// Gets or sets the maintenance user name.
        /// </summary>
        public string MaintenanceUserName { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the term code.
        /// </summary>
        public string TermCode { get; set; }

        /// <summary>
        ///     Gets or sets the term desc.
        /// </summary>
        public string TermDesc { get; set; }
        public string CertificateNum { get; set; }
        public string InstallationAddress { get; set; }
        public string MaintenanceTypeName { get; set; }
        public string Remark { get; set; }
    }
}