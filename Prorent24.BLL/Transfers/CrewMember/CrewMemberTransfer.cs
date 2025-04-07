using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Address;
using Prorent24.Entity.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.Entity.Directory;
using Prorent24.Entity;

namespace Prorent24.BLL.Transfers.CrewMember
{
    public static class CrewMemberTransfer
    {
        /// <summary>
        /// Transfer from List<CrewMemberEntity> to List<CrewMemberDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<CrewMemberDto> TransferToListDto(this List<CrewMemberEntity> entities, FunctionPermissionDto permission = null, List<DirectoryEntity> countries =null, List<Role> roles = null)
        {
            List<CrewMemberDto> crewMemberDtos = entities.Select(x => x.TransferToDto(null, permission, countries, roles)).ToList();

            return crewMemberDtos;
        }

        /// <summary>
        /// Transfer from CrewMemberDto to CrewMemberEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static CrewMemberDto TransferToDto(this CrewMemberEntity entity, string exclude = null, FunctionPermissionDto permission = null, List<DirectoryEntity> countries = null, List<Role> roles = null)
        {
            string roleId = entity.User?.Roles?.FirstOrDefault()?.RoleId;

            CrewMemberDto dto = new CrewMemberDto()
            {
                Id = entity.UserId,
                //Id = entity.Id,
                ColorAppointments = entity.ColorAppointments,
                Description = entity.Description,
                Email = entity.User?.Email,
                FirstName = string.IsNullOrWhiteSpace(entity.User?.FirstName) ? entity.User?.Email : entity.User?.FirstName,
                FolderId = entity.FolderId,
                Folder = entity.Folder?.TransferToDto(),
                LastName = entity.User?.LastName,
                MiddleName = entity.User?.MiddleName,
                Phone = entity.User?.PhoneNumber,
                Availability = entity.Availability,
                DisplayInPlanner = entity.DisplayInPlanner,
                DrivingLicense = entity.DrivingLicense,
                EmergencyContact = entity.EmergencyContact,
                ReceiveEmails = entity.ReceiveEmails,
                Active = entity.Active,
                IsPowerUser = entity.IsPowerUser,
                //LastLogin = entity.LastLogin,
                UserRoleId = entity.User?.Roles?.FirstOrDefault()?.RoleId,
                Username = entity.User?.UserName,
                DefaultRateId = entity.DefaultRateId,
                BankAccountNumber = entity.BankAccountNumber,

                //New
                //Birthdate = entity.Birthdate,
                Birthdate = entity.Birthdate != null ? ((DateTime)entity.Birthdate).ToString("dd.MM.yyyy") : null,

                CoCNumber = entity.CoCNumber,
                CompanyName = entity.CompanyName,
                ContractDate = entity.ContractDate,
                HoursInContract = entity.HoursInContract,
                PassportNumber = entity.PassportNumber,
                PassportCompany = entity.PassportCompany,
                
                //New
                //PassportDate = entity.PassportDate,
                PassportDate = entity.PassportDate != null ? ((DateTime)entity.PassportDate).ToString("dd.MM.yyyy") : null,

                VAT = entity.VAT,
                DefaultRate = entity.DefaultRate != null ? new CrewMemberRateDto() { Name = entity.DefaultRate.Name } : null,
                Notes = entity.Notes?.ToList().TransferToListDto(),
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Tasks = entity.Tasks?.ToList().TransferToListDto(exclude),
                Files = entity.Files?.ToList().TransferToListDto(),
                SocialNetworkJson = entity.SocialNetworksJson,
                IsSystemUser = entity.User?.IsSystemUser,
                BirthdateView = entity.Birthdate!=null? ((DateTime)entity.Birthdate).ToString("dd.MM.yyyy"):null,
                ContractDateView = entity.ContractDate != null ? ((DateTime)entity.ContractDate).ToString("dd.MM.yyyy") : null,
                UserRole = new UserRoleDto()
                {
                    Name = roles?.FirstOrDefault(x=>x.Id == roleId)?.Name
                }

            };

            if (entity.Address != null)
            {
                dto.AddressId = entity.Address.Id;
                dto.CountryId = entity.Address?.CountryId;
                dto.City = entity.Address?.City;
                dto.Address = entity.Address?.Address;
                dto.AdditionalAddress = entity.Address?.AdditionalAddress;
                dto.Region = entity.Address?.Region;
                dto.Alt = entity.Address?.Alt;
                dto.Long = entity.Address?.Long;
                dto.Lat = entity.Address?.Lat;
                dto.PostalCode = entity.Address?.PostalCode;
                dto.Number = entity.Address?.Number;
                dto.Country = countries?.Where(x => x.Id == entity.Address?.CountryId).FirstOrDefault()?.Locs.FirstOrDefault()?.Name;

            }

            if (permission != null)
            {
                dto.Edit = permission.Edit;
                dto.Delete = permission.Delete;
                dto.View = permission.View;
            }

            return dto;
        }
        

