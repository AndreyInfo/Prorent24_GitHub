using Prorent24.BLL.Models.General.Tag;
using Prorent24.Entity.General;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.General
{
    internal static class TagTransfer
    {
        /// <summary>
        /// Transfer from List<TagEntity> to List<TagDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TagDto> TransferToListDto(this List<TagEntity> entities)
        {
            List<TagDto> tags = entities.Select(x => new TagDto()
            {
                Id = x.Id,
                Name = x.Name,
                Entity = x.BelongsTo,
                ReferenceId = x.GetEntityId(),
                CreationDate = x.CreationDate,
                LastModifiedDate = x.LastModifiedDate
            }).ToList();

            return tags;
        }

        private static object GetEntityId(this TagEntity tagEntity)
        {
            switch (tagEntity.BelongsTo)
            {
                case ModuleTypeEnum.CrewMembers:
                    {
                        return tagEntity.CrewMemberId;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        return tagEntity.ContactId;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        return tagEntity.VehicleId;
                    }
                case ModuleTypeEnum.Tasks:
                    {
                        return tagEntity.TaskId;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        return tagEntity.EquipmentId;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        /// <summary>
        /// Transfer from TagEntity to TagDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public static TagDto TransferToDto(this TagEntity entity)
        //{
        //    TagDto tag = new TagDto()
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //        Entity = entity.BelongsTo,
        //        CreationDate = entity.CreationDate,
        //        LastModifiedDate = entity.LastModifiedDate
        //    };

        //    switch (entity.BelongsTo)
        //    {
        //        case ModuleTypeEnum.CrewMembers:
        //            {
        //                tag.ReferenceId = entity.CrewMemberId;
        //                break;
        //            }
        //        case ModuleTypeEnum.Contacts:
        //            {
        //                tag.ReferenceId = entity.ContactId.Value;
        //                break;
        //            }
        //        case ModuleTypeEnum.Vehicles:
        //            {
        //                tag.ReferenceId = entity.VehicleId.Value;
        //                break;
        //            }
        //        case ModuleTypeEnum.Tasks:
        //            {
        //                tag.ReferenceId = entity.TaskId.Value;
        //                break;
        //            }
        //        case ModuleTypeEnum.Equipment:
        //            {
        //                tag.ReferenceId = entity.EquipmentId.Value;
        //                break;
        //            }
        //    }

        //    return tag;
        //}

        /// <summary>
        /// Transfer from List<TagDto> to List<TagEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<TagEntity> TransferToListEntity(this List<TagDto> dto)
        {
            List<TagEntity> entities = dto.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }

        /// <summary>
        /// Transfer from TagDto to TagEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TagEntity TransferToEntity(this TagDto dto)
        {
            TagEntity tag = new TagEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                BelongsTo = dto.Entity,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            switch (dto.Entity)
            {
                case ModuleTypeEnum.CrewMembers:
                    {
                        tag.CrewMemberId = (string)dto.ReferenceId;
                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        tag.ContactId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        tag.VehicleId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Tasks:
                    {
                        tag.TaskId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        tag.EquipmentId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
            }

            return tag;
        }

        /// <summary>
        /// Transfer from TagDto to TagEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TagBondEntity TransferToTagBondEntity(this TagDto dto)
        {
            TagBondEntity tag = new TagBondEntity()
            {
                Id = dto.Id,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            switch (dto.Entity)
            {
                case ModuleTypeEnum.CrewMembers:
                    {
                        tag.CrewMemberId = (string)dto.ReferenceId;
                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        tag.ContactId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        tag.VehicleId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Tasks:
                    {
                        tag.TaskId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        tag.EquipmentId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Invoices:
                    {
                        tag.InvoiceId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Subhire:
                    {
                        tag.SubhireId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Projects:
                    {
                        tag.ProjectId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Repairs:
                    {
                        tag.RepairId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
                case ModuleTypeEnum.Inspections:
                    {
                        tag.InspectionId = Convert.ToInt32(dto.ReferenceId);
                        break;
                    }
            }

            return tag;
        }

        /// <summary>
        /// Transfer from TagEntity to TagDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TagDto TransferToTagDto(this TagBondEntity entity)
        {

            TagDto tag = new TagDto()
            {
                Id = entity.Id,
                DirectoryId = entity.TagDirectoryId,
                Name = entity.TagDirectory?.Name,
                Entity = entity.TagDirectory.BelongsTo,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };

            switch (entity.TagDirectory?.BelongsTo)
            {
                case ModuleTypeEnum.CrewMembers:
                    {
                        tag.ReferenceId = entity.CrewMemberId;
                        break;
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        tag.ReferenceId = entity.ContactId.Value;
                        break;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        tag.ReferenceId = entity.VehicleId.Value;
                        break;
                    }
                case ModuleTypeEnum.Tasks:
                    {
                        tag.ReferenceId = entity.TaskId.Value;
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        tag.ReferenceId = entity.EquipmentId.Value;
                        break;
                    }
                case ModuleTypeEnum.Invoices:
                    {
                        tag.ReferenceId = entity.InvoiceId.Value;
                        break;
                    }
                case ModuleTypeEnum.Subhire:
                    {
                        tag.ReferenceId = entity.SubhireId.Value;
                        break;
                    }
                case ModuleTypeEnum.Projects:
                    {
                        tag.ReferenceId = entity.ProjectId.Value;
                        break;
                    }
                case ModuleTypeEnum.Repairs:
                    {
                        tag.ReferenceId = entity.RepairId.Value;
                        break;
                    }
                case ModuleTypeEnum.Inspections:
                    {
                        tag.ReferenceId = entity.InspectionId.Value;
                        break;
                    }
            }

            return tag;
        }

        /// <summary>
        /// Transfer from List<TagBondEntity> to List<TagDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TagDto> TransferToListTagDto(this List<TagBondEntity> entities)
        {
            List<TagDto> tags = entities.Select(x => x.TransferToTagDto()).ToList();

            return tags;
        }

        /// <summary>
        /// Transfer from List<TagDto> to List<TagBondEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<TagBondEntity> TransferToListTagBondEntity(this List<TagDto> dto)
        {
            List<TagBondEntity> entities = dto.Select(x => x.TransferToTagBondEntity()).ToList();
            return entities;
        }

        /// <summary>
        /// Transfer from TagDirectoryEntity to TagDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TagDto TransferToDto(this TagDirectoryEntity entity)
        {
            TagDto tag = new TagDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Entity = entity.BelongsTo,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };
            return tag;
        }

        /// <summary>
        /// Transfer from List<TagDirectoryEntity> to List<TagDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TagDto> TransferToListDto(this List<TagDirectoryEntity> entities)
        {
            List<TagDto> tags = entities.Select(x => x.TransferToDto()).ToList();
            return tags;
        }
    }
}
