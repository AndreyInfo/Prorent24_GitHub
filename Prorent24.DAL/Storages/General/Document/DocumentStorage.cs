using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Document
{
    internal class DocumentStorage : BaseStorage<DocumentEntity>, IDocumentStorage
    {
        public DocumentStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IPagedList<DocumentEntity>> GetAll(List<Predicate<DocumentEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DocumentEntity>> GetAllByForeignId(EntityEnum dependency, int id)
        {
            var reposQuery = _repos.TableNoTracking
                .Include(x => x.CreationUser)
                .Include(x => x.Invoice)
                .ThenInclude(x => x.InvoiceLines)
                .Include(x => x.Project)
                .ThenInclude(x => x.ClientContact)
                .Include(x => x.Project)
                .ThenInclude(x => x.Times)
                .AsQueryable();
            switch (dependency)
            {
                case EntityEnum.ProjectEntity:
                    reposQuery = reposQuery.Where(x => x.ProjectId == id);
                    break;
                case EntityEnum.InvoiceEntity:
                    reposQuery = reposQuery.Where(x => x.InvoiceId == id);
                    break;
                default:
                    break;
            }
            var documentList = reposQuery.ToList();
            return documentList;
        }

        public override async Task<DocumentEntity> GetById(object id)
        {
            var reposQuery = _repos.TableNoTracking
                .Include(x => x.CreationUser)
                .Include(x => x.Invoice)
                .ThenInclude(x => x.ContactPerson)
                .Include(x => x.Invoice)
                .ThenInclude(x => x.Client)
                .Include(x => x.Invoice)
                .ThenInclude(x => x.InvoiceLines)
                .Include(x => x.Project)
                .ThenInclude(x => x.ClientContact)
                .Include(x => x.Letterhead)
                .Include(x => x.Template)
                .AsQueryable();
            //switch (dependency)
            //{
            //    case EntityEnum.ProjectEntity:
            //        reposQuery = reposQuery.Where(x => x.ProjectId == id);
            //        break;
            //    case EntityEnum.InvoiceEntity:
            //        reposQuery = reposQuery.Where(x => x.ProjectId == id);
            //        break;
            //    default:
            //        break;
            //}
            var document = await reposQuery.Where(x => x.Id == (int)id).FirstOrDefaultAsync();
            return document;
        }
    }
}
