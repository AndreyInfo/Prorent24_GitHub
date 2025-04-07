using Microsoft.AspNetCore.Http;
using Prorent24.Common.Models.Trees;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Tree
{
    internal class TreeService : BaseService, ITreeService
    {
        public TreeService(IColumnStorage сolumnStorage,
                           IHttpContextAccessor httpContextAccessor,
                           IUserRoleStorage userRoleStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage) { }

        public async Task<List<TreeGroupColumn>> GetTreeColumnByEntity(EntityEnum entity)
        {
            List<UserColumnEntity> columnEntity = await this._columnStorage.GetColumnsByEntity(entity, GetUserId());
            Dictionary<string, bool> selectedColumns = new Dictionary<string, bool>();
            columnEntity.ForEach(item =>
            {
                if(!selectedColumns.ContainsKey(item.Column))
                {
                    selectedColumns.Add(item.Column, item.ShowColumn);
                }
            });

            List<TreeGroupColumn> columnGroup = entity.GenerateColumnGroupByEntity(selectedColumns);

            return columnGroup;
        }
    }
}
