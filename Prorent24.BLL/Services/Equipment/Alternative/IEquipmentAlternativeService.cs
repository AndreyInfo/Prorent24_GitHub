using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Equipment.Alternative
{
    public interface IEquipmentAlternativeService: IBaseService<EquipmentAlternativeDto, int>, IForeignDependencyService<EquipmentAlternativeDto>
    {
       
    }
}
