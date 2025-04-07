using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Project;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Project
{
    internal class ProjectEquipmentGroupStorage : BaseStorage<ProjectEquipmentGroupEntity>, IProjectEquipmentGroupStorage
    {
        protected readonly IRepository<ProjectEquipmentEntity> _repositoryProjectEquipment;

        public ProjectEquipmentGroupStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repositoryProjectEquipment = _unitOfWork.GetRepository<ProjectEquipmentEntity>();
        }

        public async Task<IPagedList<ProjectEquipmentGroupEntity>> GetAll(List<Predicate<ProjectEquipmentGroupEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync();
        }

        public async Task<List<ProjectEquipmentGroupEntity>> GetAllByProject(int projectId)
        {
            return await _repos
                .TableNoTracking
                .Where(z => z == null ? false : z.ProjectId == projectId)
                //.Include(y => y.Equipments)
                //.ThenInclude(x => x.Children)
                .ToListAsync();
        }

        public override async Task<ProjectEquipmentGroupEntity> Create(ProjectEquipmentGroupEntity model)
        {
            try
            {
                //спочатку вставляємо тільки групу, потім обладнання, бо в ProjectEquipment - Сhildren не проставляється група
                ProjectEquipmentGroupEntity group = new ProjectEquipmentGroupEntity()
                {
                    ProjectId = model.ProjectId,
                    Name = model.Name,
                    StartPlanPeriod = model.StartPlanPeriod,
                    EndPlanPeriod = model.EndPlanPeriod,
                    StartUsePeriod = model.StartUsePeriod,
                    EndUsePeriod = model.EndUsePeriod,
                    CreationDate = model.CreationDate,
                    LastModifiedDate = model.LastModifiedDate
                };
                await _repos.InsertAsync(group);
                await _unitOfWork.SaveChangesAsync();
                model.Id = group.Id;
                if (model.Equipments?.Count > 0)
                {
                    foreach (ProjectEquipmentEntity equipment in model.Equipments)
                    {
                        equipment.ProjectEquipmentGroupId = model.Id;
                        if (equipment.Children?.Count > 0)
                        {
                            foreach (ProjectEquipmentEntity el in equipment.Children)
                            {
                                el.ProjectEquipmentGroupId = model.Id;
                            }
                        }
                    }

                    await _repositoryProjectEquipment.InsertAsync(model.Equipments);
                    await _unitOfWork.SaveChangesAsync();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public override async Task<bool> Delete(int id)
        {
            //видаляємо спершу всі Equipment по групі
            //var equipments = _repositoryProjectEquipment.TableNoTracking.Where(z => z.ProjectEquipmentGroupId == id);
            //_repositoryProjectEquipment.Delete(equipments);
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
           var delEntity = await _repos.FindAsync(id);

            if (delEntity != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override async Task<ProjectEquipmentGroupEntity> GetById(object id)
        {
            return await _repos.Table
              .Include(x => x.Equipments)
                .ThenInclude(y => y.Equipment)
                    //.ThenInclude(z => z.VatClass)
                       //.ThenInclude(r => r.VatClassSchemeRates)
              .FirstOrDefaultAsync(x => x.Id == (int)id);
        }
    }
}
