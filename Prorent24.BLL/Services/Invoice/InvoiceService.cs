using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Services.General.Document;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.BLL.Transfers.Invoice;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Financial.Vat;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Invoice;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.Invoice;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prorent24.BLL.Builders;

namespace Prorent24.BLL.Services.Invoice
{
    internal class InvoiceService : BaseService, IInvoiceService
    {
        private readonly IInvoiceStorage _invoiceStorage;
        private readonly IDocumentService _documentService;
        private readonly IVatSchemeStorage _vatSchemeStorage;

        public InvoiceService(IHttpContextAccessor httpContextAccessor,
            IInvoiceStorage invoiceStorage,
            IVatSchemeStorage vatSchemeStorage,
            IDocumentService documentService,
            IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _invoiceStorage = invoiceStorage;
            _documentService = documentService;
            _vatSchemeStorage = vatSchemeStorage;
        }

        public async Task<InvoiceDto> Create(InvoiceDto model)
        {
            InvoiceEntity transferEntity = model.TransferToEntity("create");
            //.GeneratedOn = DateTime.Now;
            InvoiceEntity entity = await _invoiceStorage.Create(transferEntity);
            InvoiceEntity result = await _invoiceStorage.GetById(entity.Id);
            InvoiceDto dto = result.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _invoiceStorage.Delete(id);
            return result;
        }

        public async Task<InvoiceDto> GetById(int id)
        {
            InvoiceEntity entity = await _invoiceStorage.GetById(id);
            InvoiceDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            InvoiceEntity entity = await _invoiceStorage.GetById(id);
            InvoiceDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<InvoiceDto>(EntityEnum.InvoiceEntity);

            return moduleDetails;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            string searchText;
            IQueryable<InvoiceEntity> queryableEntity = _invoiceStorage.QueryableEntity.CreateFilterForInvoiceEntity(filterList, out searchText);
            IPagedList<InvoiceEntity> pagedList = await _invoiceStorage.GetAllByFilter(queryableEntity, searchText);
            List<InvoiceEntity> listEntities = pagedList.Items.ToList();
            List<InvoiceDto> listDtos = listEntities.OrderByDescending(x => x.CreationDate).Where(x => x.Client != null).ToList().TransferToListDto("Invoice");
            BaseGrid grid = listDtos.CreateGrid<InvoiceDto>(await GetUserColumns(EntityEnum.InvoiceEntity));

            return new BaseListDto()
            {

                Grid = grid,
                Entity = EntityEnum.InvoiceEntity
            };
        }

        public async Task<InvoiceDto> Update(InvoiceDto model)
        {
            InvoiceEntity dbEntity = await _invoiceStorage.GetById(model.Id);
            InvoiceEntity transferEntity = model.TransferToEntity();

            dbEntity = dbEntity.TransferToEntityForUpdate(transferEntity);
            //dbEntity.GeneratedOn = DateTime.Now;
            InvoiceEntity entity = await _invoiceStorage.Update(dbEntity);

            InvoiceEntity result = await _invoiceStorage.GetById(entity.Id);
            InvoiceDto dto = result.TransferToDto();

            return dto;
        }

        public async Task GenerateInvoice(int id)
        {
            InvoiceEntity dbEntity = await _invoiceStorage.GetByDocumentId(id);
            InvoiceDto dto = dbEntity.TransferToDto();
            //dto.GenerateDocument();
            await _documentService.GenerateDocument(dto);
            // запам'ятати шлях до файлу - і таке інше
        }

        public async Task<byte[]> GetInvoiceDocument(int id)
        {
            InvoiceEntity dbEntity = await _invoiceStorage.GetById(id);
            InvoiceDto dto = dbEntity?.TransferToDto();
            if (dto != null)
            {
                byte[] document =  await _documentService.GenerateDocument(dto);
                return document;
            }
            else
            {
                return new byte[0];

            }
        }

        public async Task<InvoiceTotalDto> CalculateTotal(InvoiceDto dto)
        {
            InvoiceTotalDto result = new InvoiceTotalDto();
            VatSchemeEntity vatSchemaEntity = await _vatSchemeStorage.GetById(dto.VatSchemeId);
            VatSchemeDto vatSchemaDto = vatSchemaEntity?.TransferToVatSchemeDto();
            if (vatSchemaDto != null)
            {
                switch (vatSchemaDto.Type)
                {
                    case Enum.Configuration.VatSchemeTypeEnum.FixedRate:
                    case Enum.Configuration.VatSchemeTypeEnum.VatReverseCharge:
                        decimal? rate = vatSchemaDto.VatClassSchemeRates?.FirstOrDefault().Rate;
                        var VATClassId = vatSchemaDto.VatClassSchemeRates?.FirstOrDefault().VatClassId;
                        string VATClassName = vatSchemaDto.VatClassSchemeRates?.FirstOrDefault().VatClass?.Name;
                        var TotalPrice = dto.InvoiceLines.Sum(x => x.Price);
                        result.InvoiceVATs.Add(new InvoiceVATDto()
                        {
                            Price = TotalPrice,
                            Rate = rate,
                            VatClassId = VATClassId,
                            VATName = VATClassName
                        });
                        break;
                    case Enum.Configuration.VatSchemeTypeEnum.Rates:
                        result.InvoiceVATs = dto.InvoiceLines.GroupBy(x => x.VatClassId).Select(x => new InvoiceVATDto()
                        {
                            VatClassId = x.Key,
                            Price = x.Sum(y => y.Price),
                            Rate = vatSchemaDto.VatClassSchemeRates.Where(y => y.VatClassId == x.Key).FirstOrDefault().Rate,
                            VATName = vatSchemaDto.VatClassSchemeRates.Where(y => y.VatClassId == x.Key).FirstOrDefault().VatClass?.Name
                        }).Where(x => x.Price != 0).ToList();
                        break;
                }
                result.VAT = result.InvoiceVATs.Sum(x => x.VAT);
                result.TotalNewPrice = result.InvoiceVATs.Sum(x => x.Price);
                result.PriceExcludeVAT = result.InvoiceVATs.Sum(x => x.Price);
                result.PriceIncludeVAT = result.InvoiceVATs.Sum(x => x.Price + x.VAT);
            }
            return result;
        }
    }
}
