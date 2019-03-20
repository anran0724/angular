using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetElevatorArchivesInfoDto
    {
        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the district name.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base annual inspection unit name.
        /// </summary>
        public string ECCPBaseAnnualInspectionUnitName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community name.
        /// </summary>
        public string ECCPBaseCommunityName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator.
        /// </summary>
        public GetEccpBaseElevatorDto EccpBaseElevator { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator brand name.
        /// </summary>
        public string EccpBaseElevatorBrandName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model name.
        /// </summary>
        public string EccpBaseElevatorModelName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator subsidiary info.
        /// </summary>
        public GetEccpBaseElevatorSubsidiaryInfoDto EccpBaseElevatorSubsidiaryInfo { get; set; }

        /// <summary>
        /// Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company name.
        /// </summary>
        public string ECCPBaseProductionCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company name.
        /// </summary>
        public string ECCPBasePropertyCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base register company name.
        /// </summary>
        public string ECCPBaseRegisterCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator status name.
        /// </summary>
        public string ECCPDictElevatorStatusName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator type name.
        /// </summary>
        public string EccpDictElevatorTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict place type name.
        /// </summary>
        public string EccpDictPlaceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the province name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the street name.
        /// </summary>
        public string StreetName { get; set; }
    }
}
