using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.Configuration.Settings;
using Prorent24.WebApp.Models.Contact;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Models.Project
{
    public class ProjectViewModel //:IValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectStatusEnum Status { get; set; }

        public int? ClientContactId { get; set; }

        public virtual ContactViewModel ClientContact { get; set; }

        //public string ClientName { get; set; }

        public int? ClientContactPersonId { get; set; }

        public virtual ContactPersonViewModel ClientContactPerson { get; set; }

        public int? LocationContactId { get; set; }

        public virtual ContactViewModel LocationContact { get; set; }

        //public string LocationName { get; set; }

        public int? LocationContactPersonId { get; set; }

        public virtual ContactPersonViewModel LocationContactPerson { get; set; }

        #region Details
        public int? TypeId { get; set; }
        public ProjectTypeViewModel Type { get; set; }

        public string AccountManagerId { get; set; }

        public virtual CrewMemberViewModel AccountManager { get; set; }

        public string Color { get; set; }

        public string Reference { get; set; }
        #endregion

        public virtual List<ProjectTimeViewModel> Times { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public List<CrewMemberShortViewModel> CrewMembers { get; set; }
    }
}
