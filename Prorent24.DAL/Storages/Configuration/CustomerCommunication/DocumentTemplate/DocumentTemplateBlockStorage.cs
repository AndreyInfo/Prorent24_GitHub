using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.CustomerCommunication.DocumentTemplate
{
    internal class DocumentTemplateBlockStorage : BaseStorage<DocumentTemplateBlockEntity>, IDocumentTemplateBlockStorage
    {
        public DocumentTemplateBlockStorage(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<IPagedList<DocumentTemplateBlockEntity>> GetAll(List<Predicate<DocumentTemplateBlockEntity>> conditions = null)
        {
            throw new NotImplementedException();
        }
    }
}
