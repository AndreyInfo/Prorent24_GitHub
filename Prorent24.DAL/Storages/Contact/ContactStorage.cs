using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Contact;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.DAL.Storages.Contact
{
    internal class ContactStorage : BaseStorage<ContactEntity>, IContactStorage
    {
        public ContactStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IPagedList<ContactEntity>> GetAll(List<Predicate<ContactEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync(x => x, include: c => c.Include(x => x.Folder)
            .Include(visiting => visiting.VisitingAddress)
            .Include(defaultContact => defaultContact.DefaultContact)
            .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember), pageSize: 100);
        }

        public async Task<IPagedList<ContactEntity>> GetAllByFilter(IQueryable<ContactEntity> queryableEntity, string searchText)
        {
            var result = await queryableEntity.Include(x => x.Folder)
                .Include(visiting => visiting.VisitingAddress)
                .Include(postal => postal.PostalAddress)
                .Include(defaultContact => defaultContact.DefaultContact)
                .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                .ToListAsync();
            if (searchText?.Length > 0)
            {
                result = result.Where(x =>
                    x.CompanyName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                  || x.FirstName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                  || x.LastName?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
             ).ToList();

            }
            return result.ToPagedList(0, 500);
        }

        public override async Task<ContactEntity> GetById(object id)
        {
            return await _repos.Table
                .Include(x => x.Folder)
                .Include(x => x.Notes)
                .Include(x => x.Tags).ThenInclude(y => y.TagDirectory)
                .Include(x => x.Tasks)
                .Include(x => x.Files)
                .Include(x => x.BillingAddress)
                .Include(x => x.PostalAddress)
                .Include(x => x.VisitingAddress)
                .Include(x => x.DefaultContact)
                .Include(x => x.CrewMembers).ThenInclude(x => x.CrewMember)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }

        public async Task<List<ContactEntity>> SearchContacts(string likeString)
        {
            List<ContactEntity> result;

            if (likeString != null && !likeString.Equals("null"))
            {
                result = _repos.TableNoTracking.Select(x => new ContactEntity
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToList();
            }
            else
            {
                result = (from contact in _repos.TableNoTracking
                          where (
                         contact.CompanyName != null && contact.CompanyName.IndexOf(likeString, StringComparison.OrdinalIgnoreCase) >= 0
                          || contact.FirstName != null && contact.FirstName.IndexOf(likeString, StringComparison.OrdinalIgnoreCase) >= 0
                          || contact.LastName != null && contact.LastName.IndexOf(likeString, StringComparison.OrdinalIgnoreCase) >= 0
                           || contact.MiddleName != null && contact.MiddleName.IndexOf(likeString, StringComparison.OrdinalIgnoreCase) >= 0
                          )
                          select new ContactEntity
                          {
                              Id = contact.Id,
                              CompanyName = contact.CompanyName,
                              FirstName = contact.FirstName,
                              LastName = contact.LastName
                          }).ToList();
            }

            return result;
        }
    }
}
