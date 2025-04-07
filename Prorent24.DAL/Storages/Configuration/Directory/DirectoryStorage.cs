using Prorent24.Entity.Directory;
using Prorent24.Enum.Directory;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Directory
{
    internal class DirectoryStorage : BaseStorage<DirectoryEntity>, IDirectoryStorage
    {
        protected readonly IRepository<DirectoryLocEntity> _reposDirectoryLoc;
        public DirectoryStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<List<DirectoryEntity>> GetAllByType(DirectoryTypeEnum directoryType, string lang = "en")
        {
            //_unitOfWork.GetRepository<DirectoryLocEntity>();

            var result = _repos.TableNoTracking.Where(x => x.Type == directoryType)
                .Select(x => new DirectoryEntity()
                {
                    Id = x.Id,
                    Type = x.Type,
                    IsActive = x.IsActive,
                    Locs = x.Locs.Where(l => l.Lang == lang).Select(l => new DirectoryLocEntity
                    {
                        Name = l.Name,
                        DirectoryId = l.DirectoryId,
                        Lang = l.Lang
                    }).ToList(),
                    Key = x.Key
                }).ToAsyncEnumerable().ToList();

            return result;
        }

        public async Task<IPagedList<DirectoryEntity>> GetAll(List<Predicate<DirectoryEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync(x => x);
        }
    }
}
