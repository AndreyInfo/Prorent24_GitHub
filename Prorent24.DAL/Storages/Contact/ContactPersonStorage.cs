using Prorent24.Entity.Contact;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Prorent24.DAL.Storages.Contact
{
    internal class ContactPersonStorage : BaseStorage<ContactPersonEntity>, IContactPersonStorage
    {
        protected readonly IRepository<ContactEntity> _reposContact;
        public ContactPersonStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _reposContact = _unitOfWork.GetRepository<ContactEntity>();
        }
        public async Task<IPagedList<ContactPersonEntity>> GetAll(List<Predicate<ContactPersonEntity>> conditions = null)
        {
            return await _repos.GetPagedListAsync();
        }

        public async Task<IPagedList<ContactPersonEntity>> GetPagedList(int contactId)
        {
            return await _repos.GetPagedListAsync(x => x, predicate: c => c.ContactId == contactId, pageSize: 100);

        }

        public async Task<List<ContactPersonEntity>> SearchContactPersons(string likeString)
        {
            IEnumerable<ContactPersonEntity> result;

            if (likeString.Equals("null"))
            {
                result = _repos.TableNoTracking.Include(x => x.Contact)
                    .Select(x => new ContactPersonEntity
                    {
                        Id = x.Id,
                        ContactId = x.ContactId,
                        FirstName = x.Contact!=null && x.Contact.CompanyShortName.Length > 0 ? $"({x.Contact.CompanyShortName}) {x.FirstName}" : x.FirstName,
                        LastName = x.LastName,
                        MiddleName = x.MiddleName


                    }).ToList().Distinct();
            }
            else
            {
                result = (from person in _repos.TableNoTracking.Include(x => x.Contact)
                          where
                          (person.FirstName != null && person.FirstName.IndexOf(likeString, StringComparison.OrdinalIgnoreCase) >= 0
                          || person.LastName != null && person.LastName.IndexOf(likeString, StringComparison.OrdinalIgnoreCase) >= 0
                          || person.MiddleName != null && person.MiddleName.IndexOf(likeString, StringComparison.OrdinalIgnoreCase) >= 0
                          ||
                          person.Contact.CompanyName != null && person.Contact.CompanyName.IndexOf(likeString, StringComparison.OrdinalIgnoreCase) >= 0)
                          select new ContactPersonEntity
                          {
                              Id = person.Id,
                              ContactId = person.ContactId,
                              FirstName = person.Contact != null && person.Contact.CompanyShortName.Length > 0 ? $"({person.Contact.CompanyShortName}) {person.FirstName}" : person.FirstName,
                              LastName = person.LastName,
                              MiddleName = person.MiddleName


                          }).ToList().Distinct();
            }

            return result.ToList();
        }
    }
}
