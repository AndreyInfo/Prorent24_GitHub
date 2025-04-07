using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.BLL.Transfers.Project;
using Prorent24.BLL.Transfers.Subhire;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.Project.Movement;
using Prorent24.DAL.Storages.Subhire;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Entity;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.Project;

namespace Prorent24.BLL.Services.Project.Equipment
{
    internal class ProjectEquipmentService : BaseService, IProjectEquipmentService
    {
        private readonly IProjectEquipmentGroupStorage _projectEquipmentGroupStorage;
        private readonly IProjectEquipmentStorage _projectEquipmentStorage;
        private readonly IProjectStorage _projectStorage;
        private readonly IEquipmentStorage _equipmentStorage;
        private readonly ISystemSettingStorage _systemSettingStorage;
        private readonly IProjectEquipmentMovementStorage _projectEquipmentMovementStorage;
        private readonly ISubhireEquipmentStorage _subhireEquipmentStorage;


        public ProjectEquipmentService(IHttpContextAccessor httpContextAccessor,
                                       IUserRoleStorage userRoleStorage,
                                       IColumnStorage сolumnStorage,
                                       IProjectEquipmentGroupStorage projectEquipmentGroupStorage,
                                       IProjectEquipmentStorage projectEquipmentStorage,
                                       IProjectStorage projectStorage,
                                       IEquipmentStorage equipmentStorage, 
                                       ISystemSettingStorage systemSettingStorage,
                                       IProjectEquipmentMovementStorage projectEquipmentMovementStorage,
                                       ISubhireEquipmentStorage subhireEquipmentStorage) :base(httpContextAccessor,
                                                                                                               userRoleStorage,
                                                                                                               сolumnStorage)

        {
            _projectEquipmentGroupStorage = projectEquipmentGroupStorage;
            _projectEquipmentStorage = projectEquipmentStorage;
            _projectStorage = projectStorage;
            _equipmentStorage = equipmentStorage;
            _systemSettingStorage = systemSettingStorage;
            _projectEquipmentMovementStorage = projectEquipmentMovementStorage;
            _subhireEquipmentStorage = subhireEquipmentStorage;
        }

