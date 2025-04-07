using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.AdditionalCondition
{
    internal class AdditionalConditionStorage : BaseStorage<AdditionalConditionEntity>, IAdditionalConditionStorage
    {
        public AdditionalConditionStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Get list AdditionalConditions
        /// </summary>
        /// <returns></returns>
        public Task<IPagedList<AdditionalConditionEntity>> GetAll(List<Predicate<AdditionalConditionEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync(x => new AdditionalConditionEntity() { Id = x.Id, Name = x.Name, Text = x.Text });
        }
    }
}
