using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Transfers.Contact;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.Directory;
using Prorent24.DAL.Storages.Contact;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.Entity.Contact;
using Prorent24.Entity.Directory;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Contact
{
    internal class ContactService : BaseService, IContactService
    {
        private readonly IContactStorage _contactStorage;
        private readonly IDirectoryStorage _directoryStorage;
        private readonly IContactCrewMemberStorage _contactCrewMemberStorage;
        private readonly IContactPersonStorage _contactPersonStorage;

        public ContactService(IContactStorage contactStorage, IDirectoryStorage directoryStorage, IUserRoleStorage userRoleStorage,
            IHttpContextAccessor httpContextAccessor, 
            IContactCrewMemberStorage contactCrewMemberStorage,
            IColumnStorage сolumnStorage, IContactPersonStorage contactPersonStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _contactStorage = contactStorage;
            _directoryStorage = directoryStorage;
            _contactCrewMemberStorage = contactCrewMemberStorage;
            _contactPersonStorage = contactPersonStorage;
        }


        public async Task<ContactDto> Create(ContactDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Contacts, PermissionFieldEnum.DatabaseContract);
            if (permission.Add)
            {
                ContactEntity transferEntity = model.TransferToEntity();
                transferEntity.CrewMembers = model.CrewMembers.TransferToContactVisibilityEntity();
                ContactEntity entity = await _contactStorage.Create(transferEntity);
                ContactDto dto = entity.TransferToDto();
                ContactPersonEntity defaultContactPerson = new ContactPersonEntity()
                {
                    FirstName = transferEntity.FirstName,
                    MiddleName = transferEntity.MiddleName,
                    LastName = transferEntity.LastName,
                    ContactId = entity.Id

                };
                ContactPersonEntity person = await _contactPersonStorage.Create(defaultContactPerson);
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
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Contacts, PermissionFieldEnum.DatabaseContract);
            if (permission.Delete)
            {
                bool result = await _contactStorage.Delete(id);
                return result;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 403);
                throw new Exception("errors.access_denied_contact_administrator", innerException);
            }
        }

        public async Task<ContactDto> GetById(int id)
        {
            ContactEntity entity = await _contactStorage.GetById(id);
            ContactDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            ContactEntity entity = await _contactStorage.GetById(id);
            ContactDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<ContactDto>(EntityEnum.ContactEntity);

            return moduleDetails;
        }


        public async Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Contacts, PermissionFieldEnum.DatabaseContract);
            string searchText = String.Empty;
            IQueryable<ContactEntity> queryableEntity = _contactStorage.QueryableEntity.CreateFilterForContactEntity(filterList, out searchText);
            IPagedList<ContactEntity> pagedList = await _contactStorage.GetAllByFilter(queryableEntity, searchText);
            //IPagedList<ContactEntity> pagedList = await _contactStorage.GetAll();

            List<ContactEntity> listEntities = pagedList.Items.ToList();
            string lang = this.GetCurrentLang();
            List<DirectoryEntity> countries = await _directoryStorage.GetAllByType(DirectoryTypeEnum.Country, lang);

            List<ContactDto> listDtos = listEntities.TransferToListDto(permission, countries);
            BaseGrid grid = listDtos.CreateGrid<ContactDto>(await GetUserColumns(EntityEnum.ContactEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ContactEntity,

                Add = permission.Add,
                Edit = permission.Edit,
                Delete = permission.Delete,
                View = permission.View
            };
        }

        public async Task<List<ContactDto>> SearchContacts(string likeString)
        {
            List<ContactEntity> entities = await _contactStorage.SearchContacts(likeString);
            return entities?.TransferToListDto();
        }

        public async Task<ContactDto> Update(ContactDto model)
        {
            var permission = await this.GetFunctionPermissions(ModuleTypeEnum.Contacts, PermissionFieldEnum.DatabaseContract);

            if (permission.Edit)
            {
                ContactEntity transferEntity = model.TransferToEntity();
                ContactEntity entity = await _contactStorage.Update(transferEntity);
                ContactDto dto = entity.TransferToDto();
                if (model.CrewMembers != null)
                {
                    var isDeleted = await _contactCrewMemberStorage.DeleteAllByContactId(dto.Id);

                    List<ContactVisibilityCrewMemberEntity> visibilityTransferEntities = model.CrewMembers.TransferToContactVisibilityEntity();
                    List<CrewMemberShortDto> crewMembersList = new List<CrewMemberShortDto>();

                    foreach (var _entity in visibilityTransferEntities)
                    {
                        _entity.ContactId = entity.Id;
                        ContactVisibilityCrewMemberEntity visibilityEntity = await _contactCrewMemberStorage.Create(_entity);
                        var transferedDto = visibilityEntity.TransferToDto();
                        crewMembersList.Add(transferedDto);
                    }

                    dto.CrewMembers = crewMembersList;

                }
                ContactEntity result = await _contactStorage.GetById(model.Id);
                return result.TransferToDto();
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
