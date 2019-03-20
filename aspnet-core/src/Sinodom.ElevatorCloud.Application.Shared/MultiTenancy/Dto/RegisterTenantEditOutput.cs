// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterTenantEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.MultiTenancy.Dto
{
    using System;

    /// <summary>
    /// The register tenant edit output.
    /// </summary>
    public class RegisterTenantEditOutput
    {
        /// <summary>
        /// Gets or sets the aptitude photo id.
        /// </summary>
        public Guid? AptitudePhotoId { get; set; }

        /// <summary>
        /// Gets or sets the aptitude photo id file token.
        /// </summary>
        public string AptitudePhotoIdFileToken { get; set; }

        /// <summary>
        /// Gets or sets the business license id.
        /// </summary>
        public Guid? BusinessLicenseId { get; set; }

        /// <summary>
        /// Gets or sets the business license id file token.
        /// </summary>
        public string BusinessLicenseIdFileToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is member.
        /// </summary>
        public bool IsMember { get; set; }

        /// <summary>
        /// Gets or sets the legal person.
        /// </summary>
        public string LegalPerson { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}