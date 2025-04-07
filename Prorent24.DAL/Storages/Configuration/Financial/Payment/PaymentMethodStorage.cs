using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Configuration.Financial.Payment
{
    internal class PaymentMethodStorage : BaseStorage<PaymentMethodEntity>, IPaymentMethodStorage
    {
        public PaymentMethodStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
        public Task<IPagedList<PaymentMethodEntity>> GetAll(List<Predicate<PaymentMethodEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync(x => new PaymentMethodEntity()
            {
                Id = x.Id,
                Name = x.Name,
                AccountingCode = x.AccountingCode
              
            });
        }
    }
}
