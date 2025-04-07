using Prorent24.BLL.Models.Configuration.Financial;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.AdditionalCondition
{
    public interface IAdditionalConditionService : IBaseService<AdditionalConditionDto, int>
    {

        /// <summary>
        /// Delete data multiple
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        Task<bool> DeleteMultiple(List<int> Values);
    }
}
