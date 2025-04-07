using Prorent24.BLL.Models;
using Prorent24.Common.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services
{
    public interface IBaseService<T, TKey>
    {
        /// <summary>
        /// Get list data by filter
        /// </summary>
        /// <returns></returns>
        Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList);

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(TKey id);

        /// <summary>
        /// Create data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T> Create(T model);

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T> Update(T model);

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(TKey id);

       
    }
}
