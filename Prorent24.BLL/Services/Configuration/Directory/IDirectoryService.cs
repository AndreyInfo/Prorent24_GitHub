using Prorent24.BLL.Models.Directory;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Directory
{
    public interface IDirectoryService: IBaseService<DirectoryDto, int>
    {
        Task<List<DirectoryDto>>  GetByTypeAsync(DirectoryTypeEnum type, string lang = "en");
    }
}
