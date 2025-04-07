using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Base;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages
{
    internal abstract class BaseStorage<T> where T : class
    {
        protected readonly IRepository<T> _repos;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseStorage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repos = _unitOfWork.GetRepository<T>();
        }

       public IQueryable<T> QueryableEntity
        {
            get
            {
                return _repos.TableNoTracking;
            }
        }

        /// <summary>
        /// Get by Id Entity
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> GetById(object id)
        {
            return await _repos.FindAsync(id);
        }

        /// <summary>
        /// Create Entity
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> Create(T model)
        {
            try
            {
                await _repos.InsertAsync(model);
                await _unitOfWork.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Create Entitities
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> Create(List<T> model)
        {
            try
            {
                await _repos.InsertAsync(model);
                await _unitOfWork.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Upadte Entity
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> Update(T model)
        {
            await Task.Run(() =>
             {
                 _repos.Update(model);
                 _unitOfWork.SaveChanges();
             });

            return model;
        }

        /// <summary>
        /// Upadte Entity
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> Update(List<T> model)
        {
            await Task.Run(() =>
            {
                _repos.Update(model);
                _unitOfWork.SaveChanges();
            });

            return model;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> Delete(int id)
        {
            var entity = await _repos.FindAsync(id);
            if (entity != null)
            {
                await Task.Run(() =>
               {
                   _repos.Delete(entity);
                   _unitOfWork.SaveChanges();
               });

            }
            else
            {
                return false;
            }
            entity = await _repos.FindAsync(id);

            if (entity != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            var entity = await _repos.FindAsync(id);
            if (entity != null)
            {
                await Task.Run(() =>
                {
                    _repos.Delete(entity);
                    _unitOfWork.SaveChanges();
                });

            }
            else
            {
                return false;
            }
            entity = await _repos.FindAsync(id);

            if (entity != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Delete array
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Delete(List<string> ids)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    _repos.Delete(ids[i]);
                }
                _unitOfWork.SaveChanges();
            });

            return true;
        }

        /// <summary>
        /// Delete list int
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Delete(List<int> ids)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < ids.Count; i++)
                {
                  //  var entity = _repos.FindAsync(ids[i]);
                    _repos.Delete(ids[i]);
                }
                _unitOfWork.SaveChanges();
            });

            return true;
        }

        /// <summary>
        /// Import new list
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task Import(List<T> entities)
        {
            await _repos.InsertAsync(entities);
            await _unitOfWork.SaveChangesAsync();
        }



        public IQueryable<T> Enitity()
        {
            return _repos.Table;
        }           
    }
}
