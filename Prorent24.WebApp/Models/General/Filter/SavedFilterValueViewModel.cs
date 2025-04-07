using Prorent24.Enum.Directory;
using System;

namespace Prorent24.WebApp.Models.General.Filter
{
    /// <summary>
    /// Filter Value
    /// </summary>
    public class SavedFilterValueViewModel
    {
        /// <summary>
        /// Property from enum 
        /// </summary>
        public PropertyEnum Property { get; set; }

        /// <summary>
        /// Id crew member
        /// </summary>
        public string CrewMemberId { get; set; }

        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// User Role Id
        /// </summary>
        public int UserRoleId { get; set; }
    }
}
