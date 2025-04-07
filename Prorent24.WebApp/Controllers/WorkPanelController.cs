using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Services.WorkPanel;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPanelController : ControllerBase
    {
        private readonly IWorkPanelService _workPanelService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workPanelService"></param>
        public WorkPanelController(IWorkPanelService workPanelService)
        {
            _workPanelService = workPanelService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWorkPanelData()
        {
            return Ok(await _workPanelService.GetWorkPanel());
        }
    }
}