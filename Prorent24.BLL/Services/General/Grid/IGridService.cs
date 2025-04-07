using Prorent24.BLL.Models;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Grids;
using Prorent24.Common.Models.Trees;
using Prorent24.Enum.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Grid
{
    public interface IGridService
    {
        /// <summary>
        /// Get list column groups with include columns
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<List<ColumnGroup>> GetColumnGroupsByEntity(EntityEnum entity);

        /// <summary>
        /// Add columns
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        Task AddColumns(EntityEnum entity, params TreeColumn[] columns);

        /// <summary>
        /// Set Defauil Columns
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task SetDefauilColumns(EntityEnum entity);

        /// <summary>
        /// Update columns
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        Task UpdateColumns(EntityEnum entity, List<string> columns);

        /// <summary>
        /// Change order column
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <param name="newIndex"></param>
        /// <param name="columnsReorder"></param>
        /// <returns></returns>
        Task ChangeOrderColumn(EntityEnum entity, string column, short newIndex, List<ColumnReorder> columnsReorder);

        /// <summary>
        /// Change Width column
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        Task ChangeWidthColumn(EntityEnum entity, string column, double width);
    }
}
