using Prorent24.Entity.General;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Address
{
    internal class AddressStorage : BaseStorage<AddressEntity>, IAddressStorage
    {
        public AddressStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<AddressEntity>> GetAll(List<Predicate<AddressEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }
    }
}
