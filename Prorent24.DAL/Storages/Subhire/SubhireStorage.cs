using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Subhire;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Subhire
{
    internal class SubhireStorage : BaseStorage<SubhireEntity>, ISubhireStorage
    {

        protected readonly IRepository<SubhireProjectEntity> _reposSubhireProj;
        public SubhireStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _reposSubhireProj = _unitOfWork.GetRepository<SubhireProjectEntity>();
        }

        public async Task<List<SubhireProjectEntity>> CreateSubhireProjects(List<SubhireProjectEntity> models)
        {
            try
            {
                await _reposSubhireProj.InsertAsync(models);
                await _unitOfWork.SaveChangesAsync();
                return models;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IPagedList<SubhireEntity>> GetAll(List<Predicate<SubhireEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync(x => x, pageSize: 100); //, include: c => c.Include(proj => proj.Project).Include(supplierContact => supplierContact.SupplierContact), pageSize: 100);
        }

        public async Task<IPagedList<SubhireEntity>> GetAllByFilter(IQueryable<SubhireEntity> queryableEntity, string searchText = "")
        {
            var subhires = await queryableEntity
               .Include(x=>x.Projects).ThenInclude(y=>y.Project)
               .Include(x => x.SupplierContact)
               .ToListAsync();
            if (searchText.Length > 0)
            {
                subhires = subhires.Where(x =>
                 x.Name?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            return subhires.ToPagedList(0, 500);
        }

        public override async Task<SubhireEntity> GetById(object id)
        {
            return await _repos.Table
                .Include(x => x.Notes)
                .Include(x => x.Tags).ThenInclude(y=>y.TagDirectory)
                .Include(x => x.Tasks)
                .Include(x => x.Files)
                .Include(x => x.SupplierContact)
                .ThenInclude(x => x.VisitingAddress)
                .Include(x => x.SupplierContactPerson)
                .Include(x => x.LocationContact)
                .ThenInclude(x => x.VisitingAddress)
                .Include(x => x.LocationContactPerson)
                .Include(x => x.AccountManager)
                .Include(x => x.Projects).ThenInclude(y => y.Project)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        public async Task<List<SubhireEntity>> GetByIds(int[] ids)
        {
            return await _repos.TableNoTracking
                .Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<SubhireEntity>> GetByProjects(List<int> projectIds)
        {
            List<int> subhireIds = await _reposSubhireProj.TableNoTracking
            .Where(x => projectIds.Contains(x.ProjectId)).Select(x => x.SubhireId).ToListAsync();

            return await _repos.TableNoTracking.Include(x => x.SupplierContact)
             .Where(x => subhireIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<int>> GetSubhireProjects(int subhireId)
        {
            return await _reposSubhireProj.TableNoTracking
             .Where(x => x.SubhireId == subhireId).Select(x => x.ProjectId).ToListAsync();


        }

    }
}
