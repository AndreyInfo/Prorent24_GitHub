using Newtonsoft.Json;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.WebApp.Models.CrewMember;
using Prorent24.WebApp.Transfers.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.CrewMember
{
    public static class CrewMemberTransfer
    {
        /// <summary>
        /// Transfer from List<CrewMemberViewModel> to List<CrewMemberDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<CrewMemberDto> TransferToListDto(this List<CrewMemberViewModel> entities)
        {
            List<CrewMemberDto> crewMemberDtos = entities.Select(x => x.TransferToDto()).ToList();

            return crewMemberDtos;
        }

        /// <summary>
        /// Transfer from CrewMemberViewModel to CrewMemberDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CrewMemberDto TransferToDto(this CrewMemberViewModel model)
        {
            CrewMemberDto crewMemberDto = new CrewMemberDto()
            {
                Id = model.Id,
                ColorAppointments = model.ColorAppointments,
                Description = model.Description,
                Email = model.Email,
                FirstName = model.FirstName,
                FolderId = model.FolderId,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Phone = model.Phone,
                Availability = model.Availability,
                DisplayInPlanner = model.DisplayInPlanner,
                DrivingLicense = model.DrivingLicense,
                EmergencyContact = model.EmergencyContact,
                ReceiveEmails = model.ReceiveEmails,
                Active = model.Active,
                IsPowerUser = model.IsPowerUser,
                LastLogin = model.LastLogin,
                UserRoleId = model.UserRoleId,
                Username = model.Username,

                DefaultRateId = model.DefaultRateId,
                BankAccountNumber = model.BankAccountNumber,

                //New
                //Birthdate = model.Birthdate,
                Birthdate = model.Birthdate != null ? ((DateTime)model.Birthdate).ToString("dd.MM.yyyy") : null,

                CoCNumber = model.CoCNumber,
                CompanyName = model.CompanyName,
                ContractDate = model.ContractDate,
                HoursInContract = model.HoursInContract,
                PassportNumber = model.PassportNumber,
                PassportCompany = model.PassportCompany,

                //New
                //PassportDate = model.PassportDate,
                PassportDate = model.PassportDate != null ? ((DateTime)model.PassportDate).ToString("dd.MM.yyyy") : null,
                
                VAT = model.VAT,

                // AddressID
                //Id = model.AddressId,
                AddressId = model.AddressId,
                CountryId = model.CountryId,
                City = model.City,
                Address = model.Address,
                AdditionalAddress = model.AdditionalAddress,
                Region = model.Region,
                Alt = model.Alt,
                Long = model.Long,
                Lat = model.Lat,
                PostalCode = model.PostalCode,
                Number = model.Number,
                SocialNetworkJson = JsonConvert.SerializeObject(model.SocialNetworks),
                IsSystemUser = model.IsSystemUser
               

                // Notes = model.Notes?.TransferToDtoModel()
            };

            return crewMemberDto;
        }

        /// <summary>
        /// Transfer from CrewMemberDto to CrewMemberViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CrewMemberViewModel TransferToViewModel(this CrewMemberDto dto)
        {
            CrewMemberViewModel crewMemberView = new CrewMemberViewModel()
            {
                Id = dto.Id,
                ColorAppointments = dto.ColorAppointments,
                Description = dto.Description,
                Email = dto.Email,
                FirstName = dto.FirstName,
                FolderId = dto.FolderId,
                FolderName = dto.Folder?.Name,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Phone = dto.Phone,
                Availability = dto.Availability,
                DisplayInPlanner = dto.DisplayInPlanner,
                DrivingLicense = dto.DrivingLicense,
                EmergencyContact = dto.EmergencyContact,
                ReceiveEmails = dto.ReceiveEmails,
                Active = dto.Active,
                IsPowerUser = dto.IsPowerUser,
                LastLogin = dto.LastLogin,
                UserRoleId = dto.UserRoleId,
                Username = dto.Username,

                AddressId = dto.AddressId,

                CountryId = dto.CountryId,
                City = dto.City,
                Address = dto.Address,
                AdditionalAddress = dto.AdditionalAddress,
                Region = dto.Region,
                Alt = dto.Alt,
                Long = dto.Long,
                Lat = dto.Lat,
                PostalCode = dto.PostalCode,
                Number = dto.Number,


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
                SocialNetworks = dto.SocialNetworks?.TransferToViewModels(),
                IsSystemUser = dto.IsSystemUser,
                BirthdateView = dto.BirthdateView,
                ContractDateView = dto.ContractDateView
                //Note = dto.Note?.TransferToViewModel()
            };

            return crewMemberView;
        }

        /// <summary>
        /// Transfer from List<CrewMemberShortVieweModel> to List<CrewMemberShortDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<CrewMemberShortDto> TransferToListDto(this List<CrewMemberShortViewModel> entities)
        {
            List<CrewMemberShortDto> crewMemberDtos = entities.Select(x => x.TransferToDto()).ToList();

            return crewMemberDtos;
        }

        /// <summary>
        /// Transfer from List<CrewMemberShortDto> to List<CrewMemberShortVieweModel>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<CrewMemberShortViewModel> TransferToListModel(this List<CrewMemberShortDto> entities)
        {
            List<CrewMemberShortViewModel> crewMemberViewModels = entities.Select(x => x.TransferToViewModel()).ToList();
            return crewMemberViewModels;
        }

        /// <summary>
        /// Transfer from CrewMemberShortVieweModel to CrewMemberShortDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CrewMemberShortDto TransferToDto(this CrewMemberShortViewModel model)
        {
            CrewMemberShortDto crewMemberDto = new CrewMemberShortDto()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                ColorAppointments = model.ColorAppointments
            };

            return crewMemberDto;
        }

        /// <summary>
        /// Transfer from CrewMemberShortDto to CrewMemberShortVieweModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CrewMemberShortViewModel TransferToViewModel(this CrewMemberShortDto dto)
        {
            CrewMemberShortViewModel crewMemberView = new CrewMemberShortViewModel()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                ColorAppointments = dto.ColorAppointments
            };

            return crewMemberView;
        }
    }
}
