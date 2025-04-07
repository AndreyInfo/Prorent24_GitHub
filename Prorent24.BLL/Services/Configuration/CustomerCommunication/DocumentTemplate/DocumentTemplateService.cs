using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.CustomerCommunication.DocumentTemplate;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prorent24.BLL.Transfers.Configuration.Settings;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.Common.Models.Filters;
using Prorent24.Enum.General;
using System;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Microsoft.AspNetCore.Http;
using Prorent24.DAL.Storages.General.Column;

namespace Prorent24.BLL.Services.Configuration.CustomerCommunication.DocumentTemplate
{
    public class DocumentTemplateService : BaseService, IDocumentTemplateService
    {
        private readonly IDocumentTemplateStorage _documentTemplateStorage;
        private readonly IDocumentTemplateBlockStorage _documentTemplateBlockStorage;
        public DocumentTemplateService(IDocumentTemplateStorage documentTemplateStorage, 
            IDocumentTemplateBlockStorage documentTemplateBlockStorage, 
            IUserRoleStorage userRoleStorage, 
            IHttpContextAccessor httpContextAccessor,
            IColumnStorage сolumnStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            this._documentTemplateStorage = documentTemplateStorage;
            this._documentTemplateBlockStorage = documentTemplateBlockStorage;
        }
        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            IPagedList<DocumentTemplateEntity> pagedList = await _documentTemplateStorage.GetAll();
            List<DocumentTemplateEntity> listEntities = pagedList.Items.ToList();
            List<DocumentTemplateDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<DocumentTemplateDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.DocumentTemplateEntity
            };
        }

        public async Task<DocumentTemplateDto> GetById(int id)
        {
            DocumentTemplateEntity entity = await _documentTemplateStorage.GetById(id);
            DocumentTemplateDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<DocumentTemplateDto> Create(DocumentTemplateDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DocumentTemplate);
            if (permission.Add)
            {
                DocumentTemplateEntity transferEntity = model.TransferToEntity();
                DocumentTemplateEntity entity = await _documentTemplateStorage.Create(transferEntity);
                DocumentTemplateDto dto = entity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<DocumentTemplateDto> Update(DocumentTemplateDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DocumentTemplate);
            if (permission.Add)
            {
                DocumentTemplateEntity transferEntity = model.TransferToEntity();
                DocumentTemplateEntity entity = await _documentTemplateStorage.Update(transferEntity);
                DocumentTemplateDto dto = entity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<DocumentTemplateBlockDto> CreateBlock(DocumentTemplateBlockDto model) // int documentTemplateId,
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DocumentTemplate);
            if (permission.Add)
            {
                DocumentTemplateBlockEntity transferEntity = model.TransferToEntity();
                DocumentTemplateBlockEntity entity = await _documentTemplateBlockStorage.Create(transferEntity);
                DocumentTemplateBlockDto dto = entity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<DocumentTemplateBlockDto> UpdateBlock(DocumentTemplateBlockDto model) // int documentTemplateId,
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DocumentTemplate);
            if (permission.Add)
            {
                DocumentTemplateBlockEntity transferEntity = model.TransferToEntity();
                DocumentTemplateBlockEntity entity = await _documentTemplateBlockStorage.Update(transferEntity);
                DocumentTemplateBlockDto dto = entity.TransferToDto();
                return dto;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<bool> DeleteBlock(int id)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DocumentTemplate);
            if (permission.Add)
            {
                bool result = await _documentTemplateBlockStorage.Delete(id);
                return result;
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Configuration, PermissionFieldEnum.DocumentTemplate);
            if (permission.Add)
            {
                bool result = await _documentTemplateStorage.Delete(id);
                return result;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<List<DocumentTemplateDto>> GetByTypeAsync(DocumentTemplateTypeEnum type)
        {
            return (await _documentTemplateStorage.GetAllByType(type)).TransferToListDto();
        }
    }
}
