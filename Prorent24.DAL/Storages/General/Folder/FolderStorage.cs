using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Folder
{
    internal class FolderStorage : BaseStorage<FolderEntity>, IFolderStorage
    {
        public FolderStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<FolderEntity>> GetListFolders(ModuleTypeEnum moduleType, string search = null)
        {
            if (search?.Length > 0)
           {
                return await _repos.Table.Include(level1 => level1.Childs)
                                         .ThenInclude(level2 => level2.Childs)
                                         .ThenInclude(level3 => level3.Childs)
                                         .ThenInclude(level4 => level4.Childs)
                                         .ThenInclude(level5 => level5.Childs)
                                         .Where(x => x.ModuleType == moduleType && !x.ParentId.HasValue
                                          &&

                                          //LEVEL 0
                                          (x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||

                                              //LEVEL 1
                                              x.Childs.Any(level1 => level1.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||

                                                  //LEVEL 2
                                                  level1.Childs.Any(level2 => level2.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||

                                                      //LEVEL 3
                                                      level2.Childs.Any(level3 => level3.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||

                                                          //LEVEL 4
                                                          level3.Childs.Any(level4 => level4.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||

                                                              //LEVEL 5
                                                              level4.Childs.Any(level5 => level5.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                                                              )  //LEVEL 5 END
                                                           )//LEVEL 4 END
                                                       )//LEVEL 3 END
                                                    )//LEVEL 2 END
                                                 )//LEVEL 1 END
                                          )).ToListAsync();
            }
            else
            {
                return await _repos.Table.Include(level1 => level1.Childs)
                                          .ThenInclude(level2 => level2.Childs)
                                          .ThenInclude(level3 => level3.Childs)
                                          .ThenInclude(level4 => level4.Childs)
                                          .ThenInclude(level5 => level5.Childs)
                                          .Where(x => x.ModuleType == moduleType && !x.ParentId.HasValue).ToListAsync();
            }
        }

        #region [Obsolete("Don't use")]

        [Obsolete("Don't use")]
        public Task<IPagedList<FolderEntity>> GetAll(List<Predicate<FolderEntity>> conditions = null)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
