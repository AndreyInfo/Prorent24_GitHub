using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Transfers.Contact;
using Prorent24.BLL.Transfers.ContactPerson;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Contact;
using Prorent24.Entity.Contact;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Contact
{
    internal class ContactPersonService : IContactPersonService
    {
        private readonly IContactPersonStorage _contactPersonStorage;
        public ContactPersonService(IContactPersonStorage contactPersonStorage)
        {
            _contactPersonStorage = contactPersonStorage;
        }
        public async Task<ContactPersonDto> Create(ContactPersonDto model)
        {
            ContactPersonEntity transferEntity = model.TransferToEntity();
            ContactPersonEntity entity = await _contactPersonStorage.Create(transferEntity);
            ContactPersonDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int contactId, int id)
        {
            IPagedList<ContactPersonEntity> pagedList = await _contactPersonStorage.GetPagedList(contactId);
            int countOfPersons = pagedList.Items.Count();
            if (countOfPersons > 1)
            {
                bool result = await _contactPersonStorage.Delete(id);
                return result;
            }
            else
            {
                var innerException = new Exception();
                innerException.Data.Add("StatusCode", 428);
                throw new Exception("errors.you_must_have_at_least_one_company_contact", innerException);
            }
        }

        public  async Task<bool> DeleteMultiple(List<int> values)
        {
            bool result = await _contactPersonStorage.Delete(values);
            return result;
        }

        public async Task<ContactPersonDto> GetById(int id)
        {
            ContactPersonEntity entity = await _contactPersonStorage.GetById(id);
            ContactPersonDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<BaseListDto> GetPagedList(int contactId)
        {
            IPagedList<ContactPersonEntity> pagedList = await _contactPersonStorage.GetPagedList(contactId);
            List<ContactPersonEntity> listEntities = pagedList.Items.ToList();
            List<ContactPersonDto> listDtos = listEntities.TransferToListDto();
            BaseGrid grid = listDtos.CreateGrid<ContactPersonDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ContactPersonEntity
            };
        }

        public async Task<List<ContactPersonDto>> SearchContactPersons(string likeString)
        {
            List<ContactPersonEntity> entities = await _contactPersonStorage.SearchContactPersons(likeString);
            return entities.TransferToListDto();
        }

        public async Task<ContactPersonDto> Update(ContactPersonDto model)
        {
            ContactPersonEntity transferEntity = model.TransferToEntity();
            ContactPersonEntity entity = await _contactPersonStorage.Update(transferEntity);
            ContactPersonDto dto = entity.TransferToDto();
            return dto;
        }

    }
}
