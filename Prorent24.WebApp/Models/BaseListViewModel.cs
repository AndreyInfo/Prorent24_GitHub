using Prorent24.Common.Models.Grids;
using Prorent24.Enum.Entity;

namespace Prorent24.WebApp.Models
{
    public class BaseListViewModel
    {
        public EntityEnum Entity { get; set; }
        public BaseGrid Grid { get; set; }
    }
}
