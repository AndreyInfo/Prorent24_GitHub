using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages
{
    public interface IForeignDependencyStorage<T>
    {
        Task<IPagedList<T>> GetAllByForeignId(int id);
    }
}
