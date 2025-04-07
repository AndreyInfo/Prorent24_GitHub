using Prorent24.Entity.Directory;
using Prorent24.Enum.Directory;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Directory
{
    public interface IDirectoryStorage : IBaseStorage<DirectoryEntity>
    {
        Task<List<DirectoryEntity>> GetAllByType(DirectoryTypeEnum directoryType, string lang = "en" );
    }
}
