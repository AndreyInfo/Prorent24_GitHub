using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.BLL.Transfers.General;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Storages.General.Folder;
using Prorent24.Entity.General;
using Prorent24.Enum.General;

namespace Prorent24.BLL.Services.General.Folder
{
    internal class FolderService : IFolderService
    {
        private readonly IFolderStorage _folderStorage;

        public FolderService(IFolderStorage folderStorage)
        {
            _folderStorage = folderStorage;
        }

        /// <summary>
        /// Get list folders
        /// </summary>
        /// <returns></returns>
        public async Task<List<FolderDto>> GetFolders(ModuleTypeEnum moduleType, string search = null)
        {
            List<FolderEntity> listEntities = await _folderStorage.GetListFolders(moduleType, search);
            List<FolderDto> listDtos = listEntities.TransferToListDto();
            return listDtos;
        }

        public async Task<FolderDto> Create(FolderDto model)
        {
            FolderEntity transferEntity = model.TransferToEntity();
            FolderEntity entity = await _folderStorage.Create(transferEntity);
            FolderDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<FolderDto> Update(FolderDto model)
        {
            FolderEntity transferEntity = model.TransferToEntity();
            FolderEntity entity = await _folderStorage.Update(transferEntity);
            FolderDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _folderStorage.Delete(id);
            return result;
        }

        #region [Obsolete("Don't use")]

        [Obsolete("Don't use")]
        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new System.NotImplementedException();
        }

        [Obsolete("Don't use")]
        public Task<FolderDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        #endregion  
    }
}
