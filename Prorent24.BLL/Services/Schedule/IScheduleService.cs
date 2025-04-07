using Prorent24.Common.Models.Schedulers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Schedule
{
    public interface IScheduleService
    {
        Task<List<SchedulerModel>> GetSchedules(string userId);
    }
}
