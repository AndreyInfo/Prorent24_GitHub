using System.Collections.Generic;
using System.Threading.Tasks;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Invoice;
namespace Prorent24.BLL.Services.Invoice
{
    public interface IInvoiceService : IBaseService<InvoiceDto, int>
    {
        Task<List<ModuleDetailDto>> GetDetails(int id);
        Task GenerateInvoice(int id);
        Task<byte[]> GetInvoiceDocument(int id);
        Task<InvoiceTotalDto> CalculateTotal(InvoiceDto dto);
    }
}
