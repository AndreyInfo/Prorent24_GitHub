using Prorent24.BLL.Models.General.Note;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.General
{
    internal static class NoteTransfer
    {
        /// <summary>
        /// Transfer from NoteDto to NoteViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static NoteViewModel TransferToViewModel(this NoteDto dto)
        {
            NoteViewModel view = new NoteViewModel()
            {
                Id = dto.Id,
                Text = dto.Text,
                BelongsTo = dto.BelongsTo,
                Confidential = dto.Confidential,
                ReferenceId = dto.GetReferenceId()
            };

            return view;
        }

        public static string GetReferenceId(this NoteDto dto)
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
                case ModuleTypeEnum.Invoices:
                    {
                        return dto.InvoiceId?.ToString();
                    }
                default:
                    {
                        break;
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// Transfer from NoteViewModel to NoteDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static NoteDto TransferToDtoModel(this NoteViewModel view)
        {
            NoteDto dto = new NoteDto()
            {
                Id = view.Id,
                Text = view.Text,
                BelongsTo = view.BelongsTo,
                Confidential = view.Confidential,
                TaskId = view.BelongsTo == ModuleTypeEnum.Tasks ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                ContactId = view.BelongsTo == ModuleTypeEnum.Contacts ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                CrewMemberId = view.BelongsTo == ModuleTypeEnum.CrewMembers ? view.ReferenceId : null,
                VehicleId = view.BelongsTo == ModuleTypeEnum.Vehicles ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                ProjectId = view.BelongsTo == ModuleTypeEnum.Projects ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                SubhireId = view.BelongsTo == ModuleTypeEnum.Subhire ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                EquipmentId = view.BelongsTo == ModuleTypeEnum.Equipment ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                InspectionId = view.BelongsTo == ModuleTypeEnum.Inspections ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                RepairId = view.BelongsTo == ModuleTypeEnum.Repairs ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
                InvoiceId = view.BelongsTo == ModuleTypeEnum.Invoices ? (Nullable<int>)Convert.ToInt32(view.ReferenceId) : null,
            };

            return dto;
        }


        /// <summary>
        /// Transfer from List<NoteDto>  to List<NoteViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<NoteViewModel> TransferToViewModel(this List<NoteDto> dto)
        {
            List<NoteViewModel> views = dto.Select(x => x.TransferToViewModel()).ToList();

            return views;
        }
    }
}
