using System.Collections.Generic;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelBlockShortModel
    {
        public WorkPanelBlockShortModel()
        {
            CrewTransport = new WorkPanelCrewTransportModel();
            Equipment = new WorkPanelEquipmentModel();
            Timeline = new List<WorkPanelTimeLineModel>();
        }

        public WorkPanelCrewTransportModel CrewTransport { get; set; }
        public WorkPanelEquipmentModel Equipment { get; set; }
        public List<WorkPanelTimeLineModel> Timeline { get; set; }
    }
}
