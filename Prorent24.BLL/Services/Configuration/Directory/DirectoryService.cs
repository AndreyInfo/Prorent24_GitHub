using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Directory;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.Configuration.Directory;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Storages.Configuration.Directory;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Directory
{
    internal class DirectoryService : IDirectoryService
    {
        private readonly IDirectoryStorage _directoryStorage;

        public DirectoryService(IDirectoryStorage directoryStorage)
        {
            _directoryStorage = directoryStorage ?? throw new ArgumentNullException(nameof(directoryStorage));
        }

        public Task<DirectoryDto> Create(DirectoryDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DirectoryDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DirectoryDto>> GetByTypeAsync(DirectoryTypeEnum type, string lang = "en")
        {
            return (await _directoryStorage.GetAllByType(type, lang)).TransferToListDto();
        }

        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public Task<DirectoryDto> Update(DirectoryDto model)
        {
            throw new NotImplementedException();
        }
    }
}
