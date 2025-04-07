using Prorent24.BLL.Models.General.Document;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Document
{
    public interface IDocumentService: IBaseService<DocumentDto, int>
    {
        Task<byte[]> GenerateDocument(InvoiceDto invoiceDto);
        Task<byte[]> GetDocumentFileById(int id);
        Task<List<ProjectDocumentGroupDto>> GetDocumentsByProjectId(int projectId);
        Task<DocumentDto> CreateDocument(DocumentTemplateTypeEnum documentType, int? projectId);
    }
}
