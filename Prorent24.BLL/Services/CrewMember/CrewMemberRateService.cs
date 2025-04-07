using Prorent24.BLL.Models;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.CrewMemberRate;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.CrewMember;
using Prorent24.Entity.CrewMember;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.CrewMember
{
    internal class CrewMemberRateService : ICrewMemberRateService
    {
        private readonly ICrewMemberRateStorage _crewMemberRateStorage;

        public CrewMemberRateService(ICrewMemberRateStorage crewMemberRateStorage)
        {
            _crewMemberRateStorage = crewMemberRateStorage ?? throw new ArgumentNullException(nameof(crewMemberRateStorage));
        }

        public async Task<CrewMemberRateDto> Create(CrewMemberRateDto model)
        {
            CrewMemberRateEntity transferEntity = model.TransferToEntity();
            CrewMemberRateEntity entity = await _crewMemberRateStorage.Create(transferEntity);
            CrewMemberRateDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _crewMemberRateStorage.Delete(id);
            return result;
        }

        public async Task<CrewMemberRateDto> GetById(int id)
        {
            CrewMemberRateEntity entity = await _crewMemberRateStorage.GetById(id);
            CrewMemberRateDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<BaseListDto> GetPagedList(string crewMemberId)
        {
            IPagedList<CrewMemberRateEntity> pagedList = await _crewMemberRateStorage.GetAllByCrewMember(crewMemberId);
            List<CrewMemberRateEntity> listEntities = pagedList.Items.ToList();
            List<CrewMemberRateDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<CrewMemberRateDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.CrewMemberRateEntity
            };
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<CrewMemberRateEntity> pagedList = await _crewMemberRateStorage.GetAll();
            List<CrewMemberRateEntity> listEntities = pagedList.Items.ToList();
            List<CrewMemberRateDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<CrewMemberRateDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.CrewMemberRateEntity
            };
        }

        public async Task<CrewMemberRateDto> Update(CrewMemberRateDto model)
        {
            CrewMemberRateEntity transferEntity = model.TransferToEntity();
            CrewMemberRateEntity entity = await _crewMemberRateStorage.Update(transferEntity);
            CrewMemberRateDto dto = entity.TransferToDto();
            return dto;
        }
    }
}
