using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment
{
    internal class EquipmentStorage : BaseStorage<EquipmentEntity>, IEquipmentStorage
    {
        public EquipmentStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async override Task<EquipmentEntity> GetById(object id)
        {

            EquipmentEntity result = await _repos.Table
                .Include(x => x.Folder)
                .Include(x => x.Notes)
                .Include(x => x.Tags).ThenInclude(y => y.TagDirectory)
                .Include(x => x.Tasks)
                .Include(x => x.Files)
                .Include(x => x.VatClass)
                .ThenInclude(z => z.VatClassSchemeRates)
                .Include(x => x.EquipmentContents)
                .ThenInclude(y => y.Content)
                .Include(x => x.EquipmentSerialNumbers)
                .FirstOrDefaultAsync(x => x.Id == (int)id);

            return result != null ? result : new EquipmentEntity();
        }

        public async Task<List<EquipmentEntity>> GetByIds(List<int> ids)
        {
            return await _repos.TableNoTracking
                .Include(x => x.EquipmentContents) //для прорахунку к-ті
                .ThenInclude(y => y.Content)
                .Include(x => x.EquipmentSerialNumbers)
                .Where(x => ids.Contains(x.Id)).ToListAsync();
        }


        //public async Task<IPagedList<EquipmentEntity>> GetAllByFilter(List<SelectedFilter> filterList)
        //{
        //    var reposQuery = _repos.TableNoTracking.Include(x => x.EquipmentContents) //для прорахунку к-ті
        //        .ThenInclude(y => y.Content).Include(x => x.EquipmentSerialNumbers).AsQueryable();
            
        //    foreach (SelectedFilter filter in filterList)
        //    {
        //        if (filter.Values != null && filter.Values.Any())
        //        {
        //            FilterEnum filterEnum = (FilterEnum)System.Enum.Parse(typeof(FilterEnum), filter.FilterType);
        //            switch (filterEnum)
        //            {
        //                case FilterEnum.SearchText:
        //                    {
        //                        List<string> values = filter.Values.Select(x => x != null ? x.ToString().ToLower() : "").Where(x => !String.IsNullOrWhiteSpace(x.ToString())).ToList();

        //                        if (values != null && values.Count > 0)
        //                        {
        //                            reposQuery = reposQuery.Where(x => values.Contains(x.Name.ToLower()));
        //                        }
        //                        break;
        //                    }
        //                case FilterEnum.Folders:
        //                    {
        //                        reposQuery = reposQuery.Where(x => x.FolderId.HasValue && filter.Values.Any(i => Convert.ToInt32(i) == x.FolderId.Value));
        //                        break;
        //                    }
        //                case FilterEnum.Tags:
        //                    {
        //                        if (filter.Values?.Count > 0)
        //                        {
        //                            reposQuery = reposQuery.Include(x => x.Tags).ThenInclude(y => y.TagDirectory);

        //                            if (filter.SelectedAll)
        //                            {
        //                                reposQuery = reposQuery.Where(x => filter.Values.Any(i => (int)i == x.Id));
        //                            }
        //                            else
        //                            {
        //                                reposQuery = reposQuery.Where(x => x.Tags.Any(t => filter.Values.Contains(t.TagDirectoryId)));
        //                            }
        //                        }

        //                        break;
        //                    }
        //                case FilterEnum.AddedFilters:
        //                    {

        //                        break;
        //                    }
        //            }
        //        }
        //    }

        //    return await reposQuery.Include(x => x.Folder).OrderByDescending(x => x.CreationDate).ToPagedListAsync(0, 500);
        //}


        public async Task<IPagedList<EquipmentEntity>> GetAll(List<Predicate<EquipmentEntity>> conditions = null)
        {
            return await _repos.TableNoTracking
                .Include(x => x.Folder).Where(x=>x.Name!=null).ToPagedListAsync(0, 1000000000);
        }

        public async Task<List<EquipmentEntity>> Search(string text)
        {
            string likeSearch = text.ToLower();

            IQueryable<EquipmentEntity> result = (from quipment in _repos.TableNoTracking
                                                  where EF.Functions.Like(quipment.Name, $"%{likeSearch}%") ||
                                                        EF.Functions.Like(quipment.Code, $"%{likeSearch}%")
                                                  select new EquipmentEntity
                                                  {
                                                      Id = quipment.Id,
                                                      Name = quipment.Name,
                                                      Code = quipment.Code
                                                  });

            return await result.ToListAsync();
        }

        public async Task SetFolderId(int equipmentId, int folderId)
        {
            EquipmentEntity equipment = _repos.Table.SingleOrDefault(x => x.Id == equipmentId);
            if (equipment != null)
            {
                equipment.FolderId = folderId;
                await this.Update(equipment);
            }
        }

        public async Task<IPagedList<EquipmentEntity>> GetAllByFilter(IQueryable<EquipmentEntity> queryableEntity, string searchText = "")
        {
            string likeSearchCharUpper = string.Empty;
            string likeSearchCharLower = string.Empty;

            if(searchText.Length > 0)
            {
                likeSearchCharUpper = $"%{char.ToUpper(searchText[0]) + searchText.Substring(1)}%";
                likeSearchCharLower = $"%{char.ToLower(searchText[0]) + searchText.Substring(1)}%";
            }

            var equipments = await queryableEntity.Include(x => x.Folder)
                .Include(x => x.EquipmentContents) //для прорахунку к-ті
                .ThenInclude(y => y.Content).Include(x => x.EquipmentSerialNumbers)
                .Where(x => searchText.Length > 0 ? EF.Functions.Like(x.Name, likeSearchCharUpper) || EF.Functions.Like(x.Name, likeSearchCharLower) : true)
                .OrderByDescending(x=>x.CreationDate)
                .ToListAsync();

            return equipments.ToPagedList(0, 500);
        }
        
    }
}
