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
    [Route("api/configuration/financial/factor_groups")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public class FactorGroupController : ControllerBase
    {
        private readonly IFactorGroupService _factorGroupService;
        public FactorGroupController(IFactorGroupService factorGroupService)
        {
            _factorGroupService = factorGroupService;
        }
        /// <summary>
        /// get factor groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFactorGroups()
        {
            BaseListDto dto = await _factorGroupService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }
        /// <summary>
        ///  get factor group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactorGroupById(int id)
        {
            FactorGroupDto dto = await _factorGroupService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// create factor group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateFactorGroup(FactorGroupViewModel model)
        {
            FactorGroupDto dto = await _factorGroupService.Create(model.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }
        /// <summary>
        /// update factor group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateFactorGroup(int id, FactorGroupViewModel model)
        {
            FactorGroupDto dto = await _factorGroupService.Update(model.TransferToDtoModel());
            return Ok(dto.TransferToViewModel());
        }
        /// <summary>
        /// delete factor group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteFactorGroup(int id)
        {
            bool result = await _factorGroupService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Delete factor group multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteFactorGroupMultiple([FromBody]List<int> ids)
        {
            bool result = await _factorGroupService.DeleteMultiple(ids);
            return Ok(result);
        }
    }
}