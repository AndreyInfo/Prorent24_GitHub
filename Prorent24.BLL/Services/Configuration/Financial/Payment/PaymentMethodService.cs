using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.DAL.Storages.Configuration.Financial.Payment;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.BLL.Transfers.Configuration.Financial;
using System.Linq;
using Prorent24.Common.Models.Grids;
using Prorent24.UnitOfWork.PagedList;
using Prorent24.Common.Extentions;
using Prorent24.BLL.Models;
using Prorent24.Enum.Entity;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Microsoft.AspNetCore.Http;
using Prorent24.Enum.General;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Configuration.Financial.Payment
{
    internal class PaymentMethodService : BaseService, IPaymentMethodService
    {
        private readonly IPaymentMethodStorage _paymentMethodStorage;



        public PaymentMethodService(IPaymentMethodStorage paymentMethodStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _paymentMethodStorage = paymentMethodStorage;

        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<PaymentMethodEntity> pagedList = await _paymentMethodStorage.GetAll();
            List<PaymentMethodEntity> listEntities = pagedList.Items.ToList();
            List<PaymentMethodDto> listDtos = listEntities.Select(x => x.TransferToMethodDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<PaymentMethodDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.PaymentMethodEntity
            };
        }
        public async Task<PaymentMethodDto> GetById(int id)
        {
            PaymentMethodEntity paymentMethodEntity = await _paymentMethodStorage.GetById(id);
            PaymentMethodDto paymentMethodDto = paymentMethodEntity.TransferToMethodDto();
            return paymentMethodDto;
        }

        public async Task<PaymentMethodDto> Create(PaymentMethodDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.PaymentConditions);
            if (permission.Add)
            {
                PaymentMethodEntity entity = model.TransferToMethodEntity();
                PaymentMethodEntity createEntity = await _paymentMethodStorage.Create(entity);
                PaymentMethodDto dto = createEntity.TransferToMethodDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }


        public async Task<PaymentMethodDto> Update(PaymentMethodDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.PaymentConditions);
            if (permission.Add)
            {
                PaymentMethodEntity entity = model.TransferToMethodEntity();
                PaymentMethodEntity updateEntity = await _paymentMethodStorage.Update(entity);
                PaymentMethodDto dto = updateEntity.TransferToMethodDto();
                return dto;
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.PaymentConditions);
            if (permission.Add)
            {
                bool result = await _paymentMethodStorage.Delete(id);
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.PaymentConditions);
            if (permission.Add)
            {
                bool result = await _paymentMethodStorage.Delete(Values);
                return result;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }
    }
}
