using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Financial.DiscountGroup;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Configuration.Financial.DiscountGroup
{
    internal class DiscountGroupService : BaseService, IDiscountGroupService
    {
        private readonly IDiscountGroupStorage _discountGroupStorage;
        public DiscountGroupService(IDiscountGroupStorage discountGroupStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _discountGroupStorage = discountGroupStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<DiscountGroupEntity> pagedList = await _discountGroupStorage.GetAll();
            List<DiscountGroupEntity> listEntities = pagedList.Items.ToList();
            List<DiscountGroupDto> listDtos = listEntities.Select(x => x.TransferToDiscountGroupDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<DiscountGroupDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.DiscountGroupEntity
            };
        }

        public async Task<DiscountGroupDto> GetById(int id)
        {
            DiscountGroupEntity discountGroupEntity = await _discountGroupStorage.GetById(id);
            DiscountGroupDto discountGroupDto = discountGroupEntity.TransferToDiscountGroupDto();
            return discountGroupDto;
        }

        public async Task<DiscountGroupDto> Create(DiscountGroupDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DiscountGroups);
            if (permission.Add)
            {
                DiscountGroupEntity entity = model.TransferToDiscountGroupEntity();
                DiscountGroupEntity discountGroupEntity = await _discountGroupStorage.Create(entity);
                DiscountGroupDto discountGroupDto = discountGroupEntity.TransferToDiscountGroupDto();
                return discountGroupDto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<DiscountGroupDto> Update(DiscountGroupDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DiscountGroups);
            if (permission.Add)
            {
                DiscountGroupEntity entity = model.TransferToDiscountGroupEntity();
                DiscountGroupEntity discountGroupEntity = await _discountGroupStorage.Update(entity);
                DiscountGroupDto discountGroupDto = discountGroupEntity.TransferToDiscountGroupDto();
                return discountGroupDto;
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DiscountGroups);
            if (permission.Add)
            {
                bool result = await _discountGroupStorage.Delete(id);
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DiscountGroups);
            if (permission.Add)
            {
                // перевірити чи не використовується деінде
                bool result = await _discountGroupStorage.Delete(Values);
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
