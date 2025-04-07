using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Services.Abstract;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Equipment.Stock
{
    public interface IEquipmentStockService: IBaseService<EquipmentStockDto, int>, IForeignDependencyService<EquipmentStockDto>
    {
        Task<EquipmentStockDto> Correct(EquipmentStockCorrectDto dto);
    }
}
