using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.Folder;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Models.System;
using Prorent24.BLL.Models.Tasks;
using Prorent24.BLL.Models.TimeRegistration;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Trees;
using Prorent24.Entity.General;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using System.Collections.Generic;

namespace Prorent24.BLL.Services.General.Tree
{
    public static class TreeColumnEntity
    {
        public static List<TreeGroupColumn> GenerateColumnGroupByEntity(this EntityEnum entity, Dictionary<string, bool> selectedColumns = null)
        {
            List<TreeGroupColumn> treeColumnGroups = new List<TreeGroupColumn>();


            switch (entity)
            {
                case EntityEnum.AdditionalConditionEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<AdditionalConditionDto>(selectedColumns);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.TimeRegistrationEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<TimeRegistrationDto>(selectedColumns,true);
                        break;
                    }
                case EntityEnum.CrewMemberEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<CrewMemberDto>(selectedColumns,true);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.UserEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<CrewMemberDto>(selectedColumns);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.FolderEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<FolderDto>(selectedColumns);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.ProjectEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<ProjectDto>(selectedColumns);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.ProjectEquipmentEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<ProjectEquipmentGridDto>(selectedColumns);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.TaskEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<TaskDto>(selectedColumns,true);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.SubhireEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<SubhireDto>(selectedColumns);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.SubhireShortageEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<SubhireShortageDto>(selectedColumns);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.ContactEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<ContactDto>(selectedColumns,true);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
                case EntityEnum.InvoiceEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<InvoiceDto>(selectedColumns);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }

                case EntityEnum.VehicleEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<VehicleDto>(selectedColumns,true);
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;

                    }

                case EntityEnum.EquipmentEntity:
                    {
                        treeColumnGroups.AddTreeColumnsGroup<EquipmentDto>(selectedColumns); 
                        //treeColumnGroups.AddTreeColumnsGroup<SystemDto>(selectedColumns);
                        break;
                    }
            }

            return treeColumnGroups;
        }
    }
}
