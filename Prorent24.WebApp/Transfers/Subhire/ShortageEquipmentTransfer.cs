using Prorent24.BLL.Models.Subhire;
using Prorent24.WebApp.Models.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Subhire
{
    public static class ShortageEquipmentTransfer
    {
        /// <summary>
        /// Transfer from ShortageEquipmentViewModel to ShortageEquipmentDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ShortageEquipmentDto TransferToDto(this ShortageEquipmentViewModel model)
        {
            ShortageEquipmentDto dto = new ShortageEquipmentDto()
            {
                EquipmentId = model.EquipmentId,
                Quantity = model.Quantity,
                ProjectId = model.ProjectId,
            };

            return dto;
        }

    }
}
