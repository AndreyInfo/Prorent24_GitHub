using Prorent24.Common.Models.WorkPanel;

namespace Prorent24.BLL.Models.WorkPanel
{
    public class WorkPanelModel
    {
        public WorkPanelModel()
        {
            BlockData = new WorkPanelBlockModel();
            TimeSelections = new WorkPanelTimeSelectionsModel();
            Today = new WorkPanelToday();
            Tomorrow = new WorkPanelToday();
        }

        public WorkPanelBlockModel BlockData { get; set; }
        public WorkPanelTimeSelectionsModel TimeSelections { get; set; }
        public WorkPanelToday Today { get; set; }
        public WorkPanelToday Tomorrow { get; set; }
    }

    public class WorkPanelToday
    {
        public WorkPanelToday()
        {
            BlockData = new WorkPanelBlockShortModel();
        }

        public string Date { get; set; }

        public WorkPanelBlockShortModel BlockData { get; set; }
    }

    public class WorkPanelTomorrow
    {
        public WorkPanelTomorrow()
        {
            BlockData = new WorkPanelBlockShortModel();
        }

        public WorkPanelBlockShortModel BlockData { get; set; }
    }
}
