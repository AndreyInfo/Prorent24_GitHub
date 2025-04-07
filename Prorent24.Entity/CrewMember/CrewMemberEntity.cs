using Prorent24.Entity.General;
using Prorent24.Entity.Tasks;
using Prorent24.Enum.CrewMember;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prorent24.Entity.CrewMember
{
    [Table("dbo_crew_members")]
    public class CrewMemberEntity : ICloneable // : BaseEntity
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string ColorAppointments { get; set; }

        public string Description { get; set; }

        public int? FolderId { get; set; }
        [ForeignKey("FolderId")]
        public virtual FolderEntity Folder { get; set; }

        // Details
        public AvailabilityCrewMemberTypeEnum Availability { get; set; }

        public bool DisplayInPlanner { get; set; }

        public string DrivingLicense { get; set; }

        public string EmergencyContact { get; set; }

        public string ReceiveEmails { get; set; }

        // Login information to AspNetUsers
        [Column(TypeName = "BOOLEAN")]
        public bool Active { get; set; }

        [Column(TypeName = "BOOLEAN")]
        public bool IsPowerUser { get; set; }

        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual AddressEntity Address { get; set; }

        public int? DefaultRateId { get; set; }

        // [InverseProperty("CrewMember")]
        [ForeignKey("DefaultRateId")]
        public virtual CrewMemberRateEntity DefaultRate { get; set; }

        // Administrative
        public string BankAccountNumber { get; set; }

        public DateTime? Birthdate { get; set; }

        public string CoCNumber { get; set; }

        public string CompanyName { get; set; }

        public DateTime? ContractDate { get; set; }

        public int? HoursInContract { get; set; }

        public string PassportNumber { get; set; }

        public string PassportCompany { get; set; }

        public DateTime? PassportDate { get; set; }

        // VAT identification number
        public string VAT { get; set; }

        public virtual ICollection<NoteEntity> Notes { get; set; }
        //public virtual ICollection<TagEntity> Tags { get; set; }
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<FileEntity> Files { get; set; }

        public virtual ICollection<TagBondEntity> Tags { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? LastModifiedDate { get; set; }

        public string CreationUserId { get; set; }

        [ForeignKey("CreationUserId")]
        public virtual User CreationUser { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string SocialNetworksJson { get; set; }
    }
}