        public async Task<BaseListDto> GetBaseGridAllEquipmentsByProjectId(int projectId)
        {
            List<ProjectEquipmentGroupEntity> groups = await _projectEquipmentGroupStorage.GetAllByProject(projectId);
            List<ProjectEquipmentGroupDto> listgroupDtos = groups.TransferToListDto();

            List<ProjectEquipmentEntity> entities = await _projectEquipmentStorage.GetAllByProject(projectId);
            List<ProjectEquipmentDto> listDtos = entities.TransferToListDto();

            List<ProjectEquipmentGridDto> gridData = listgroupDtos.TransferToListGridDto();
            gridData.Select(x => { x.Childrens = listDtos.Where(y => y.GroupId == x.GroupId).ToList().TransferToListGridDto(); return x; }).ToList();

            BaseGrid grid = gridData.CreateGrid<ProjectEquipmentGridDto>(await GetUserColumns(EntityEnum.ProjectEquipmentEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.ProjectEquipmentEntity
            };
        }

        public async Task<List<ProjectEquipmentGridDto>> GetEquipmentsByProjectId(int projectId)
        {
            List<ProjectEquipmentGroupEntity> groups = await _projectEquipmentGroupStorage.GetAllByProject(projectId);
            List<ProjectEquipmentGroupDto> listgroupDtos = groups.TransferToListDto();

            List<ProjectEquipmentEntity> entities = await _projectEquipmentStorage.GetAllByProject(projectId);
            List<ProjectEquipmentDto> listDtos = entities.TransferToListDto();

            List<ProjectEquipmentGridDto> gridData = listgroupDtos.TransferToListGridDto();
            gridData.Select(x => { x.Childrens = listDtos.Where(y => y.GroupId == x.GroupId).ToList().TransferToListGridDto(); return x; }).ToList();

            return gridData;
        }
        public async Task<ProjectEquipmentGridDto> CreateEquipment(ProjectEquipmentDto model)
        {
            ProjectEquipmentEntity transferEntity = model.TransferToEntity();
            // поки по замовчуванню фактор 1
            if (model.Factor <= 0)
            {
                transferEntity.Factor = 1;
            }
            EquipmentEntity equipmentEntity = await _equipmentStorage.GetById(model.EquipmentId);
            if (equipmentEntity?.SupplyType == SupplyTypeEnum.Rental)
            {
                transferEntity.Price = equipmentEntity.SubhirePrice ?? 0;
                transferEntity.TotalPrice = model.Quantity * (equipmentEntity.SubhirePrice ?? 0) * (100 - model.Discount) / 100 * transferEntity.Factor;
            }
            else
            {
                transferEntity.Price = equipmentEntity.SalePrice ?? 0;
                transferEntity.TotalPrice = model.Quantity * (equipmentEntity.SalePrice ?? 0) * (100 - model.Discount) / 100;
            }
            //якщо на фронті не вибрано вид податку - беремо дефолтне значення із с-ми
            if (transferEntity.VatClassId == null)
            {
                SystemSettingEntity sysSetting = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.FinancialSetting);
                FinancialSettingDto dtoSysFin = sysSetting?.TransferToDto<FinancialSettingDto>();
                transferEntity.VatClassId = dtoSysFin.DefaultVatClassId;
            }
            EquipmentEntity equipment = await _equipmentStorage.GetById(model.EquipmentId);
            transferEntity.Children = equipment.SetType == SetTypeEnum.Kit ? equipment.EquipmentContents?.Select(x => new ProjectEquipmentEntity()
            {
                Name = x.Content.Name,
                ProjectId = model.ProjectId,
                ProjectEquipmentGroupId = model.GroupId,
                EquipmentId = x.ContentId,
                Quantity = x.Quantity,
                Price = 0,
                TotalPrice = 0,
                Discount = 0,
                Factor = 0,

            }).ToList() : null;


            ProjectEquipmentEntity entity = await _projectEquipmentStorage.Create(transferEntity);
            ProjectEquipmentEntity result = await _projectEquipmentStorage.GetById(transferEntity.Id);
            //if (result.Equipment.SetType == SetTypeEnum.Kit)
            //{
            //    if (result.Equipment != null && result.Equipment.EquipmentContents != null && result.Equipment.EquipmentContents.Count > 0)
            //    {
            //        entity.Children = new List<ProjectEquipmentEntity>();

            //        List<ProjectEquipmentEntity> childrenEquipments = model.TransferToListEntity(result.Equipment.EquipmentContents.ToList(), result.Id);

            //        foreach (ProjectEquipmentEntity item in childrenEquipments)
            //        {
            //            entity.Children.Add(item);

            //            if (equipmentEntity?.SupplyType == SupplyTypeEnum.Rental)
            //            {
            //                entity.TotalPrice = item.Quantity * item.Price * (100 - item.Discount) / 100 * transferEntity.Factor;
            //            }
            //            else
            //            {
            //                item.TotalPrice = item.Quantity * item.Price * (100 - item.Discount) / 100;

            //            }
            //        }

            //        entity = await _projectEquipmentStorage.Update(entity);
            //    }
            //}

            ProjectEquipmentDto dto = result.TransferToDto();
            ProjectEquipmentMovementEntity entityMovement = new ProjectEquipmentMovementEntity()
            {
                ProjectId = dto.ProjectId,
                GroupId = dto.GroupId,
                ProjectEquipmentId = dto.Id,
                EquipmentId = dto.EquipmentId,
                SelectedQuantity = dto.Quantity,
                TotalQuantity = dto.Quantity,
                MovementStatus = Enum.Project.ProjectEquipmentMovementStatusEnum.Pack,
                KitCaseEquipments = dto.Equipment?.SetType == SetTypeEnum.Kit ? dto.Children?.Select(x => new ProjectEquipmentMovementEntity()
                {
                    ProjectId = dto.ProjectId,
                    GroupId = dto.GroupId,
                    ProjectEquipmentId = x.Id,
                    EquipmentId = x.EquipmentId,
                    SelectedQuantity = x.Quantity * dto.Quantity,
                    TotalQuantity = x.Quantity * dto.Quantity,
                    MovementStatus = Enum.Project.ProjectEquipmentMovementStatusEnum.Pack,
                }).ToList() : null

            };
            await CreateMovementEquipment(entityMovement);


            return dto.TransferToGridDto();
        }

