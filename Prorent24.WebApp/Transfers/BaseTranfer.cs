using Prorent24.BLL.Models;
using Prorent24.WebApp.Models;

namespace Prorent24.WebApp.Transfers
{
    public static class BaseTranfer
    {
        public static BaseListViewModel TransferToViewModel(this BaseListDto dto)
        {
            BaseListViewModel view = new BaseListViewModel()
            {
                Entity = dto.Entity,
                Grid = dto.Grid
            };

            return view;
        }
    }
}
