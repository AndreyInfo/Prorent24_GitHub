using Microsoft.AspNetCore.Identity;
using Prorent24.Entity.Identity;
using System;
using System.Collections.Generic;

namespace Prorent24.Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? LastLogin { get; set; }

        public virtual ICollection<UserRoleEntity> Roles { get; set; }

        public bool? IsSystemUser { get; set; }


        public string ProfileImage { get; set; }

        public bool Removed { get; set; }

    }
}
