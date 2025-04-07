using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Transfers.General;
using Prorent24.DAL.Storages.General.Note;
using Prorent24.Entity.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Note
{
    internal class NoteService : INoteService
    {
        private readonly INoteStorage _noteStorage;

        public NoteService(INoteStorage noteStorage)
        {
            _noteStorage = noteStorage;
        }

        public async Task<NoteDto> CreateNote(NoteDto dto)
        {
            NoteEntity entity = await _noteStorage.Create(dto.TransferToEntity());
            return entity.TransferToDto();
        }

        public async Task<bool> DeleteNote(int id)
        {
            bool result = await _noteStorage.Delete(id);
            return result;
        }

        public async Task<NoteDto> UpdateNote(NoteDto dto)
        {
            NoteEntity entity = await _noteStorage.Update(dto.TransferToEntity());
            return entity.TransferToDto();
        }
    }
}
