using Prorent24.Common.Models.Grids;
using Prorent24.Enum.Entity;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Models
{
    public class BaseListDto
    {
        public BaseGrid Grid { get; set; }
        public EntityEnum Entity { get; set; }

        public bool View { get; set; } = true;
        public bool Add { get; set; } = true;
        public bool Edit { get; set; } = true;
        public bool Delete { get; set; } = true;
    }
}
