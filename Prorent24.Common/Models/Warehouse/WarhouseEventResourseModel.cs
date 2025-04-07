using System.Collections.Generic;

namespace Prorent24.Common.Models.Warehouse
{
    public class WarhouseEventResourseModel
    {
        public WarhouseEventResourseModel()
        {
            Resources = new List<WarhouseResourceModel>();
            Events = new List<WarhouseEventModel>();
        }

        public List<WarhouseResourceModel> Resources { get; set; }
        public List<WarhouseEventModel> Events { get; set; }
    }
}
