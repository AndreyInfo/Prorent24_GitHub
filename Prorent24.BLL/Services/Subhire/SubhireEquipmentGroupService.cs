using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Prorent24.BLL.Builders;
using Prorent24.BLL.Extentions;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.General.Period;
using Prorent24.BLL.Models.Subhire;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.BLL.Transfers.Subhire;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Storages.Configuration.Account.UserRole;
using Prorent24.DAL.Storages.Equipment;
using Prorent24.DAL.Storages.General.Column;
using Prorent24.DAL.Storages.Project;
using Prorent24.DAL.Storages.Subhire;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.General;

namespace Prorent24.BLL.Services.Subhire
{
    internal class SubhireEquipmentGroupService : BaseService, ISubhireEquipmentGroupService
    {
        private readonly ISubhireEquipmentGroupStorage _subhireEquipmentGroupStorage;
        private readonly ISubhireEquipmentStorage _subhireEquipmentStorage;
        private readonly IProjectEquipmentStorage _projectEquipmentStorage;
        private readonly IEquipmentStorage _equipmentStorage;

        public SubhireEquipmentGroupService(IHttpContextAccessor httpContextAccessor, IUserRoleStorage userRoleStorage,
            IColumnStorage сolumnStorage, ISubhireEquipmentGroupStorage subhireEquipmentGroupStorage,
            ISubhireEquipmentStorage subhireEquipmentStorage, IProjectEquipmentStorage projectEquipmentStorage,
            IEquipmentStorage equipmentStorage) : base(httpContextAccessor, userRoleStorage, сolumnStorage)
        {
            _subhireEquipmentGroupStorage = subhireEquipmentGroupStorage;
            _subhireEquipmentStorage = subhireEquipmentStorage;
            _projectEquipmentStorage = projectEquipmentStorage;
            _equipmentStorage = equipmentStorage;
        }


        public async Task<SubhireEquipmentGridDto> CreateEquipment(SubhireEquipmentDto model)
        {
            //SubhireEquipmentEntity transferEntity = model.TransferToEntity();
            EquipmentEntity equipment = await _equipmentStorage.GetById(model.EquipmentId);
            SubhireEquipmentEntity transferEntity = new SubhireEquipmentEntity()
            {
                Name = equipment.Name,
                SubhireId = model.SubhireId,
                SubhireEquipmentGroupId = model.GroupId,
                EquipmentId = model.EquipmentId,
                Children = equipment.SetType == SetTypeEnum.Kit ? equipment.EquipmentContents?.Select(x => new SubhireEquipmentEntity()
                {
                    Name = x.Content.Name,
                    SubhireId = model.SubhireId,
                    SubhireEquipmentGroupId = model.GroupId,
                    EquipmentId = x.ContentId,
                    Quantity = x.Quantity,
                    Price = 0,
                    TotalPrice = 0,
                    Discount = 0,
                    Factor = 0
                }).ToList() : null,
                Quantity = model.Quantity,
                Price = equipment.SubhirePrice ?? 0,
                TotalPrice = equipment.SubhirePrice!=null?(((decimal)equipment.SubhirePrice).TotalPriceCalculation(0, 1, model.Quantity)):0,
                Discount = 0,
                Factor = model.Factor

            };
            SubhireEquipmentEntity entity = await _subhireEquipmentStorage.Create(transferEntity);
            SubhireEquipmentEntity result = await _subhireEquipmentStorage.GetById(transferEntity.Id);

            return result.TransferToTreeGridDto();
        }

        public async Task<SubhireEquipmentGroupDto> CreateGroup(SubhireEquipmentGroupDto model)
        {
            SubhireEquipmentGroupEntity transferEntity = model.TransferToEntity();
            SubhireEquipmentGroupEntity entity = await _subhireEquipmentGroupStorage.Create(transferEntity);
            SubhireEquipmentGroupEntity result = await _subhireEquipmentGroupStorage.GetById(transferEntity.Id);
            SubhireEquipmentGroupDto dto = result.TransferToDto();
            return dto;
        }


        public async Task<SubhireEquipmentGroupDto> GetGroupById(int id)
        {
            SubhireEquipmentGroupEntity entity = await _subhireEquipmentGroupStorage.GetById(id);
            SubhireEquipmentGroupDto dto = entity.TransferToDto();
            return dto;
        }

        public async Task<bool> DeleteEquipment(int id)
        {
            bool result = await _subhireEquipmentStorage.Delete(id);
            return result;
        }

        public async Task<bool> DeleteGroup(int id)
        {
            bool result = await _subhireEquipmentGroupStorage.Delete(id);
            return result;
        }

        public async Task<List<SubhireEquipmentGroupDto>> GetAllGroupBySubhire(int subhireId)
        {
            List<SubhireEquipmentGroupEntity> entities = await _subhireEquipmentGroupStorage.GetAllBySubhire(subhireId);
            List<SubhireEquipmentGroupDto> listDtos = entities.TransferToListDto();
            return listDtos;

        }

