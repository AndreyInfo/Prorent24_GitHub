using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.General.Tag
{
    internal class TagDirectoryStorage : BaseStorage<TagDirectoryEntity>, ITagDirectoryStorage
    {
        protected readonly IRepository<TagBondEntity> _reposTagBond;
        public TagDirectoryStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _reposTagBond = _unitOfWork.GetRepository<TagBondEntity>();
        }

        public Task<IPagedList<TagDirectoryEntity>> GetAll(List<Predicate<TagDirectoryEntity>> conditions = null)
        {
            return _repos.Table.ToPagedListAsync(0, 200);
            // .FirstOrDefaultAsync(x => x.LowerName == name && x.BelongsTo == module);
        }

        public async Task<TagDirectoryEntity> GetByNameAndBelong(string name, ModuleTypeEnum module)
        {
            return await _repos.Table
                .FirstOrDefaultAsync(x => x.LowerName == name && x.BelongsTo == module);
        }

        public async Task<TagBondEntity> CreateTagBond(TagBondEntity model)
        {
            try
            {
                await _reposTagBond.InsertAsync(model);
                await _unitOfWork.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<TagBondEntity>> GetTagBonds(ModuleTypeEnum module)
        {
            var reposQuery = _reposTagBond.TableNoTracking.Include(x => x.TagDirectory).AsQueryable();
            switch (module)
            {
                case ModuleTypeEnum.Tasks:
                    {
                        reposQuery = reposQuery.Include(x => x.Task);
                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        reposQuery = reposQuery.Include(x => x.Contact);
                        break;
                    }
                case ModuleTypeEnum.CrewMembers:
                    {
                        reposQuery = reposQuery.Include(x => x.CrewMembers);
                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        reposQuery = reposQuery.Include(x => x.Vehicle);
                        break;
                    }
                case ModuleTypeEnum.Projects:
                    {
                        reposQuery = reposQuery.Include(x => x.Project);
                        break;
                    }
                case ModuleTypeEnum.Subhire:
                    {
                        reposQuery = reposQuery.Include(x => x.Subhire);
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        reposQuery = reposQuery.Include(x => x.Equipment);
                        break;
                    }
                case ModuleTypeEnum.Repairs:
                    {
                        reposQuery = reposQuery.Include(x => x.Repair);
                        break;
                    }
                case ModuleTypeEnum.Inspections:
                    {
                        reposQuery = reposQuery.Include(x => x.Inspection);
                        break;
                    }
                case ModuleTypeEnum.Invoices:
                    {
                        reposQuery = reposQuery.Include(x=>x.Invoice);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return await reposQuery.ToListAsync();
        }

        public async Task<TagBondEntity> GetTagBond(ModuleTypeEnum module, TagBondEntity model)
        {
            var reposQuery = _reposTagBond.TableNoTracking.Include(x=>x.TagDirectory).AsQueryable();
            switch (module)
            {
                case ModuleTypeEnum.Tasks:
                    {
                        reposQuery = reposQuery.Where(x => x.TaskId == model.TaskId);
                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        reposQuery = reposQuery.Where(x => x.ContactId == model.ContactId);
                        break;
                    }
                case ModuleTypeEnum.CrewMembers:
                    {
                        reposQuery = reposQuery.Where(x => x.CrewMemberId == model.CrewMemberId);
                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        reposQuery = reposQuery.Where(x => x.VehicleId == model.VehicleId);
                        break;
                    }
                case ModuleTypeEnum.Projects:
                    {
                        reposQuery = reposQuery.Where(x => x.ProjectId == model.ProjectId);
                        break;
                    }
                case ModuleTypeEnum.Subhire:
                    {
                        reposQuery = reposQuery.Where(x => x.SubhireId == model.SubhireId);
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        reposQuery = reposQuery.Where(x => x.EquipmentId == model.EquipmentId);
                        break;
                    }
                case ModuleTypeEnum.Repairs:
                    {
                        reposQuery = reposQuery.Where(x => x.RepairId == model.RepairId);
                        break;
                    }
                case ModuleTypeEnum.Inspections:
                    {
                        reposQuery = reposQuery.Where(x => x.InspectionId == model.InspectionId);
                        break;
                    }
                case ModuleTypeEnum.Invoices:
                    {
                        reposQuery = reposQuery.Where(x => x.InvoiceId == model.InvoiceId);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            reposQuery = reposQuery.Where(x => x.TagDirectoryId == model.TagDirectoryId);

            return await reposQuery.FirstOrDefaultAsync();
        }

        public async Task<TagBondEntity> GetTagBondById(int id)
        {
            return await _reposTagBond.Table
                .Include(x => x.TagDirectory)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        public Task<List<TagDirectoryEntity>> GetListTags(ModuleTypeEnum moduleType)
        {
            return _repos.TableNoTracking.Where(x => x.BelongsTo == moduleType).ToListAsync();
        }

        public Task<List<TagDirectoryEntity>> SearchListTags(ModuleTypeEnum moduleType, string search_string)
        {
            return _repos.TableNoTracking.Where(x => x.BelongsTo == moduleType && x.LowerName.StartsWith(search_string.ToLower())).ToListAsync();
        }

        public async Task<bool> DeleteBond(TagBondEntity entity)
        {
            if (entity != null)
            {
                await Task.Run(() =>
                {
                    _reposTagBond.Delete(entity);
                    _unitOfWork.SaveChanges();
                });

            }
            else
            {
                return false;
            }
            var getEntity = await _reposTagBond.FindAsync(entity.Id);

            if (getEntity != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Task<List<TagBondEntity>> GetListBondByDirectoryId(int tagDirectoryId)
        {
            return _reposTagBond.TableNoTracking.Where(x => x.TagDirectoryId == tagDirectoryId).ToListAsync();
        }

        public async Task<bool> DeleteDirectoryTag(TagDirectoryEntity entity)
        {
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
            var getEntity = await _repos.FindAsync(entity.Id);

            if (getEntity != null)
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
