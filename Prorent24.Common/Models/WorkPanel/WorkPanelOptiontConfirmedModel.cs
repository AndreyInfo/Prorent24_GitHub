using System.Collections.Generic;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelOptiontConfirmedModel
    {
        public WorkPanelOptiontConfirmedModel()
        {
            OptionData = new Dictionary<string, string>();
            ConfirmedData = new Dictionary<string, string>();
        }

        public int Option { get; set; }
        public Dictionary<string,string> OptionData { get; set; }
        public int Confirmed { get; set; }
        public Dictionary<string, string> ConfirmedData { get; set; }
    }
}
