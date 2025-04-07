using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Financial.AdditionalCondition;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.AdditionalCondition
{
    internal class AdditionalConditionService : IAdditionalConditionService
    {
        private readonly IAdditionalConditionStorage _additionalConditionStorage;

        public AdditionalConditionService(IAdditionalConditionStorage additionalConditionStorage)
        {
            _additionalConditionStorage = additionalConditionStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<AdditionalConditionEntity> pagedList = await _additionalConditionStorage.GetAll();
            List<AdditionalConditionEntity> listEntities = pagedList.Items.ToList();
            List<AdditionalConditionDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<AdditionalConditionDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.AdditionalConditionEntity
            };
        }

        public async Task<AdditionalConditionDto> GetById(int id)
        {
            AdditionalConditionEntity entity = await _additionalConditionStorage.GetById(id);
            AdditionalConditionDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<AdditionalConditionDto> Create(AdditionalConditionDto model)
        {
            AdditionalConditionEntity transferEntity = model.TransferToEntity();
            AdditionalConditionEntity entity = await _additionalConditionStorage.Create(transferEntity);
            AdditionalConditionDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<AdditionalConditionDto> Update(AdditionalConditionDto model)
        {
            AdditionalConditionEntity transferEntity = model.TransferToEntity();
            AdditionalConditionEntity entity = await _additionalConditionStorage.Update(transferEntity);
            AdditionalConditionDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _additionalConditionStorage.Delete(id);
            return result;
        }

        public async Task<bool> DeleteMultiple(List<int> Values)
        {
            bool result = await _additionalConditionStorage.Delete(Values);
            return result;
        }
    }
}
