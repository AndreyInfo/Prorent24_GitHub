namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelTimeSelectionsModel : WorkPanelStartEndModel
    {
        public WorkPanelTimeSelectionsModel()
        {
            Projects = new WorkPanelStartEndModel();
            Planning = new WorkPanelStartEndModel();
            Todo = new WorkPanelStartEndModel();
        }

        public WorkPanelStartEndModel Projects { get; set; }
        public WorkPanelStartEndModel Planning { get; set; }
        public WorkPanelStartEndModel Todo { get; set; }

    }
}
