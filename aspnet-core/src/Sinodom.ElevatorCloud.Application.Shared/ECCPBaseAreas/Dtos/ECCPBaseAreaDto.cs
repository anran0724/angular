// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAreaDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp base area dto.
    /// </summary>
    public class ECCPBaseAreaDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        public int ParentId { get; set; }
    }
}