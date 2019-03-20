using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Http;
using Sinodom.ElevatorCloud.Authorization.Users.Profile.Dto;
using Sinodom.ElevatorCloud.Dto;
using Sinodom.ElevatorCloud.Storage;
using Sinodom.ElevatorCloud.Web.Helpers;

namespace Sinodom.ElevatorCloud.Web.Controllers
{
    using Abp.Authorization;

    using Microsoft.AspNetCore.Authorization;

    public abstract class ProfileAppVersionControllerBase : ElevatorCloudControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const int MaxProfilePictureSize = 5242880; //5MB

        protected ProfileAppVersionControllerBase(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        [AllowAnonymous]
        public UploadProfilePictureOutput UploadProfilePicture(FileDto input)
        {
            try
            {
                IFormFile profilePictureFile = null;

                foreach (var file in Request.Form.Files)
                {
                    if (file.Name == input.FileName)
                    {
                        profilePictureFile = file;
                    }
                }

                //Check input
                if (profilePictureFile == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }
                

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                _tempFileCacheManager.SetFile(input.FileToken, fileBytes);

                return new UploadProfilePictureOutput
                {
                    FileToken = input.FileToken,
                    FileName = input.FileName,
                    FileType = input.FileType
                };

            }
            catch (UserFriendlyException ex)
            {
                return new UploadProfilePictureOutput(new ErrorInfo(ex.Message));
            }
        }
    }
}
