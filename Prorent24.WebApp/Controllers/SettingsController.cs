using Microsoft.AspNetCore.Mvc;

namespace Prorent24.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        //protected readonly IServiceProvider _serviceProvider;

        //public SettingsController(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}

        //[HttpGet("MenuItems")]
        //public async Task<IActionResult> GetMenuItems()
        //{
        //    IModuleService menuService = _serviceProvider.GetService(typeof(IModuleService)) as IModuleService;
        //    await menuService.ImportModules();
        //    List<ModuleDto> menuDto = await menuService.ModuleItems();
        //    return Ok(menuDto.TransferToModuleViewModel());
        //}
    }
}