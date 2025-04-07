using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Folder;
using Prorent24.WebApp.Transfers.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    /// <summary>
    /// Folders
    /// </summary>
    public partial class GeneralController : ControllerBase
    {
        /// <summary>
        /// Get folders by type
        /// </summary>
        /// <returns></returns>
        [HttpGet("folders/type/{moduleType}/{search?}")]
        public async Task<IActionResult> GetFoldersByType(ModuleTypeEnum moduleType, string search = null)
        {
            List<FolderDto> dto = await _folderService.GetFolders(moduleType, search);
            List<FolderViewModel> model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Get folder by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("folders/{id}")]
        public async Task<IActionResult> GetFolderById(int id)
        {
            FolderDto dto = await _folderService.GetById(id);
            FolderViewModel model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Create new folder
        /// </summary>
        /// <returns></returns>
        [HttpPost("folders")]
        public async Task<IActionResult> CreateFolder(FolderViewModel model)
        {
            FolderDto dto = model.TransferToDtoModel();
            dto = await _folderService.Create(dto);
            model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Update folder
        /// </summary>
        /// <returns></returns>
        [HttpPost("folders/{id}")]
        public async Task<IActionResult> UpdateFolder(int id, FolderViewModel model)
        {
            FolderDto dto = model.TransferToDtoModel();
            dto = await _folderService.Update(dto);
            model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Delete folder
        /// </summary>
        /// <returns></returns>
        [HttpPost("folders/{id}/delete")]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            bool result = await _folderService.Delete(id);
            return Ok(result);
        }
    }
}