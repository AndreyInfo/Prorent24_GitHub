namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelEquipmentModel
    {
        public WorkPanelEquipmentModel()
        {
            Overtime = new WorkPanelInOutModel();
            Subhire = new WorkPanelInOutModel();
            Dryhire = new WorkPanelInOutModel();
            Project = new WorkPanelInOutModel();
        }

        public WorkPanelInOutModel Overtime { get; set; }
        public WorkPanelInOutModel Subhire { get; set; }
        public WorkPanelInOutModel Dryhire { get; set; }
        public WorkPanelInOutModel Project { get; set; }
    }
}
