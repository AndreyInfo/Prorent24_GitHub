using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.BLL.Transfers.Configuration.CustomerCommunication;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.CustomerCommunication.Letterhead;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.CustomerCommunication.Letterhead
{
    internal class LetterheadService : ILetterheadService
    {
        private readonly ILetterheadStorage _letterheadStorage;

        public LetterheadService(ILetterheadStorage letterheadStorage)
        {
            _letterheadStorage = letterheadStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<LetterheadEntity> pagedList = await _letterheadStorage.GetAll();
            List<LetterheadEntity> listEntities = pagedList.Items.ToList();
            List<LetterheadDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<LetterheadDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.LetterheadEntity
            };
        }

        public async Task<LetterheadDto> GetById(int id)
        {
            LetterheadEntity entity = await _letterheadStorage.GetById(id);
            LetterheadDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<LetterheadDto> Create(LetterheadDto model)
        {
            LetterheadEntity transferEntity = model.TransferToEntity();
            LetterheadEntity entity = await _letterheadStorage.Create(transferEntity);
            LetterheadDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<LetterheadDto> Update(LetterheadDto model)
        {
            LetterheadEntity transferEntity = model.TransferToEntity();
            LetterheadEntity entity = await _letterheadStorage.Update(transferEntity);
            LetterheadDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _letterheadStorage.Delete(id);
            return result;
        }
    }
}
