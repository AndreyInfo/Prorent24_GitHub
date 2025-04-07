using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.Common.ApplicationSettings;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Excels;
using Prorent24.Enum.General;
using Prorent24.WebApp.Extentions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    partial class GeneralController : ControllerBase
    {
        /// <summary>
        /// Parce Excel to Worksheet model
        /// </summary>
        /// <returns></returns>
        [HttpPost("excel/{entity}")]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> ParceToExcelWorksheet([FromForm]IFormCollection files, [FromRoute]ModuleTypeEnum entity)
        {
            using (var memoryStream = new MemoryStream())
            {
                string path = string.Empty;
                string fileName = string.Empty;
                IFormFile file = files.Files[0];
                int index = file.FileName.IndexOf(".");
                if (index > 0)
                {
                    string format = file.FileName.Substring(index, file.FileName.Length - index);
                    fileName = string.Concat(Guid.NewGuid().ToString(), format);
                    path = Path.Combine(
                      Directory.GetCurrentDirectory(), DirectorySettings.fileDirectory,
                      fileName);

                    if (!System.IO.File.Exists(path))
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }

                await files.Files[0].CopyToAsync(memoryStream);

                ExcelCustomWorksheet worksheet = await _excelService.GetWorksheet(entity, memoryStream);
                worksheet.Rows = worksheet.Rows.Take(10).ToList();
                worksheet.FilePath = LogoExtention.GeneratePathToCompanyLogo(this.HttpContext,_env.EnvironmentName, fileName);

                if (worksheet == null)
                {
                    return BadRequest();
                }

                return Ok(worksheet);
            }
        }

        /// <summary>
        /// Import from Excel
        /// </summary>
        /// <returns></returns>
        [HttpPost("excel/{entity}/import")]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Import([FromRoute]ModuleTypeEnum entity, ExcelCustomWorksheet worksheet)
        {
            await _excelService.Import(entity, worksheet);
            return Ok();
        }

        /// <summary>
        /// Import from Excel
        /// </summary>
        /// <returns></returns>
        [HttpGet("excel/{entity}/download-template")]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> DownloadTemplate([FromRoute]ModuleTypeEnum entity)
        {
            string filePath = await _excelService.DownloadTemplate(entity, DirectorySettings.fileDirectory);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, FileExtention.GetMimeType(filePath), Path.GetFileName(filePath));
        }
    }
}