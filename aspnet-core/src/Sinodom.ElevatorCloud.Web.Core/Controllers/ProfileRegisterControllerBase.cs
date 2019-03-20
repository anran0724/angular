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
    public abstract class ProfileRegisterControllerBase : ElevatorCloudControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const int MaxProfilePictureSize = 5242880; //5MB

        protected ProfileRegisterControllerBase(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }
        [Abp.Authorization.AbpAllowAnonymous]
        public UploadProfilePictureOutput UploadProfilePicture(FileDto input)
        {
            try
            {
                IFormFile profilePictureFile = null;

                foreach(var  file  in Request.Form.Files)
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

                if (profilePictureFile.Length > MaxProfilePictureSize)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit", AppConsts.MaxProfilPictureBytesUserFriendlyValue));
                }

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception(L("IncorrectImageFormat"));
                }

                _tempFileCacheManager.SetFile(input.FileToken, fileBytes);

                using (var bmpImage = new Bitmap(new MemoryStream(fileBytes)))
                {
                    return new UploadProfilePictureOutput
                    {
                        FileToken = input.FileToken,
                        FileName = input.FileName,
                        FileType = input.FileType,
                        Width = bmpImage.Width,
                        Height = bmpImage.Height
                    };
                }
            }
            catch (UserFriendlyException ex)
            {
                return new UploadProfilePictureOutput(new ErrorInfo(ex.Message));
            }
        }
    }
}
