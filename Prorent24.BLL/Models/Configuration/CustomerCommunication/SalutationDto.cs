using Prorent24.Common.Attributes;

namespace Prorent24.BLL.Models.Configuration.CustomerCommunication
{
    public class SalutationDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6)]
        public string DisplayView { get; set; }
    }
}
