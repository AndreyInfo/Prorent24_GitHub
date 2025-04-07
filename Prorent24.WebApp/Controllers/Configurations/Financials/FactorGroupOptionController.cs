using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Services.Configuration.Financial.FactorGroup;
using Prorent24.WebApp.Models.Configuration.Financials;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial/factor_group_options")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public class FactorGroupOptionController : ControllerBase
    {
        private readonly IFactorGroupOptionService _factorGroupOptionService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factorGroupOptionService"></param>
        public FactorGroupOptionController(IFactorGroupOptionService factorGroupOptionService)
        {
            _factorGroupOptionService = factorGroupOptionService;
        }
       


        /// <summary>
        ///  get factorGroupOption by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactorGroupById(int id)
        {
            FactorGroupOptionDto dto = await _factorGroupOptionService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// create factorGroupOption
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateFactorGroup(FactorGroupOptionViewModel model)
        {
            FactorGroupOptionDto dto = await _factorGroupOptionService.Create(model.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// update factorGroupOption
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateFactorGroup(int id, FactorGroupOptionViewModel model)
        {
            FactorGroupOptionDto dto = await _factorGroupOptionService.Update(model.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// delete factorGroupOption
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteFactorGroup(int id)
        {
            bool result = await _factorGroupOptionService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Delete factorGroupOption multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteFactorGroupMultiple([FromBody]List<int> ids)
        {
            bool result = await _factorGroupOptionService.DeleteMultiple(ids);
            return Ok(result);
        }
    }
}