using Prorent24.BLL.Models.WorkPanel;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.WorkPanel
{
    public interface IWorkPanelService
    {
        Task<WorkPanelModel> GetWorkPanel();
    }
}
