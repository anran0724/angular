// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpBaseElevatorsForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    using System;

    /// <summary>
    /// The get all eccp base elevators for excel input.
    /// </summary>
    public class GetAllEccpBaseElevatorsForExcelInput
    {
        /// <summary>
        /// Gets or sets the certificate num filter.
        /// </summary>
        public string CertificateNumFilter { get; set; }

        /// <summary>
        /// Gets or sets the city name filter.
        /// </summary>
        public string CityNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the district name filter.
        /// </summary>
        public string DistrictNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base annual inspection unit name filter.
        /// </summary>
        public string ECCPBaseAnnualInspectionUnitNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community name filter.
        /// </summary>
        public string ECCPBaseCommunityNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator brand name filter.
        /// </summary>
        public string EccpBaseElevatorBrandNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model name filter.
        /// </summary>
        public string EccpBaseElevatorModelNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base maintenance company name filter.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company name filter.
        /// </summary>
        public string ECCPBaseProductionCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company name filter.
        /// </summary>
        public string ECCPBasePropertyCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp base register company name filter.
        /// </summary>
        public string ECCPBaseRegisterCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator status name filter.
        /// </summary>
        public string ECCPDictElevatorStatusNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator type name filter.
        /// </summary>
        public string EccpDictElevatorTypeNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict place type name filter.
        /// </summary>
        public string EccpDictPlaceTypeNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the installation address filter.
        /// </summary>
        public string InstallationAddressFilter { get; set; }

        /// <summary>
        /// Gets or sets the machine num filter.
        /// </summary>
        public string MachineNumFilter { get; set; }

        /// <summary>
        /// Gets or sets the max installation datetime filter.
        /// </summary>
        public DateTime? MaxInstallationDatetimeFilter { get; set; }

        /// <summary>
        /// Gets or sets the min installation datetime filter.
        /// </summary>
        public DateTime? MinInstallationDatetimeFilter { get; set; }

        /// <summary>
        /// Gets or sets the province name filter.
        /// </summary>
        public string ProvinceNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the street name filter.
        /// </summary>
        public string StreetNameFilter { get; set; }
    }
}