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
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Configuration.Financial.FactorGroup
{
    internal class FactorGroupOptionService : IFactorGroupOptionService
    {
        private readonly IFactorGroupOptionStorage _factorGroupOptionStorage;
        public FactorGroupOptionService(IFactorGroupOptionStorage factorGroupOptionStorage)
        {
            _factorGroupOptionStorage = factorGroupOptionStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<FactorGroupOptionEntity> pagedList = await _factorGroupOptionStorage.GetAll();
            List<FactorGroupOptionEntity> listEntities = pagedList.Items.ToList();
            List<FactorGroupOptionDto> listDtos = listEntities.Select(x => x.TransferToFactorGroupOptionDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<FactorGroupOptionDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.FactorGroupOptionEntity
            };
        }

        public async Task<FactorGroupOptionDto> GetById(int id)
        {
            FactorGroupOptionEntity entity = await _factorGroupOptionStorage.GetById(id);
            FactorGroupOptionDto factorGroupDto = entity.TransferToFactorGroupOptionDto();
            return factorGroupDto;
        }

        public async Task<FactorGroupOptionDto> Create(FactorGroupOptionDto model)
        {
            FactorGroupOptionEntity entity = model.TransferToFactorGroupOptionEntity();
            FactorGroupOptionEntity factorGroupOptionEntity = await _factorGroupOptionStorage.Create(entity);
            FactorGroupOptionDto factorGroupOptionDto = factorGroupOptionEntity.TransferToFactorGroupOptionDto();
            return factorGroupOptionDto;
        }

        public async Task<FactorGroupOptionDto> Update(FactorGroupOptionDto model)
        {
            FactorGroupOptionEntity entity = model.TransferToFactorGroupOptionEntity();
            FactorGroupOptionEntity factorGroupOptionEntity = await _factorGroupOptionStorage.Update(entity);
            FactorGroupOptionDto factorGroupOptionDto = factorGroupOptionEntity.TransferToFactorGroupOptionDto();
            return factorGroupOptionDto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _factorGroupOptionStorage.Delete(id);
            return result;
        }

        public async Task<bool> DeleteMultiple(List<int> Values)
        {
            bool result = await _factorGroupOptionStorage.Delete(Values);
            return result;
        }
    }
}
