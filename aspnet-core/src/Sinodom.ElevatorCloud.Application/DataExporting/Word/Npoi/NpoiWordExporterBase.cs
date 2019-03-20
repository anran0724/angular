using System;
using System.Collections.Generic;
using Abp.AspNetZeroCore.Net;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Sinodom.ElevatorCloud.Dto;
using Sinodom.ElevatorCloud.Storage;
using OfficeOpenXml;

namespace Sinodom.ElevatorCloud.DataExporting.Word.Npoi
{
    public abstract class NpoiWordExporterBase : ElevatorCloudServiceBase, ITransientDependency
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        protected NpoiWordExporterBase(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        protected FileDto CreateWordPackage(string fileName, byte[] bytes)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentWordprocessingmlDocument);

            Save(bytes, file);

            return file;
        }
        
        protected void Save(byte[] bytes, FileDto file)
        {
            _tempFileCacheManager.SetFile(file.FileToken, bytes);
        }
    }
}
