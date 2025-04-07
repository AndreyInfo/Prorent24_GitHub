using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Services.Configuration.Financial.AdditionalCondition;
using Prorent24.WebApp.Models.Configuration.Financials.AdditionalCondition;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Configurations.Financials
{
    [Route("api/configuration/financial/additional_conditions")]
    [ApiController]
    [SwaggerTag("Configuration -> Financial")]
    public class AdditionalConditionController : ControllerBase
    {
        private readonly IAdditionalConditionService _additionalConditionService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="additionalConditionService"></param>
        public AdditionalConditionController(IAdditionalConditionService additionalConditionService)
        {
            _additionalConditionService = additionalConditionService;
        }
        /// <summary>
        /// Get list AdditionalConditions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAdditionalConditions()
        {
            BaseListDto dto = await _additionalConditionService.GetPagedList(null);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get AdditionalCondition by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdditionalConditionById(int id)
        {
            AdditionalConditionDto dto = await _additionalConditionService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Create AdditionalConition
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAdditionalConition(AdditionalConditionViewModel model)
        {
            try
            {
                AdditionalConditionDto dto = await _additionalConditionService.Create(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Update AdditionalConition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateAdditionalCondition(int id, AdditionalConditionViewModel model)
        {
            try
            {
                AdditionalConditionDto dto = await _additionalConditionService.Update(model.TransferToDtoModel());
                return Ok(dto.TransferToViewModel());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
      
        }

        /// <summary>
        /// Delete AdditionalConition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteAdditionalCondition(int id)
        {
            bool result = await _additionalConditionService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Delete AdditionalConition Multiple
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAdditionalConditionMultiple([FromBody]List<int> ids)
        {
            bool result = await _additionalConditionService.DeleteMultiple(ids);
            return Ok(result);
        }
    }
}