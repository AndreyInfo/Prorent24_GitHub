using Humanizer.Bytes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Transfers.General;
using Prorent24.Common.ApplicationSettings;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Grids;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.File;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    partial class GeneralController : ControllerBase
    {

        /// <summary>
        /// Upload new file
        /// </summary>
        /// <returns></returns>
        [HttpGet("files/{entity}/{id}/{isImage}/{search}")]
        public async Task<IActionResult> GetFiles([FromRoute]ModuleTypeEnum entity, [FromRoute]int id, [FromRoute]bool isImage, [FromRoute] string search)
        {
            var result = await _fileService.GetList(isImage, entity, id, search);
            return Ok(result);
        }

        /// <summary>
        /// Upload new file
        /// </summary>
        /// <returns></returns>
        [HttpPost("files/{entity}/{referenceId}/{isImage?}")]
        public async Task<IActionResult> UploadFile([FromRoute]ModuleTypeEnum entity, [FromRoute]string referenceId, [FromRoute]bool isImage, IFormFile file)
        {
            // EntityEnum entity
            if (file == null || file.Length == 0)
            {
                return NoContent();
            }

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), DirectorySettings.fileDirectory,
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            FileViewModel view = new FileViewModel()
            {
                BelongsTo = entity,
                ReferenceId = referenceId,
                Name = file.FileName,
                Path = path
            };

            FileDto dto = view.TransferToDto();

            dto.IsImage = isImage;
            dto.Size = ByteSize.FromBytes(file.Length).Kilobytes;
            dto.Size = dto.Size.HasValue ? Math.Round(dto.Size.Value) : 0;

           dto = await _fileService.Create(dto);

            return Ok(dto.TransferToView());
        }

        /// <summary>
        /// Download file
        /// </summary>
        /// <returns></returns>
        [HttpGet("files/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            FileDto file = await _fileService.GetById(id);

            var memory = new MemoryStream();
            using (var stream = new FileStream(file.Path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, FileExtention.GetMimeType(file.Path), Path.GetFileName(file.Path));
        }

        
        /// <summary>
        /// Update file
        /// </summary>
        /// <returns></returns>
        [HttpPost("files/{id}")]
        public async Task<IActionResult> UpdateFile(int id, FileViewModel model)
        {
            FileDto dto = await _fileService.Update(model.TransferToDto());
            return Ok(dto.TransferToView());
        }

        /// <summary>
        /// Delete file
        /// </summary>
        /// <returns></returns>
        [HttpPost("files/{id}/delete")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            bool result = await _fileService.Delete(id);
            return Ok(result);
        }
    }
}