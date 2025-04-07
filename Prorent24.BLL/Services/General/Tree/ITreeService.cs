using Prorent24.Common.Models.Trees;
using Prorent24.Enum.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Tree
{
    public interface ITreeService
    {
        /// <summary>
        /// Get list tree columns by entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<List<TreeGroupColumn>> GetTreeColumnByEntity(EntityEnum entity);
    }
}
