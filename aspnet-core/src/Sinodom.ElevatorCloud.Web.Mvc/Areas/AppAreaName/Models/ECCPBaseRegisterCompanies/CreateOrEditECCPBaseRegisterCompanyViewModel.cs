// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseRegisterCompanyViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseRegisterCompanies
{
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos;

    /// <summary>
    ///     The create or edit eccp base register company view model.
    /// </summary>
    public class CreateOrEditECCPBaseRegisterCompanyViewModel
    {
        /// <summary>
        ///     Gets or sets the city name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        ///     Gets or sets the district name.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base register company.
        /// </summary>
        public CreateOrEditECCPBaseRegisterCompanyDto EccpBaseRegisterCompany { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseRegisterCompany.Id.HasValue;

        /// <summary>
        ///     Gets or sets the province name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        ///     Gets or sets the street name.
        /// </summary>
        public string StreetName { get; set; }
    }
}