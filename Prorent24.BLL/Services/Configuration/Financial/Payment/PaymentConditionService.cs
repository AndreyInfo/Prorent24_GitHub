using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Financial.Payment;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.Payment
{
    internal class PaymentConditionService : BaseService, IPaymentConditionService
    {
        private readonly IPaymentConditionStorage _paymentConditionStorage;
        private readonly ISystemSettingStorage _systemSettingStorage;

        public PaymentConditionService(IPaymentConditionStorage paymentConditionStorage, 
            ISystemSettingStorage systemSettingStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _paymentConditionStorage = paymentConditionStorage;
            _systemSettingStorage = systemSettingStorage;
        }


        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<PaymentConditionEntity> pagedList = await _paymentConditionStorage.GetAll();
            List<PaymentConditionEntity> listEntities = pagedList.Items.ToList();
            List<PaymentConditionDto> listDtos = listEntities.Select(x => x.TransferToConditionDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<PaymentConditionDto>();
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.PaymentConditionEntity
            };
        }
        public async Task<PaymentConditionDto> GetById(int id)
        {
            PaymentConditionEntity paymentConditionEntity = await _paymentConditionStorage.GetById(id);
            PaymentConditionDto paymentConditionDto = paymentConditionEntity?.TransferToConditionDto();
            return paymentConditionDto;
        }

        public async Task<PaymentConditionDto> Create(PaymentConditionDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.PaymentConditions);
            if (permission.Add)
            {
                PaymentConditionEntity entity = model.TransferToConditionEntity();
                PaymentConditionEntity createEntity = await _paymentConditionStorage.Create(entity);
                PaymentConditionDto dto = createEntity.TransferToConditionDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<PaymentConditionDto> Update(PaymentConditionDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.PaymentConditions);
            if (permission.Add)
            {
                PaymentConditionEntity entity = model.TransferToConditionEntity();
                PaymentConditionEntity updateEntity = await _paymentConditionStorage.Update(entity);
                PaymentConditionDto dto = updateEntity.TransferToConditionDto();
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
                bool result = await _paymentConditionStorage.Delete(id);
                return result;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<PaymentConditionDefaultDto> UpdatePaymentConditionByDefault(PaymentConditionDefaultDto model)
        {
            SystemSettingEntity transferEntity = model.TransferToEntity();

            transferEntity.Type = SystemSettingsTypeEnum.DefaultPaymentCondition;
            transferEntity.LastModifiedDate = DateTime.UtcNow;

            SystemSettingEntity entity = await _systemSettingStorage.Update(transferEntity);
            PaymentConditionDefaultDto dto = entity.TransferToDto<PaymentConditionDefaultDto>();
            return dto;
        }

        public async Task<PaymentConditionDefaultDto> GetPaymentConditionByDefault()
        {

            SystemSettingEntity entity = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.DefaultPaymentCondition);

            PaymentConditionDefaultDto dto = new PaymentConditionDefaultDto();
            if (entity != null)
            {
                dto = entity.TransferToDto<PaymentConditionDefaultDto>();
            }
            return dto;
        }

        public async Task<bool> DeleteMultiple(List<int> Values)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.PaymentConditions);
            if (permission.Add)
            {
                bool result = await _paymentConditionStorage.Delete(Values);
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
