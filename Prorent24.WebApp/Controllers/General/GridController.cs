using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Trees;
using Prorent24.Context;
using Prorent24.Enum.Entity;
using Prorent24.WebApp.Models.Grid;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers.General
{
    public partial class GeneralController : ControllerBase
    {

        /// <summary>
        /// Get list column groups for tree
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("grids/trees/column_groups/{entity}")]
        public async Task<IActionResult> GetTreeColumnGroups(EntityEnum entity)
        {
            List<TreeGroupColumn> treeColumns = await _treeService.GetTreeColumnByEntity(entity);
            return Ok(treeColumns);
        }
        
        /// <summary>
        /// Get list column groups for grid
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("grids/column_groups/{entity}")]
        public async Task<IActionResult> GetColumnGroups(EntityEnum entity)
        {
            List<ColumnGroup> columnGroups = await _gridService.GetColumnGroupsByEntity(entity);
            return Ok(columnGroups);
        }

        /// <summary>
        /// Add list columns to entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("grids/column_groups/add")]
        public async Task<IActionResult> AddColumns(GridEntityViewModel entity)
        {
            await _gridService.AddColumns(entity.ModuleEntity, entity.Columns.ToArray());
            return Ok();
        }


        

        [HttpPost("grids/column_groups/set")]
        public async Task<IActionResult> SetDefauilColumns(GridEntityViewModel entity) //EntityEnum entity
        {
            await _gridService.SetDefauilColumns(entity.ModuleEntity);
            return Ok();
        }


        /// <summary>
        /// Update list column groups for grid
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        [HttpPost("grids/column_groups/{entity}/update")]
        public async Task<IActionResult> UpdateColumn([FromRoute]EntityEnum entity, [FromBody]List<string> columns)
        {
           await _gridService.UpdateColumns(entity, columns);
            return Ok();
        }

        /// <summary>
        /// Get list column groups
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <param name="newIndex"></param>
        /// <param name="columnsReorder"></param>
        /// <returns></returns>
        [HttpPost("grids/column_groups/{entity}/columns/{column}/order/{newIndex}")]
        public async Task<IActionResult> ChangeOrderColumn([FromRoute]EntityEnum entity, [FromRoute]string column, [FromRoute]short newIndex, [FromBody]List<ColumnReorder> columnsReorder)
        {
            await _gridService.ChangeOrderColumn(entity, column, newIndex, columnsReorder);

            return Ok();
        }

        /// <summary>
        /// Get list column groups
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        [HttpPost("grids/column_groups/{entity}/columns/{column}/size/{width}")]
        public async Task<IActionResult> ChangeSizeColumn(EntityEnum entity, string column, double width)
        {
            await _gridService.ChangeWidthColumn(entity, column, width);
            return Ok();
        }
    }
}