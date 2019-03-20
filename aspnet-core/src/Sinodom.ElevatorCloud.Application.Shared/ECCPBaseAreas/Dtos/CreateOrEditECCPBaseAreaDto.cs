// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseAreaDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp base area dto.
    /// </summary>
    public class CreateOrEditECCPBaseAreaDto : FullAuditedEntityDto<int?>
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [StringLength(ECCPBaseAreaConsts.MaxCodeLength, MinimumLength = ECCPBaseAreaConsts.MinCodeLength)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(ECCPBaseAreaConsts.MaxNameLength, MinimumLength = ECCPBaseAreaConsts.MinNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }
    }
}