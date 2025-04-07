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
    internal class VatSchemeService : BaseService, IVatSchemeService
    {

        private readonly IVatSchemeStorage _vatSchemeStorage;
        private readonly IVatClassSchemeRateStorage _vatClassSchemeRateStorage;
        public VatSchemeService(IVatSchemeStorage vatSchemeStorage, 
            IVatClassSchemeRateStorage vatClassSchemeRateStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _vatClassSchemeRateStorage = vatClassSchemeRateStorage;
            _vatSchemeStorage = vatSchemeStorage;
        }


        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<VatSchemeEntity> pagedList = await _vatSchemeStorage.GetAll();
            List<VatSchemeEntity> listEntities = pagedList.Items.ToList();
            List<VatSchemeDto> listDtos = listEntities.Select(x => x.TransferToVatSchemeDto()).ToList();
            BaseGrid grid = listDtos.CreateGrid<VatSchemeDto>();
            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.VatSchemeEntity
            };
        }
        public async Task<VatSchemeDto> GetById(int id)
        {
            VatSchemeEntity vatClassEntity = await _vatSchemeStorage.GetById(id);
            VatSchemeDto vatClassDto = vatClassEntity != null ? vatClassEntity.TransferToVatSchemeDto() : new VatSchemeDto();
            return vatClassDto;
        }

        public async Task<VatSchemeDto> Create(VatSchemeDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.VatSchemes);
            if (permission.Add)
            {
                VatSchemeEntity entity = model.TransferToVatSchemeEntity();
                VatSchemeEntity createEntity = await _vatSchemeStorage.Create(entity);
                VatSchemeDto dto = createEntity.TransferToVatSchemeDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<VatSchemeDto> Update(VatSchemeDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.VatSchemes);
            if (permission.Add)
            {
                VatSchemeEntity entity = model.TransferToVatSchemeEntity();
                VatSchemeEntity updateEntity = await _vatSchemeStorage.Update(entity);
                VatSchemeDto dto = updateEntity.TransferToVatSchemeDto();
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
                bool result = await _vatSchemeStorage.Delete(id);

                return result;

                //bool canDelete = true;

                //List<VatClassSchemeRateEntity> vatClassSchemeRates = await _vatClassSchemeRateStorage.GetBySchemeId(id);
                //if (vatClassSchemeRates != null && vatClassSchemeRates.Count > 0)
                //{
                //    List<int> Values = vatClassSchemeRates?.Select(x => x.Id).ToList();
                //    canDelete = await DeleteMultiple(Values);
                //}
                //if (canDelete)
                //{
                //    bool result = await _vatSchemeStorage.Delete(id);
                //    return result;
                //}
                //else
                //{
                //    return false;
                //}

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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.VatSchemes);
            if (permission.Add)
            {
                bool result = await _vatSchemeStorage.Delete(Values);
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
