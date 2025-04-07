using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.General.Address;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.CrewMember;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Models.CrewMember
{
    public class CrewMemberDto : AddressDto
    {
        [IncludeToGrid(Order = 1, IsDisplay = false, IsKey = true, KeyType = "string")]
        public string Id { get; set; }

        public string ColorAppointments { get; set; }

        [IncludeToGrid(Order = 3, TreeColumn = true, TreeColumnOrder = 8, ColumnGroup = ColumnGroupEnum.General)]
        public string Description { get; set; }

        [IncludeToGrid(Order = 4, TreeColumn = true, TreeColumnOrder = 7, ColumnGroup = ColumnGroupEnum.General)]
        public string Email { get; set; }

        [IncludeToGrid(Order = 5, TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.General)]
        public string FirstName { get; set; }

        //[IncludeToGrid(IsDisplay = false, IsEntity = true, EntityKey = "FolderEntity", ColumnGroup = ColumnGroupEnum.General)]
        public FolderDto Folder { get; set; }

        [IncludeToGrid(Order = 6, TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.General)]
        public string LastName { get; set; }

        [IncludeToGrid(Order = 7, TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.General)]
        public string MiddleName { get; set; }

        [IncludeToGrid(Order = 8, TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string Phone { get; set; }

        [IncludeToGrid(Order = 1, TreeColumn = true, TreeColumnOrder = 1, IsDisplay = true, ColumnName = "folderName", ColumnGroup = ColumnGroupEnum.General)]
        public string FolderName
        {
            get
            {
                return Folder?.Name;
            }
        }

        //PerconalData
        [IncludeToGrid(Order = 10, TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.Data)]
        public string DrivingLicense { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        //[IncludeToGrid(Order = 11, IsDisplay = false, ColumnGroup = ColumnGroupEnum.PerconalData)]
        public AvailabilityCrewMemberTypeEnum Availability { get; set; }

        [IncludeToGrid(Order = 12, IsDisplay = false, ColumnGroup = ColumnGroupEnum.PerconalData)]
        public bool DisplayInPlanner { get; set; }

        //[IncludeToGrid(Order = 13, IsDisplay = false, ColumnGroup = ColumnGroupEnum.PerconalData)]
        public string ReceiveEmails { get; set; }

        [IncludeToGrid(Order = 14, TreeColumn = true, TreeColumnOrder = 6, ColumnGroup = ColumnGroupEnum.Data)]
        public string EmergencyContact { get; set; }


        //User Info

        [IncludeToGrid(Order = 15, IsDisplay = false, ColumnGroup = ColumnGroupEnum.UserInfo)]
        public bool Active { get; set; }

        [IncludeToGrid(Order = 16, IsDisplay = false, ColumnGroup = ColumnGroupEnum.UserInfo)]
        public string Username { get; set; }

        //[IncludeToGrid(Order = 17, IsDisplay = false, ColumnGroup = ColumnGroupEnum.UserInfo)]
        public DateTime LastLogin { get; set; }

        [IncludeToGrid(Order = 6, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.General)]
        public string MemberRole
        {
            get
            {
                return UserRole?.Name;
            }
        }

        public UserRoleDto UserRole { get; set; }

        //[IncludeToGrid(Order = 19, IsDisplay = false, ColumnGroup = ColumnGroupEnum.UserInfo)]
        public bool IsPowerUser { get; set; }


        //Financial

        [IncludeToGrid(Order = 20, TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.PerconalData)]
        public string RateByDefault
        {
            get
            {
                return DefaultRate != null ? DefaultRate.Name : null;
            }
        }
        public CrewMemberRateDto DefaultRate { get; set; }


        //Administrative

        [IncludeToGrid(Order = 21, TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.PerconalData)]
        public string BankAccountNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        [IncludeToGrid(Order = 22, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Administrative, KeyType = "string", DisplayName = "ContractDate")]
        public string ContractDateView { get; set; }

        [IncludeToGrid(Order = 33, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.Data)]
        //New
        //public DateTime? Birthdate { get; set; }
        public string Birthdate { get; set; }

        [IncludeToGrid(Order = 23, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Administrative, KeyType = "string", DisplayName = "Birthdate")]
        public string BirthdateView { get; set; }

        [IncludeToGrid(Order = 24, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Administrative)]
        public int? HoursInContract { get; set; }

        [IncludeToGrid(Order = 25, TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.PerconalData)]
        public string CompanyName { get; set; }

        [IncludeToGrid(Order = 26, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.PerconalData)]
        public string CoCNumber { get; set; }

        [IncludeToGrid(Order = 27, TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.Data)]
        public string PassportNumber { get; set; }

        [IncludeToGrid(Order = 32, TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.Data)]
        public string PassportCompany { get; set; }

        [IncludeToGrid(Order = 31, TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.Data)]
        //New
        //public DateTime? PassportDate { get; set; }
        public string PassportDate { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public int? FolderId { get; set; }
        public string UserRoleId { get; set; }
        public int? AddressId { get; set; }
        public int? DefaultRateId { get; set; }

        [IncludeToGrid(Order = 30, TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.PerconalData)]
        public string VAT { get; set; }

        public List<NoteDto> Notes { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<FileDto> Files { get; set; }
        public string SocialNetworkJson { get; set; }
        public List<SocialNetworkDto> SocialNetworks
        {
            get
            {
                if (this.SocialNetworkJson != null)
                    return JsonConvert.DeserializeObject<List<SocialNetworkDto>>(this.SocialNetworkJson);
                else
                    return null;
            }
        }

        [IncludeToGrid(Order = 28, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Administrative)]
        public bool? IsSystemUser { get; set; }

        [IncludeToGrid(Order = 29, TreeColumn = true, TreeColumnOrder = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string Skype
        {
            get
            {
                return SocialNetworks?.FirstOrDefault(x => x.Type == SocialNetworkTypeEnum.Skype)?.Name;
            }
        }
    }
}
