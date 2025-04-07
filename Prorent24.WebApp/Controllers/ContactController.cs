using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Services.Contact;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Contact;
using Prorent24.WebApp.Transfers.ContactPerson;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Modules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IContactPersonService _contactPersonService;
        private readonly IContactPaymentService _contactPaymentService;
        private readonly IContactElectronicInvoiceService _contactElectronicInvoiceService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="contactService"></param>
        /// <param name="contactPersonService"></param>
        /// <param name="contactPaymentService"></param>
        /// <param name="contactElectronicInvoiceService"></param>
        public ContactController(IContactService contactService, IContactPersonService contactPersonService,
            IContactPaymentService contactPaymentService, IContactElectronicInvoiceService contactElectronicInvoiceService)
        {
            _contactService = contactService;
            _contactPersonService = contactPersonService;
            _contactPaymentService = contactPaymentService;
            _contactElectronicInvoiceService = contactElectronicInvoiceService;
        }

        #region Contact
        /// <summary>
        /// Get list contacts
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetContacts([FromQuery]string filters)
        {
            List<SelectedFilterViewModel> resultFilter = !string.IsNullOrWhiteSpace(filters) ?
                  JsonConvert.DeserializeObject<List<SelectedFilterViewModel>>(filters) : new List<SelectedFilterViewModel>();

            BaseListDto dto = await _contactService.GetPagedList(resultFilter.TransferToDtoModel());
            
            //object valId = "8";
            //foreach (var item in dto.Grid.Data)
            //{
            //    if (item.TryGetValue("Id", out valId)) // ContainsKey("city"))
            //    {
            //        item.Remove("country");
            //        item.Add("country", "Россссс");
                    
            //}
            //}

            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            ContactDto dto = await _contactService.GetById(id);
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get Contact Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetContactDetails(int id)
        {
            List<ModuleDetailDto> dtos = await _contactService.GetDetails(id);
            return Ok(dtos?.TransferToModuleDetailViewModel());
        }

        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactViewModel model)
        {
            ContactDto dto = await _contactService.Create(model.TransferToDto());
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactViewModel model)
        {
            try
            {
                ContactDto dto = await _contactService.Update(model.TransferToDto());
                return Ok(dto.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            bool result = await _contactService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Get list contacts search like string
        /// </summary>
        /// <returns></returns>
        [HttpGet("search/{likeString}")]
        public async Task<IActionResult> SearchContacts(string likeString)
        {
            List<ContactDto> dtos = await _contactService.SearchContacts(likeString);
            return Ok(dtos?.TransferToListShortViewModel());
        }
        #endregion

        #region ContactPerson
        /// <summary>
        /// Create ContactPerson
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{contactId}/contact_persons")]
        public async Task<IActionResult> CreateContactPerson([FromRoute]int contactId, ContactPersonViewModel model)
        {
            try
            {
                ContactPersonDto dto = model.TransferToDto();
                dto.ContactId = contactId;

                var result = await _contactPersonService.Create(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Update ContactPerson
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{contactId}/contact_persons/{id}")]
        public async Task<IActionResult> UpdateContactPerson([FromRoute]int contactId, [FromRoute]int id, ContactPersonViewModel model)
        {
            try
            {
                ContactPersonDto dto = model.TransferToDto();
                dto.Id = id;
                dto.ContactId = contactId;
                ContactPersonDto result = await _contactPersonService.Update(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Delete ContactPerson
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{contactId}/contact_persons/{id}/delete")]
        public async Task<IActionResult> DeleteContactPerson([FromRoute]int contactId, int id)
        {
            bool result = await _contactPersonService.Delete(contactId, id);
            return Ok(result);
        }

        /// <summary>
        /// Get ContactPerson By Id
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{contactId}/contact_persons/{id}")]
        public async Task<IActionResult> GetContactPerson([FromRoute]int contactId, [FromRoute]int id)
        {
            ContactPersonDto result = await _contactPersonService.GetById(id);
            return Ok(result.TransferToViewModel());
        }

        /// <summary>
        /// Delete ContactPerson Multiple
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("{contactId}/contact_persons/delete")]
        public async Task<IActionResult> DeleteContactPersonMultiple([FromRoute]int contactId, [FromBody]List<int> ids)
        {
            bool result = await _contactPersonService.DeleteMultiple(ids);
            return Ok(result);

        }

        /// <summary>
        /// Get ContactPersons list by ContactId
        /// </summary>
        /// <returns></returns>
        [HttpGet("{contactId}/contact_persons")]
        public async Task<IActionResult> GetContactPersons(int contactId)
        {
            BaseListDto dto = await _contactPersonService.GetPagedList(contactId);
            return Ok(dto.TransferToViewModel());
        }


        /// <summary>
        /// Get list contactPersons search like string
        /// </summary>
        /// <returns></returns>
        [HttpGet("contact_persons/search/{likeString}")]
        public async Task<IActionResult> SearchContactPersonss(string likeString)
        {
            List<ContactPersonDto> dtos = await _contactPersonService.SearchContactPersons(likeString);
            return Ok(dtos?.TransferToListShortViewModel());
        }
        #endregion

        #region Payment

        /// <summary>
        /// Save contact payment
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{contactId}/contact_payment")]
        public async Task<IActionResult> SaveContactPayment([FromRoute]int contactId, ContactPaymentViewModel model)
        {
            try
            {
                ContactPaymentDto dto = model.TransferToDto();
                dto.ContactId = contactId;
                ContactPaymentDto result = await _contactPaymentService.Save(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Get Payment by ContactId
        /// </summary>
        /// <returns></returns>
        [HttpGet("{contactId}/contact_payment")]
        public async Task<IActionResult> GetContactPayment([FromRoute]int contactId)
        {
            ContactPaymentDto dto = await _contactPaymentService.GetByContactId(contactId);
            return Ok(dto.TransferToViewModel());
        }
        #endregion

        #region ElectronicInvoice
        /// <summary>
        /// Save contact electronic invoice
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{contactId}/contact_electronic_invoice")]
        public async Task<IActionResult> SaveElectronicInvoice([FromRoute]int contactId, ContactElectronicInvoiceViewModel model)
        {
            try
            {
                ContactElectronicInvoiceDto dto = model.TransferToDto();
                dto.ContactId = contactId;
                ContactElectronicInvoiceDto result = await _contactElectronicInvoiceService.Save(dto);
                return Ok(result.TransferToViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Get  electronic invoice by contactId
        /// </summary>
        /// <returns></returns>
        [HttpGet("{contactId}/contact_electronic_invoice")]
        public async Task<IActionResult> GetElectronicInvoice([FromRoute]int contactId)
        {
            ContactElectronicInvoiceDto dto = await _contactElectronicInvoiceService.GetByContactId(contactId);
            return Ok(dto?.TransferToViewModel());
        }
        #endregion


    }
}