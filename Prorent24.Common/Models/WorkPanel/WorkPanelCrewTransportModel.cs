using System.Collections.Generic;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelCrewTransportModel
    {
        public WorkPanelCrewTransportModel()
        {
            PlannedCrewData = new Dictionary<string, string>();
            ProjectsWithPlanningData = new Dictionary<string, string>();
            PlannedTransportData = new Dictionary<string, string>();
        }

        public int PlannedCrew { get; set; }
        public Dictionary<string,string> PlannedCrewData { get; set; }

        public int ProjectsWithPlanning { get; set; }
        public Dictionary<string, string> ProjectsWithPlanningData { get; set; }

        public int PlannedTransport { get; set; }
        public Dictionary<string, string> PlannedTransportData { get; set; }
    }
}
