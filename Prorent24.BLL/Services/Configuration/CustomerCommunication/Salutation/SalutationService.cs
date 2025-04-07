using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.Configuration.CustomerCommunication;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.CustomerCommunication.Salutation;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.CustomerCommunication.Salutation
{
    internal class SalutationService : ISalutationService
    {
        private readonly ISalutationStorage _salutationStorage;

        public SalutationService(ISalutationStorage salutationStorage)
        {
            _salutationStorage = salutationStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<SalutationEntity> pagedList = await _salutationStorage.GetAll();
            List<SalutationEntity> listEntities = pagedList.Items.ToList();
            List<SalutationDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<SalutationDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.SalutationEntity
            };
        }

        public async Task<SalutationDto> GetById(int id)
        {
            SalutationEntity entity = await _salutationStorage.GetById(id);
            SalutationDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<SalutationDto> Create(SalutationDto model)
        {
            SalutationEntity transferEntity = model.TransferToEntity();
            SalutationEntity entity = await _salutationStorage.Create(transferEntity);
            SalutationDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<SalutationDto> Update(SalutationDto model)
        {
            SalutationEntity transferEntity = model.TransferToEntity();
            SalutationEntity entity = await _salutationStorage.Update(transferEntity);
            SalutationDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _salutationStorage.Delete(id);
            return result;
        }
    }
}
