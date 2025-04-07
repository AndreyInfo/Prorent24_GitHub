using Prorent24.Entity.Base;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages
{
    public interface IBaseStorage<T>
    {
        /// <summary>
        /// Queryable Entity
        /// </summary>
        IQueryable<T> QueryableEntity { get; }

        /// <summary>
        /// Get list
        /// </summary>
        /// <returns></returns>
        Task<IPagedList<T>> GetAll(List<Predicate<T>> conditions = null);

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <returns></returns>
        Task<T> GetById(object id);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T> Create(T model);

        /// <summary>
        /// Create list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<T>> Create(List<T> models);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T>Update(T model);

        /// <summary>
        /// Update List
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<T>> Update(List<T> model);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);


        /// <summary>
        /// Delete array int 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(List<int> ids);

        /// <summary>
        /// Delete array string 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(List<string> ids);

        /// <summary>
        /// Import
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task Import(List<T> entities);

        IQueryable<T> Enitity();
    }
}
