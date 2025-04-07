using Prorent24.BLL.Models.General.Folder;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Folder
{
    public interface IFolderService : IBaseService<FolderDto, int>
    {
        /// <summary>
        /// Get list folders
        /// </summary>
        /// <returns></returns>
        Task<List<FolderDto>> GetFolders(ModuleTypeEnum moduleType, string search = null);
    }
}
