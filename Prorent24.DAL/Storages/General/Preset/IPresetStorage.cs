using Prorent24.Entity.General;
using Prorent24.Enum.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Preset
{
    public interface IPresetStorage : IBaseStorage<PresetEntity>
    {
        Task<List<PresetEntity>> GetPresets(ModuleTypeEnum moduleType);
    }
}
