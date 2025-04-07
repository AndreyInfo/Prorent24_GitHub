using MediatR;
using System;

namespace Prorent24.BLL.Models.Handler
{
    public class EquipmentReservedHandlerModel : INotification
    {
        public int ProjectEquipmentId { get; set; }
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public int Quantity { get; set; }
    }
}
