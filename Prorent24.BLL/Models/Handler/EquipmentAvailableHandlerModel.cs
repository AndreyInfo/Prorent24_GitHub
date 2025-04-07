using MediatR;
using Prorent24.Enum.Entity;
using System;

namespace Prorent24.BLL.Models.Handler
{
    public class EquipmentAvailableHandlerModel : INotification
    {
        public EquipmentAvailableEnum Type { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime ReservedFrom { get; set; }
        public DateTime ReservedUntil { get; set; }
        public bool Reserved { get; set; }
    }
}
