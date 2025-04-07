using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prorent24.Entity.Configuration;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Configuration.SystemSettings
{
    internal class SystemSettingStorage : BaseStorage<SystemSettingEntity>, ISystemSettingStorage
    {
        public SystemSettingStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IPagedList<SystemSettingEntity>> GetAll(List<Predicate<SystemSettingEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync(x => x);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <returns></returns>
        public override async Task<SystemSettingEntity> Update(SystemSettingEntity model)
        {
            var entity = await _repos.FindAsync(model.Type);

            if (entity != null)
            {
                entity.Value = model.Value;
                await Task.Run(() =>
                {
                    _repos.Update(entity);
                    _unitOfWork.SaveChanges();
                });
            }
            else
            {
                await _repos.InsertAsync(model);
                await _unitOfWork.SaveChangesAsync();
            }

            return model;
        }
    }
}
