using Prorent24.Common.Attributes;

namespace Prorent24.BLL.Models.Configuration.Financial
{
    public class AdditionalConditionDto : BaseDto<int>
    {
        [IncludeToGrid(Order = 5)]
        public string Name { get; set; }

        [IncludeToGrid(Order = 6)]
        public string Text { get; set; }
    }
}
