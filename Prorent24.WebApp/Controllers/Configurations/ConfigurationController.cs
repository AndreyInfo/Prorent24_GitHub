using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Services.General.Module;
using Prorent24.WebApp.Transfers.Modules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Configurations
{
    /// <summary>
    /// Configuration Controller (controller for get global configurations)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
       

        public ConfigurationController()
        {
            
        }

       
    }
}