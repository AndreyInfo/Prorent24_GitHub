using Prorent24.Enum.Entity;
using Prorent24.Enum.General;

namespace Prorent24.BLL.Models.General.Tag
{
    public class TagDto : BaseDto<int>
    {
        public int DirectoryId { get; set; }
        public string Name { get; set; }
        public object ReferenceId { get; set; }
        public ModuleTypeEnum Entity { get; set; }
        public bool Selected { get; set; }
    }
}
