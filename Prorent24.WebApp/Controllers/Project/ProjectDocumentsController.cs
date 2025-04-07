using Microsoft.AspNetCore.Mvc;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using Prorent24.WebApp.Transfers.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.Project
{
    public partial class ProjectController
    {
        /// <summary>
        /// Get ProjectEquipments
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}/documents")]
        public async Task<IActionResult> GetProjectDocuments([FromRoute]int projectId)
        {
            try
            {
                var result = await _documentService.GetDocumentsByProjectId(projectId);
                return Ok(result?.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }
    }
}
