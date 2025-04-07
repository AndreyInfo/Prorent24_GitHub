using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Project;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.Project.Movement;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.Project;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Project.Movement
{
    public class ProjectEquipmentMovementService : IProjectEquipmentMovementService
    {
        private readonly IProjectEquipmentMovementStorage _projectEquipmentMovementStorage;
        private readonly IProjectEquipmentStorage _projectEquipmentStorage;

        public ProjectEquipmentMovementService(IProjectEquipmentMovementStorage projectEquipmentMovementStorage,
                                               IProjectEquipmentStorage projectEquipmentStorage)
        {
            _projectEquipmentMovementStorage = projectEquipmentMovementStorage;
            _projectEquipmentStorage = projectEquipmentStorage;
        }

        public async Task<List<ProjectEquipmentMovementDto>> GetListEquipmentMovement(int projectId)
        {
            List<ProjectEquipmentMovementEntity> projEquipmentMovementEntities = await _projectEquipmentMovementStorage.GetEquipmentMovementByProjectId(projectId);
            return projEquipmentMovementEntities.TransferToListDto();
        }

        /// <summary>
        /// Move Equipment
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ProjectEquipmentMovementDto> MovementEquipment(int projectId, int groupId, ProjectEquipmentMovementDto dto)
        {
            ProjectEquipmentMovementEntity dbEntity = await _projectEquipmentMovementStorage.GetById(dto.Id);
            if (dto.MovementStatus != dbEntity.MovementStatus)
            {
                List<ProjectEquipmentMovementEntity> entitiesByProjEquipment = await _projectEquipmentMovementStorage.GetEquipmentMovementByProjectEquipmentId(Convert.ToInt32(dto.ProjectEquipmentId));
                ProjectEquipmentMovementEntity prepareEntity = entitiesByProjEquipment.Where(x => x.MovementStatus == dto.MovementStatus).FirstOrDefault();
                if (prepareEntity != null)
                {
                    var prepareSelectedQuantity = prepareEntity.SelectedQuantity;
                    int selectedQuantity = dto.SelectedQuantity + prepareSelectedQuantity > dbEntity?.TotalQuantity ? dbEntity?.TotalQuantity ?? 0 : dto.SelectedQuantity + prepareSelectedQuantity;
                    prepareEntity.SelectedQuantity = selectedQuantity;

                    if (dto.KitCaseEquipments?.Count > 0)
                    {
                        foreach (ProjectEquipmentMovementDto kit in dto.KitCaseEquipments)
                        {
                            foreach (ProjectEquipmentMovementEntity dbKit in prepareEntity.KitCaseEquipments.Where(y => y.IsRemoved != true).ToList())
                            {
                                if (kit.ProjectEquipmentId == dbKit.ProjectEquipmentId)
                                {
                                    var dbSelectedQuantity = dbKit.SelectedQuantity;
                                    var newSelectedQuantity = kit.SelectedQuantity + dbSelectedQuantity;
                                    dbKit.SelectedQuantity = newSelectedQuantity > kit.TotalQuantity ? kit.TotalQuantity : newSelectedQuantity;
                                }
                            }
                        }
                        List<ProjectEquipmentMovementDto> movemnts = dto.KitCaseEquipments.Where(y => !prepareEntity.KitCaseEquipments.Where(x => x.IsRemoved != true).Select(z => z.ProjectEquipmentId).ToList().Contains(y.ProjectEquipmentId)).ToList();
                        if (movemnts?.Count > 0)
                        {
                            foreach (ProjectEquipmentMovementDto newKit in movemnts)
                            {
                                prepareEntity.KitCaseEquipments.Add(new ProjectEquipmentMovementEntity()
                                {
                                    ProjectId = dbEntity.ProjectId,
                                    GroupId = dbEntity.GroupId,
                                    EquipmentId = newKit.EquipmentId,
                                    SelectedQuantity = newKit.SelectedQuantity,
                                    TotalQuantity = newKit.TotalQuantity,
                                    ProjectEquipmentId = newKit.ProjectEquipmentId,
                                    KitCaseEquipmentId = dto.Id,
                                    MovementStatus = dto.MovementStatus
                                });
                            }
                        }
                    }
                    ProjectEquipmentMovementEntity updateEntity = await _projectEquipmentMovementStorage.Update(prepareEntity);
                    if (dbEntity != null)
                    {
                        await UpdateExistMovementEquipment(dto, dbEntity);
                    }
                    ProjectEquipmentMovementEntity dbUpdateEntity = await _projectEquipmentMovementStorage.GetById(updateEntity.Id);
                    return dbUpdateEntity.TransferToDto(dbUpdateEntity.MovementStatus);
                }
                else
                {
                    int selectedQuantity = dto.SelectedQuantity > dto.TotalQuantity ? dto.TotalQuantity : dto.SelectedQuantity;
                    dto.SelectedQuantity = selectedQuantity;
                    ProjectEquipmentMovementEntity createEntity = await _projectEquipmentMovementStorage.Create(dto.TransferToEntity(projectId, groupId, dto.MovementStatus));
                    if (dbEntity != null)
                    {
                        await UpdateExistMovementEquipment(dto, dbEntity);
                    }
                    ProjectEquipmentMovementEntity dbCreateEntity = _projectEquipmentMovementStorage.QueryableEntity.Include(x => x.Equipment).First(x => x.Id.Equals(createEntity.Id));
                    return dbCreateEntity.TransferToDto(dbCreateEntity.MovementStatus, dto.EquipmentType);
                }
            }
            else
            {
                return null;
                // throw new Exception("Bad MovementStatus");
            }
        }

        private async Task UpdateExistMovementEquipment(ProjectEquipmentMovementDto dto, ProjectEquipmentMovementEntity dbEntity)
        {
            if ((dto.SelectedQuantity == dto.TotalQuantity || dbEntity.SelectedQuantity - dto.SelectedQuantity < 0) && (dto.KitCaseEquipments == null || dto.KitCaseEquipments?.Count == 0))
            {
                dbEntity.IsRemoved = true;
                await _projectEquipmentMovementStorage.Update(dbEntity);

            }
            else if (dto.SelectedQuantity < dto.TotalQuantity && (dto.KitCaseEquipments == null || dto.KitCaseEquipments?.Count == 0))
            {
                var dbSelectedQuantity = dbEntity.SelectedQuantity;
                var newSelectedQuantity = dbSelectedQuantity - dto.SelectedQuantity;
                dbEntity.SelectedQuantity = newSelectedQuantity < 0 ? 0 : newSelectedQuantity;
                if (dbEntity.SelectedQuantity == 0)
                {
                    dbEntity.IsRemoved = true;
                }
                await _projectEquipmentMovementStorage.Update(dbEntity);
            }
            else if (dto.KitCaseEquipments?.Count > 0)
            {
                foreach (ProjectEquipmentMovementDto kit in dto.KitCaseEquipments)
                {
                    foreach (ProjectEquipmentMovementEntity dbKit in dbEntity.KitCaseEquipments.ToList())
                    {
                        if (kit.ProjectEquipmentId == dbKit.ProjectEquipmentId)
                        {
                            if (kit.SelectedQuantity == kit.TotalQuantity || dbKit.SelectedQuantity - kit.SelectedQuantity < 0)
                            {
                                dbKit.SelectedQuantity = 0;
                                dbKit.IsRemoved = true;
                            }
                            else if (kit.SelectedQuantity < kit.TotalQuantity)
                            {
                                var dbSelectedQuantity = dbKit.SelectedQuantity;
                                dbKit.SelectedQuantity = dbSelectedQuantity - kit.SelectedQuantity;
                                if (dbKit.SelectedQuantity == 0)
                                {
                                    dbKit.IsRemoved = true;
                                }
                            }
                        }
                    }
                }
                if (Convert.ToBoolean(dbEntity.KitCaseEquipments?.All(x => x.IsRemoved == true)))
                {
                    dbEntity.IsRemoved = true;

                }
                await _projectEquipmentMovementStorage.Update(dbEntity);
            }
        }

        /// <summary>
        /// Move Equipments
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<List<ProjectEquipmentMovementDto>> MovementEquipmentsByProjectId(int projectId, ProjectChangeStatusEnum status)
        {
            List<ProjectEquipmentMovementDto> dtos = await GetListEquipmentMovement(projectId);
            dtos.ForEach(x =>
            {
                x.MovementStatus = GetEquipmentMovementStatus(status);
            });

            List<ProjectEquipmentMovementDto> result = new List<ProjectEquipmentMovementDto>();
            foreach (ProjectEquipmentMovementDto el in dtos)
            {
                ProjectEquipmentMovementDto newEl = await MovementEquipment(projectId, el.GroupId, el);
                result.Add(newEl);
            }

            return result;
        }

        /// <summary>
        /// Move Equipments
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<List<ProjectEquipmentMovementDto>> MovementEquipmentsByProjectId(int projectId, ProjectStatusEnum status)
        {
            List<ProjectEquipmentMovementDto> dtos = await GetListEquipmentMovement(projectId);
            dtos.ForEach(x =>
            {
                x.MovementStatus = GetEquipmentMovementStatus(status);
            });

            List<ProjectEquipmentMovementDto> result = new List<ProjectEquipmentMovementDto>();
            foreach (ProjectEquipmentMovementDto el in dtos)
            {
                ProjectEquipmentMovementDto newEl = await MovementEquipment(projectId, el.GroupId, el);
                result.Add(newEl);
            }

            return result;
        }

        /// <summary>
        /// Move Equipments
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<List<ProjectEquipmentMovementDto>> MovementEquipments(int projectId, List<ProjectEquipmentMovementDto> dtos)
        {
            List<ProjectEquipmentMovementDto> result = new List<ProjectEquipmentMovementDto>();
            foreach (ProjectEquipmentMovementDto el in dtos)
            {
                ProjectEquipmentMovementDto newEl = await MovementEquipment(projectId, el.GroupId, el);
                result.Add(newEl);
            }

            return result;
        }

        #region PRIVATE

        private ProjectEquipmentMovementStatusEnum GetEquipmentMovementStatus(ProjectChangeStatusEnum status)
        {
            switch (status)
            {
                case ProjectChangeStatusEnum.Pack:
                case ProjectChangeStatusEnum.Default:
                    {
                        return ProjectEquipmentMovementStatusEnum.Pack;
                    }
                case ProjectChangeStatusEnum.Prepped:
                    {
                        return ProjectEquipmentMovementStatusEnum.Packed;
                    }
                case ProjectChangeStatusEnum.OnLocation:
                    {
                        return ProjectEquipmentMovementStatusEnum.Transportation;
                    }
                case ProjectChangeStatusEnum.InUse:
                    {
                        return ProjectEquipmentMovementStatusEnum.InUse;
                    }
            }

            return ProjectEquipmentMovementStatusEnum.Pack;
        }

        private ProjectEquipmentMovementStatusEnum GetEquipmentMovementStatus(ProjectStatusEnum status)
        {
            switch (status)
            {
                case ProjectStatusEnum.Application:
                case ProjectStatusEnum.Concept:
                case ProjectStatusEnum.Option:
                case ProjectStatusEnum.Confirmed:
                    {
                        return ProjectEquipmentMovementStatusEnum.Pack;
                    }
                case ProjectStatusEnum.Packed:
                    {
                        return ProjectEquipmentMovementStatusEnum.Packed;
                    }
                case ProjectStatusEnum.OnLocation:
                    {
                        return ProjectEquipmentMovementStatusEnum.Transportation;
                    }
                case ProjectStatusEnum.InUse:
                    {
                        return ProjectEquipmentMovementStatusEnum.InUse;
                    }
                case ProjectStatusEnum.Cancelled:
                case ProjectStatusEnum.Returned:
                    {
                        return ProjectEquipmentMovementStatusEnum.Returned;
                    }
            }

            return ProjectEquipmentMovementStatusEnum.Pack;
        }

        #endregion
    }
}
