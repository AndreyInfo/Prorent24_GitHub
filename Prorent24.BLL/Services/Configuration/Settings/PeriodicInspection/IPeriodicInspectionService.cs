using Prorent24.BLL.Models.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Settings.PeriodicInspection
{
    public interface IPeriodicInspectionService : IBaseService<PeriodicInspectionDto, int>
    {
        /// <summary>
        /// Delete data multiple
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        Task<bool> DeleteMultiple(List<int> Values);
    }
}
