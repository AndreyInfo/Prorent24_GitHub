using Prorent24.Common.Models.Excels;
using Prorent24.Enum.General;
using System.IO;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Excel
{
    public interface IExcelService
    {
        Task<ExcelCustomWorksheet> GetWorksheet(ModuleTypeEnum moduleType, MemoryStream memoryStream);
        Task Import(ModuleTypeEnum moduleType, ExcelCustomWorksheet worksheet);
        Task<string> DownloadTemplate(ModuleTypeEnum moduleType, string path);
    }
}
