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
    internal class FileStorage : BaseStorage<FileEntity>, IFileStorage
    {
        public FileStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }


        #region [Obsolete("Don't use")]

        [Obsolete("Don't use")]
        public Task<IPagedList<FileEntity>> GetAll(List<Predicate<FileEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
