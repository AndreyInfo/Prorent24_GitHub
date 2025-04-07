using Prorent24.BLL.Models.Contact;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.General.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.ContactPerson
{
    public static class ContactPersonPersonTransfer
    {
        /// <summary>
        /// Transfer from List<ContactPersonViewModel> to List<ContactPersonDto>
        /// </summary>
        /// <param name="viewmodels"></param>
        /// <returns></returns>
        public static List<ContactPersonDto> TransferToListDto(this List<ContactPersonViewModel> viewmodels)
        {
            List<ContactPersonDto> ContactPersonDtos = viewmodels.Select(x => x.TransferToDto()).ToList();

            return ContactPersonDtos;
        }

       

        /// <summary>
        /// Transfer from List<ContactPersonDto> to List<ContactPersonViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ContactPersonViewModel> TransferToListViewModel(this List<ContactPersonDto> dtos)
        {
            List<ContactPersonViewModel> viewModels = dtos.Select(x => x.TransferToViewModel()).ToList();

            return viewModels;
        }


        /// <summary>
        /// Transfer from List<ContactPersonDto> to List<ContactPersonShortViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ContactPersonShortViewModel> TransferToListShortViewModel(this List<ContactPersonDto> dtos)
        {
            List<ContactPersonShortViewModel> viewModels = dtos.Select(x => x.TransferToShortViewModel()).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from ContactPersonDto to ContactPersonViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ContactPersonDto TransferToDto(this ContactPersonViewModel model)
        {
            ContactPersonDto ContactPersonDto = new ContactPersonDto()
            {
                Id = model.Id,
                ContactId = model.ContactId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                SalutationId = model.SalutationId,
                Salutation = model.Salutation,
                // State = model.State,

                Email = model.Email,
                Phone = model.Phone,
                Function = model.Function,
                Mobile = model.Mobile
            };
            if (model.Address != null)
            {
                ContactPersonDto.Address = model.Address.Address;
                ContactPersonDto.City = model.Address.City;
                ContactPersonDto.CountryId = model.Address.CountryId;
                ContactPersonDto.HouseNumber = model.Address.Number;
                ContactPersonDto.PostalCode = model.Address.PostalCode;
                ContactPersonDto.Province = model.Address.Region;
            }

            return ContactPersonDto;
        }

        /// <summary>
        /// Transfer from ContactPersonViewModel to ContactPersonDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactPersonViewModel TransferToViewModel(this ContactPersonDto dto, ContactDto contact  = null)
        {
            ContactPersonViewModel ContactPersonViewModel = new ContactPersonViewModel()
            {
                Id = dto.Id,
                ContactId = dto.ContactId,
                Email = dto.Email,
                Phone = dto.Phone,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Function = dto.Function,
                Mobile = dto.Mobile,
                SalutationId = dto.SalutationId,
                Salutation = dto.Salutation,
                CompanyName = contact?.CompanyName,
                Address = new AddressViewModel()
                {
                    Address = dto.Address??"",
                    City = dto.City??"",
                    CountryId = dto.CountryId,
                    Number = dto.HouseNumber??"",
                    PostalCode = dto.PostalCode??"",
                    Region = dto.Province??"",
                    AdditionalAddress = "",
                    Alt = 0,
                    Lat = 0,
                    Long = 0
                }
            };

            return ContactPersonViewModel;
        }

        /// <summary>
        /// Transfer from ContactPersonViewModel to ContactPersonDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactPersonShortViewModel TransferToShortViewModel(this ContactPersonDto dto)
        {
            ContactPersonShortViewModel contactPersonViewModel = new ContactPersonShortViewModel()
            {
                Id = dto.Id,
                ContactId = dto.ContactId ,
                Name = dto.FirstName + " " + dto.LastName
            };

            return contactPersonViewModel;
        }
    }
}
