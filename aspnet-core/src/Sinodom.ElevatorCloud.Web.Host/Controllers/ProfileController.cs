using Abp.AspNetCore.Mvc.Authorization;
using Sinodom.ElevatorCloud.Storage;

namespace Sinodom.ElevatorCloud.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Abp.AspNetZeroCore.Net;
    using Abp.Authorization;

    using Microsoft.AspNetCore.Mvc;

    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        /// <summary>
        /// The _binary object manager.
        /// </summary>
        private readonly IBinaryObjectManager _binaryObjectManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        /// <param name="binaryObjectManager">
        /// The binary object manager.
        /// </param>
        public ProfileController(ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
            : base(tempFileCacheManager)
        {
            this._binaryObjectManager = binaryObjectManager;
        }

        /// <summary>
        /// The get profile picture by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAllowAnonymous]
        public async Task<FileResult> GetProfilePictureById(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return await this.GetProfilePictureById(Guid.Parse(id));
        }

        /// <summary>
        /// The get profile picture by id.
        /// </summary>
        /// <param name="profilePictureId">
        /// The profile picture id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<FileResult> GetProfilePictureById(Guid profilePictureId)
        {
            var file = await this._binaryObjectManager.GetOrNullAsync(profilePictureId);
            if (file == null)
            {
                return null;
            }

            return this.File(file.Bytes, MimeTypeNames.ImageJpeg);
        }
    }
}
