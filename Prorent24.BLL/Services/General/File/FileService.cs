using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.General;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.General.Folder;
using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.File
{
    class FileService : BaseService, IFileService
    {
        private readonly IFileStorage _fileStorage;

        public FileService(IFileStorage fileStorage,         
            IHttpContextAccessor httpContextAccessor,
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<object> GetList(bool isImage, ModuleTypeEnum BelongsTo, int id, string search)
        {
            //StringComparison comp = new StringComparison(); //StringComparison.Ordinal;
            //comp = StringComparison.OrdinalIgnoreCase;

            //search = search.ToUpper();

            List<FileDto> entities = await _fileStorage.QueryableEntity
                                                          .Where(x => x.BelongsTo.Equals(BelongsTo) && 
                                                                      x.EquipmentId.Equals(id) && x.IsImage.Equals(isImage) &&
                                                                      (search!="null" ? x.Name.ToUpper().IndexOf(search.ToUpper()) != -1 || x.Description.ToUpper().Contains(search.ToUpper()) : true))

                                                          .Select(f => new FileDto
                                                          {
                                                            Id = f.Id,
                                                            Name = f.Name,
                                                            Description = f.Description,
                                                            CreationDate =  f.CreationDate,
                                                            Path = f.Path,
                                                            Size = f.Size,
                                                            Author = f.CreationUser!=null ? (f.CreationUser.FirstName + " " + f.CreationUser.LastName) : "",
                                                          })
                                                          .ToListAsync();
            BaseGrid grid = entities.CreateGrid<FileDto>(await GetUserColumns(EntityEnum.FileEntity));
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.EquipmentEntity
            };
        }

        public async Task<FileDto> Create(FileDto model)
        {
            FileEntity entity = await _fileStorage.Create(model.TransferToEntity());
            return entity.TransferToDto();
        }

        public async Task<FileDto> Update(FileDto model)
        {
            FileEntity entity = await _fileStorage.GetById(model.Id);
            entity.Name = model.Name;
            entity.Description = model.Description;
            FileEntity resultEntity = await _fileStorage.Update(entity);
            return resultEntity.TransferToDto();
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _fileStorage.Delete(id);
            return result;
        }

        public async Task<FileDto> GetById(int id)
        {
            FileEntity entity = await _fileStorage.GetById(id);
            return entity.TransferToDto();
        }

        [Obsolete("don't use")]
        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }
    }
}
