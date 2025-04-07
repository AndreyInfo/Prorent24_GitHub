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
    internal class SubhireEquipmentGroupStorage : BaseStorage<SubhireEquipmentGroupEntity>, ISubhireEquipmentGroupStorage
    {
        protected readonly IRepository<SubhireEquipmentEntity> _repositorySubhireEquipment;
        public SubhireEquipmentGroupStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repositorySubhireEquipment = _unitOfWork.GetRepository<SubhireEquipmentEntity>();
        }

        public async Task<IPagedList<SubhireEquipmentGroupEntity>> GetAll(List<Predicate<SubhireEquipmentGroupEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync();
        }

        public async Task<List<SubhireEquipmentGroupEntity>> GetAllBySubhire(int subhireId)
        {
            return await _repos.TableNoTracking.Include(y => y.Equipments).Where(z => z.SubhireId == subhireId).ToListAsync();

        }

        public override async Task<SubhireEquipmentGroupEntity> Create(SubhireEquipmentGroupEntity model)
        {
            try
            {
                //спочатку вставляємо тільки групу, потім обладнання, бо в SubhireEquipment - Сhildren не проставляється група
                SubhireEquipmentGroupEntity group = new SubhireEquipmentGroupEntity()
                {
                    SubhireId = model.SubhireId,
                    Name = model.Name,
                    CreationDate = model.CreationDate,
                    LastModifiedDate = model.LastModifiedDate
                };
                await _repos.InsertAsync(group);
                await _unitOfWork.SaveChangesAsync();
                model.Id = group.Id;
                if (model.Equipments?.Count > 0)
                {
                    foreach (SubhireEquipmentEntity equipment in model.Equipments)
                    {
                        equipment.SubhireEquipmentGroupId = model.Id;
                        if (equipment.Children?.Count > 0)
                        {
                            foreach (SubhireEquipmentEntity el in equipment.Children)
                            {
                                el.SubhireEquipmentGroupId = model.Id;
                            }
                        }
                    }

                    await _repositorySubhireEquipment.InsertAsync(model.Equipments);
                    await _unitOfWork.SaveChangesAsync();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public override async Task<bool> Delete(int id)
        {
             //видаляємо спершу всі Equipment по групі
            var equipments = _repositorySubhireEquipment.TableNoTracking.Where(z => z.SubhireEquipmentGroupId == id);
            _repositorySubhireEquipment.Delete(equipments);
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
    }
}
