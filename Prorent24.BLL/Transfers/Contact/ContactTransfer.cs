using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Transfers.ContactPerson;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Contact;
using Prorent24.Entity.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Contact
{
    public static class ContactTransfer
    {
        /// <summary>
        /// Transfer from List<ContactEntity> to List<ContactDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ContactDto> TransferToListDto(this List<ContactEntity> entities, FunctionPermissionDto permissions = null, List<DirectoryEntity> countries = null)
        {
            List<ContactDto> dtos = entities.Select(x => x.TransferToDto(null, permissions, countries)).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ContactEntity to ContactDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ContactDto TransferToDto(this ContactEntity entity, string exclude = null, FunctionPermissionDto permissions = null, List<DirectoryEntity> countries = null)
        {
            ContactDto dto = new ContactDto()
            {
                Id = entity.Id,
                BankAccountNumber = entity.BankAccountNumber,
                Bic = entity.Bic,
                //BillingAddressId = entity.BillingAddressId,
                //BillingAddress = entity.BillingAddress?.TransferToDto(),
                CocNumber = entity.CocNumber,
                CompanyName = entity.CompanyName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,

                DatabaseNumber = entity.DatabaseNumber,
                DefaultContact = entity.DefaultContact?.TransferToDto(),
                DefaultContactPersonId = entity.DefaultContactPersonId,
                Email = entity.Email,
                Email2 = entity.Email2,
                FiscalCode = entity.FiscalCode,
                Folder = entity.Folder?.TransferToDto(),
                FolderId = entity.FolderId,
                NameLine = entity.NameLine,
                Phone = entity.Phone,
                Phone2 = entity.Phone2,

                PostalAddress = entity.PostalAddress?.TransferToDto(countries),
                PostalAddressId = entity.PostalAddressId,

                VisitingAddress = entity.VisitingAddress?.TransferToDto(countries),
                VisitingAddressId = entity.VisitingAddressId,

                ProjNote = entity.ProjNote,
                PurchaseNumber = entity.PurchaseNumber,
                SubjectProjNote = entity.SubjectProjNote,
                Type = entity.Type,
                VatIdentificationNumber = entity.VatIdentificationNumber,
                
                Warning = entity.Warning,
                Website = entity.Website,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                //VisitingCountry = countries?.Where(x => x.Id == entity.VisitingAddress?.CountryId).FirstOrDefault()?.Locs.FirstOrDefault()?.Name,
                
                //New
                //Country = countries?.Where(x => x.Id == entity.VisitingAddress?.CountryId).FirstOrDefault()?.Locs.FirstOrDefault()?.Name,

                Notes = entity.Notes?.ToList().TransferToListDto(),
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Tasks = entity.Tasks?.ToList().TransferToListDto(exclude),
                Files = entity.Files?.ToList().TransferToListDto(),
                CompanyTypeId = entity.CompanyTypeId,
                SocialNetworkJson = entity.SocialNetworkJson,
                PhonesJson = entity.PhonesJson,
                CompanyPhonesJson = entity.CompanyPhonesJson,
                CompanyShortName = entity.CompanyShortName,
                Inn = entity.Inn,
                Kpp = entity.Kpp,
                Ogrn = entity.Ogrn,
                CheckingAccount = entity.CheckingAccount,
                CorrespondentAccount = entity.CorrespondentAccount,
                Bank = entity.Bank,
                EmailsJson = entity.EmailsJson,

                //New
                //BirthDate = entity.BirthDate,
                BirthDate = entity.BirthDate != null ? ((DateTime)entity.BirthDate).ToString("dd.MM.yyyy") : null,

                Specialization = entity.Specialization,
                IsCompany = entity.IsCompany,
                IsPublic = entity.IsPublic ?? true,
                CrewMembers = entity.CrewMembers != null ? entity.CrewMembers.ToList().TransferToDto() : new List<CrewMemberShortDto>(),

            };

            if (permissions != null)
            {
                dto.View = permissions.View;
                dto.Edit = permissions.Edit;
                dto.Delete = permissions.Delete;
            }

            return dto;
        }

        /// <summary>
        /// Transfer from ContactDto to ContactEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactEntity TransferToEntity(this ContactDto dto)
        {
            ContactEntity entity = new ContactEntity()
            {
                Id = dto.Id,
                BankAccountNumber = dto.BankAccountNumber,
                Bic = dto.Bic,
                //BillingAddressId = dto.BillingAddressId,
                //BillingAddress = dto.BillingAddress?.TransferToEntity(),
                CocNumber = dto.CocNumber,
                CompanyName = dto.CompanyName,

                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,

                DatabaseNumber = dto.DatabaseNumber,
                DefaultContact = dto.DefaultContact?.TransferToEntity(),
                DefaultContactPersonId = dto.DefaultContactPersonId,
                Email = dto.Email,
                Email2 = dto.Email2,
                FiscalCode = dto.FiscalCode,
                Folder = dto.Folder?.TransferToEntity(),
                FolderId = dto.FolderId,
                NameLine = dto.NameLine,
                Phone = dto.Phone,
                Phone2 = dto.Phone2,
                PostalAddress = dto.PostalAddress?.TransferToEntity(),
                PostalAddressId = dto.PostalAddressId,
                ProjNote = dto.ProjNote,
                PurchaseNumber = dto.PurchaseNumber,
                SubjectProjNote = dto.SubjectProjNote,
                Type = dto.Type,
                VatIdentificationNumber = dto.VatIdentificationNumber,
                VisitingAddress = dto.VisitingAddress?.TransferToEntity(),
                VisitingAddressId = dto.VisitingAddressId,
                Warning = dto.Warning,
                Website = dto.Website,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                CompanyTypeId = dto.CompanyTypeId,
                SocialNetworkJson = dto.SocialNetworkJson,
                PhonesJson = dto.PhonesJson,
                CompanyPhonesJson = dto.CompanyPhonesJson,
                CompanyShortName = dto.CompanyShortName,
                Inn = dto.Inn,
                Kpp = dto.Kpp,
                Ogrn = dto.Ogrn,
                CheckingAccount = dto.CheckingAccount,
                CorrespondentAccount = dto.CorrespondentAccount,
                Bank = dto.Bank,
                EmailsJson = dto.EmailsJson,

                //New
                //BirthDate = dto.BirthDate,
                BirthDate = DateTime.TryParse(dto.BirthDate, out DateTime resultBirthdate) ? (DateTime?)resultBirthdate : null,

                Specialization = dto.Specialization,
                IsCompany = dto.IsCompany,
                IsPublic = dto.IsPublic

            };

            return entity;
        }

        public static List<CrewMemberShortDto> TransferToDto(this List<ContactVisibilityCrewMemberEntity> entities)
        {
            List<CrewMemberShortDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        public static CrewMemberShortDto TransferToDto(this ContactVisibilityCrewMemberEntity entity)
        {
            CrewMemberShortDto dto = new CrewMemberShortDto()
            {
                Id = entity.CrewMemberId,
                FirstName = entity.CrewMember?.FirstName,
                LastName = entity.CrewMember?.LastName,
                MiddleName = entity.CrewMember?.MiddleName
            };
            return dto;
        }

        public static List<ContactVisibilityCrewMemberEntity> TransferToContactVisibilityEntity(this List<CrewMemberShortDto> dtos)
        {
            List<ContactVisibilityCrewMemberEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }

        public static ContactVisibilityCrewMemberEntity TransferToEntity(this CrewMemberShortDto dto)
        {
            ContactVisibilityCrewMemberEntity entity = new ContactVisibilityCrewMemberEntity()
            {
                CrewMemberId = dto.Id
            };
            return entity;
        }
    }
}

