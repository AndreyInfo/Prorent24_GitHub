using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Address;
using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Contact;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Models.Contact
{
    public class ContactDto : BaseDto<int>
    {
        #region Data

        [IncludeToGrid(Order = 5, TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.General)]
        public string CompanyName { get; set; }

        [IncludeToGrid(Order = 6, TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.General)]
        public string FirstName { get; set; }

        [IncludeToGrid(Order = 7, TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.General)]
        public string LastName { get; set; }

        [IncludeToGrid(Order = 8, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.General)]
        public string MiddleName { get; set; }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public string Name { get { return this.Type == ContactType.Company ? this.CompanyName : $"{LastName} {FirstName}"; } }

        //[IncludeToGrid(Order = 10, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContactType Type { get; set; }

        //[IncludeToGrid(Order = 11, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string NameLine { get; set; }

        [IncludeToGrid(Order = 12, TreeColumn = true, TreeColumnOrder = 6, ColumnGroup = ColumnGroupEnum.General)]
        public string Phone {
            get
            {
                if (this.PhonesJson != null)
                    return JsonConvert.DeserializeObject<List<string>>(this.PhonesJson).FirstOrDefault();
                else
                    return null;
            }
            set { }
        }

        [IncludeToGrid(Order = 13, TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string Email { get; set; }

        //[IncludeToGrid(Order = 14, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string BankAccountNumber { get; set; }

        [IncludeToGrid(Order = 15, TreeColumn = true, TreeColumnOrder = 6, ColumnGroup = ColumnGroupEnum.Requisites)]
        public string Bic { get; set; }

        //[IncludeToGrid(Order = 16, IsDisplay = false, ColumnGroup = ColumnGroupEnum.System)]
        public string DatabaseNumber { get; set; }

        public int? DefaultContactPersonId { get; set; }

        //[IncludeToGrid(Order = 17, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string DefaultContactPersonName { get { return DefaultContact?.LastName + DefaultContact?.FirstName; } }

        public ContactPersonDto DefaultContact { get; set; }

        public int FolderId { get; set; }

        //[IncludeToGrid(Order = 18, IsDisplay = false, ColumnGroup = ColumnGroupEnum.General)]
        [IncludeToGrid(Order = 1, TreeColumn = true, TreeColumnOrder = 1, IsDisplay = true, ColumnName = "folderName", ColumnGroup = ColumnGroupEnum.General)]
        public string FolderName
        {
            get
            {
                return Folder?.Name;
            }
        }

        public FolderDto Folder { get; set; }

        #endregion

        public int? VisitingAddressId { get; set; }

        [IncludeToGrid(Order = 19)]
        public string VisitingStreet { get { return VisitingAddress?.Address; } }

        [IncludeToGrid(Order = 20)]
        public string VisitingCity { get { return VisitingAddress?.City; } }

        [IncludeToGrid(Order = 21)]
        public string VisitingRegion { get { return VisitingAddress?.Region; } }

        [IncludeToGrid(Order = 22)]
        //public string VisitingCountry { get; set; }
        //New
        public string VisitingCountry { get { return VisitingAddress?.Country; } }

        public virtual AddressDto VisitingAddress { get; set; }

        public int? PostalAddressId { get; set; }

        public virtual AddressDto PostalAddress { get; set; }

        //public int? BillingAddressId { get; set; }

        //public virtual AddressDto BillingAddress { get; set; }

        #region Address

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.Address)]
        public string Country
        {
            get
            {
                return PostalAddress?.Country;
            }
        }

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.Address)]
        public string City
        {
            get
            {
                return PostalAddress?.City;
            }
        }

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.Address)]
        public string PostalCode
        {
            get
            {
                return PostalAddress?.PostalCode;
            }
        }

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.Address)]
        public string Address
        {
            get
            {
                return PostalAddress?.Address;
            }
        }

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.Address)]
        public string Number
        {
            get
            {
                return PostalAddress?.Number;
            }
        }

        #endregion


        #region Detail

        //[IncludeToGrid(Order = 23, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string Phone2 { get; set; }

        //[IncludeToGrid(Order = 24, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string Email2 { get; set; }

        [IncludeToGrid(Order = 25, TreeColumn = true, TreeColumnOrder = 7, ColumnGroup = ColumnGroupEnum.General)]
        public string Website { get; set; }

        //[IncludeToGrid(Order = 26, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string CocNumber { get; set; }

        //[IncludeToGrid(Order = 27, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string VatIdentificationNumber { get; set; }

        //[IncludeToGrid(Order = 28, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string FiscalCode { get; set; }

        //[IncludeToGrid(Order = 29, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string PurchaseNumber { get; set; }

        //[IncludeToGrid(Order = 30, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string Warning { get; set; }

        //[IncludeToGrid(Order = 30, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string SubjectProjNote { get; set; }

        [IncludeToGrid(Order = 30, ColumnName = "notes", TreeColumn = true, TreeColumnOrder = 7, ColumnGroup = ColumnGroupEnum.Requisites)]
        public string ProjNote { get; set; }

        #endregion

        public List<NoteDto> Notes { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<FileDto> Files { get; set; }

        //#region ADDITIONAL FIELDS FOR GRID

        public int? CompanyTypeId { get; set; }
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
        public string PhonesJson { get; set; }
        public List<string> Phones
        {
            get
            {
                if (this.PhonesJson != null)
                    return JsonConvert.DeserializeObject<List<string>>(this.PhonesJson);
                else
                    return null;
            }
        }
        public string CompanyPhonesJson { get; set; }
        public List<string> CompanyPhones
        {
            get
            {
                if (this.CompanyPhonesJson != null)
                    return JsonConvert.DeserializeObject<List<string>>(this.CompanyPhonesJson);
                else
                    return null;
            }
        }
        public string CompanyShortName { get; set; }
        //[IncludeToGrid(Order = 30, IsDisplay = false, ColumnGroup = ColumnGroupEnum.Detail)]
        public string Inn { get; set; }

        [IncludeToGrid(Order = 31, TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.Requisites)]
        public string Kpp { get; set; }

        [IncludeToGrid(Order = 32, TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.Requisites)]
        public string Ogrn { get; set; }

        [IncludeToGrid(Order = 33, TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.Requisites)]
        public string CheckingAccount { get; set; }

        [IncludeToGrid(Order = 34, TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.Requisites)]
        public string CorrespondentAccount { get; set; }

        [IncludeToGrid(Order = 35, TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.Requisites)]
        public string Bank { get; set; }

        public List<string> Emails {
            get
            {
                if (this.EmailsJson != null)
                    return JsonConvert.DeserializeObject<List<string>>(this.EmailsJson);
                else
                    return null;
            }
        }
        public string EmailsJson { get; set; }
        [IncludeToGrid(Order = 36, IsDisplay = false)]

        //New
        //public DateTime? BirthDate { get; set; }
        public string BirthDate { get; set; }

        [IncludeToGrid(Order = 37, IsDisplay = false)]
        public string Specialization { get; set; }

        public bool? IsCompany { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public bool IsPublic { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public List<CrewMemberShortDto> CrewMembers { get; set; }
    }
}
