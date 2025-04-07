using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.WebApp.Models.Configuration.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Account
{
    public static class CompanyDetailsTransfer
    {
        /// <summary>
        /// Transfer from  CompanyDetailsDto to  CompanyDetailsViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CompanyDetailsViewModel TransferToViewModel(this CompanyDetailsDto dto)
        {
            CompanyDetailsViewModel viewModel = new CompanyDetailsViewModel()
            {
                CompanyName = dto.CompanyName,
                City = dto.City,
                ContactPersonEmail = dto.ContactPersonEmail,
                ContactPersonName = dto.ContactPersonName,
                CountryId = dto.CountryId,
                HouseNumber = dto.HouseNumber,
                InvoiceEmail = dto.InvoiceEmail,
                Postcode = dto.Postcode,
                Street = dto.Street,
                AccountantInfo = dto.AccountantInfo == null ? new ContactInfoViewModel() : dto.AccountantInfo?.TransferToViewModel(),
                DirectorInfo = dto.DirectorInfo == null ? new ContactInfoViewModel() : dto.DirectorInfo?.TransferToViewModel(),
                Inn = dto.Inn,
                Kpp = dto.Kpp,
                Ogrn = dto.Ogrn,
                Okpo = dto.Okpo,
                CheckingAccount = dto.CheckingAccount,
                CorrespondentAccount = dto.CorrespondentAccount,
                Bank = dto.Bank,
                Bic = dto.Bic,
                Website = dto.Website,
                ShortName = dto.ShortName,
                Phones = dto.Phones,
                AdditionalOffice = dto.AdditionalOffice?.TransferToViewModel(),
                LogoImage = dto.LogoImage,
                BackgroundImage = dto.BackgroundImage

            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from  CompanyDetailsViewModel to  CompanyDetailsDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static CompanyDetailsDto TransferToDto(this CompanyDetailsViewModel view)
        {
            CompanyDetailsDto dto = new CompanyDetailsDto()
            {
                CompanyName = view.CompanyName,
                City = view.City,
                ContactPersonEmail = view.ContactPersonEmail,
                ContactPersonName = view.ContactPersonName,
                CountryId = view.CountryId,
                HouseNumber = view.HouseNumber,
                InvoiceEmail = view.InvoiceEmail,
                Postcode = view.Postcode,
                Street = view.Street,
                AccountantInfo = view.AccountantInfo?.TransferToDto(),
                DirectorInfo = view.DirectorInfo?.TransferToDto(),
                Inn = view.Inn,
                Kpp = view.Kpp,
                Ogrn = view.Ogrn,
                Okpo = view.Okpo,
                CheckingAccount = view.CheckingAccount,
                CorrespondentAccount = view.CorrespondentAccount,
                Bank = view.Bank,
                Bic = view.Bic,
                Website = view.Website,
                ShortName = view.ShortName,
                Phones = view.Phones,
                AdditionalOffice = view.AdditionalOffice?.TransferToDto(),
                LogoImage = view.LogoImage,
                BackgroundImage = view.BackgroundImage
            };

            return dto;
        }

        public static ContactInfoViewModel TransferToViewModel(this ContacInfoDto dto)
        {
            ContactInfoViewModel model = new ContactInfoViewModel()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName
            };

            return model;
        }

        public static ContacInfoDto TransferToDto(this ContactInfoViewModel view)
        {
            ContacInfoDto dto = new ContacInfoDto()
            {
                FirstName = view.FirstName,
                LastName = view.LastName,
                MiddleName = view.MiddleName
            };

            return dto;
        }

        public static CompanyOfficeViewModel TransferToViewModel(this CompanyOfficeDto dto)
        {
            CompanyOfficeViewModel model = new CompanyOfficeViewModel()
            {
                City = dto.City,
                CountryId = dto.CountryId,
                HouseNumber = dto.HouseNumber,
                Postcode = dto.Postcode,
                Street = dto.Street,
            };

            return model;
        }

        public static CompanyOfficeDto TransferToDto(this CompanyOfficeViewModel view)
        {
            CompanyOfficeDto dto = new CompanyOfficeDto()
            {
                City = view.City,
                CountryId = view.CountryId,
                HouseNumber = view.HouseNumber,
                Postcode = view.Postcode,
                Street = view.Street,
            };

            return dto;
        }
    }
}