        /// <summary>
        /// Transfer from CrewMemberEntity to CrewMemberDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CrewMemberEntity TransferToEntity(this CrewMemberDto dto)
        {
            CrewMemberEntity crewMemberEntity = new CrewMemberEntity()
            {
                UserId = dto.Id,
                FolderId = dto.FolderId,
                //Id = dto.Id,
                ColorAppointments = dto.ColorAppointments,
                Description = dto.Description,
                //Email = dto.Email,
                //FirstName = dto.FirstName,
                //FolderId = dto.FolderId,
                //LastName = dto.LastName,
                //MiddleName = dto.MiddleName,
                //Phone = dto.Phone,

                Availability = dto.Availability,
                DisplayInPlanner = dto.DisplayInPlanner,
                DrivingLicense = dto.DrivingLicense,
                EmergencyContact = dto.EmergencyContact,
                ReceiveEmails = dto.ReceiveEmails,
                Active = dto.Active,
                IsPowerUser = dto.IsPowerUser,

                //LastLogin = dto.LastLogin,
                //UserRoleId = dto.UserRoleId,
                //Username = dto.Username,

                DefaultRateId = dto.DefaultRateId,
                BankAccountNumber = dto.BankAccountNumber,

                //New
                //Birthdate = dto.Birthdate,
                Birthdate = DateTime.TryParse(dto.Birthdate, out DateTime resultBirthdate) ? (DateTime?)resultBirthdate : null,

                CoCNumber = dto.CoCNumber,
                CompanyName = dto.CompanyName,
                ContractDate = dto.ContractDate,
                HoursInContract = dto.HoursInContract,
                PassportNumber = dto.PassportNumber,
                PassportCompany = dto.PassportCompany,
                
                //New
                //PassportDate = dto.PassportDate,
                PassportDate = DateTime.TryParse(dto.PassportDate, out DateTime resultPassportDate) ? (DateTime?)resultPassportDate : null, 

                VAT = dto.VAT,

                AddressId = dto.AddressId,
                Address = ((AddressDto)dto).TransferToEntity(),

                Notes = dto.Notes?.ToList().TransferToListEntity(),
                Tags = dto.Tags?.ToList().TransferToListTagBondEntity(),
                SocialNetworksJson = dto.SocialNetworkJson,
                //CreationDate = dto.CreationDate,
                //LastModifiedDate = dto.LastModifiedDate
            };

            return crewMemberEntity;
        }

        public static CrewMemberEntity TransferFromEntity(this CrewMemberEntity target, CrewMemberEntity source, bool ownUser = false)
        {
            target.UserId = source.UserId;
            target.ColorAppointments = source.ColorAppointments;
            target.Description = source.Description;
            target.FolderId = source.FolderId;

            target.Availability = source.Availability;
            target.DisplayInPlanner = source.DisplayInPlanner;
            target.DrivingLicense = source.DrivingLicense;
            target.EmergencyContact = source.EmergencyContact;
            target.ReceiveEmails = source.ReceiveEmails;
            target.Active = ownUser? target.Active : source.Active;
            target.IsPowerUser = source.IsPowerUser;

            target.DefaultRateId = source.DefaultRateId;
            target.BankAccountNumber = source.BankAccountNumber;
            target.Birthdate = source.Birthdate;
            target.CoCNumber = source.CoCNumber;
            target.CompanyName = source.CompanyName;
            target.ContractDate = source.ContractDate;
            target.HoursInContract = source.HoursInContract;
            target.PassportNumber = source.PassportNumber;
            target.PassportCompany = source.PassportCompany;
            target.PassportDate = source.PassportDate;
            target.VAT = source.VAT;
            target.SocialNetworksJson = source.SocialNetworksJson;
            
            //target.AddressId = source.AddressId;
            //target.Address = source.Address;

            return target;
        }


        /// <summary>
        /// Transfer from List<CrewMemberEntity> to List<CrewMemberForProjectPlanningDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<CrewMemberForProjectPlanningDto> TransferToListForProjectPlanningDto(this List<CrewMemberEntity> entities)
        {
            List<CrewMemberForProjectPlanningDto> crewMemberDtos = entities.Select(x => x.TransferForProjectPlanningToDto()).ToList();

            return crewMemberDtos;
        }

        /// <summary>
        /// Transfer from CrewMemberDto to CrewMemberEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static CrewMemberForProjectPlanningDto TransferForProjectPlanningToDto(this CrewMemberEntity entity, string exclude = null)
        {
            CrewMemberForProjectPlanningDto crewMemberDto = new CrewMemberForProjectPlanningDto()
            {
                Id = entity.UserId,
                Name = string.IsNullOrWhiteSpace(entity.User?.FirstName) ? entity.User?.Email : entity.User?.FirstName + " " + entity.User?.LastName,
                FolderName = entity.FolderId != null ? entity.Folder?.Name : "Empty"

            };

            return crewMemberDto;
        }


        public static CrewMemberShortDto TransferForCrewMemberShortDto(this User entity)
        {
            CrewMemberShortDto dto = new CrewMemberShortDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName
            };
            return dto;
        }
    }
}
