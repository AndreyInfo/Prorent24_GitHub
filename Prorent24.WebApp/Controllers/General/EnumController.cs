using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Extentions;

namespace Prorent24.WebApp.Controllers.General
{
    partial class GeneralController : ControllerBase
    {
        /// <summary>
        /// Get list enums by enum name
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("enums/{enumName}")]
        public IActionResult Get(string enumName)
        {

            List<KeyValuePair<string, int>> result = enumName.GetEnumList();
            return Ok(result);
        }

    }
}