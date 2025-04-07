using Prorent24.Enum.Directory;
using System;

namespace Prorent24.BLL.Models.General.Filter
{
    public class SavedFilterValueDto
    {
        public PropertyEnum Property { get; set; }
        public string CrewMemberId { get; set; }
        public DateTime Date { get; set; }
        public int UserRoleId { get; set; }
    }
}