        public async Task CreateMovementEquipment(ProjectEquipmentMovementEntity entity)
        {
            await _projectEquipmentMovementStorage.Create(entity);

        }

        public async Task<ProjectEquipmentGridDto> CreateGroup(ProjectEquipmentGroupDto model)
        {
            ProjectEquipmentGroupEntity transferEntity = model.TransferToEntity();
            ProjectEntity proj = await _projectStorage.GetById(model.ProjectId);
            if (model.StartUsePeriod == null || model.EndUsePeriod == null)
            {
                ProjectTimeEntity usePeriod = proj.Times?.Where(x => x.DefaultUsagePeriod == true).FirstOrDefault();
                transferEntity.StartUsePeriod = usePeriod?.From;
                transferEntity.EndUsePeriod = usePeriod?.Until;
            }
            var planPeriod = proj.Times.FirstOrDefault(x => x.DefaultPlanPeriod);
            transferEntity.StartPlanPeriod = planPeriod?.From;
            transferEntity.EndPlanPeriod = planPeriod?.Until;

            ProjectEquipmentGroupEntity entity = await _projectEquipmentGroupStorage.Create(transferEntity);
            ProjectEquipmentGridDto gridModel = entity.TransferToDto().TransferToGridDto();
            return gridModel;
        }

        public async Task<bool> DeleteEquipment(int id)
        {
            List<ProjectEquipmentMovementEntity> movements = await _projectEquipmentMovementStorage.GetEquipmentMovementByProjectEquipmentId(id);
            foreach (ProjectEquipmentMovementEntity el in movements)
            {
                el.IsRemoved = true;
                if (el.KitCaseEquipments?.Count > 0)
                {
                    foreach (ProjectEquipmentMovementEntity kit in el.KitCaseEquipments)
                    {
                        kit.IsRemoved = true;
                    }
                }
            }
            await _projectEquipmentMovementStorage.UpdateMoveEquipments(movements);
            bool result = await _projectEquipmentStorage.Delete(id);
            return result;
        }

        public async Task<bool> DeleteGroup(int id)
        {
            bool result = await _projectEquipmentGroupStorage.Delete(id);
            return result;
        }

        public async Task<List<ProjectEquipmentGroupDto>> GetAllGroupByProject(int projectId)
        {
            List<ProjectEquipmentGroupEntity> entities = await _projectEquipmentGroupStorage.GetAllByProject(projectId);
            List<ProjectEquipmentGroupDto> listDtos = entities.TransferToListDto();
            // приводимо вкладеності до одного списку
            //foreach (ProjectEquipmentGroupEntity entity in entities)
            //{
            //    listDtos.Add(new ProjectEquipmentDto()
            //    {
            //        GroupId = entity.Id,
            //        Name = entity.Name + ((entity.StartUsePeriod!=null && entity.EndUsePeriod!=null)? "(" + entity.StartUsePeriod + " - " + entity.EndUsePeriod + ")": "")
            //    });

            //    //if (el.Equipments?.Count > 0)
            //    //{
            //    //    foreach (ProjectEquipmentEntity equipment in el.Equipments)
            //    //    {
            //    //        listDtos.Add(new ProjectEquipmentDto()
            //    //        {
            //    //            Id = equipment.Id,
            //    //            GroupId = el.Id,
            //    //            Name = equipment.Name,
            //    //            ParentId = equipment.ParentId,
            //    //            Quantity = equipment.Quantity,
            //    //            Price = equipment.Price,
            //    //            Discount = equipment.Discount,
            //    //            Factor = equipment.Factor,
            //    //            Remark = equipment.Remark
            //    //        });
            //    //    }
            //    //}
            //}

            //List<SubhireEquipmentGroupDto> listDtos = entities.TransferToListDto();
            //BaseGrid grid = listDtos.CreateGrid<ProjectEquipmentDto>();

            return listDtos;
        }

