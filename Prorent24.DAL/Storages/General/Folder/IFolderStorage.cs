using Prorent24.Entity.General;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Folder
{
    public interface IFolderStorage : IBaseStorage<FolderEntity>
    {
        Task<List<FolderEntity>> GetListFolders(ModuleTypeEnum menuType, string search = null);
    } 
}
