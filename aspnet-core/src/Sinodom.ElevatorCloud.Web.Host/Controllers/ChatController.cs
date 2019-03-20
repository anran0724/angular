using System;
using System.Net;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Mvc;
using Sinodom.ElevatorCloud.Chat;
using Sinodom.ElevatorCloud.Storage;

namespace Sinodom.ElevatorCloud.Web.Controllers
{
    public class ChatController : ChatControllerBase
    {
        public ChatController(IBinaryObjectManager binaryObjectManager, IChatMessageManager chatMessageManager) : 
            base(binaryObjectManager, chatMessageManager)
        {
        }

        public async Task<ActionResult> GetUploadedObject(Guid fileId, string contentType)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                
                var fileObject = await BinaryObjectManager.GetOrNullAsync(fileId);
                if (fileObject == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                return File(fileObject.Bytes, contentType);
            }
        }
    }
}