        public async Task<ProjectEquipmentGridDto> UpdateEquipment(ProjectEquipmentDto model)
        {
            ProjectEquipmentEntity entity = await _projectEquipmentStorage.GetById(model.Id);
            bool isChangeMovements = model.Quantity != entity.Quantity;
            //can edit only Name,Quantity,Discount,Remark
            entity.Name = model.Name;
            entity.Quantity = model.Quantity;
            entity.Discount = model.Discount;
            entity.Remark = model.Remark;
            entity.VatClassId = model.VatClassId;
            if (model.ParentId == null) //if ParentId == null + can edit Price, Factor
            {
                entity.Price = model.Price;
                entity.Factor = model.Factor;
            }
            entity.TotalPrice = model.Quantity * model.Price * (100 - model.Discount) / 100 * model.Factor;
            ProjectEquipmentEntity entityUpd = await _projectEquipmentStorage.Update(entity);
            ProjectEquipmentEntity result = await _projectEquipmentStorage.GetById(entity.Id);
            if (isChangeMovements)
            {
                List<ProjectEquipmentMovementEntity> movements = await _projectEquipmentMovementStorage.GetEquipmentMovementByProjectEquipmentId(result.Id);
                foreach (ProjectEquipmentMovementEntity el in movements.Where(x => x.MovementStatus == ProjectEquipmentMovementStatusEnum.Pack))
                {
                    var dbSelectedQuantity = el.SelectedQuantity;
                    var dbTotalQuantity = el.TotalQuantity;
                    if (el.SelectedQuantity == el.TotalQuantity)
                    {
                        el.SelectedQuantity = result.Quantity;
                        el.TotalQuantity = result.Quantity;
                    }
                    else
                    {
                        el.SelectedQuantity = result.Quantity - el.TotalQuantity + dbSelectedQuantity;
                        el.TotalQuantity = result.Quantity;
                    }

                    if (el.KitCaseEquipments?.Count > 0)
                    {
                        foreach (ProjectEquipmentMovementEntity kit in el.KitCaseEquipments.Where(x => x.MovementStatus == ProjectEquipmentMovementStatusEnum.Pack))
                        {
                            if (kit.SelectedQuantity == kit.TotalQuantity)
                            {
                                kit.SelectedQuantity = kit.ProjectEquipment.Quantity * result.Quantity;
                                kit.TotalQuantity = kit.ProjectEquipment.Quantity * result.Quantity;
                            }
                            else
                            {
                                kit.SelectedQuantity = kit.ProjectEquipment.Quantity * (result.Quantity - dbTotalQuantity) + kit.SelectedQuantity;
                                kit.TotalQuantity = kit.ProjectEquipment.Quantity * result.Quantity;
                            }
                        }
                    }
                }

                //movements.ForEach(x => x.TotalQuantity = result.Quantity);
                await _projectEquipmentMovementStorage.UpdateMoveEquipments(movements);
            }
            ProjectEquipmentGridDto gridModel = result.TransferToDto().TransferToGridDto();
            return gridModel;
        }

        public async Task<ProjectEquipmentGridDto> UpdateGroup(ProjectEquipmentGroupDto model)
        {
            // can edit only fields Name, StartPlanPeriod, StartUsePeriod, EndUsePeriod
            ProjectEquipmentGroupEntity entity = await _projectEquipmentGroupStorage.GetById(model.Id);
            entity.Name = model.Name;
            entity.StartPlanPeriod = model.StartPlanPeriod;
            entity.EndPlanPeriod = model.EndPlanPeriod;
            entity.StartUsePeriod = model.StartUsePeriod;
            entity.EndUsePeriod = model.EndUsePeriod;
            ProjectEquipmentGroupEntity entityUpd = await _projectEquipmentGroupStorage.Update(entity);
            ProjectEquipmentGridDto gridModel = entityUpd.TransferToDto().TransferToGridDto();
            return gridModel;
        }

