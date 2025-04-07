using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Maintenance;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Models.Tasks;
using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.Common.Extentions;
using Prorent24.Enum.Entity;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Extentions
{
    public static class DetailsExtention
    {
        public static List<ModuleDetailDto> CreateDetails<T>(this T model, EntityEnum moduleType, string userId = null)
        {
            switch (moduleType)
            {
                case EntityEnum.ContactEntity:
                    {
                        ContactDto dto = model as ContactDto;
                        return dto.CreateContactDetails();
                    }
                case EntityEnum.CrewMemberEntity:
                    {
                        CrewMemberDto dto = model as CrewMemberDto;
                        return dto.CreateCrewMembersDetails(userId);
                    }
                case EntityEnum.TaskEntity:
                    {
                        TaskDto dto = model as TaskDto;
                        return dto.CreateTaskDetails();
                    }
                case EntityEnum.VehicleEntity:
                    {
                        VehicleDto dto = model as VehicleDto;
                        return dto.CreateVehicleDetails();
                    }
                case EntityEnum.TimeRegistrationEntity:
                    {
                        TimeRegistrationDto dto = model as TimeRegistrationDto;
                        return dto.CreateTimeRegistrationDetails();
                    }
                case EntityEnum.EquipmentEntity:
                    {
                        EquipmentDto dto = model as EquipmentDto;
                        return dto.CreateEquipmentDetails();
                    }
                case EntityEnum.InspectionEntity:
                    {
                        InspectionDto dto = model as InspectionDto;
                        return dto.CreateInspectionDetails();
                    }
                case EntityEnum.RepairEntity:
                    {
                        RepairDto dto = model as RepairDto;
                        return dto.CreateInspectionDetails();
                    }
                case EntityEnum.InvoiceEntity:
                    {
                        InvoiceDto dto = model as InvoiceDto;
                        return dto.CreateInvoiceDetails();
                    }
                case EntityEnum.SubhireEntity:
                    {
                        SubhireDto dto = model as SubhireDto;
                        return dto.CreateSubhireDetails();
                    }
                case EntityEnum.ProjectEntity:
                    {
                        ProjectDto dto = model as ProjectDto;
                        return dto.CreateProjectDetails();
                    }
                case EntityEnum.SubhireShortageEntity:
                    {
                        SubhireShortageDetailDto dto = model as SubhireShortageDetailDto;
                        return dto.CreateSubhireShortageDetails();
                    }
                case EntityEnum.ProjectEquipmentEntity:
                    {
                        ProjectEquipmentDto dto = model as ProjectEquipmentDto;
                        return dto.CreateProjectEquipmentDetails();
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        #region Private CreateDetails

        /// <summary>
        /// Contact Details
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static List<ModuleDetailDto> CreateContactDetails(this ContactDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            //Details
            List<DetailDto> details = dto.CreateMainDetails();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = details
            });

            Dictionary<string, string> details_ = new Dictionary<string, string>();

            details_.Add("Contact person", string.Concat(dto.DefaultContact?.FirstName, " ", dto.DefaultContact?.LastName));

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailContacts,
                Data = details
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                Data = dto.Tasks
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateCrewMembersDetails(this CrewMemberDto dto, string userId)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            //Details
            List<DetailDto> details = dto.CreateMainDetails();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = details
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                //New
                Data = dto.Tasks?.Where(x => DateTime.Parse(x.DeadLine) >= DateTime.UtcNow && (x.IsPublic || (!x.IsPublic && x.CrewMembers != null && x.CrewMembers.Any(c => c.Id == userId)) || x.CreationUserId == userId)).ToList()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateEquipmentDetails(this EquipmentDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            //Details
            List<DetailDto> details = dto.CreateMainDetails();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = details
            });
            //moduleDetails.Add(new ModuleDetailDto()
            //{
            //    Entity = DetailsEntityEnum.Structure,
            //    Data = dto.Tags
            //});
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                Data = dto.Tasks
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateTaskDetails(this TaskDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            //Details
            string delimiter = ", ";
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "Task", Type = dto.Name?.GetType().ConvertToTypeScriptType(), Value = dto.Name });
            details.Add(new DetailDto() { Key = "Created by", Type = dto.Name?.GetType().ConvertToTypeScriptType(), Value = string.Concat(dto.LastName, " ", dto.FirstName, " ", dto.MiddleName) });
            details.Add(new DetailDto() { Key = "Deadline", Type = dto.DeadLine.GetType().ConvertToTypeScriptType(), Value = dto.DeadLine });
            details.Add(new DetailDto() { Key = "Executor", Type = "string", Value = string.Join(delimiter, dto.Executors?.Select(x => string.Concat(x.LastName, " ", x.FirstName, " ", x.MiddleName)).ToList()) });
            details.Add(new DetailDto() { Key = "Description", Type = dto.Description?.GetType().ConvertToTypeScriptType(), Value = dto.Description });

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = details
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });

            //LinkedDetails
            dto.CrewMember?.CreateLinkedDetails<CrewMemberDto>(moduleDetails, ModuleTypeEnum.CrewMembers);
            dto.Contact.CreateLinkedDetails<ContactDto>(moduleDetails, ModuleTypeEnum.Contacts);
            dto.Vehicle?.CreateLinkedDetails<VehicleDto>(moduleDetails, ModuleTypeEnum.Vehicles);
            dto.Equipment?.CreateLinkedDetails<EquipmentDto>(moduleDetails, ModuleTypeEnum.Equipment);

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateVehicleDetails(this VehicleDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = dto.CreateMainDetails()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                Data = dto.Tasks
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateInspectionDetails(this InspectionDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = dto.CreateMainDetails()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                Data = dto.Tasks
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateInspectionDetails(this RepairDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = dto.CreateMainDetails()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                Data = dto.Tasks
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateInvoiceDetails(this InvoiceDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = dto.CreateMainDetails()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            //moduleDetails.Add(new ModuleDetailDto()
            //{
            //    Entity = DetailsEntityEnum.FileEntity,
            //    Data = dto.Files
            //});
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                Data = dto.Tasks
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateSubhireDetails(this SubhireDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = dto.CreateMainDetails()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.ProjectList,
                Data = dto.Projects.CreateProjectShorts()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                Data = dto.Tasks
            });

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateProjectDetails(this ProjectDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = dto.CreateMainDetails()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TagEntity,
                Data = dto.Tags
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.NoteEntity,
                Data = dto.Notes
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.FileEntity,
                Data = dto.Files
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.TaskEntity,
                Data = dto.Tasks
            });

            return moduleDetails;
        }

        private static List<ModuleDetailDto> CreateProjectEquipmentDetails(this ProjectEquipmentDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = dto.CreateMainDetails()
            });

            return moduleDetails;
        }


        private static List<ModuleDetailDto> CreateSubhireShortageDetails(this SubhireShortageDetailDto dto)
        {
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.Project,
                Data = dto.Project.CreateMainDetails()
            });
            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.Equipment,
                Data = dto.Equipment.CreateMainDetails()
            });
            foreach (SubhireDto subhire in dto.Subhires)
                moduleDetails.Add(new ModuleDetailDto()
                {
                    Entity = DetailsEntityEnum.Subhire,
                    Data = subhire.CreateMainDetails()
                });


            return moduleDetails;
        }


        private static List<ModuleDetailDto> CreateTimeRegistrationDetails(this TimeRegistrationDto dto)
        {
            Localize localize = new Localize();
            List<ModuleDetailDto> moduleDetails = new List<ModuleDetailDto>();

            //Details
            //Dictionary<string, string> details = new Dictionary<string, string>();
            List<DetailDto> details = new List<DetailDto>();
            double totalDays = Math.Round((dto.Until - dto.From).TotalDays);
            double totalMinutes = (dto.Until.Hour - dto.From.Hour);

            details.Add(new DetailDto() { Key = "FIO", Type = "string", Value = dto.CrewMember });
            details.Add(new DetailDto() { Key = "From", Type = dto.From.GetType().ConvertToTypeScriptType(), Value = dto.From.ToString("dd.MM.yyyy HH:mm") });
            details.Add(new DetailDto() { Key = "Until", Type = dto.Until.GetType().ConvertToTypeScriptType(), Value = dto.Until.ToString("dd.MM.yyyy HH:mm") });
            details.Add(new DetailDto() { Key = "Total", Type = "string", Value = string.Concat((totalDays > 0 ? totalDays : 1).ToString(), " дней, ", (totalMinutes > 0 ? totalMinutes : 0).ToString(), " часов") });
            details.Add(new DetailDto() { Key = "Break", Type = "string", Value = string.Concat(dto.BreakDuration, " минут") });
            details.Add(new DetailDto() { Key = "Travel time", Type = "string", Value = string.Concat(dto.TravelDuration, " минут") });
            details.Add(new DetailDto() { Key = "Distance", Type = "string", Value = (dto.Distance > 0 ? dto.Distance.ToString() : "0") + " км" });
            details.Add(new DetailDto() { Key = "Lunch", Type = "string", Value = dto.Lunch ? "Да" : "Нет" });
            details.Add(new DetailDto() { Key = "HourRegistrationType", Type = dto.HourRegistrationType.GetType().ConvertToTypeScriptType(), Value = localize[dto.HourRegistrationType.ToString().ToLower()] });
            details.Add(new DetailDto() { Key = "Comment", Type = "string", Value = dto.Remark });

            moduleDetails.Add(new ModuleDetailDto()
            {
                Entity = DetailsEntityEnum.DetailEntity,
                Data = details
            });


            //List<DetailDto> viewTimeLine = new List<DetailDto>();
            //viewTimeLine.Add(new DetailDto() { Key = "Working hours from", Type = dto.From.GetType().ConvertToTypeScriptType(), Value = dto.From });
            //viewTimeLine.Add(new DetailDto() { Key = "Working hours from", Type = dto.Until.GetType().ConvertToTypeScriptType(), Value = dto.Until });

            ////List<Dictionary<string, object>> times = new List<Dictionary<string, object>>();

            ////Dictionary<string, object> timeBlock = new Dictionary<string, object>();

            //int workedHours = 0;

            //dto.Activities.ForEach(x =>
            //{
            //    workedHours += x.Duration;
            //});

            //viewTimeLine.Add(new DetailDto() { Key = "Worked hours", Type = workedHours.GetType().ConvertToTypeScriptType(), Value = workedHours });
            //viewTimeLine.Add(new DetailDto() { Key = "Holiday", Type = "number", Value = 0 });
            //viewTimeLine.Add(new DetailDto() { Key = "Disease", Type = "number", Value = 0 });
            //viewTimeLine.Add(new DetailDto() { Key = "HolFixesiday", Type = "number", Value = 0 });
            ////times.Add(timeBlock);

            ////timeBlock = new Dictionary<string, object>();

            ////timeBlock.Add("Worked hours", workedHours);
            //viewTimeLine.Add(new DetailDto() { Key = "Break", Type = "number", Value = dto.BreakDuration });
            //viewTimeLine.Add(new DetailDto() { Key = "Travel Duration", Type = "number", Value = dto.TravelDuration });
            ////times.Add(timeBlock);

            ////timeBlock = new Dictionary<string, object>();

            //viewTimeLine.Add(new DetailDto() { Key = "Subtotal", Type = workedHours.GetType().ConvertToTypeScriptType(), Value = workedHours });
            //viewTimeLine.Add(new DetailDto() { Key = "Hours surcharge", Type = "number", Value = 0 });
            ////timeBlock.Add("Subtotal", workedHours);
            ////timeBlock.Add("Hours surcharge", 0);
            ////times.Add(timeBlock);

            ////viewTimeLine.Add("times", times);

            //viewTimeLine.Add(new DetailDto() { Key = "Total", Type = workedHours.GetType().ConvertToTypeScriptType(), Value = workedHours });
            ////viewTimeLine.Add("Total", workedHours);

            ////List<Dictionary<string, object>> additionalInfo = new List<Dictionary<string, object>>();

            ////Dictionary<string, object> additionalInfoBlock = new Dictionary<string, object>();

            //viewTimeLine.Add(new DetailDto() { Key = "Compensation", Type = "number", Value = 0 });
            //viewTimeLine.Add(new DetailDto() { Key = "Hours in the contract", Type = "number", Value = 0 });

            ////additionalInfoBlock.Add("Compensation", 0);
            ////additionalInfoBlock.Add("Hours in the contract", 0);
            ////additionalInfo.Add(additionalInfoBlock);

            ////viewTimeLine.Add("additional", additionalInfo);

            //moduleDetails.Add(new ModuleDetailDto()
            //{
            //    Entity = DetailsEntityEnum.ViewTimeLine,
            //    Data = viewTimeLine
            //});

            //



            //double totalDays = Math.Round((dto.From - dto.Until).TotalDays);
            //double totalMinutes = Math.Round((dto.From - dto.Until).TotalHours);

            //hoursOverView.Add("Until", dto.HourRegistrationType);
            //hoursOverView.Add("Total", string.Concat((totalDays > 0 ? totalDays : 1).ToString(), " days, ", (totalMinutes > 0 ? totalMinutes : 0).ToString(), " hours"));
            //hoursOverView.Add("Break", string.Concat(dto.BreakDuration, " minutes"));
            //hoursOverView.Add("Travel time", string.Concat(dto.TravelDuration, " minutes"));
            //hoursOverView.Add("Distance", dto.Distance.ToString());
            //hoursOverView.Add("Lunch", dto.Lunch ? "Yes" : "No");


            //moduleDetails.Add(new ModuleDetailDto()
            //{
            //    Entity = DetailsEntityEnum.NoteEntity,
            //    Data = dto.Notes
            //});
            //moduleDetails.Add(new ModuleDetailDto()
            //{
            //    Entity = DetailsEntityEnum.FileEntity,
            //    Data = dto.Files
            //});

            return moduleDetails;
        }

        #endregion

        #region Private CreateMainDetails


        private static List<DetailDto> CreateMainDetails(this EquipmentDto dto)
        {
            Localize localize = new Localize();
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "Name", Type = dto.Name?.GetType().ConvertToTypeScriptType(), Value = dto.Name });
            details.Add(new DetailDto() { Key = "Code", Type = dto.Code?.GetType().ConvertToTypeScriptType(), Value = dto.Code });
            details.Add(new DetailDto() { Key = "TypeEquipment", Type = dto.SetType.GetType().ConvertToTypeScriptType(), Value = localize[dto.SetType.ToString().ToLower()] });
            details.Add(new DetailDto() { Key = "SubhirePrice", Type = dto.SubhirePrice.GetType().ConvertToTypeScriptType(), Value = dto.SubhirePrice.ToString("F") });
            details.Add(new DetailDto() { Key = "RentalPrice", Type = dto.RentalPrice.GetType().ConvertToTypeScriptType(), Value = dto.RentalPrice.ToString("F") });
            details.Add(new DetailDto() { Key = "PriceIncludesVAT", Type = dto.RentalPrice.GetType().ConvertToTypeScriptType(), Value = dto.VatClassSchemeRate.VATCalculation(dto.RentalPrice).ToString("F") });
            details.Add(new DetailDto() { Key = "Weight", Type = "string", Value = dto.WeightDim + " " + localize[dto.WeightUnit.ToString().ToLower()] });
            details.Add(new DetailDto() { Key = "Volume", Type = "string", Value = dto.Volume + " " + localize[dto.VolumeUnit.ToString().ToLower()]});
            details.Add(new DetailDto() { Key = "Dimensions (lxwxh)", Type = "string", Value = $"{dto.LengthDim + " " + localize[dto.LengthUnit.ToString().ToLower()]} × {dto.WidthDim + " " + localize[dto.WidthUnit.ToString().ToLower()]} ×  {dto.HeightDim + " " + localize[dto.HeightUnit.ToString().ToLower()]} " });
            details.Add(new DetailDto() { Key = "Power", Type = "string", Value = dto.Power + " " + localize[dto.PowerUnit.ToString().ToLower()] });
            details.Add(new DetailDto() { Key = "Current", Type = "string", Value = dto.Current + " "+ localize[dto.CurrentUnit.ToString().ToLower()] });
            details.Add(new DetailDto() { Key = "Remark", Type = "string", Value = dto.InternalRemark });
            return details;
        }

        private static List<DetailDto> CreateMainDetails(this ContactDto dto)
        {
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "FullName", Type = dto.FirstName?.GetType().ConvertToTypeScriptType(), Value = dto.LastName + " " + dto.FirstName + " " + dto.MiddleName });
            details.Add(new DetailDto() { Key = "Phone", Type = dto.Phone?.GetType().ConvertToTypeScriptType(), Value = dto.Phones?.FirstOrDefault() ?? dto.Phone });
            details.Add(new DetailDto() { Key = "Email", Type = dto.Email?.GetType().ConvertToTypeScriptType(), Value = dto.Email });
            details.Add(new DetailDto() { Key = "Company", Type = dto.CompanyShortName?.GetType().ConvertToTypeScriptType(), Value = dto.CompanyShortName ?? dto.CompanyName });
            
            //New
            details.Add(new DetailDto() { Key = "BirthDate", Type = dto.BirthDate?.GetType().ConvertToTypeScriptType(), Value = dto.BirthDate });
            //if (dto.BirthDate.HasValue)
            //{
            //    details.Add(new DetailDto() { Key = "BirthDate", Type = dto.BirthDate.Value.GetType().ConvertToTypeScriptType(), Value = dto.BirthDate.Value });
            //}

            if (dto.Website!=null && dto.Website.Length > 0)
            {
                details.Add(new DetailDto() { Key = "Website", Type = dto.Website.GetType().ConvertToTypeScriptType(), Value = dto.Website });
            }
            if (dto.Specialization != null && dto.Specialization.Length > 0)
            {
                details.Add(new DetailDto() { Key = "Specialization", Type = dto.Specialization.GetType().ConvertToTypeScriptType(), Value = dto.Specialization});
            }

            details.Add(new DetailDto() { Key = "Projnote", Type = "string", Value = dto.ProjNote });

            return details;
        }

        private static List<DetailDto> CreateMainDetails(this InspectionDto dto)
        {
            Localize localize = new Localize();
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "PeriodicInspectionName", Type = "string", Value = dto.PeriodicInspectionName });
            details.Add(new DetailDto() { Key = "Status", Type = "string", Value = localize[dto.Status.ToString().ToLower()] });
            details.Add(new DetailDto() { Key = "Date", Type = dto.Date.GetType().ConvertToTypeScriptType(), Value = dto.Date });
            details.Add(new DetailDto() { Key = "Description", Type = dto.Description?.GetType().ConvertToTypeScriptType(), Value = dto.Description });
            return details;
        }

        private static List<DetailDto> CreateMainDetails(this RepairDto dto)
        {
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "EquipmentName", Type = dto.EquipmentName?.GetType().ConvertToTypeScriptType(), Value = dto.EquipmentName });
            details.Add(new DetailDto() { Key = "From", Type = dto.From?.GetType().ConvertToTypeScriptType(), Value = dto.From });
            details.Add(new DetailDto() { Key = "Until", Type = dto.Until?.GetType().ConvertToTypeScriptType(), Value = dto.Until });
            details.Add(new DetailDto() { Key = "Quantity", Type = dto.Quantity?.GetType().ConvertToTypeScriptType(), Value = dto.Quantity.ToString() });
            details.Add(new DetailDto() { Key = "Costs", Type = dto.Cost.GetType().ConvertToTypeScriptType(), Value = dto.Cost.ToString() });
            details.Add(new DetailDto() { Key = "Usable", Type = "string", Value = (int)dto.Usable == 1 ? "Может быть использован" : "Не может быть использован" });
            details.Add(new DetailDto() { Key = "Remark", Type = dto.Remark?.GetType().ConvertToTypeScriptType(), Value = dto.Remark });
            return details;
        }


        private static List<DetailDto> CreateMainDetails(this InvoiceDto dto)
        {
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "Client", Type = "string", Value = dto.Client?.CompanyName +" " + dto.Client?.FirstName + " " + dto.Client?.LastName});
            details.Add(new DetailDto() { Key = "Number", Type = "string", Value = dto.Id.ToString() });
            details.Add(new DetailDto() { Key = "Price excl. VAT", Type = "decimal", Value = dto.InvoiceLines?.Sum(x=>x.Price) });
            details.Add(new DetailDto() { Key = "duedate", Type = "string", Value = dto.DueDate?.ToString("dd.MM.yyyy") });
            
            return details;
        }


        private static List<DetailDto> CreateMainDetails(this SubhireDto dto)
        {
            Localize localize = new Localize();
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "Name", Type = dto.Name?.GetType().ConvertToTypeScriptType(), Value = dto.Name.ToString() });
            details.Add(new DetailDto() { Key = "Status", Type = "string", Value = localize[dto.Status.ToString().ToLower()] });
            details.Add(new DetailDto() { Key = "Account", Type = "string", Value = $"{dto.AccountManager?.LastName} {dto.AccountManager?.FirstName}" });
            details.Add(new DetailDto() { Key = "Usage period start", Type = dto.UsagePeriodStart?.GetType().ConvertToTypeScriptType(), Value = dto.UsagePeriodStart });
            details.Add(new DetailDto() { Key = "Usage period end", Type = dto.UsagePeriodEnd?.GetType().ConvertToTypeScriptType(), Value = dto.UsagePeriodEnd });
            details.Add(new DetailDto() { Key = "Planning period start", Type = dto.PlanningPeriodStart.GetType().ConvertToTypeScriptType(), Value = dto.PlanningPeriodStart });
            details.Add(new DetailDto() { Key = "Planning period end", Type = dto.PlanningPeriodEnd.GetType().ConvertToTypeScriptType(), Value = dto.PlanningPeriodEnd });
            details.Add(new DetailDto() { Key = "Remark", Type = "string", Value = dto.Remark });
            return details;
        }

        private static List<string> CreateProjectShorts(this List<ProjectDto> dtos)
        {
            List<string> details = new List<string>();
            foreach (ProjectDto el in dtos)
            {
                details.Add(el.Name);
            }
            return details;
        }

        private static List<DetailDto> CreateMainDetails(this CrewMemberDto dto)
        {
            List<DetailDto> details = new List<DetailDto>();
            //details.Add(new DetailDto() { Key = "FirstName", Type = dto.FirstName?.GetType().ConvertToTypeScriptType(), Value = dto.FirstName });
            //details.Add(new DetailDto() { Key = "LastName", Type = dto.LastName?.GetType().ConvertToTypeScriptType(), Value = dto.LastName });
            //details.Add(new DetailDto() { Key = "MiddleName", Type = dto.MiddleName?.GetType().ConvertToTypeScriptType(), Value = dto.MiddleName });

            details.Add(new DetailDto() { Key = "FIO", Type = "string", Value = dto.LastName + " " + dto.FirstName + " " + dto.MiddleName });

            details.Add(new DetailDto() { Key = "Email", Type = dto.Email?.GetType().ConvertToTypeScriptType(), Value = dto.Email });
            details.Add(new DetailDto() { Key = "Phone", Type = dto.Phone?.GetType().ConvertToTypeScriptType(), Value = dto.Phone });

            details.Add(new DetailDto() { Key = "CompanyName", Type = dto.CompanyName?.GetType().ConvertToTypeScriptType(), Value = dto.CompanyName });
            details.Add(new DetailDto() { Key = "Description", Type = dto.CompanyName?.GetType().ConvertToTypeScriptType(), Value = dto.Description });

            return details;
        }

        private static Dictionary<string, string> CreateMainDetails(this TaskDto dto)
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            details.Add("Task", dto.Name);
            details.Add("Description", dto.Description);
            details.Add("Created by", string.Concat(dto.FirstName, " ", dto.LastName));
            //New
            details.Add("Deadline", DateTime.Parse(dto.DeadLine).ToString("dd.MM.yyyy hh:mm") );
            return details;
        }

        private static List<DetailDto> CreateMainDetails(this VehicleDto dto)
        {
            Localize localize = new Localize();
            List<DetailDto> details = new List<DetailDto>();
            
            details.Add(new DetailDto() { Key = "Name", Type = dto.Name?.GetType().ConvertToTypeScriptType(), Value = dto.Name });
            details.Add(new DetailDto() { Key = "Registration number", Type = dto.RegistrationNumber?.GetType().ConvertToTypeScriptType(), Value = dto.RegistrationNumber });
            details.Add(new DetailDto() { Key = "Load capacity", Type = "string", Value = dto.LoadCapacity.ToString() + localize[dto.LoadCapacityUnit.ToString().ToLower()] });

            //New
            //details.Add(new DetailDto() { Key = "MOT date", Type = dto.MOTDate?.GetType().ConvertToTypeScriptType(), Value = dto.MOTDate.ToString() });
            details.Add(new DetailDto() { Key = "MOT date", Type = dto.MOTDate?.GetType().ConvertToTypeScriptType(), Value = dto.MOTDate });

            details.Add(new DetailDto() { Key = "Description", Type = "string", Value = dto.Description });
            return details;
        }
        private static List<DetailDto> CreateMainDetails(this ProjectDto dto)
        {
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "Name", Type = dto.Name?.GetType().ConvertToTypeScriptType(), Value = dto.Name });
            details.Add(new DetailDto() { Key = "Number", Type = dto.Number?.GetType().ConvertToTypeScriptType(), Value = dto.Number });
            details.Add(new DetailDto() { Key = "Created on", Type = dto.CreationDate.GetType().ConvertToTypeScriptType(), Value = dto.CreationDate });
            details.Add(new DetailDto() { Key = "Client", Type = "string", Value = $"{dto.ClientContact?.CompanyName} { dto.ClientContact?.PostalAddress?.Address} { dto.ClientContact?.PostalAddress?.PostalCode} { dto.ClientContact?.PostalAddress?.City}" });
            details.Add(new DetailDto() { Key = "Contact person", Type = "string", Value = $"{dto.ClientContactPerson?.FirstName} {dto.ClientContactPerson?.LastName}" });
            details.Add(new DetailDto() { Key = "Location", Type = "string", Value = $"{dto.LocationContact?.CompanyName} { dto.LocationContact?.PostalAddress?.Address} { dto.LocationContact?.PostalAddress?.PostalCode} { dto.LocationContact?.PostalAddress?.City}" });
            details.Add(new DetailDto() { Key = "Location contact person", Type = "string", Value = $"{dto.LocationContactPerson?.FirstName} {dto.LocationContactPerson?.LastName}" });
            details.Add(new DetailDto() { Key = "Calculated planning period start", Type = dto.StartPlanPeriod?.GetType().ConvertToTypeScriptType(), Value = dto.StartPlanPeriod });
            details.Add(new DetailDto() { Key = "Calculated planning period end", Type = dto.EndPlanPeriod?.GetType().ConvertToTypeScriptType(), Value = dto.EndPlanPeriod });
            details.Add(new DetailDto() { Key = "Calculated usage period start", Type = dto.StartUsePeriod.GetType().ConvertToTypeScriptType(), Value = dto.StartUsePeriod });
            details.Add(new DetailDto() { Key = "Calculated usage period end", Type = dto.EndUsePeriod.GetType().ConvertToTypeScriptType(), Value = dto.EndUsePeriod });
            details.Add(new DetailDto() { Key = "Description", Type = "string", Value = dto.Description });

            return details;
        }

        private static List<DetailDto> CreateMainDetails(this ProjectEquipmentDto dto)
        {
            Localize localize = new Localize();
            List<DetailDto> details = new List<DetailDto>();
            details.Add(new DetailDto() { Key = "Name", Type = dto.EquipmentName?.GetType().ConvertToTypeScriptType(), Value = dto.EquipmentName });
            details.Add(new DetailDto() { Key = "Number", Type = dto.Equipment?.Code?.GetType().ConvertToTypeScriptType(), Value = dto.Equipment?.Code });
            details.Add(new DetailDto() { Key = "Type", Type = "string", Value = localize[dto.Equipment?.SetType.ToString().ToLower()] });
            details.Add(new DetailDto() { Key = "Subhire price", Type = dto.Equipment?.SubhirePrice.GetType().ConvertToTypeScriptType(), Value = dto.Equipment?.SubhirePrice.ToString() });
            details.Add(new DetailDto() { Key = "Rental price", Type = dto.Equipment?.RentalPrice.GetType().ConvertToTypeScriptType(), Value = dto.Equipment?.RentalPrice.ToString() });
            //details.Add("priceIncludesVAT", dto.Equipment?.RentalPrice.ToString());  //TODO
            return details;
        }

        #endregion

        #region Private CreateLinkedDetails

        private static void CreateLinkedDetails<T>(this T model, List<ModuleDetailDto> moduleDetails, ModuleTypeEnum moduleType)
        {
            if (model != null)
            {
                switch (moduleType)
                {
                    case ModuleTypeEnum.CrewMembers:
                        {
                            CrewMemberDto dto = model as CrewMemberDto;

                            if (dto != null)
                            {
                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.DetailsOfLinkedItem,
                                    Data = dto.CreateMainDetails()
                                });

                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.LastNotesLinkedToThisItem,
                                    Data = dto.Notes
                                });

                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.LastTasksLinkedToThisItem,
                                    Data = dto.Tasks
                                });
                            }

                            break;
                        }
                    case ModuleTypeEnum.Vehicles:
                        {
                            VehicleDto dto = model as VehicleDto;

                            if (dto != null)
                            {
                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.DetailsOfLinkedItem,
                                    Data = dto.CreateMainDetails()
                                });

                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.LastNotesLinkedToThisItem,
                                    Data = dto.Notes
                                });

                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.LastTasksLinkedToThisItem,
                                    Data = dto.Tasks
                                });
                            }

                            break;
                        }
                    case ModuleTypeEnum.Equipment:
                        {
                            EquipmentDto dto = model as EquipmentDto;

                            if (dto != null)
                            {
                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.DetailsOfLinkedItem,
                                    Data = dto.CreateMainDetails()
                                });

                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.LastNotesLinkedToThisItem,
                                    Data = dto.Notes
                                });

                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.LastTasksLinkedToThisItem,
                                    Data = dto.Tasks
                                });
                            }

                            break;
                        }
                    case ModuleTypeEnum.Contacts:
                        {
                            ContactDto dto = model as ContactDto;

                            if (dto != null)
                            {
                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.DetailsOfLinkedItem,
                                    Data = dto.CreateMainDetails()
                                });

                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.LastNotesLinkedToThisItem,
                                    Data = dto.Notes
                                });

                                moduleDetails.Add(new ModuleDetailDto()
                                {
                                    Entity = DetailsEntityEnum.LastTasksLinkedToThisItem,
                                    Data = dto.Tasks
                                });
                            }

                            break;
                        }
                }
            }
        }

        #endregion

    }
}
