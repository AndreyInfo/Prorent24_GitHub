using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Enum.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.CustomerCommunication.DocumentTemplate
{
    public interface IDocumentTemplateStorage : IBaseStorage<DocumentTemplateEntity> {

        Task<List<DocumentTemplateEntity>> GetAllByType(DocumentTemplateTypeEnum directoryType);

    }
}
