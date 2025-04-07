using Microsoft.EntityFrameworkCore;
using Prorent24.Common.Models.Filters;
using Prorent24.Entity.Invoice;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Invoice
{
    internal class InvoiceStorage : BaseStorage<InvoiceEntity>, IInvoiceStorage
    {
        public InvoiceStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<InvoiceEntity>> GetAll(List<Predicate<InvoiceEntity>> conditions = null)
        {
            var result = _repos.GetPagedListAsync(x => x,
                include: i => i.Include(x => x.ContactPerson)
                               .Include(x => x.Client)
                               .Include(x => x.Document)
                               .Include(x => x.CreationUser)
                               .Include(x => x.Owner)
                               .Include(x => x.Tags)
                               .Include(x => x.Tasks)
                               .Include(x => x.Files)
                               .Include(x => x.Notes));
            return result;
        }
        
        public override async Task<InvoiceEntity> GetById(object id)
        {
            // return base.GetById(id);
            var result = _repos.GetFirstOrDefault(x => x,
               predicate: x => x.Id == (int)id,
               include: i => i.Include(x => x.ContactPerson)
                              .Include(x => x.CreationUser)
                              .Include(x => x.Client)
                              .ThenInclude(x => x.VisitingAddress)
                              .Include(x => x.Client)
                              .ThenInclude(x => x.BillingAddress)
                              .Include(x => x.Document)
                              .ThenInclude(x => x.Template)
                              .Include(x => x.Document)
                              .ThenInclude(x => x.Letterhead)
                              .Include(x => x.InvoiceLines)
                              .Include(x => x.PaymentCondition)
                              .Include(x => x.PaymentMethod)
                              .Include(x => x.VatScheme)
                              .Include(x => x.Owner)
                              .Include(x => x.Tags)
                              .ThenInclude(x => x.TagDirectory)
                              .Include(x => x.Tasks)
                              .Include(x => x.Files)
                              .Include(x => x.Notes)
               );
            return result;
        }
        public async Task<InvoiceEntity> GetByDocumentId(int id)
        {
            var result = _repos.GetFirstOrDefault(x => x,
              predicate: x => x.Document.Id == (int)id,
              include: i => i.Include(x => x.ContactPerson)
                             .Include(x => x.CreationUser)
                             .Include(x => x.Client)
                             .ThenInclude(x => x.VisitingAddress)
                             .Include(x => x.Client)
                             .ThenInclude(x => x.BillingAddress)
                             .Include(x => x.Document)
                             .ThenInclude(x => x.Template)
                             .Include(x => x.Document)
                             .ThenInclude(x => x.Letterhead)
                             .Include(x => x.InvoiceLines)
                             .Include(x => x.PaymentCondition)
                             .Include(x => x.PaymentMethod)
                             .Include(x => x.VatScheme)
                             .Include(x => x.Owner)
                             .Include(x => x.Tags)
                             .Include(x => x.Tasks)
                             .Include(x => x.Files)
                             .Include(x => x.Notes)
              );
            return result;
        }

        public async Task<IPagedList<InvoiceEntity>> GetAllByFilter(IQueryable<InvoiceEntity> queryableEntity, string searchText = "")
        {
            var invoices = await queryableEntity.Include(x => x.ContactPerson)
                               .Include(x => x.Client)
                               .Include(x => x.CreationUser)
                               .Include(x => x.Document)
                               .Include(x => x.Client)
                               .ThenInclude(x => x.VisitingAddress)
                               //.ThenInclude(x=>x.Letterhead)
                               .Include(x => x.Owner)
                               .Include(x => x.Tags).ThenInclude(y => y.TagDirectory)
                               .Include(x => x.Tasks)
                               .Include(x => x.Files)
                               .Include(x => x.Notes)
                               .Include(x => x.InvoiceLines).ToListAsync();
            if (searchText.Length > 0)
            {
                invoices = invoices.Where(x =>
                 x.Client?.FirstName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.Client?.LastName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                || x.Client?.CompanyName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
               ).ToList();
            }
            return invoices.ToPagedList(0, 500);
        }
    }
}
