using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.General.Period;
using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.BLL.Transfers.TimeRegistration;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.TimeRegistration;
using Prorent24.Entity.TimeRegistration;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prorent24.BLL.Builders;

namespace Prorent24.BLL.Services.TimeRegistration
{
    internal class TimeRegistrationService: BaseService, ITimeRegistrationService
    {
        private readonly ITimeRegistrationStorage _timeRegistrationStorage;

        public TimeRegistrationService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage, ITimeRegistrationStorage timeRegistrationStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _timeRegistrationStorage = timeRegistrationStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            string searchText;
            IQueryable<TimeRegistrationEntity> queryableEntity = _timeRegistrationStorage.QueryableEntity.CreateFilterForTimeRegisterEntity(filterList, out searchText);
            IPagedList<TimeRegistrationEntity> pagedList = await _timeRegistrationStorage.GetAllByFilter(queryableEntity, searchText);
            List<TimeRegistrationDto> listDtos = pagedList.Items.ToList().TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<TimeRegistrationDto>(await GetUserColumns(EntityEnum.TimeRegistrationEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.TimeRegistrationEntity
            };
        }

        public async Task<TimeRegistrationDto> GetById(int id)
        {
            TimeRegistrationEntity entity = await _timeRegistrationStorage.GetById(id);
            TimeRegistrationDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<TimeRegistrationDto> Create(TimeRegistrationDto model)
        {
            List<TimeRegistrationEntity> transferEntities = model.TransferToListEntity();
            List<TimeRegistrationEntity> entities = await _timeRegistrationStorage.Create(transferEntities);
            TimeRegistrationDto dto = entities.First().TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            TimeRegistrationEntity entity = await _timeRegistrationStorage.GetById(id);
            TimeRegistrationDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<TimeRegistrationDto>(EntityEnum.TimeRegistrationEntity);

            //double totalDays = Math.Round((dto.From - dto.Until).TotalDays);
            //double totalMinutes = Math.Round((dto.From - dto.Until).TotalHours);

            return moduleDetails;
        }

        public async Task<TimeRegistrationDto> Update(TimeRegistrationDto model)
        {
            TimeRegistrationEntity transferEntity = model.TransferToEntity(model.CrewMemberId);
            TimeRegistrationEntity entity = await _timeRegistrationStorage.Update(transferEntity);
            TimeRegistrationDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _timeRegistrationStorage.Delete(id);
            return result;
        }
    }
}
