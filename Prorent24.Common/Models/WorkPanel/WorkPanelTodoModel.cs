using System.Collections.Generic;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelTodoModel
    {
        public WorkPanelTodoModel()
        {
            OpenTasks = new WorkPanelOpenModel();
            OpenInvoices = new WorkPanelOpenModel();
            CriticalStockData = new Dictionary<string, string>();
            OpenRepairsData = new Dictionary<string, string>();
        }

        public int Subhireoption { get; set; }
        public int CriticalStock { get; set; }
        public Dictionary<string, string> CriticalStockData { get; set; }
        public int UnassignedNotes { get; set; }
        public int OpenRepairs { get; set; }
        public Dictionary<string,string> OpenRepairsData { get; set; }
        public WorkPanelOpenModel OpenTasks { get; set; }
        public WorkPanelOpenModel OpenInvoices { get; set; }
    }
}
