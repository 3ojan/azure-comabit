using Comabit.BL.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.UI.Controllers
{
    public class FileController : BaseController
    {
        private FileManager _fileManager;

        public FileController(FileManager fileManager, ILogger<FileController> logger) : base(logger)
        {
            this._fileManager = fileManager;
        }

        public async Task<ActionResult> DownloadFile(Guid id)
        {
            var fileItem = await this._fileManager.Get(id);

            return File(fileItem.FileData.Data, fileItem.MimeType, fileItem.FileName);
        }
    }
}