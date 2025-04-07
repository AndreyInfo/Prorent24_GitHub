using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Services.General.Excel;
using Prorent24.BLL.Services.General.File;
using Prorent24.BLL.Services.General.Filter;
using Prorent24.BLL.Services.General.Folder;
using Prorent24.BLL.Services.General.Grid;
using Prorent24.BLL.Services.General.Module;
using Prorent24.BLL.Services.General.Note;
using Prorent24.BLL.Services.General.Tag;
using Prorent24.BLL.Services.General.Tree;
using Prorent24.Context;
using Prorent24.WebApp.Transfers.Modules;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    /// <summary>
    /// Generals
    /// </summary>
    [Route("api/generals")]
    [ApiController]
    public partial class GeneralController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        private readonly IGridService _gridService;
        private readonly ITreeService _treeService;
        private readonly IPresetService _presetService;
        private readonly IFolderService _folderService;
        private readonly ITagService _tagService;
        private readonly IFilterService _filterService;
        private readonly INoteService _noteService;
        private readonly IFileService _fileService;
        private readonly IExcelService _excelService;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Generals
        /// </summary>
        /// <param name="moduleService"></param>
        /// <param name="gridService"></param>
        /// <param name="treeService"></param>
        /// <param name="presetService"></param>
        /// <param name="folderService"></param>
        /// <param name="tagService"></param>
        /// <param name="filterService"></param>
        /// <param name="noteService"></param>
        /// <param name="fileService"></param>
        /// <param name="excelService"></param>
        /// <param name="env"></param>
        public GeneralController(IModuleService moduleService,
                                 IGridService gridService,
                                 ITreeService treeService,
                                 IPresetService presetService,
                                 IFolderService folderService,
                                 ITagService tagService,
                                 IFilterService filterService,
                                 INoteService noteService,
                                 IFileService fileService,
                                 IExcelService excelService,
                                 IHostingEnvironment env)
        {
            _moduleService = moduleService;
            _gridService = gridService;
            _treeService = treeService;
            _presetService = presetService;
            _folderService = folderService;
            _filterService = filterService;
            _tagService = tagService;
            _noteService = noteService;
            _fileService = fileService;
            _excelService = excelService;
            _env = env;
        }

        /// <summary>
        /// Get list modules
        /// </summary>
        /// <returns></returns>
        [HttpGet("modules")]
        public async Task<IActionResult> GetModules()
        {
            List<ModuleDto> modules = await _moduleService.GetModuleItems();
            return Ok(modules.TransferToModuleViewModel());
        }
    }
}