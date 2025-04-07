
using System;

namespace Prorent24.Common.Models.Warehouse
{
    public class WarhouseEventModel
    {
        public string Id { get; set; } = Guid.NewGuid().GetHashCode().ToString().Replace("-", string.Empty);
        public string EntityId { get; set; }
        public string Resource { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Text { get; set; }
       // public string BackColor { get; set; } = "#E6E6E6";

        public string Type { get; set; }
        public int? NeedQuantity { get; set; }
        public int? PlannedQuantity { get; set; }
    }
}
