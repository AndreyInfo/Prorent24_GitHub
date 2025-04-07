using Prorent24.BLL.Models.General.File;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.File;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.General
{
    internal static class FileTransfer
    {

        /// <summary>
        /// Transfer from FileViewModel to FileDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static FileDto TransferToDto(this FileViewModel view)
        {
            FileDto dto = new FileDto()
            {
                Id = view.Id,
                Name = view.Name,
                BelongsTo = view.BelongsTo,
                Confidential = view.Confidential,
                Description = view.Description,
                Path = view.Path,
                TaskId = view.BelongsTo == ModuleTypeEnum.Tasks ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                ContactId = view.BelongsTo == ModuleTypeEnum.Contacts ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                CrewMemberId = view.BelongsTo == ModuleTypeEnum.CrewMembers ? view.ReferenceId : null,
                VehicleId = view.BelongsTo == ModuleTypeEnum.Vehicles ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                EquipmentId = view.BelongsTo == ModuleTypeEnum.Equipment ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                RepairId = view.BelongsTo == ModuleTypeEnum.Repairs ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                InspectionId = view.BelongsTo == ModuleTypeEnum.Inspections ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                SubhireId = view.BelongsTo == ModuleTypeEnum.Subhire ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                ProjectId = view.BelongsTo == ModuleTypeEnum.Projects ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null
            };

            return dto;
        }

        /// <summary>
        /// Transfer from FileDto to FileViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static FileViewModel TransferToView(this FileDto dto)
        {
            FileViewModel view = new FileViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                BelongsTo = dto.BelongsTo,
                Confidential = dto.Confidential,
                Description = dto.Description,
                ReferenceId = dto.GetReferenceId()
                //Path = dto.Path
            };

            return view;
        }

        public static string GetReferenceId(this FileDto dto)
        {
            switch (dto.BelongsTo)
            {
                case ModuleTypeEnum.Tasks:
                    {
                        return dto.TaskId?.ToString();
                    }
                case ModuleTypeEnum.Contacts:
                    {
                        return dto.ContactId?.ToString();
                    }
                case ModuleTypeEnum.CrewMembers:
                    {
                        return dto.CrewMemberId;
                    }
                case ModuleTypeEnum.Vehicles:
                    {
                        return dto.VehicleId?.ToString();
                    }
                case ModuleTypeEnum.Projects:
                    {
                        return dto.ProjectId?.ToString();
                    }
                case ModuleTypeEnum.Subhire:
                    {
                        return dto.SubhireId?.ToString();
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        return dto.EquipmentId?.ToString();
                    }
                case ModuleTypeEnum.Repairs:
                    {
                        return dto.RepairId?.ToString();
                    }
                case ModuleTypeEnum.Inspections:
                    {
                        return dto.InspectionId?.ToString();
                    }
                default:
                    {
                        break;
                    }
            }
            return string.Empty;
        }


        /// <summary>
        /// Transfer from List<FileDto> to List<FileViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<FileViewModel> TransferToListView(this List<FileDto> dtos)
        {
            List<FileViewModel> views = dtos.Select(x => x.TransferToView()).ToList();
            return views;
        }

    }
}
