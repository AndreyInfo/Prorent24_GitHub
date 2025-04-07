using System.Collections.Generic;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelBlockModel
    {
        public WorkPanelBlockModel()
        {
            CrewTransport = new WorkPanelCrewTransportModel();
            Equipment = new WorkPanelEquipmentModel();
            Todo = new WorkPanelTodoModel();
            Projects = new WorkPanelProjectModel();
            Planning = new WorkPanelPlanningModel();
            Timeline = new List<WorkPanelTimeLineModel>();
        }

        public WorkPanelCrewTransportModel CrewTransport { get; set; }
        public WorkPanelEquipmentModel Equipment { get; set; }
        public WorkPanelTodoModel Todo { get; set; }
        public WorkPanelProjectModel Projects { get; set; }
        public WorkPanelPlanningModel Planning { get; set; }
        public List<WorkPanelTimeLineModel> Timeline { get; set; }
    }
}
