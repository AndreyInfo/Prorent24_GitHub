using Prorent24.BLL.Models.WorkPanel;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.WorkPanelStorage
{
    public interface IWorkPanelStorage
    {
        Task<WorkPanelModel> GetWorkPanelAggregateData(string userId);
    }
}
