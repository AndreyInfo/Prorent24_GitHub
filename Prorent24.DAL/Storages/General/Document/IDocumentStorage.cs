using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Document
{
    public interface IDocumentStorage : IBaseStorage<DocumentEntity>
    {
        Task<List<DocumentEntity>> GetAllByForeignId(EntityEnum dependency, int id);
    }
}
