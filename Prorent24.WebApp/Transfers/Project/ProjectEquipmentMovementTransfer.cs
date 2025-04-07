using Prorent24.BLL.Models.Project;
using Prorent24.Enum.Project;
using Prorent24.WebApp.Models.Project.ProjectEquipmentMovement;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.Project
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProjectEquipmentMovementTransfer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static ProjectEquipmentMovementListViewModel TransfetToListViewModel(this List<ProjectEquipmentMovementDto> dtos)
        {
            ProjectEquipmentMovementListViewModel result = new ProjectEquipmentMovementListViewModel();
            var groups = dtos.GroupBy(x => new { x.GroupId });

            foreach (var group in groups)
            {
                List<ProjectEquipmentMovementDto> equipmentMovements = group.ToList();
                ProjectEquipmentMovementDto infoForGroup = equipmentMovements.FirstOrDefault();

                if (infoForGroup != null)
                {
                    if (string.IsNullOrWhiteSpace(result.ProjectName))
                    {
                        result.ProjectName = infoForGroup.ProjectName;
                    }

                    ProjectGroupMovementViewModel groupMovement = new ProjectGroupMovementViewModel()
                    {
                        GroupId = infoForGroup.GroupId,
                        GroupName = infoForGroup.GroupName,
                        MovementStatus = infoForGroup.MovementStatus
                    };

                    foreach (var item in result.Data)
                    {
                        if (item.Key == groupMovement.MovementStatus)
                        {
                            groupMovement.Equipments = equipmentMovements.Where(x => x.MovementStatus == item.Key).ToList().TransferToListViewModel();
                            result.Data[item.Key].Add(groupMovement);
                        }
                        else
                        {
                            result.Data[item.Key].Add(new ProjectGroupMovementViewModel()
                            {
                                GroupId = infoForGroup.GroupId,
                                GroupName = infoForGroup.GroupName,
                                MovementStatus = item.Key,
                                Equipments = equipmentMovements.Where(x => x.MovementStatus == item.Key).ToList().TransferToListViewModel()
                            });
                        }

                    }

                }
            }

            return result;
        }

        public static List<ProjectGroupMovementViewModel> TransferToListGroupViewModel(this List<ProjectEquipmentMovementDto> dtos,  ProjectEquipmentMovementStatusEnum movementStatus, List<ProjectEquipmentGroupDto> groups = null)
        {
            List<ProjectGroupMovementViewModel> groupList = new List<ProjectGroupMovementViewModel>();
            if (groups != null && groups.Count > 0)
            {
                foreach (ProjectEquipmentGroupDto el in groups)
                {
                    groupList.Add(new ProjectGroupMovementViewModel()
                    {
                        GroupId = el.Id,
                        GroupName = el.Name,
                        MovementStatus = movementStatus
                        
                    });
                }

            }
            foreach (ProjectEquipmentMovementDto el in dtos)
            {
                if (groupList.Any(x => x.GroupId == el.GroupId))
                {
                    groupList.Where(x => x.GroupId == el.GroupId).FirstOrDefault().Equipments.Add(el.TransferToViewModel());
                }
                else
                {
                    groupList.Add(new ProjectGroupMovementViewModel()
                    {
                        GroupId = el.GroupId,
                        GroupName = el.GroupName,
                        MovementStatus = el.MovementStatus,
                        Equipments = new List<ProjectEquipmentMovementViewModel>()
                        {
                            el.TransferToViewModel()
                        }
                    });
                }

            }
            return groupList;
        }


        /// <summary>
        /// Transfer from List<ProjectEquipmentMovementDto> to List<ProjectEquipmentMovementViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentMovementViewModel> TransferToListViewModel(this List<ProjectEquipmentMovementDto> dtos)
        {
            List<ProjectEquipmentMovementViewModel> viewModels = dtos.Select(x => x.TransferToViewModel()).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentMovementDto to ProjectEquipmentMovementViewModel
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static ProjectEquipmentMovementViewModel TransferToViewModel(this ProjectEquipmentMovementDto dto)
        {
            ProjectEquipmentMovementViewModel viewModel = new ProjectEquipmentMovementViewModel()
            {
                Id = dto.Id,
                GroupId = dto.GroupId,
                ProjectEquipmentId = dto.ProjectEquipmentId,
                KitCaseParentEquipmentId = dto.KitCaseEquipmentId,
                EquipmentId = dto.EquipmentId,
                EquipmentName = dto.EquipmentName,
                MovementStatus = dto.MovementStatus,
                EquipmentType = dto.KitCaseEquipments != null && dto.KitCaseEquipments.Count > 0 ? Enum.Equipment.SetTypeEnum.Kit : Enum.Equipment.SetTypeEnum.Item,
                SelectedQuantity = dto.SelectedQuantity,
                TotalQuantity = dto.TotalQuantity,
                KitCaseEquipments = dto.KitCaseEquipments?.TransferToListViewModel(),
                LimitQuantity = dto.LimitQuantity,
                SerialNumbers = dto.SerialNumbers
            };
            return viewModel;
        }

        /// <summary>
        /// Transfer from ProjectEquipmentMovementDto to ProjectEquipmentMovementDto
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static ProjectEquipmentMovementDto TransferToDto(this ProjectEquipmentMovementViewModel view)
        {
            ProjectEquipmentMovementDto dto = new ProjectEquipmentMovementDto()
            {
                Id = view.Id,
                GroupId = view.GroupId,
                ProjectEquipmentId = view.ProjectEquipmentId,
                EquipmentId = view.EquipmentId,
                EquipmentName = view.EquipmentName,
                KitCaseEquipmentId = view.KitCaseParentEquipmentId,
                MovementStatus = view.MovementStatus,
                SelectedQuantity = view.SelectedQuantity,
                TotalQuantity = view.TotalQuantity,
                KitCaseEquipments = view.KitCaseEquipments != null ? view.KitCaseEquipments.TransferToListDto() : null

            };

            return dto;
        }

        /// <summary>
        /// Transfer from List<ProjectEquipmentMovementViewModel> to List<ProjectEquipmentMovementDto>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ProjectEquipmentMovementDto> TransferToListDto(this List<ProjectEquipmentMovementViewModel> views)
        {
            List<ProjectEquipmentMovementDto> dtos = views.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }
    }
}
