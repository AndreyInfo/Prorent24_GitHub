using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Invoice;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Storages.Invoice;
using Prorent24.Entity.Invoice;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prorent24.BLL.Transfers.Invoice;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Invoice.InvoiceLine
{
    internal class InvoiceLineService : BaseService, IInvoiceLineService
    {
        private readonly IInvoiceLineStorage _invoiceLineStorage;

        public InvoiceLineService(IHttpContextAccessor httpContextAccessor, 
            IInvoiceLineStorage invoiceLineStorage, 
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _invoiceLineStorage = invoiceLineStorage;
        }

        public async Task<InvoiceLineDto> Create(InvoiceLineDto model)
        {
            InvoiceLineEntity transferEntity = model.TransferToEntity();

            InvoiceLineEntity entity = await _invoiceLineStorage.Create(transferEntity);
            InvoiceLineDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _invoiceLineStorage.Delete(id);
            return result;
        }

        public async Task<InvoiceLineDto> GetById(int id)
        {
            InvoiceLineEntity entity = await _invoiceLineStorage.GetById(id);
            InvoiceLineDto dto = entity?.TransferToDto();
            return dto;
        }
        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new Exception();
        }

        public async Task<InvoiceLineDto> Update(InvoiceLineDto model)
        {
            InvoiceLineEntity transferEntity = model.TransferToEntity();
            InvoiceLineEntity entity = await _invoiceLineStorage.Update(transferEntity);
            InvoiceLineDto dto = entity.TransferToDto();
            return dto;
        }


    }
}
