using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Tasks;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.Tasks;
using Prorent24.WebApp.Transfers.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Tasks
{
    public static class TaskTransfer
    {
        /// <summary>
        /// Transfer from List<TaskViewModel> to List<TaskDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<TaskDto> TransferToListDto(this List<TaskViewModel> entities)
        {
            List<TaskDto> taskDtos = entities.Select(x => x.TransferToDto()).ToList();

            return taskDtos;
        }

        /// <summary>
        /// Transfer from TaskDto to TaskViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TaskDto TransferToDto(this TaskViewModel view)
        {
            TaskDto taskDto = new TaskDto()
            {
                Id = view.Id,
                Name = view.Name,
                CrewMembers = view.CrewMembers != null ? view.CrewMembers.TransferToListDto() : new List<CrewMemberShortDto>(),
                Executors = view.Executors != null ? view.Executors.TransferToListDto() : new List<CrewMemberShortDto>(),
                Description = view.Description,
                BelongsTo = view.BelongsTo,

                //New
                //DeadLine = view.DeadLine,
                DeadLine = view.DeadLine != null ? ((DateTime)view.DeadLine).ToString("dd.MM.yyyy hh:mm") : null,
                //CompletedIn = view.CompletedIn,

                IsPublic = view.IsPublic,
                ContactId = view.BelongsTo == ModuleTypeEnum.Contacts ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                CrewMemberId = view.BelongsTo == ModuleTypeEnum.CrewMembers ? view.ReferenceId : null,
                VehicleId = view.BelongsTo == ModuleTypeEnum.Vehicles ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                EquipmentId = view.BelongsTo == ModuleTypeEnum.Equipment ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                InspectionId = view.BelongsTo == ModuleTypeEnum.Inspections ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                RepairId = view.BelongsTo == ModuleTypeEnum.Repairs ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                InvoiceId = view.BelongsTo == ModuleTypeEnum.Invoices ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
            };

            return taskDto;
        }

        /// <summary>
        /// Transfer from TaskViewModel to TaskDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TaskViewModel TransferToViewModel(this TaskDto dto)
        {
            TaskViewModel taskViewModel = new TaskViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Author = string.IsNullOrWhiteSpace(dto.LastName) ? dto.FirstName : string.Concat(dto.FirstName, " ", dto.LastName),
                CompletedBy = dto.CompletedBy,
                Description = dto.Description,
                BelongsTo = dto.BelongsTo,
                ExpiredTime = dto.ExpiredTime,
                CompletedIn = dto.CompletedIn,

                //New
                //DeadLine = dto.DeadLine,
                DeadLine = DateTime.TryParse(dto.DeadLine, out DateTime resultDeadLine) ? (DateTime)resultDeadLine : new DateTime(2000,1,1),

                IsPublic = dto.IsPublic,
                ReferenceId = dto.GetReferenceId(),
                DeadLineGroupName = dto.DeadLineGroupName,
                CrewMembers = dto.CrewMembers?.TransferToListModel(),
                Executors = dto.Executors?.TransferToListModel(),
                ExecutorView = dto.ExecutorView
            };

            return taskViewModel;
        }

        /// <summary>
        /// Transfer from List<TaskDto> to List<TaskViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<TaskViewModel> TransferToViewModel(this List<TaskDto> dto)
        {
            List<TaskViewModel> taskViewModel = dto.Select(x => x.TransferToViewModel()).ToList();
            return taskViewModel;
        }

        public static string GetReferenceId(this TaskDto dto)
        {
            switch (dto.BelongsTo)
            {
                case ModuleTypeEnum.Contacts:
                    return dto.ContactId?.ToString();
                case ModuleTypeEnum.CrewMembers:
                    return dto.CrewMemberId;
                case ModuleTypeEnum.Vehicles:
                    return dto.VehicleId?.ToString();
                case ModuleTypeEnum.Equipment:
                    return dto.EquipmentId?.ToString();
                case ModuleTypeEnum.Inspections:
                    return dto.InspectionId?.ToString();
                case ModuleTypeEnum.Repairs:
                    return dto.RepairId?.ToString();
                case ModuleTypeEnum.Invoices:
                    return dto.InvoiceId?.ToString();
                default:
                    break;

            }
            return string.Empty;
        }
    }
}
