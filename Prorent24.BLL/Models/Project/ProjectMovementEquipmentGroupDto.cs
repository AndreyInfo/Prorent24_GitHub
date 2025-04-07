using Prorent24.Enum.Project;

namespace Prorent24.BLL.Models.Project
{
    public class ProjectMovementEquipmentGroupDto
    {
        public string GroupName { get; set; }
        public int EqupmentName { get; set; }
        public int ProjectEquipmentId { get; set; }
        public int DeficitQuantity { get; set; }
        public int SelectedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int TotalQuantity { get; set; }
        public ProjectMovementStatusEnum MovementStatus { get; set; }
    }
}
