using Prorent24.Enum.General;

namespace Prorent24.BLL.Models.General.Filter
{
    public class FilterListDto
    {
        public FilterEnum FilterType { get; set; }
        public object Data { get; set; }
    }
}
