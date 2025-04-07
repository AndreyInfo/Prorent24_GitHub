using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.Financial.Payment
{
    internal class PaymentConditionStorage : BaseStorage<PaymentConditionEntity>, IPaymentConditionStorage
    {
        public PaymentConditionStorage(IUnitOfWork unitOfWork) :base(unitOfWork)
        {

        }
        
        public Task<IPagedList<PaymentConditionEntity>> GetAll(List<Predicate<PaymentConditionEntity>> conditions = null)
        {
            return _repos.GetPagedListAsync(x => new PaymentConditionEntity() { Id = x.Id,
                Name = x.Name,
                TextOnInvoice = x.TextOnInvoice,
                AccountingCode = x.AccountingCode,
                TermInDays = x.TermInDays,
                PaymentMethodId = x.PaymentMethodId,
                PaymentMethod = x.PaymentMethod
            });
        }
    }
}
