using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Common.Models.WorkPanel
{
    public class WorkPanelPlanningModel
    {
        public WorkPanelPlanningModel()
        {
            UnscheduledCrew = new WorkPanelOptiontConfirmedModel();
            UnscheduledTransport = new WorkPanelOptiontConfirmedModel();
            NotEnoughCrew = new WorkPanelOptiontConfirmedModel();
            NotEnoughTransport = new WorkPanelOptiontConfirmedModel();
            OpenInvitations = new WorkPanelOptiontConfirmedModel();
        }

        public WorkPanelOptiontConfirmedModel UnscheduledCrew { get; set; }
        public WorkPanelOptiontConfirmedModel UnscheduledTransport { get; set; }
        public WorkPanelOptiontConfirmedModel NotEnoughCrew { get; set; }
        public WorkPanelOptiontConfirmedModel NotEnoughTransport { get; set; }
        public WorkPanelOptiontConfirmedModel OpenInvitations { get; set; }
    }
}
