using Prorent24.BLL.Models.General.Note;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Note
{
    public interface INoteService
    {
        Task<NoteDto> CreateNote(NoteDto dto);
        Task<NoteDto> UpdateNote(NoteDto dto);
        Task<bool> DeleteNote(int id);
    }
}
