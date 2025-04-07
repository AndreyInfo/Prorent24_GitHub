using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Financial.InvoiceMoment;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Configuration.Financial.InvoiceMoment
{
    internal class InvoiceMomentService : BaseService, IInvoiceMomentService
    {
        private readonly IInvoiceMomentStorage _invoiceMomentStorage;
        public InvoiceMomentService(IInvoiceMomentStorage invoiceMomentStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _invoiceMomentStorage = invoiceMomentStorage;
        }
        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<InvoiceMomentEntity> pagedList = await _invoiceMomentStorage.GetAll();
            List<InvoiceMomentEntity> listEntities = pagedList.Items.ToList();
            List<InvoiceMomentDto> listDtos = listEntities.Select(x => x.TransferToInvoiceMomentDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<InvoiceMomentDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.InvoiceMomentEntity
            };
        }

        public async Task<InvoiceMomentDto> GetById(int id)
        {
            InvoiceMomentEntity invoiceMomentEntity = await _invoiceMomentStorage.GetById(id);
            InvoiceMomentDto invoiceMomentDto = invoiceMomentEntity.TransferToInvoiceMomentDto();
            return invoiceMomentDto;
        }

        public async Task<InvoiceMomentDto> Create(InvoiceMomentDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.InvoiceMoments);
            if (permission.Add)
            {
                InvoiceMomentEntity entity = model.TransferToInvoiceMomentEntity();
                InvoiceMomentEntity invoiceMomentEntity = await _invoiceMomentStorage.Create(entity);
                InvoiceMomentDto invoiceMomentDto = invoiceMomentEntity.TransferToInvoiceMomentDto();
                return invoiceMomentDto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<InvoiceMomentDto> Update(InvoiceMomentDto model)
        {

            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.InvoiceMoments);
            if (permission.Add)
            {
                InvoiceMomentEntity entity = model.TransferToInvoiceMomentEntity();
                InvoiceMomentEntity invoiceMomentEntity = await _invoiceMomentStorage.Update(entity);
                InvoiceMomentDto invoiceMomentDto = invoiceMomentEntity.TransferToInvoiceMomentDto();
                return invoiceMomentDto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.InvoiceMoments);
            if (permission.Add)
            {
                // TODO: check inUse
                bool result = await _invoiceMomentStorage.Delete(id);
                return result;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<bool> DeleteMultiple(List<int> Values)
        {
            bool result = await _invoiceMomentStorage.Delete(Values);
            return result;
        }
    }
}
