using Prorent24.Entity.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Note
{
    internal class NoteStorage : BaseStorage<NoteEntity>, INoteStorage
    {
        public NoteStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<IPagedList<NoteEntity>> GetAll(List<Predicate<NoteEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async override Task<NoteEntity> Update(NoteEntity model)
        {
            NoteEntity note =  await base.GetById(model.Id);
            note.Text = model.Text;
            note.Confidential = model.Confidential;

            return await base.Update(note);
        }
    }
}
