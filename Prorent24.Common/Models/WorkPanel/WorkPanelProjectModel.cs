using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelProjectModel
    {
        public WorkPanelProjectModel()
        {
            OpenTasks = new WorkPanelOpenModel();
        }

        public int ProjectsOptions { get; set; }
        public Dictionary<string, string> ProjectsOptionsData { get; set; }
        public int ProjectsInvoice { get; set; }
        public Dictionary<string, string> ProjectsInvoiceData { get; set; }
        public int ProjectRequests { get; set; }
        public Dictionary<string, string> ProjectRequestsData { get; set; }
        public int CancelledWithCrew { get; set; }
        public Dictionary<string, string> CancelledWithCrewData { get; set; }
        public int CancelledWithTransport { get; set; }
        public Dictionary<string, string> CancelledWithTransportData { get; set; }
        public WorkPanelOpenModel OpenTasks { get; set; }
    }
}
