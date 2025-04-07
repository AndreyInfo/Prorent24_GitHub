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
using Prorent24.DAL.Storages.Configuration.Financial.Vat;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Configuration.Financial.Vat
{
    internal class VatClassService : BaseService, IVatClassService
    {
        private readonly IVatClassStorage _vatClassStorage;

        public VatClassService(IVatClassStorage vatClassStorage,
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _vatClassStorage = vatClassStorage;
        }

        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<VatClassEntity> pagedList = await _vatClassStorage.GetAll();
            List<VatClassEntity> listEntities = pagedList.Items.ToList();
            List<VatClassDto> listDtos = listEntities.Select(x => x.TransferToVatClassDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<VatClassDto>();
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.VatClassEntity
            };

        }

        public async Task<List<VatClassDto>> GetList()
        {
            IPagedList<VatClassEntity> pagedList = await _vatClassStorage.GetAll();
            List<VatClassEntity> listEntities = pagedList.Items.ToList();
            List<VatClassDto> listDtos = listEntities.Select(x => x.TransferToVatClassDto()).ToList();
            return listDtos;

        }

        public async Task<VatClassDto> GetById(int id)
        {
            VatClassEntity vatClassEntity = await _vatClassStorage.GetById(id);
            VatClassDto vatClassDto = vatClassEntity.TransferToVatClassDto();
            return vatClassDto;
        }

        public async Task<VatClassDto> Create(VatClassDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.VatSchemes);
            if (permission.Add)
            {
                VatClassEntity entity = model.TransferToVatClassEntity();
                VatClassEntity createEntity = await _vatClassStorage.Create(entity);
                VatClassDto dto = createEntity.TransferToVatClassDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<VatClassDto> Update(VatClassDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.VatSchemes);
            if (permission.Add)
            {
                VatClassEntity entity = model.TransferToVatClassEntity();
                VatClassEntity updateEntity = await _vatClassStorage.Update(entity);
                VatClassDto dto = updateEntity.TransferToVatClassDto();
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.VatSchemes);
            if (permission.Add)
            {
                // перевірити залежності!
                bool result = await _vatClassStorage.Delete(id);
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
            bool result = await _vatClassStorage.Delete(Values);
            return result;
        }

    }
}
