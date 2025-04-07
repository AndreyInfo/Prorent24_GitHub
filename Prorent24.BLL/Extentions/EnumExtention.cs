using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.CrewMember;
using Prorent24.Enum.General;
using Prorent24.Enum.TimeRegistration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Prorent24.Common.Extentions;
using System.Reflection;

namespace Prorent24.BLL.Extentions
{
    public static class EnumToListExtention
    {
        public static List<KeyValuePair<string, int>> GetEnumList(this string enumName)
        {

            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();

            switch (enumName)
            {
                //case "entity":
                //    result = GetEnumList<EntityEnum>();
                //    break;
                case "ConfidentialType":
                    result = EnumExtention.GetEnumList<ConfidentialTypeEnum>();
                    break;
                case "AvailabilityCrewMemberType":
                    result = EnumExtention.GetEnumList<AvailabilityCrewMemberTypeEnum>();
                    break;
                case "TimeUnitType":
                    result = EnumExtention.GetEnumList<TimeUnitTypeEnum>();
                    break;
                case "HourRegistrationType":
                    result = EnumExtention.GetEnumList<HourRegistrationTypeEnum>();
                    break;
                case "DownloadFilePermission":
                    result = EnumExtention.GetEnumList<DownloadFilePermissionEnum>();
                    break;
                case "BooleanSelectPermission":
                    result = EnumExtention.GetEnumList<BooleanSelectPermissionEnum>();
                    break;
                case "TemplatesEmailsPersonalTextPermission":
                    result = EnumExtention.GetEnumList<TemplatesEmailsPersonalTextPermissionEnum>();
                    break;
                case "WhichProjectPermission":
                    result = EnumExtention.GetEnumList<WhichProjectPermissionEnum>();
                    break;
                case "ChangeProjectPermission":
                    result = EnumExtention.GetEnumList<ChangeProjectPermissionEnum>();
                    break;
                case "AvailabilityOfEquipmentPermission":
                    result = EnumExtention.GetEnumList<AvailabilityOfEquipmentPermissionEnum>();
                    break;
                case "EditSubhirePermission":
                    result = EnumExtention.GetEnumList<EditSubhirePermissionEnum>();
                    break;
                case "EntityModificationPermission":
                    result = EnumExtention.GetEnumList<EntityModificationPermissionEnum>();
                    break;
                case "EntityFullModificationPermission":
                    result = EnumExtention.GetEnumList<EntityFullModificationPermissionEnum>();
                    break;
                case "EntityShortModificationPermission":
                    result = EnumExtention.GetEnumList<EntityShortModificationPermissionEnum>();
                    break;
                case "EntityNoShortModificationPermission":
                    result = EnumExtention.GetEnumList<EntityNoShortModificationPermissionEnum>();
                    break;
                case "ProjectPlanningPermission":
                    result = EnumExtention.GetEnumList<ProjectPlanningPermissionEnum>();
                    break;
                case "WhichProjectWarehousePermission":
                    result = EnumExtention.GetEnumList<WhichProjectWarehousePermissionEnum>();
                    //WhichProjectWarehousePermissionEnum.AllProjects.ToKayValue();
                    
                    // як віріант!
                    //(typeof(WhichProjectWarehousePermissionEnum)).ToKayValue();
                    break;
                default:
                    break;
            }

            return result;
        }

        public static FilterEnum ToFilterEnum(this string filterEnum)
        {
            FilterEnum filterEnumResult = FilterEnum.Default;

            if (!String.IsNullOrWhiteSpace(filterEnum))
            {
                 System.Enum.TryParse(filterEnum, out filterEnumResult);
            }

            return filterEnumResult;
        }
    }
}
