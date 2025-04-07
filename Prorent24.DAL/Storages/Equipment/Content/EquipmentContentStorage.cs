using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Equipment;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Equipment
{
    internal class EquipmentContentStorage : BaseStorage<EquipmentContentEntity>, IEquipmentContentStorage
    {
        public EquipmentContentStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<EquipmentContentEntity>> GetAllByForeignId(int id)
        {
            var result = _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Content)
                .Where(x => x.EquipmentId == id).Select(x => x).ToPagedListAsync(0, 100);
            return result;
        }

        public async Task<IPagedList<EquipmentContentEntity>> GetAll(List<Predicate<EquipmentContentEntity>> conditions = null)
        {
            return await _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Content)
                .Select(x => x)
                .ToPagedListAsync(0, 500);
        }

        public async override Task<EquipmentContentEntity> GetById(object id)
        {
            var result = await _repos.Table   
                .Include(y => y.Content)
                .Include(x => x.Equipment)
             
                .Where(x => x.Id == (int)id).Select(x => x).FirstOrDefaultAsync();
            return result;
        }

        public async Task<EquipmentContentEntity> GetByKeys(int equipmentId, int contentId)
        {
            var result = await _repos.Table
                .Include(x => x.Equipment)
                .Include(x => x.Content)
                .Where(x => x.EquipmentId == equipmentId && x.ContentId == contentId).FirstOrDefaultAsync();
            return result;
        }


        public override async Task<EquipmentContentEntity> Update(EquipmentContentEntity model)
        {
            await Task.Run(() =>
            {
                _repos.Update(model, f => f.Quantity, f => f.UnfoldFinancialDocuments, f=>f.UnfoldPackingSlip);
                _unitOfWork.SaveChanges();
            });

            return model;
        }
    }
}
