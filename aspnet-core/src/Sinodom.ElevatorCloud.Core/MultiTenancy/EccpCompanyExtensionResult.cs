// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpCompanyExtensionResult.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.MultiTenancy
{
    using System;

    /// <summary>
    /// The eccp company extension result.
    /// </summary>
    public class EccpCompanyExtensionResult
    {
        /// <summary>
        /// Gets or sets the aptitude photo id.
        /// </summary>
        public Guid? AptitudePhotoId { get; set; }

        /// <summary>
        /// Gets or sets the business license id.
        /// </summary>
        public Guid? BusinessLicenseId { get; set; }

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
    }
}