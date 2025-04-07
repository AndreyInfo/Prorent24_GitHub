using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Subhire;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Subhire
{
    public class SubhireViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SubhireStatusEnum Status { get; set; }

        public int? SupplierContactId { get; set; }

        public string SupplierContactCompany { get; set; }

        public virtual ContactViewModel SupplierContact { get; set; }

        public int? SupplierContactPersonId { get; set; }
        public virtual ContactPersonViewModel SupplierContactPerson { get; set; }
        public int? LocationContactId { get; set; }
        public virtual ContactViewModel LocationContact { get; set; }


        public int? LocationContactPersonId { get; set; }
        public virtual ContactPersonViewModel LocationContactPerson { get; set; }

        #region Details
        public string AccountManagerId { get; set; }

        public virtual CrewMemberViewModel AccountManager { get; set; }

        public string Reference { get; set; }

        public decimal EquipmentCost { get; set; }

        public decimal AdditionalCost { get; set; }

        public decimal TotalCost { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]

        public SubhireTypeEnum Type { get; set; }

        public string Remark { get; set; }
        #endregion

        #region TimeSchedule

        public DateTime? UsagePeriodStart { get; set; }


        public DateTime? UsagePeriodEnd { get; set; }


        public DateTime PlanningPeriodStart { get; set; }


        public DateTime PlanningPeriodEnd { get; set; }


        public DateTime? DeliveryCollectionStart { get; set; }


        public DateTime? DeliveryCollectionEnd { get; set; }
        #endregion
        public int? ProjectId { get; set; }
        
        public string ProjectName { get; set; }

       
    }
}
