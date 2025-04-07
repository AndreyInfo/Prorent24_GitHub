using MediatR;
using Prorent24.BLL.Models.Handler;
using Prorent24.DAL.Storages.Equipment.EquipmentReserved;
using Prorent24.Entity.Equipment;
using System.Threading;
using System.Threading.Tasks;

namespace Prorent24.BLL.Handlers.EquipmentHandlers
{
    public class ReserveEquipmentHandler : INotificationHandler<EquipmentReservedHandlerModel>
    {
        private readonly IEquipmentReservedStorage _equipmentReservedStorage;

        public ReserveEquipmentHandler(IEquipmentReservedStorage equipmentReservedStorage)
        {
            _equipmentReservedStorage = equipmentReservedStorage;
        }

        public async Task Handle(EquipmentReservedHandlerModel notification, CancellationToken cancellationToken)
        {
            await _equipmentReservedStorage.PutInReserve(new EquipmentReservedEntity()
            {
                ProjectEquipmentId = notification.ProjectEquipmentId,
                From = notification.From,
                Until = notification.Until,
                Quantity = notification.Quantity
            });
        }
    }
}
