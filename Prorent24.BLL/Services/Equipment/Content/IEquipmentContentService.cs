using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Services.Equipment.Content
{
    public interface IEquipmentContentService: IBaseService<EquipmentContentDto, int>, IForeignDependencyService<EquipmentContentDto>
    {

    }
}
