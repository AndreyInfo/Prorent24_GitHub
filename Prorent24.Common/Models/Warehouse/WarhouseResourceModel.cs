using System;
using System.Collections.Generic;

namespace Prorent24.Common.Models.Warehouse
{
    public class WarhouseResourceModel
    {
        public WarhouseResourceModel()
        {
            Availabilities = new List<AvailabilityResourceModel>();
            Children = new List<WarhouseResourceModel>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<AvailabilityResourceModel> Availabilities { get; set; }
        public List<WarhouseResourceModel> Children { get; set; } //like project planning

    }

    public class AvailabilityResourceModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }

    }
    
}
