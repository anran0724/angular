using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    public class GetEccpBaseElevatorsInfoDto
    {
        public DateTime? LastMaintenanceTime { get; set; }
        public string LastMaintenanceUserNames { get; set; }
        public string CertificateNum { get; set; }
        /// <summary>
        /// Gets or sets the machine num.
        /// </summary>
        public string MachineNum { get; set; }
        /// <summary>
        /// Gets or sets the installation address.
        /// </summary>
        public string InstallationAddress { get; set; }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// Gets or sets the eccp dict place type name.
        /// </summary>
        public string EccpDictPlaceTypeName { get; set; }
        /// <summary>
        /// Gets or sets the eccp dict elevator type name.
        /// </summary>
        public string EccpDictElevatorTypeName { get; set; }
        /// <summary>
        /// Gets or sets the eccp base property company name.
        /// </summary>
        public string ECCPBasePropertyCompanyName { get; set; }
        /// <summary>
        /// Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyName { get; set; }
        /// <summary>
        /// Gets or sets the eccp base annual inspection unit name.
        /// </summary>
        public string ECCPBaseAnnualInspectionUnitName { get; set; }
        /// <summary>
        /// Gets or sets the eccp base elevator brand name.
        /// </summary>
        public string EccpBaseElevatorBrandName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model name.
        /// </summary>
        public string EccpBaseElevatorModelName { get; set; }
    }
}
