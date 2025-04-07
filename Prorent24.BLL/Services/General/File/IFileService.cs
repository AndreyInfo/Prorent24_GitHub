using Prorent24.BLL.Models.General.File;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.File
{
    public interface IFileService : IBaseService<FileDto, int>
    {
        Task<object> GetList(bool isImage, ModuleTypeEnum BelongsTo, int id, string search);
    }
}
