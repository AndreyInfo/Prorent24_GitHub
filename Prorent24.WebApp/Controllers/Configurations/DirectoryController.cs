using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.Directory;
using Prorent24.BLL.Services.Configuration.Directory;
using Prorent24.Enum.Directory;
using Prorent24.WebApp.Transfers.Directory;

namespace Prorent24.WebApp.Controllers.Configurations
{
    [Route("api/directories")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {
        private readonly IDirectoryService _directoryService;

        public DirectoryController(IDirectoryService directoryService)
        {
            _directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService));
        }


        /// <summary>
        /// Get GetDirectoriesByType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("{type}")]
        public async Task<IActionResult> GetDirectoriesByType(DirectoryTypeEnum type, string lang = "ru")
        {
            List<DirectoryDto> dtos = await _directoryService.GetByTypeAsync(type, lang);
            return Ok(dtos.TransferToViewModels());
        }

    }
}