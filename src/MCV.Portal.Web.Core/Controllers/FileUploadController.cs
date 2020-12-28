using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using MCV.Portal.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCV.Portal.Web.Core.Controllers
{
    [DisableValidation]
    public class FileUploadController : PortalControllerBase
    {
        [HttpPost]
        [Route("/api/UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, string ServiceName)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");
            var filePath = Path.Combine(
            Directory.GetCurrentDirectory(), "Source/Uploads/" + ServiceName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fileUniqueId = Guid.NewGuid().ToString().ToLower().Replace("-", string.Empty);
            var uniqueFileName = $"{fileUniqueId}_{file.FileName}";

            string loc;
            using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var result = new
            {
                UploadFileName = uniqueFileName
            };

            return new JsonResult(result);
        }
    }
}