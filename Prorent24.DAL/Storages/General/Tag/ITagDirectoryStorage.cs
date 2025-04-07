using Prorent24.Entity.General;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.General.Tag
{
    public interface ITagDirectoryStorage:IBaseStorage<TagDirectoryEntity>
    {
        Task<TagDirectoryEntity> GetByNameAndBelong(string name, ModuleTypeEnum module);
        Task<TagBondEntity> CreateTagBond(TagBondEntity model);
        Task<List<TagBondEntity>> GetTagBonds(ModuleTypeEnum module);
        Task<TagBondEntity> GetTagBond(ModuleTypeEnum module, TagBondEntity model);
        Task<TagBondEntity> GetTagBondById(int id);
        Task<List<TagDirectoryEntity>> GetListTags(ModuleTypeEnum menuType);
        Task<List<TagDirectoryEntity>> SearchListTags(ModuleTypeEnum moduleType, string search_string);
        Task<bool> DeleteBond(TagBondEntity entity);
        Task<List<TagBondEntity>> GetListBondByDirectoryId(int tagDirectoryId);
        Task<bool> DeleteDirectoryTag(TagDirectoryEntity entity);
    }
}