        public async Task<BaseListDto> GetEquipmentsBySubhire(int subhireId)
        {
            List<SubhireEquipmentGroupEntity> entityGroups = await _subhireEquipmentGroupStorage.GetAllBySubhire(subhireId);
            List<SubhireEquipmentEntity> entities = await _subhireEquipmentStorage.GetAllBySubhire(subhireId);

            List<SubhireEquipmentGridDto> prepareGrid = new List<SubhireEquipmentGridDto>();

            foreach (SubhireEquipmentGroupEntity groupEntity in entityGroups)
            {
                var group = new SubhireEquipmentGridDto()
                {
                    Id = groupEntity.Id,
                    GroupId = groupEntity.Id,
                    GroupName = groupEntity.Name,
                    Name = groupEntity.Name,
                    Price = null,
                    Quantity = null,
                    Factor = null,
                    Discount = null
                };
                foreach (SubhireEquipmentEntity equipment in entities)
                {
                    if (groupEntity.Id == equipment.SubhireEquipmentGroupId)
                    {
                        group.Childrens.Add(
                            equipment.TransferToTreeGridDto(groupEntity.Subhire?.UsagePeriodStart, groupEntity.Subhire?.UsagePeriodEnd)
                       );
                    }
                }
                prepareGrid.Add(group);

            }
            BaseGrid grid = prepareGrid.CreateGrid<SubhireEquipmentGridDto>(await GetUserColumns(EntityEnum.SubhireEquipmentEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.SubhireEquipmentEntity
            };
        }

        public async Task<SubhireEquipmentGridDto> UpdateEquipment(SubhireEquipmentDto model)
        {
            SubhireEquipmentEntity entity = await _subhireEquipmentStorage.GetById(model.Id);
            //can edit only Quantity,Discount,Remark 
            entity.Quantity = model.Quantity;
            entity.Discount = model.Discount;
            entity.Remark = model.Remark;
            if (model.ParentId == null) //if ParentId == null + can edit Price, Factor, TotalPrice
            {
                entity.Price = model.Price;
                entity.Factor = model.Factor;
                entity.TotalPrice = model.Price.TotalPriceCalculation(model.Discount, model.Factor, model.Quantity); 
            }
            SubhireEquipmentEntity entityUpd = await _subhireEquipmentStorage.Update(entity);
            SubhireEquipmentEntity result = await _subhireEquipmentStorage.GetById(entityUpd.Id);
            return result.TransferToTreeGridDto();
        }

        public async Task<SubhireEquipmentGroupDto> UpdateGroup(SubhireEquipmentGroupDto model)
        {
            // can edit only field Name
            SubhireEquipmentGroupEntity entity = await _subhireEquipmentGroupStorage.GetById(model.Id);
            entity.Name = model.Name;
            SubhireEquipmentGroupEntity entityUpd = await _subhireEquipmentGroupStorage.Update(entity);
            SubhireEquipmentGroupDto dto = entityUpd.TransferToDto();
            return dto;
        }

        public async Task<BaseListDto> GetShortages(List<SelectedFilter> filters)
        {
            string searchText = string.Empty;
            IQueryable<ProjectEquipmentEntity> queryableEntity = _projectEquipmentStorage.QueryableEntity.CreateFilterForProjectEquipmentEntity(filters, out searchText);
            List<ProjectEquipmentEntity> equipmentList = await _projectEquipmentStorage.GetAllByFilter(queryableEntity, searchText);
            //не видаляти, може знадобитися для складної логіки по к-ті взятих в суборенду
            //DateTime? dateFrom = DateTime.MinValue;
            //DateTime? dateTo = DateTime.MaxValue;
            //SelectedFilter periodFilter = filters.Where(x => (FilterEnum)System.Enum.Parse(typeof(FilterEnum), x.FilterType) == FilterEnum.Period).FirstOrDefault();
            //if (periodFilter?.Values != null && periodFilter?.Values.Count > 0)
            //{
            //    PeriodDto period = JsonConvert.DeserializeObject<PeriodDto>(periodFilter.Values[0].ToString());

            //    if (period != null)
            //    {
            //        if (period.PeriodType == PeriodTypeEnum.Period)
            //        {
            //            DateTime date = DateTime.Now;

            //            if (period.DurationTime == DurationTimeEnum.Next)
            //            {
            //                dateFrom = date;
            //                dateTo = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, period.Duration);
            //            }
            //            else if (period.DurationTime == DurationTimeEnum.Past)
            //            {
            //                dateTo = date;
            //                dateFrom = date.AddAndGetDateByTimeUnitTypeEnum(period.TimeUnit, -period.Duration);
            //            }
            //        }
            //        else if (period.PeriodType == PeriodTypeEnum.FromToUntil)
            //        {
            //            if (period.FromDate.HasValue && period.FromDate.Value != DateTime.MinValue)
            //            {
            //                dateFrom = period.FromDate.Value;
            //            }

            //            if (period.ToDate.HasValue && period.ToDate.Value != DateTime.MinValue)
            //            {
            //                dateTo = period.ToDate.Value;
            //            }
            //        }
            //    }
            //}
            //if (dateFrom == DateTime.MinValue)
            //{
            //    dateFrom = equipmentList.Min(x => x.ProjectEquipmentGroup?.StartUsePeriod);
            //}
            //if (dateTo == DateTime.MaxValue)
            //{
            //    dateTo = equipmentList.Max(x => x.ProjectEquipmentGroup?.EndUsePeriod);
            //}
            List<SubhireShortageDto> result = equipmentList.Select(x => new SubhireShortageDto()
            {
                EquipmentId = (int)x.EquipmentId,
                ProjectId = (int)x.ProjectId,
                ProjectName = x.ProjectEquipmentGroup?.Project.Name,
                EquipmentName = x.Equipment?.Name,
                PlannedQuantity = x.Quantity,
                OwnStockQuantity = x.Equipment!=null ? (int)x.Equipment?.Quantity : 0,
                //SubhiredQuantity = Convert.ToInt32(x.Equipment?.SubhireEquipments.Sum(y => y.Quantity)),
                Explanation = String.Empty,
                StartUsePeriod = x.ProjectEquipmentGroup?.StartUsePeriod,
                EndUsePeriod = x.ProjectEquipmentGroup?.EndUsePeriod,
                SubhireIds = new int[] { }

            }).OrderBy(d => d.StartUsePeriod).ToList();

            //int g = (int)((DateTime)dateTo - (DateTime)dateFrom).TotalDays;
            //List<QuantityByPerid> listDays = new List<QuantityByPerid>();
            //for (int i = 0; i <= g; g++)
            //{
            //    listDays.Add(new QuantityByPerid
            //    {
            //        Quantity = 0,
            //        DateInPeriod = ((DateTime)dateFrom).AddDays(i)
            //    });
            //}
            List<int> distinctEquipmentIds = equipmentList.GroupBy(x => x.EquipmentId).Select(y => Convert.ToInt32(y.First().EquipmentId)).ToList();
            List<EquipmentEntity> distinctEquipment = await _equipmentStorage.GetByIds(distinctEquipmentIds);
            List<EquipmentDto> distinctEquipmentDto = distinctEquipment.TransferToListDto();//конвертуємо в dto, для розрахунку к-ті
            // конвертуємо в EquipmentQuantityByPeriodDto для складного виключення к-ті обладнання по періодам з власного асортименту
            //List<EquipmentQuantityByPeriodDto> distinctEquipmentPeriodDto = distinctEquipmentDto.Select(x => new EquipmentQuantityByPeriodDto
            //{
            //    Id = x.Id,
            //    QuantityByPerids = listDays.Select(y=> { y.Quantity = x.Quantity; return y; }).ToList()


            //}).ToList();

            //foreach (SubhireShortageDto el in result)
            //{
            //    foreach (EquipmentQuantityByPeriodDto equipment in distinctEquipmentPeriodDto)
            //    {
            //        if (el.EquipmentId == equipment.Id)
            //        {
            //            if (el.PlannedQuantity > 0)
            //            {
            //                //TODO розбиття на нові періоди по днях, різниця мін в 1 день
            //                foreach (QuantityByPerid quantityPeriod in equipment.QuantityByPerids)
            //                {
            //                    if (el.StartUsePeriod >= quantityPeriod.DateInPeriod && el.EndUsePeriod <= quantityPeriod.DateInPeriod)
            //                    {
            //                        if (quantityPeriod.Quantity > el.PlannedQuantity)
            //                        {
            //                            el.OwnStockQuantity = el.PlannedQuantity;
            //                            quantityPeriod.Quantity = quantityPeriod.Quantity - el.PlannedQuantity;
            //                            break;

            //                        }
            //                        else
            //                        {
            //                            el.OwnStockQuantity = quantityPeriod.Quantity;
            //                            quantityPeriod.Quantity = 0;
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}


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

            List<SubhireShortageGridDto> treeGrid = prepareList.TransferToTreeGridDto();
            foreach (SubhireShortageGridDto el in treeGrid)
            {
                el.OwnStockQuantity = null;
                el.PlannedQuantity = null;
                el.ShortageQuantity = null;

            }
            treeGrid.Select(c => { c.OwnStockQuantity = null; c.PlannedQuantity = null; c.ShortageQuantity = null; c.SubhiredQuantity = null; return c; }).ToList();
            BaseGrid grid = treeGrid.CreateGrid<SubhireShortageGridDto>(await GetUserColumns(EntityEnum.SubhireShortageEntity));

            return new BaseListDto()
            {
                Grid = grid,
                Entity = EntityEnum.SubhireShortageEntity
            };
        }
    }
}
