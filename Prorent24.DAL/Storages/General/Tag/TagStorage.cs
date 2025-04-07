using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.General.Tag
{
    internal class TagStorage : BaseStorage<TagEntity>, ITagStorage
    {
        public TagStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<TagEntity>> GetListTags(ModuleTypeEnum moduleType)
        {
            IQueryable<TagEntity> table = _repos.Table;

            switch (moduleType)
            {
                case ModuleTypeEnum.CrewMembers:
                    {
                        table = table.Where(x => x.BelongsTo == ModuleTypeEnum.CrewMembers && !string.IsNullOrEmpty(x.CrewMemberId));
                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        table = table.Where(x => x.BelongsTo == ModuleTypeEnum.Contacts && x.ContactId.HasValue);
                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        table = table.Where(x => x.BelongsTo == ModuleTypeEnum.Vehicles && x.VehicleId.HasValue);
                        break;
                    }
                case ModuleTypeEnum.Tasks:
                    {
                        table = table.Where(x => x.BelongsTo == ModuleTypeEnum.Tasks && x.TaskId.HasValue);
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        table = table.Where(x => x.BelongsTo == ModuleTypeEnum.Equipment && x.EquipmentId.HasValue);
                        break;
                    }
            }

            return await table.ToListAsync();
        }


        public async Task<List<TagEntity>> SearchListTags(ModuleTypeEnum moduleType, string search_string)
        {

            IQueryable<TagEntity> table = _repos.Table;

            switch (moduleType)
            {
                case ModuleTypeEnum.CrewMembers:
                    {
                        table.Where(x => x.BelongsTo == ModuleTypeEnum.CrewMembers && !string.IsNullOrEmpty(x.CrewMemberId));
                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        table.Where(x => x.BelongsTo == ModuleTypeEnum.Contacts && x.ContactId.HasValue);
                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        table.Where(x => x.BelongsTo == ModuleTypeEnum.Vehicles && x.VehicleId.HasValue);
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        table.Where(x => x.BelongsTo == ModuleTypeEnum.Equipment && x.EquipmentId.HasValue);
                        break;
                    }
            }

            return await table.Where(x=>x.Name.ToLower().StartsWith(search_string.ToLower())).ToListAsync();
        }

        #region [Obsolete("Don't use")]

        [Obsolete("Don't use")]
        public Task<IPagedList<TagEntity>> GetAll(List<Predicate<TagEntity>> conditions = null)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
