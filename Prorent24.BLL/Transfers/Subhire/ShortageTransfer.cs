using Prorent24.BLL.Models.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Subhire
{
    public static class ShortageTransfer
    {
        public static SubhireShortageGridDto TransferToTreeGridDto(this SubhireShortageDto dto, List<SubhireShortageDto> children=null)
        {
            SubhireShortageGridDto model = new SubhireShortageGridDto()
            {
                EquipmentId = dto.EquipmentId,
                ProjectId = dto.ProjectId,
                ProjectName = dto.ProjectName,
                EquipmentName = dto.EquipmentName,
                PlannedQuantity = dto.PlannedQuantity,
                OwnStockQuantity = dto.OwnStockQuantity,
                Explanation = dto.Explanation,
                StartUsePeriod = dto.StartUsePeriod,
                EndUsePeriod = dto.EndUsePeriod,
                SubhireIds = dto.SubhireIds,
                ShortageQuantity = dto.ShortageQuantity,
                SubhiredQuantity = dto.SubhiredQuantity,
                Childrens = children?.TransferToTreeGridDto(),
            };

            return model;
        }
        
        public static List<SubhireShortageGridDto> TransferToTreeGridDto(this List<SubhireShortageDto> dtos)
        {
            List<SubhireShortageGridDto> dto = dtos.Select(x => x.TransferToTreeGridDto(x.Childrens)).ToList();
            return dto;
        }
    }
}
