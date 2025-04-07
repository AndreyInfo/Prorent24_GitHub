using Prorent24.BLL.Models.Contact;
using Prorent24.WebApp.Models.Contact;
using System.Collections.Generic;
using System.Linq;
using Prorent24.WebApp.Transfers.General;
using Newtonsoft.Json;
using Prorent24.WebApp.Transfers.CrewMember;
using Prorent24.BLL.Models.CrewMember;
using System;

namespace Prorent24.WebApp.Transfers.Contact
{
    public static class ContactTransfer
    {
        /// <summary>
        /// Transfer from List<ContactViewModel> to List<ContactDto>
        /// </summary>
        /// <param name="viewmodels"></param>
        /// <returns></returns>
        public static List<ContactDto> TransferToListDto(this List<ContactViewModel> viewmodels)
        {
            List<ContactDto> ContactDtos = viewmodels.Select(x => x.TransferToDto()).ToList();

            return ContactDtos;
        }

        /// <summary>
        /// Transfer from List<ContactDto> to List<ContactViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ContactViewModel> TransferToListViewModel(this List<ContactDto> dtos)
        {
            List<ContactViewModel> viewModels = dtos.Select(x => x.TransferToViewModel()).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from ContactDto to ContactViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ContactDto TransferToDto(this ContactViewModel model)
        {
            ContactDto ContactDto = new ContactDto()
            {
                Id = model.Id,
                FolderId = model.FolderId,
                Email = model.Email,
                //Email2 = model.Email2,
                Phone = model.Phone,
                //Phone2 = model.Phone2,
                BankAccountNumber = model.BankAccountNumber,
                CocNumber = model.CocNumber,
                //NameLine = model.NameLine,
                FiscalCode = model.FiscalCode,
                CompanyName = model.CompanyName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                DefaultContactPersonId = model.DefaultContactPersonId,

                Bic = model.Bic,
                ProjNote = model.ProjNote,
                SubjectProjNote = model.SubjectProjNote,
                Type = model.Type,
                PurchaseNumber = model.PurchaseNumber,
                DatabaseNumber = model.DatabaseNumber,
                Warning = model.Warning,
                Website = model.Website,
                VatIdentificationNumber = model.VatIdentificationNumber,
                VisitingAddressId = model.VisitingAddressId,
                //BillingAddressId = model.BillingAddressId,
                PostalAddressId = model.PostalAddressId,

                VisitingAddress = model.VisitingAddress?.TransferToDto(),
                PostalAddress = model.PostalAddress?.TransferToDto(),
                //BillingAddress = model.BillingAddress?.TransferToDto()
                SocialNetworkJson = JsonConvert.SerializeObject(model.SocialNetworks),
                CompanyTypeId = model.CompanyTypeId,
                PhonesJson = JsonConvert.SerializeObject(model.Phones),
                CompanyPhonesJson = JsonConvert.SerializeObject(model.CompanyPhones),
                CompanyShortName = model.CompanyShortName,
                Inn = model.Inn,
                Kpp = model.Kpp,
                Ogrn = model.Ogrn,
                CheckingAccount = model.CheckingAccount,
                CorrespondentAccount = model.CorrespondentAccount,
                Bank = model.Bank,
                EmailsJson = JsonConvert.SerializeObject(model.Emails),

                //New
                //BirthDate = model.BirthDate,
                BirthDate = model.BirthDate != null ? ((DateTime)model.BirthDate).ToString("dd.MM.yyyy") : null,

                Specialization = model.Specialization,
                IsCompany = model.IsCompany,
                IsPublic = model.IsPublic,
                CrewMembers = model.CrewMembers != null ? model.CrewMembers.TransferToListDto() : new List<CrewMemberShortDto>(),

            };

            return ContactDto;
        }

        /// <summary>
        /// Transfer from ContactViewModel to ContactDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactViewModel TransferToViewModel(this ContactDto dto)
        {
            ContactViewModel ContactViewModel = new ContactViewModel()
            {
                Id = dto.Id,
                FolderId = dto.FolderId,
                FolderName = dto.Folder?.Name,
                Email = dto.Email,
                //Email2 = dto.Email2,
                Phone = dto.Phone,
                //Phone2 = dto.Phone2,
                BankAccountNumber = dto.BankAccountNumber,
                CocNumber = dto.CocNumber,
                //NameLine = dto.NameLine,
                FiscalCode = dto.FiscalCode,
                CompanyName = dto.CompanyName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                DefaultContactPersonId = dto.DefaultContactPersonId,

                Bic = dto.Bic,
                ProjNote = dto.ProjNote,
                SubjectProjNote = dto.SubjectProjNote,
                Type = dto.Type,
                PurchaseNumber = dto.PurchaseNumber,
                DatabaseNumber = dto.DatabaseNumber,
                Warning = dto.Warning,
                Website = dto.Website,
                VatIdentificationNumber = dto.VatIdentificationNumber,
                // Addresses
                //BillingAddressId = dto.BillingAddressId,
                //BillingAddress = dto.BillingAddress?.TransferToView(),
                PostalAddressId = dto.PostalAddressId,
                PostalAddress = dto.PostalAddress?.TransferToView(),
                VisitingAddressId = dto.VisitingAddressId,
                VisitingAddress = dto.VisitingAddress?.TransferToView(),
                SocialNetworks = dto.SocialNetworks?.TransferToViewModels(),
                CompanyTypeId = dto.CompanyTypeId,
                Phones = dto.Phones,
                CompanyPhones = dto.CompanyPhones,
                CompanyShortName = dto.CompanyShortName,
                Inn = dto.Inn,
                Kpp = dto.Kpp,
                Ogrn = dto.Ogrn,
                CheckingAccount = dto.CheckingAccount,
                CorrespondentAccount = dto.CorrespondentAccount,
                Bank = dto.Bank,
                Emails = dto.Emails,

                //New
                //BirthDate = dto.BirthDate,
                BirthDate = DateTime.TryParse(dto.BirthDate, out DateTime resultBirthdate) ? (DateTime?)resultBirthdate : null,

                Specialization = dto.Specialization,
                IsCompany = dto.IsCompany,
                IsPublic = dto.IsPublic,
                CrewMembers = dto.CrewMembers?.TransferToListModel(),
                Name = dto.CompanyName + " " + dto.FirstName + " " + dto.LastName

            };
            return ContactViewModel;
        }

        /// <summary>
        /// Transfer from List<ContactDto> to List<ContactPersonShortViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ContactShortViewModel> TransferToListShortViewModel(this List<ContactDto> dtos)
        {
            List<ContactShortViewModel> viewModels = dtos.Select(x => x.TransferToShortViewModel()).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from ContactDto to ContactPersonViewModel 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactShortViewModel TransferToShortViewModel(this ContactDto dto)
        {
            ContactShortViewModel contactPersoViewModel = new ContactShortViewModel()
            {
                Id = dto.Id,
                Name = dto.CompanyName + " " +dto.FirstName + " " + dto.LastName
            };

            return contactPersoViewModel;
        }

    }
}
