using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Financial.Ledger;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Configuration.Financial.Ledger
{
    internal class LedgerService : BaseService, ILedgerService
    {
        private readonly ILedgerStorage _ledgerStorage;
        public LedgerService(ILedgerStorage ledgerStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _ledgerStorage = ledgerStorage;
        }
        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<LedgerEntity> pagedList = await _ledgerStorage.GetAll();
            List<LedgerEntity> listEntities = pagedList.Items.ToList();
            List<LedgerDto> listDtos = listEntities.Select(x => x.TransferToLedgerDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<LedgerDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.LedgerEntity
            };
        }

        public async Task<LedgerDto> GetById(int id)
        {
            LedgerEntity ledgerEntity = await _ledgerStorage.GetById(id);
            LedgerDto ledgerDto = ledgerEntity.TransferToLedgerDto();
            return ledgerDto;
        }

        public async Task<LedgerDto> Create(LedgerDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.Ledger);
            if (permission.Add)
            {
                LedgerEntity entity = model.TransferToLedgerEntity();
                LedgerEntity ledgerEntity = await _ledgerStorage.Create(entity);
                LedgerDto ledgerDto = ledgerEntity.TransferToLedgerDto();
                return ledgerDto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<LedgerDto> Update(LedgerDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.Ledger);
            if (permission.Add)
            {
                LedgerEntity entity = model.TransferToLedgerEntity();
                LedgerEntity ledgerEntity = await _ledgerStorage.Update(entity);
                LedgerDto ledgerDto = ledgerEntity.TransferToLedgerDto();
                return ledgerDto;
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.Ledger);
            if (permission.Add)
            {
                LedgerEntity ledgerEntity = await _ledgerStorage.GetById(id);
                if (ledgerEntity.IsSystem != true)
                {
                    bool result = await _ledgerStorage.Delete(id);
                    return result;
                }
                else
                {
                    //не можна видаляти системні реєстраційні записи
                    return false;
                }
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.Ledger);
            if (permission.Add)
            {
                foreach (int el in Values)
                {
                    LedgerEntity ledgerEntity = await _ledgerStorage.GetById(el);
                    if (ledgerEntity.IsSystem == true)
                    {
                        //не можна видаляти системні реєстраційні записи
                        return false;
                    }
                }

                bool result = await _ledgerStorage.Delete(Values);
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
