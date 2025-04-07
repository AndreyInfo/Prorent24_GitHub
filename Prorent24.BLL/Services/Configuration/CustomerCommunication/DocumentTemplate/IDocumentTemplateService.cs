using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Enum.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.CustomerCommunication.DocumentTemplate
{
    public interface IDocumentTemplateService: IBaseService<DocumentTemplateDto, int> // , IBaseService<Do>
    {
        Task<List<DocumentTemplateDto>> GetByTypeAsync(DocumentTemplateTypeEnum type);

        Task<DocumentTemplateBlockDto> CreateBlock(DocumentTemplateBlockDto model);
        Task<DocumentTemplateBlockDto> UpdateBlock(DocumentTemplateBlockDto model);

        Task<bool> DeleteBlock(int id);
    }
}
