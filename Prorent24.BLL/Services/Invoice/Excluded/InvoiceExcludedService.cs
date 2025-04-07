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

namespace Prorent24.BLL.Services.Invoice.Excluded
{
    internal class InvoiceExcludedService: BaseService, IInvoiceExcludedService
    {
        private readonly IInvoiceExcludedStorage _invoiceExcludedStorage;

        public InvoiceExcludedService(IHttpContextAccessor httpContextAccessor,
            IInvoiceExcludedStorage invoiceExcludedStorage,
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _invoiceExcludedStorage = invoiceExcludedStorage;
        }

        public async Task<InvoiceExcludedDto> Create(InvoiceExcludedDto model)
        {
            InvoiceExcludedEntity transferEntity = model.TransferToEntity();

            InvoiceExcludedEntity entity = await _invoiceExcludedStorage.Create(transferEntity);
            InvoiceExcludedDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _invoiceExcludedStorage.Delete(id);
            return result;
        }

        public async Task<InvoiceExcludedDto> GetById(int id)
        {
            InvoiceExcludedEntity entity = await _invoiceExcludedStorage.GetById(id);
            InvoiceExcludedDto dto = entity?.TransferToDto();
            return dto;
        }
        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new Exception();
        }

        public async Task<InvoiceExcludedDto> Update(InvoiceExcludedDto model)
        {
            InvoiceExcludedEntity transferEntity = model.TransferToEntity();
            InvoiceExcludedEntity entity = await _invoiceExcludedStorage.Update(transferEntity);
            InvoiceExcludedDto dto = entity.TransferToDto();
            return dto;
        }
    }
}
