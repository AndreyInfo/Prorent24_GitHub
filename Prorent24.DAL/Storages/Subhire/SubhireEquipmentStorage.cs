using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Subhire;
using Prorent24.UnitOfWork;

namespace Prorent24.DAL.Storages.Subhire
{
    internal class SubhireEquipmentStorage : BaseStorage<SubhireEquipmentEntity>, ISubhireEquipmentStorage
    {
        public SubhireEquipmentStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public override async Task<bool> Delete(int id)
        {
            //видаляємо спершу всі Children по групі
            var children = _repos.TableNoTracking.Where(z => z.ParentId == id);
            _repos.Delete(children);
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

        public async override Task<SubhireEquipmentEntity> GetById(object id)
        {
            return await _repos.TableNoTracking
                .Include(x => x.Equipment)
                .ThenInclude(y=>y.EquipmentContents)
                .Include(x => x.SubhireEquipmentGroup)
                .Include(x => x.CreationUser)
                .Include(x=>x.Children)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }


        public async Task<List<SubhireEquipmentEntity>> GetAllBySubhire(int subhireId)
        {
            return await _repos.TableNoTracking
                .Include(x => x.Equipment)
                .Include(x => x.SubhireEquipmentGroup)
                .Include(x => x.CreationUser)
                .Include(x => x.Children)
                .ThenInclude(y => y.Equipment)
                .Where(x => x.SubhireId == subhireId && x.ParentId == null)
                .Select(x => x)
                .ToListAsync();
        }

        public async Task<List<SubhireEquipmentEntity>> GetAllByFilter(IQueryable<SubhireEquipmentEntity> queryableEntity)
        {
            var projects = await queryableEntity.Include(c => c.Subhire).ToListAsync();
            return projects;
        }
        
    }
}
