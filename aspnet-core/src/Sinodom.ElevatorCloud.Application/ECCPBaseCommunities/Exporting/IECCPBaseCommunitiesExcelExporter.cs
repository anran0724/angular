// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IECCPBaseCommunitiesExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseCommunities.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos;

    /// <summary>
    ///     The ECCPBaseCommunitiesExcelExporter interface.
    /// </summary>
    public interface IECCPBaseCommunitiesExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseCommunities">
        /// The eccp base communities.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetECCPBaseCommunityForView> eccpBaseCommunities);
    }
}