using System.Collections.Generic;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelOpenModel
    {
        public WorkPanelOpenModel()
        {
            OpenData = new Dictionary<string, string>();
            ExpiredData = new Dictionary<string, string>();
        }

        public Dictionary<string,string> OpenData { get; set; }
        public Dictionary<string, string> ExpiredData { get; set; }
        public int Expired { get; set; }
        public int Open { get; set; }
    }
}
