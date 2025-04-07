using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Financial.FactorGroup;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Configuration.Financial.FactorGroup
{
    internal class FactorGroupService : IFactorGroupService
    {
        private readonly IFactorGroupStorage _factorGroupStorage;
        public FactorGroupService(IFactorGroupStorage factorGroupStorage)
        {
            _factorGroupStorage = factorGroupStorage;
        }
        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<FactorGroupEntity> pagedList = await _factorGroupStorage.GetAll();
            List<FactorGroupEntity> listEntities = pagedList.Items.ToList();
            List<FactorGroupDto> listDtos = listEntities.Select(x => x.TransferToFactorGroupDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<FactorGroupDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.FactorGroupEntity
            };
        }

        public async Task<FactorGroupDto> GetById(int id)
        {
            FactorGroupEntity factorGroupEntity = await _factorGroupStorage.GetById(id);
            FactorGroupDto factorGroupDto = factorGroupEntity.TransferToFactorGroupDto();
            return factorGroupDto;
        }

        public async Task<FactorGroupDto> Create(FactorGroupDto model)
        {
            FactorGroupEntity entity = model.TransferToFactorGroupEntity();
            FactorGroupEntity factorGroupEntity = await _factorGroupStorage.Create(entity);
            FactorGroupDto factorGroupDto = factorGroupEntity.TransferToFactorGroupDto();
            return factorGroupDto;
        }

        public async Task<FactorGroupDto> Update(FactorGroupDto model)
        {
            // додати відповідні 
            //var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.FactorGroup);
            //if (permission.Add)
            FactorGroupEntity entity = model.TransferToFactorGroupEntity();
            FactorGroupEntity factorGroupEntity = await _factorGroupStorage.Update(entity);
            FactorGroupDto factorGroupDto = factorGroupEntity.TransferToFactorGroupDto();
            return factorGroupDto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _factorGroupStorage.Delete(id);
            return result;
        }

        public async Task<bool> DeleteMultiple(List<int> Values)
        {
            bool result = await _factorGroupStorage.Delete(Values);
            return result;
        }
    }
}
