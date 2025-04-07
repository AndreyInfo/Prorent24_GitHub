using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models.General.Note;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Note;
using Prorent24.WebApp.Transfers.General;

namespace Prorent24.WebApp.Controllers.General
{
    /// <summary>
    /// Notes
    /// </summary>
    public partial class GeneralController : ControllerBase
    {
        /// <summary>
        /// Create new note
        /// </summary>
        /// <returns></returns>
        [HttpPost("notes/{entity}/{referenceId}")]
        public async Task<IActionResult> CreateNote([FromRoute] ModuleTypeEnum entity, [FromRoute] string referenceId, NoteViewModel model)
        {
            model.BelongsTo = entity;
            model.ReferenceId = referenceId;

            NoteDto dto = await _noteService.CreateNote(model.TransferToDtoModel());
            model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Update note
        /// </summary>
        /// <returns></returns>
        [HttpPost("notes/{id}")]
        public async Task<IActionResult> UpdateNote(int id, NoteViewModel model)
        {
            NoteDto dto = await _noteService.UpdateNote(model.TransferToDtoModel());
            model = dto.TransferToViewModel();
            return Ok(model);
        }

        /// <summary>
        /// Delete note
        /// </summary>
        /// <returns></returns>
        [HttpPost("notes/{id}/delete")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            bool result = await _noteService.DeleteNote(id);
            return Ok(result);
        }


    }
}