        public async Task<ProjectEquipmentDto> GetById(int id)
        {
            ProjectEquipmentEntity entity = await _projectEquipmentStorage.GetById(id);
            ProjectEquipmentDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<List<ModuleDetailDto>> GetDetails(int id)
        {
            ProjectEquipmentEntity entity = await _projectEquipmentStorage.GetById(id);
            ProjectEquipmentDto dto = entity?.TransferToDto();
            List<ModuleDetailDto> moduleDetails = dto?.CreateDetails<ProjectEquipmentDto>(EntityEnum.ProjectEquipmentEntity);

            return moduleDetails;
        }

        public async Task<BaseListDto> GetSublist(object id)
        {
            string searchText = string.Empty;
            List<SelectedFilter> filters = new List<SelectedFilter>();

            IQueryable<ProjectEquipmentEntity> queryableEntity = _projectEquipmentStorage.QueryableEntity.CreateFilterForProjectEquipmentEntity(filters, out searchText);
            queryableEntity = queryableEntity.Where(p => p.ProjectId == (int)id);
            
            List<ProjectEquipmentEntity> equipmentList = await _projectEquipmentStorage.GetAllByFilter(queryableEntity, searchText);

            List<SubhireShortageDto> result = equipmentList.Select(x => new SubhireShortageDto()
            {
                EquipmentId = (int)x.EquipmentId,
                ProjectId = (int)x.ProjectId,
                ProjectName = x.ProjectEquipmentGroup?.Project.Name,
                EquipmentName = x.Equipment?.Name,
                PlannedQuantity = x.Quantity,
                OwnStockQuantity = x.Equipment != null ? (int)x.Equipment?.Quantity : 0,
                //SubhiredQuantity = Convert.ToInt32(x.Equipment?.SubhireEquipments.Sum(y => y.Quantity)),
                Explanation = String.Empty,
                StartUsePeriod = x.ProjectEquipmentGroup?.StartUsePeriod,
                EndUsePeriod = x.ProjectEquipmentGroup?.EndUsePeriod,
                SubhireIds = new int[] { }

            }).OrderBy(d => d.StartUsePeriod).ToList();

            List<int> distinctEquipmentIds = equipmentList.GroupBy(x => x.EquipmentId).Select(y => Convert.ToInt32(y.First().EquipmentId)).ToList();
            List<EquipmentEntity> distinctEquipment = await _equipmentStorage.GetByIds(distinctEquipmentIds);
            List<EquipmentDto> distinctEquipmentDto = distinctEquipment.TransferToListDto();//конвертуємо в dto, для розрахунку к-ті


            foreach (SubhireShortageDto el in result)
            {
                foreach (EquipmentDto equipment in distinctEquipmentDto)
                {
                    if (el.EquipmentId == equipment.Id)
                    {
                        if (el.PlannedQuantity > 0)
                        {
                            if ((int)equipment.Quantity > el.PlannedQuantity)
                            {
                                el.OwnStockQuantity = el.PlannedQuantity;
                                equipment.Quantity = equipment.Quantity - el.PlannedQuantity;
                                break;

                            }
                            else
                            {
                                el.OwnStockQuantity = (int)equipment.Quantity;
                                equipment.Quantity = 0;
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            IQueryable<SubhireEquipmentEntity> subhireQueryableEntity = _subhireEquipmentStorage.QueryableEntity.CreateFilterForSubhireEquipmentEntity(filters);
            List<SubhireEquipmentEntity> subhireEquipmentList = await _subhireEquipmentStorage.GetAllByFilter(subhireQueryableEntity);
            foreach (SubhireShortageDto el in result)
            {
                foreach (SubhireEquipmentEntity equipment in subhireEquipmentList)

                {
                    if (el.EquipmentId == equipment.EquipmentId && el.PlannedQuantity - el.OwnStockQuantity > 0 && el.StartUsePeriod >= equipment.Subhire?.UsagePeriodStart && el.EndUsePeriod <= equipment.Subhire?.UsagePeriodEnd)
                    {
                        if (equipment.Quantity > 0)
                        {
                            int[] subhireArray = el.SubhireIds;
                            if (el.SubhiredQuantity < el.PlannedQuantity - el.OwnStockQuantity && (int)equipment.Quantity >= el.PlannedQuantity - el.OwnStockQuantity)
                            {
                                int preSubhiredQuantity = el.SubhiredQuantity;
                                if (preSubhiredQuantity > 0)
                                {
                                    if (preSubhiredQuantity + el.OwnStockQuantity + equipment.Quantity >= el.PlannedQuantity)
                                    {
                                        el.SubhiredQuantity = el.PlannedQuantity;

                                        Array.Resize(ref subhireArray, el.SubhireIds.Length + 1);
                                        subhireArray[subhireArray.Length - 1] = (int)equipment.SubhireId;
                                        el.SubhireIds = subhireArray;

                                        equipment.Quantity = equipment.Quantity - el.PlannedQuantity + el.OwnStockQuantity + preSubhiredQuantity;
                                        break;
                                    }
                                    else
                                    {
                                        el.SubhiredQuantity = equipment.Quantity + preSubhiredQuantity;

                                        Array.Resize(ref subhireArray, el.SubhireIds.Length + 1);
                                        subhireArray[subhireArray.Length - 1] = (int)equipment.SubhireId;
                                        el.SubhireIds = subhireArray;

                                        equipment.Quantity = 0;
                                        break;
                                    }
                                }
                                else
                                {

                                    if (el.OwnStockQuantity + equipment.Quantity >= el.PlannedQuantity)
                                    {
                                        el.SubhiredQuantity = el.PlannedQuantity - el.OwnStockQuantity;

                                        Array.Resize(ref subhireArray, el.SubhireIds.Length + 1);
                                        subhireArray[subhireArray.Length - 1] = (int)equipment.SubhireId;
                                        el.SubhireIds = subhireArray;

                                        equipment.Quantity = equipment.Quantity - el.PlannedQuantity + el.OwnStockQuantity;
                                        break;
                                    }
                                    else
                                    {
                                        el.SubhiredQuantity = equipment.Quantity;

                                        Array.Resize(ref subhireArray, el.SubhireIds.Length + 1);
                                        subhireArray[subhireArray.Length - 1] = (int)equipment.SubhireId;
                                        el.SubhireIds = subhireArray;

                                        equipment.Quantity = 0;
                                        break;
                                    }
                                }

                            }
                        }
                    }
                }
            }

            foreach (SubhireShortageDto el in result)
            {
                el.ShortageQuantity = el.PlannedQuantity - el.OwnStockQuantity - el.SubhiredQuantity < 0 ? 0 : el.PlannedQuantity - el.OwnStockQuantity - el.SubhiredQuantity;
            }

            List<SubhireShortageDto> prepareList = result.Where(x => x.PlannedQuantity > 0).GroupBy(x => x.ProjectId).Select(x => new SubhireShortageDto()
            {
                ProjectId = x.Key
            }).ToList();

            foreach (SubhireShortageDto el in prepareList)
            {
                foreach (SubhireShortageDto child in result)
                {
                    if (el.ProjectId == child.ProjectId)
                    {
                        el.ProjectName = child.ProjectName;
                        el.Childrens.Add(child);
                    }
                }
            }

            var treeGrid = prepareList.TransferToTreeGridDto().FirstOrDefault()?.Childrens?.Where(y=>y.ShortageQuantity>0)?.ToList();
            
            BaseGrid grid = treeGrid.CreateGrid<SubhireShortageGridDto>();

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.SubhireShortageEntity
            };
            // return treeGrid;
        }
    }
}
