using Prorent24.BLL.Models;
using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Transfers.General;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Storages.General.Tag;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Prorent24.BLL.Services.General.Tag
{
    internal class TagService : ITagService
    {
        private readonly ITagStorage _tagStorage;
        private readonly ITagDirectoryStorage _tagDirectoryStorage;

        public TagService(ITagStorage tagStorage, ITagDirectoryStorage tagDirectoryStorage)
        {
            _tagStorage = tagStorage;
            _tagDirectoryStorage = tagDirectoryStorage;
        }

        /// <summary>
        /// Get list tags
        /// </summary>
        /// <returns></returns>
        public async Task<List<TagDto>> GetTags(ModuleTypeEnum moduleType)
        {
            List<TagBondEntity> bonds = await _tagDirectoryStorage.GetTagBonds(moduleType);
            switch (moduleType)
            {
                case ModuleTypeEnum.Tasks:
                    {
                        bonds = bonds.Where(x => x.Task != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        bonds = bonds.Where(x => x.Contact != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.CrewMembers:
                    {
                        bonds = bonds.Where(x => x.CrewMembers != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        bonds = bonds.Where(x => x.Vehicle != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.Projects:
                    {
                        bonds = bonds.Where(x => x.Project != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.Subhire:
                    {
                        bonds = bonds.Where(x => x.Subhire != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        bonds = bonds.Where(x => x.Equipment != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.Repairs:
                    {
                        bonds = bonds.Where(x => x.Repair != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.Inspections:
                    {
                        bonds = bonds.Where(x => x.Inspection != null).ToList();
                        break;
                    }
                case ModuleTypeEnum.Invoices:
                    {
                        bonds = bonds.Where(x => x.Invoice != null).ToList();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            List<TagDirectoryEntity> listEntities = await _tagDirectoryStorage.GetListTags(moduleType);
            listEntities = listEntities.Where(x => bonds.Any(b => b.TagDirectoryId.Equals(x.Id))).ToList();
            List<TagDto> listDtos = listEntities.TransferToListDto();
            return listDtos;
        }


        public async Task<List<TagDto>> SearchTags(ModuleTypeEnum moduleType, string search_string)
        {
            List<TagDirectoryEntity> listEntities = await _tagDirectoryStorage.SearchListTags(moduleType, search_string);
            List<TagDto> listDtos = listEntities.TransferToListDto();
            return listDtos;
        }

        public async Task<TagDto> CreateTag(TagDto model)
        {
            TagDirectoryEntity tag = await _tagDirectoryStorage.GetByNameAndBelong(model.Name.ToLower(), model.Entity);
            TagDirectoryEntity createDirectoryTag = new TagDirectoryEntity();
            if (tag == null)
            {
                createDirectoryTag = await _tagDirectoryStorage.Create(new TagDirectoryEntity()
                {
                    Name = model.Name,
                    LowerName = model.Name.ToLower(),
                    BelongsTo = model.Entity
                });
            }
            TagBondEntity transfer = model.TransferToTagBondEntity();
            transfer.TagDirectoryId = tag != null ? tag.Id : createDirectoryTag.Id;

            TagBondEntity entity = await _tagDirectoryStorage.GetTagBond(model.Entity, transfer);
            if (entity == null)
            {
                TagBondEntity createBond = await _tagDirectoryStorage.CreateTagBond(transfer);
                TagBondEntity bond = await _tagDirectoryStorage.GetTagBondById(createBond.Id);
                //bond.Id = bond.TagDirectoryId;//переприсвоюємо на значення із діректорі тегів, щоб далі коректно пошук відпрацьовував
                return bond.TransferToTagDto();
            }
            else
            {

                var tagDto = entity.TransferToTagDto();
                //tagDto.Id = entity.TagDirectoryId;
                return tagDto;
            }
        }



        /// <summary>
        /// Delete Tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            TagBondEntity bond = await _tagDirectoryStorage.GetTagBondById(id);
            if (bond != null)
            {
                List<TagBondEntity> bonds = await _tagDirectoryStorage.GetListBondByDirectoryId(bond.TagDirectoryId);
                bool result = await _tagDirectoryStorage.DeleteBond(bond);
                if (bonds?.Count <= 1)
                {
                    bool directoryTag = await _tagDirectoryStorage.DeleteDirectoryTag(bond.TagDirectory);
                }

                return result;
            }
            else
            {
                return true;
            }
        }
    }
}
