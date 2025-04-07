using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Configuration;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.BLL.Transfers.Project;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Filters;
using Prorent24.Common.Models.Grids;
using Prorent24.DAL.Models.Project;
using Prorent24.DAL.Storages.Configuration.Financial.Vat;
using Prorent24.DAL.Storages.Configuration.SystemSettings;
using Prorent24.DAL.Storages.Project;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.Project;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Entity;
using Prorent24.Enum.Equipment;
using Prorent24.Enum.Project;
using Prorent24.UnitOfWork.PagedList;

namespace Prorent24.BLL.Services.Project.Financial
{
    internal class ProjectFinancialService : IProjectFinancialService
    {
        private readonly IProjectFinancialStorage _projectFinancialStorage;
        private readonly IProjectFinancialCategoryStorage _projectFinancialCategoryStorage;
        private readonly IProjectEquipmentGroupStorage _projectEquipmentGroupStorage;
        private readonly IProjectAdditionalCostStorage _projectAdditionalCostStorage;
        private readonly IProjectFunctionStorage _projectFunctionStorage;
        private readonly IProjectPlanningStorage _projectPlanningStorage;
        private readonly ISystemSettingStorage _systemSettingStorage;
        private readonly IVatSchemeStorage _vatSchemeStorage;


        public ProjectFinancialService(IProjectFinancialStorage projectFinancialStorage,
            IProjectFinancialCategoryStorage projectFinancialCategoryStorage,
            IProjectEquipmentGroupStorage projectEquipmentGroupStorage,
            IProjectFunctionStorage projectFunctionStorage,
            IProjectPlanningStorage projectPlanningStorage,
            ISystemSettingStorage systemSettingStorage,
            IVatSchemeStorage vatSchemeStorage,
            IProjectAdditionalCostStorage projectAdditionalCostStorage)
        {
            _projectFinancialStorage = projectFinancialStorage;
            _projectFinancialCategoryStorage = projectFinancialCategoryStorage;
            _projectEquipmentGroupStorage = projectEquipmentGroupStorage;
            _projectFunctionStorage = projectFunctionStorage;
            _projectPlanningStorage = projectPlanningStorage;
            _projectAdditionalCostStorage = projectAdditionalCostStorage;
            _systemSettingStorage = systemSettingStorage;
            _vatSchemeStorage = vatSchemeStorage;
        }

        public async Task<ProjectFinancialDto> GetByProjectId(int projectId)
        {
            ProjectFinancialEntity entity = await _projectFinancialStorage.GetByProjectId(projectId);
            ProjectFinancialDto dto = entity?.TransferToDto();
            return dto;
        }

        public async Task<ProjectFinancialDto> Update(ProjectFinancialDto dto)
        {
            ProjectFinancialEntity dbEntity = await _projectFinancialStorage.GetByProjectId(dto.ProjectId);
            bool isChangeVatScheme = false;
            if (dbEntity.VatSchemeId != dto.VatSchemeId)
            {
                isChangeVatScheme = true;
            }
            dbEntity.Deposit = dto.Deposit;
            dbEntity.AdditionalConditionId = dto.AdditionalConditionId;
            dbEntity.InvoiceMomentId = dto.InvoiceMomentId;
            dbEntity.LastModifiedDate = dto.LastModifiedDate;
            dbEntity.VatSchemeId = dto.VatSchemeId;


            ProjectFinancialDto result;
            ProjectFinancialEntity entity;
            //якщо змінюється схема оподаткування  - переховуємо TotalIncVat
            if (isChangeVatScheme)
            {
                decimal totalIncVat = await ReCalculateTotalIncVat(dto.ProjectId, dto.VatSchemeId);
                dbEntity.TotalIncVat = totalIncVat;
            }

            entity = await _projectFinancialStorage.Update(dbEntity);


            result = dbEntity.TransferToDto();

            return result;
        }


        public async Task CreateBaseCategoriesByProject(int projectId)
        {
            List<ProjectFinancialCategoryEntity> entities = new List<ProjectFinancialCategoryEntity>();

            foreach (ProjectFinancialCategoryEnum el in System.Enum.GetValues(typeof(ProjectFinancialCategoryEnum)))
            {
                entities.Add(new ProjectFinancialCategoryEntity()
                {
                    ProjectId = projectId,
                    Category = el

                });
            }

            await _projectFinancialCategoryStorage.CreateBaseCategoriesByProject(entities);
        }


        public async Task<ProjectFinancialDto> CreateDefaultFinancial(int projectId)
        {
            SystemSettingEntity sysSetting = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.FinancialSetting);
            FinancialSettingDto dtoSysFin = sysSetting?.TransferToDto<FinancialSettingDto>();
            int vatSchemeId;
            List<VatSchemeEntity> vatSchemes = await _vatSchemeStorage.GetList();
            if (dtoSysFin != null && vatSchemes.Select(x => x.Id).Contains(dtoSysFin.DefaultVatSchemeId))
            {
                vatSchemeId = dtoSysFin.DefaultVatSchemeId;
            }
            else
            {
                vatSchemeId = (await _vatSchemeStorage.GetList()).FirstOrDefault().Id;
            }
            ProjectFinancialEntity defaultEntity = new ProjectFinancialEntity()
            {
                Deposit = 0,
                ProjectId = projectId,
                VatSchemeId = vatSchemeId,
                TotalIncVat = 0
            };
            ProjectFinancialEntity entity = await _projectFinancialStorage.Create(defaultEntity);
            ProjectFinancialDto result = entity.TransferToDto();
            return result;
        }

        public async Task SaveSaleRentalSubCategoriesByProject(int projectId, int equipmentGroupId, bool isDeleteGroup)
        {
            ProjectEquipmentGroupEntity qroupEntity = await _projectEquipmentGroupStorage.GetById(equipmentGroupId);
            List<ProjectEquipmentEntity> saleList = qroupEntity?.Equipments?.Where(y => y.Equipment.SupplyType == SupplyTypeEnum.Sale).ToList();
            List<ProjectEquipmentEntity> rentalList = qroupEntity?.Equipments?.Where(y => y.Equipment.SupplyType == SupplyTypeEnum.Rental).ToList();
            ProjectFinancialCategoryEntity saleCategoryEntity = await _projectFinancialCategoryStorage.GetByCategoryAndEquipmentGroup(projectId, equipmentGroupId, ProjectFinancialCategoryEnum.Sale);
            ProjectFinancialCategoryEntity rentalCategoryEntity = await _projectFinancialCategoryStorage.GetByCategoryAndEquipmentGroup(projectId, equipmentGroupId, ProjectFinancialCategoryEnum.Rental);
            ProjectFinancialCategoryEntity parentSaleCategoryEntity = await _projectFinancialCategoryStorage.GetByCategory(projectId, ProjectFinancialCategoryEnum.Sale);
            ProjectFinancialCategoryEntity parentRentalCategoryEntity = await _projectFinancialCategoryStorage.GetByCategory(projectId, ProjectFinancialCategoryEnum.Rental);

            if (saleList?.Count > 0)
            {
                //Task TaskSaveSubCategory = SaveSubCategory(projectId, equipmentGroupId, qroupEntity, saleList, saleCategoryEntity, parentSaleCategoryEntity);
                //Task TaskCalculateRentalSaleFinanceCategory = CalculateRentalSaleFinanceCategory(projectId, parentSaleCategoryEntity, ProjectFinancialCategoryEnum.Sale);
                //await Task.Factory.StartNew(() => TaskSaveSubCategory).ContinueWith((t1) => TaskCalculateRentalSaleFinanceCategory);
                bool resultSubCategory = await SaveSubCategory(projectId, equipmentGroupId, qroupEntity, saleList, saleCategoryEntity, parentSaleCategoryEntity, ProjectFinancialCategoryEnum.Sale);
                bool result = await CalculateRentalSaleFinanceCategory(projectId, parentSaleCategoryEntity, ProjectFinancialCategoryEnum.Sale);
            }
            else if (saleCategoryEntity != null && isDeleteGroup == true)
            {
                bool result = await _projectFinancialCategoryStorage.Delete(saleCategoryEntity.Id);
                bool res = await CalculateRentalSaleFinanceCategory(projectId, parentSaleCategoryEntity, ProjectFinancialCategoryEnum.Sale);

            }
            if (rentalList?.Count > 0)
            {
                bool resultSubCategory = await SaveSubCategory(projectId, equipmentGroupId, qroupEntity, rentalList, rentalCategoryEntity, parentRentalCategoryEntity, ProjectFinancialCategoryEnum.Rental);
                bool result = await CalculateRentalSaleFinanceCategory(projectId, parentRentalCategoryEntity, ProjectFinancialCategoryEnum.Rental);

            }
            else if (rentalCategoryEntity != null && isDeleteGroup == true)
            {
                bool result = await _projectFinancialCategoryStorage.Delete(rentalCategoryEntity.Id);
                bool res = await CalculateRentalSaleFinanceCategory(projectId, parentRentalCategoryEntity, ProjectFinancialCategoryEnum.Rental);

            }
            await UpdateTotalCategoryAndTotalIncVat(projectId);
        }



        /// <summary>
        /// прорахунок фінансів в категоріях Sale, Rental - на основі суми по вкладеним групам обладнання 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="categoryEntity"></param>
        /// <param name="categoryType"></param>
        /// <returns></returns>
        private async Task<bool> CalculateRentalSaleFinanceCategory(int projectId, ProjectFinancialCategoryEntity categoryEntity, ProjectFinancialCategoryEnum categoryType)
        {
            List<ProjectFinancialCategoryEntity> subCategories = new List<ProjectFinancialCategoryEntity>();
            if (categoryType == ProjectFinancialCategoryEnum.Rental)
            {
                subCategories = await _projectFinancialCategoryStorage.GetSubCategories(projectId, ProjectFinancialCategoryEnum.Rental);
            }
            else if (categoryType == ProjectFinancialCategoryEnum.Sale)
            {
                subCategories = await _projectFinancialCategoryStorage.GetSubCategories(projectId, ProjectFinancialCategoryEnum.Sale);
            }

            categoryEntity.EstimatedCosts = subCategories.Sum(x => x.EstimatedCosts);
            categoryEntity.PlannedCosts = subCategories.Sum(x => x.PlannedCosts);
            categoryEntity.ActualCosts = subCategories.Sum(x => x.ActualCosts);
            categoryEntity.Revenue = subCategories.Sum(x => x.Revenue);
            categoryEntity.Profit = subCategories.Sum(x => x.Profit);
            categoryEntity.Total = subCategories.Sum(x => x.Total);
            categoryEntity.TotalIncVat = subCategories.Sum(x => x.TotalIncVat);
            await _projectFinancialCategoryStorage.Update(categoryEntity);
            return true;
        }


        private async Task<bool> SaveSubCategory(int projectId, int equipmentGroupId, ProjectEquipmentGroupEntity qroupEntity,
        List<ProjectEquipmentEntity> equipmentList, ProjectFinancialCategoryEntity categoryEntity,
        ProjectFinancialCategoryEntity parentCategoryEntity, ProjectFinancialCategoryEnum categoryType, VatSchemeRateDto vatScheme = null)
        {

            decimal costs = Convert.ToDecimal(equipmentList.Sum(x => x.Equipment?.SubhirePrice * x.Quantity));
            decimal revenue = equipmentList.Sum(y => y.TotalPrice);
            decimal total = revenue * (100 - parentCategoryEntity.Discount) / 100;
            if (vatScheme == null)
            {
                vatScheme = await GetVatScheme(projectId);
            }
            decimal totalIncVat = 0;
            if (vatScheme.Type == VatSchemeTypeEnum.Rates)
            {
                totalIncVat = Convert.ToDecimal(equipmentList.Sum(x => x.Equipment?.SupplyType == SupplyTypeEnum.Sale
                 ? (x.Price != 0 ? x.Price : x.Equipment?.SalePrice) * (100 + (vatScheme.VatScheme?.VatClassSchemeRates?.Where(y => y.VatClassId == (x.VatClassId ?? x.Equipment?.VatClass?.Id)).FirstOrDefault()?.Rate ?? 0)) / 100 * x.Quantity
                 : (x.Price != 0 ? x.Price : x.Equipment?.RentalPrice) * (100 + (vatScheme.VatScheme?.VatClassSchemeRates?.Where(y => y.VatClassId == (x.VatClassId ?? x.Equipment?.VatClass?.Id)).FirstOrDefault()?.Rate ?? 0)) / 100 * x.Quantity * x.Factor));
            }
            else if (vatScheme.Type == VatSchemeTypeEnum.FixedRate)
            {
                totalIncVat = Convert.ToDecimal(equipmentList.Sum(x => x.Equipment?.SupplyType == SupplyTypeEnum.Sale
                  ? (x.Price != 0 ? x.Price : x.Equipment?.SalePrice) * (100 + vatScheme.Rate) / 100 * x.Quantity
                  : (x.Price != 0 ? x.Price : x.Equipment?.RentalPrice) * (100 + vatScheme.Rate) / 100 * x.Quantity * x.Factor));
            }
            else if (vatScheme.Type == VatSchemeTypeEnum.VatReverseCharge)
            {
                totalIncVat = Convert.ToDecimal(equipmentList.Sum(x => x.Equipment?.SupplyType == SupplyTypeEnum.Sale
                  ? (x.Price != 0 ? x.Price : x.Equipment?.SalePrice) * 100 / (100 + vatScheme.Rate) * x.Quantity
                  : (x.Price != 0 ? x.Price : x.Equipment?.RentalPrice) * 100 / (100 + vatScheme.Rate) * x.Quantity * x.Factor));

            }
            if (categoryEntity != null)
            {
                categoryEntity.EstimatedCosts = costs;
                categoryEntity.PlannedCosts = costs;
                categoryEntity.ActualCosts = costs;
                categoryEntity.Revenue = revenue;
                categoryEntity.Discount = parentCategoryEntity.Discount;
                categoryEntity.Profit = total - costs;
                categoryEntity.Total = total;
                categoryEntity.TotalIncVat = totalIncVat * (100 - parentCategoryEntity.Discount) / 100;
                ProjectFinancialCategoryEntity categoryRentalCreate = await _projectFinancialCategoryStorage.Update(categoryEntity);

                return true;
            }
            else
            {
                ProjectFinancialCategoryEntity category = new ProjectFinancialCategoryEntity()
                {
                    ProjectId = projectId,
                    Category = categoryType,
                    ParentId = parentCategoryEntity.Id,
                    EquipmentGroupId = equipmentGroupId,
                    Name = qroupEntity.Name,
                    EstimatedCosts = costs,
                    PlannedCosts = costs,
                    ActualCosts = costs,
                    Revenue = revenue,
                    Discount = parentCategoryEntity.Discount,
                    Profit = total - costs,
                    Total = total,
                    TotalIncVat = totalIncVat * (100 - parentCategoryEntity.Discount) / 100

                };
                ProjectFinancialCategoryEntity categoryRentalCreate = await _projectFinancialCategoryStorage.Create(category);
                return true;
            }
        }

        public async Task<VatSchemeRateDto> GetVatScheme(int projectId, int? vatSchemeId = null)
        {
            ProjectFinancialEntity finEntity = await _projectFinancialStorage.GetByProjectId(projectId);
            if (finEntity == null)
            {
                ProjectFinancialDto financial = await CreateDefaultFinancial(projectId);
                finEntity = financial.TransferToEntity();
            }
            VatSchemeTypeEnum type = VatSchemeTypeEnum.Rates; //продумати
            decimal rate = 0;
            VatSchemeEntity scheme = await _vatSchemeStorage.GetById(vatSchemeId ?? finEntity.VatSchemeId);
            type = scheme.Type;
            if (type == VatSchemeTypeEnum.FixedRate || type == VatSchemeTypeEnum.VatReverseCharge)
            {
                rate = scheme.VatClassSchemeRates.Where(x => x.Type == scheme.Type && x.VatSchemeId == scheme.Id).FirstOrDefault().Rate;
            }
            return new VatSchemeRateDto()
            {
                VatSchemeId = vatSchemeId ?? finEntity.VatSchemeId,
                Type = type,
                Rate = rate,
                VatScheme = scheme.TransferToVatSchemeDto()
            };
        }



        public async Task<ProjectFinancialCategoryUpdateDto> UpdateCategory(ProjectFinancialCategoryDto model)
        {
            ProjectFinancialCategoryEntity dbEntity = await _projectFinancialCategoryStorage.GetByCategory(model.ProjectId, model.Category);

            List<ProjectFinancialCategoryEntity> children = new List<ProjectFinancialCategoryEntity>();

            if (model.Category == ProjectFinancialCategoryEnum.Insuranse)
            {
                VatSchemeRateDto vatScheme = await GetVatScheme(model.ProjectId);
                if (vatScheme.Type == VatSchemeTypeEnum.Rates)
                {
                    SystemSettingEntity sysSetting = await _systemSettingStorage.GetById(SystemSettingsTypeEnum.FinancialSetting);
                    FinancialSettingDto dtoSysFin = sysSetting?.TransferToDto<FinancialSettingDto>();
                    int vatClassId;
                    if (dtoSysFin != null)
                    {
                        vatClassId = dtoSysFin.VatClassInsuranceId;
                        dbEntity.TotalIncVat = model.Total * (100 + (vatScheme.VatScheme.VatClassSchemeRates?.Where(y => y.VatClassId == vatClassId).FirstOrDefault()?.Rate ?? 0)) / 100;

                    }

                }
                else if (vatScheme.Type == VatSchemeTypeEnum.FixedRate)
                {
                    dbEntity.TotalIncVat = Convert.ToDecimal(model.Total * (100 + vatScheme.Rate) / 100);

                }
                else if (vatScheme.Type == VatSchemeTypeEnum.VatReverseCharge)
                {
                    dbEntity.TotalIncVat = Convert.ToDecimal(model.Total * 100 / (100 + vatScheme.Rate));

                }
                dbEntity.Revenue = model.Revenue;
            }

            if (model.Discount != dbEntity.Discount)
            {
                if (dbEntity.Discount != 100)
                {
                    dbEntity.TotalIncVat = dbEntity.TotalIncVat / (100 - dbEntity.Discount) * (100 - model.Discount);
                }
                else
                {
                    dbEntity.TotalIncVat = dbEntity.TotalIncVat * (100 - model.Discount) / 100;
                }
                //застосовуємо знижку до вкладених категорій
                if (dbEntity.Category == ProjectFinancialCategoryEnum.Sale || dbEntity.Category == ProjectFinancialCategoryEnum.Rental)
                {
                    List<ProjectFinancialCategoryEntity> listEntities = await _projectFinancialCategoryStorage.GetAllByProject(model.ProjectId);
                    List<ProjectFinancialCategoryEntity> listRentalSaleSubCategories = listEntities.Where(x => x.ParentId != null && x.Category == dbEntity.Category).ToList();

                    foreach (ProjectFinancialCategoryEntity el in listRentalSaleSubCategories)
                    {
                        if (dbEntity.Discount != 100)
                        {
                            var total = el.Total / (100 - el.Discount) * (100 - model.Discount);
                            el.Discount = model.Discount;
                            el.Total = total;
                            el.TotalIncVat = el.TotalIncVat / (100 - el.Discount) * (100 - model.Discount);
                            el.Profit = total - el.EstimatedCosts;
                        }
                        else
                        {
                            var total = el.Total * (100 - model.Discount) / 100;
                            el.Discount = model.Discount;
                            el.Total = total;
                            el.TotalIncVat = el.TotalIncVat * (100 - model.Discount) / 100;
                            el.Profit = total - el.EstimatedCosts;
                        }
                        ProjectFinancialCategoryEntity child = await _projectFinancialCategoryStorage.Update(el);
                        children.Add(child);
                    }
                }
            }

            //на фронті можуть змінюватися тільки поля Discount, Total, а також перераховується Profit 
            dbEntity.Discount = model.Discount;
            dbEntity.Total = model.Total;
            dbEntity.Profit = model.Profit;
            ProjectFinancialCategoryEntity entity = await _projectFinancialCategoryStorage.Update(dbEntity);
            entity.Children = children;

            decimal totalIncVat = await CalculateTotalIncVat(model.ProjectId);
            ProjectFinancialCategoryDto totalExcludeVatCategory = new ProjectFinancialCategoryDto();
            if (dbEntity.Category != ProjectFinancialCategoryEnum.TotalExcludeVat)
            {
                totalExcludeVatCategory = await CalculateTotalFinancialCategory(model.ProjectId);
            }

            ProjectFinancialCategoryDto dto = entity.TransferToDto();
            return new ProjectFinancialCategoryUpdateDto()
            {
                UpdateCategory = dto,
                TotalExcludeVatCategory = dbEntity.Category != ProjectFinancialCategoryEnum.TotalExcludeVat ? totalExcludeVatCategory : dto,
                TotalIncludeVat = totalIncVat
            };
        }

        //TODO: DELETE

        //public async Task<BaseListDto> GetCategoriesByProject(int projectId)
        //{
        //    {
        //        List<ProjectFinancialCategoryEntity> listEntities = await _projectFinancialCategoryStorage.GetAllByProject(projectId);
        //        List<ProjectFinancialCategoryDto> listDtos = listEntities.TransferToListDto();
        //        BaseGrid grid = listDtos.CreateGrid<ProjectFinancialCategoryDto>();

        //        return new BaseListDto()
        //        {
        //            Grid = grid,
        //            Entity = EntityEnum.ProjectEntity
        //        };
        //    }
        //}

        public async Task<List<ProjectFinancialCategoryDto>> GetCategoriesByProject(int projectId)
        {
            List<ProjectFinancialCategoryEntity> listEntities = await _projectFinancialCategoryStorage.GetAllByProject(projectId);
            List<ProjectFinancialCategoryDto> listDtos = listEntities.TransferToListDto();
            return listDtos;
        }

        public async Task<List<ProjectFinancialCategoryDto>> GetCategoriesIncludeChildrenByProject(int projectId)
        {
            List<ProjectFinancialCategoryEntity> listEntities = await _projectFinancialCategoryStorage.GetAllIncludeChildrenByProject(projectId);
            List<ProjectFinancialCategoryDto> listDtos = listEntities.TransferToListDto();
            return listDtos;
        }

        public async Task CalculateAdditionalCostFinancialCategory(int projectId)
        {
            List<ProjectAdditionalCostEntity> listEntities = await _projectAdditionalCostStorage.GetAllByProject(projectId);
            ProjectFinancialCategoryEntity categoryAddCostEntity = await _projectFinancialCategoryStorage.GetByCategory(projectId, ProjectFinancialCategoryEnum.AdditionalCosts);
            decimal costs = 0;
            decimal revenue = 0;
            decimal total = 0;
            decimal totalIncVat = 0;

            VatSchemeRateDto vatScheme = await GetVatScheme(projectId);

            if (vatScheme.Type == VatSchemeTypeEnum.Rates)
            {
                totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.SalePrice * (100 + (vatScheme.VatScheme.VatClassSchemeRates?.Where(y => y.VatClassId == x.VatClassId).FirstOrDefault()?.Rate ?? 0)) / 100));
            }
            else if (vatScheme.Type == VatSchemeTypeEnum.FixedRate)
            {
                totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.SalePrice) * (100 + vatScheme.Rate) / 100);
            }
            else if (vatScheme.Type == VatSchemeTypeEnum.VatReverseCharge)
            {
                totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.SalePrice) * 100 / (100 + vatScheme.Rate));

            }
            if (listEntities.Count > 0)
            {
                costs = Convert.ToDecimal(listEntities.Sum(x => x.PurchasePrice));
                revenue = Convert.ToDecimal(listEntities.Sum(x => x.SalePrice));
                total = revenue * (100 - categoryAddCostEntity.Discount) / 100;
            }
            categoryAddCostEntity.EstimatedCosts = costs;
            categoryAddCostEntity.PlannedCosts = costs;
            categoryAddCostEntity.ActualCosts = costs;
            categoryAddCostEntity.Revenue = revenue;
            categoryAddCostEntity.Profit = total - costs;
            categoryAddCostEntity.Total = total;
            categoryAddCostEntity.TotalIncVat = totalIncVat * (100 - categoryAddCostEntity.Discount) / 100;
            await _projectFinancialCategoryStorage.Update(categoryAddCostEntity);
            await UpdateTotalCategoryAndTotalIncVat(projectId);
        }


        public async Task CalculateCrewTransporFinancialCategory(int projectId, ProjectFunctionTypeEnum functionType)
        {
            ProjectFinancialCategoryEntity categoryEntity;
            VatSchemeRateDto vatScheme = await GetVatScheme(projectId);
            try
            {
                if (functionType == ProjectFunctionTypeEnum.Crew)
                {
                    categoryEntity = await _projectFinancialCategoryStorage.GetByCategory(projectId, ProjectFinancialCategoryEnum.Crew);
                    List<ProjectPlanningEntity> listPlanningEntities = await _projectPlanningStorage.GetAllByProject(new ProjectPlanningFilterModel() { ProjectId = projectId, Type = ProjectFunctionTypeEnum.Crew });
                    List<ProjectFunctionEntity> listEntities = await _projectFunctionStorage.GetAllByProject(projectId, ProjectFunctionTypeEnum.Crew);
                    CalculateCrewTransportFinancialCategory(ref categoryEntity, listPlanningEntities, listEntities, vatScheme);

                }
                else
                {
                    categoryEntity = await _projectFinancialCategoryStorage.GetByCategory(projectId, ProjectFinancialCategoryEnum.Transport);
                    List<ProjectPlanningEntity> listPlanningEntities = await _projectPlanningStorage.GetAllByProject(new ProjectPlanningFilterModel() { ProjectId = projectId, Type = ProjectFunctionTypeEnum.Transport });
                    List<ProjectFunctionEntity> listEntities = await _projectFunctionStorage.GetAllByProject(projectId, ProjectFunctionTypeEnum.Transport);
                    CalculateCrewTransportFinancialCategory(ref categoryEntity, listPlanningEntities, listEntities, vatScheme);
                }
                await _projectFinancialCategoryStorage.Update(categoryEntity);
                await UpdateTotalCategoryAndTotalIncVat(projectId);
            }
            catch (Exception ex)
            {

            }

        }


        private static void CalculateCrewTransportFinancialCategory(ref ProjectFinancialCategoryEntity crewTransportCategoryEntity, List<ProjectPlanningEntity> listPlanningEntities, List<ProjectFunctionEntity> listEntities, VatSchemeRateDto vatScheme)
        {
            decimal costs = 0;
            decimal revenue = 0;
            decimal total = 0;
            decimal totalIncVat = 0;

            if (listEntities.Count > 0)
            {
                costs = Convert.ToDecimal(listEntities.Sum(x => x.TotalCosts));
                revenue = Convert.ToDecimal(listEntities.Sum(x => x.TotalPrice));
                total = revenue * (100 - crewTransportCategoryEntity.Discount) / 100;
                if (vatScheme.Type == VatSchemeTypeEnum.Rates)
                {
                    totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.TotalPrice * (100 + (vatScheme.VatScheme.VatClassSchemeRates?.Where(y => y.VatClassId == x.VatClassId).FirstOrDefault()?.Rate ?? 0)) / 100));
                }
                else if (vatScheme.Type == VatSchemeTypeEnum.FixedRate)
                {
                    totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.TotalPrice) * (100 + vatScheme.Rate) / 100);

                }
                else if (vatScheme.Type == VatSchemeTypeEnum.VatReverseCharge)
                {
                    totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.TotalPrice) * 100 / (100 + vatScheme.Rate));

                }
            }
            crewTransportCategoryEntity.EstimatedCosts = costs;
            if (listPlanningEntities.Count > 0)
            {
                var planningCosts = Convert.ToDecimal(listPlanningEntities.Sum(x => x.PlannedCosts));
                crewTransportCategoryEntity.PlannedCosts = planningCosts;
                crewTransportCategoryEntity.ActualCosts = planningCosts; //TODO потрібно буде робити, після закриття плану реальними задачами
            }
            else
            {
                crewTransportCategoryEntity.PlannedCosts = costs;
                crewTransportCategoryEntity.ActualCosts = costs;
            }
            crewTransportCategoryEntity.Revenue = revenue;
            crewTransportCategoryEntity.Profit = total - costs;
            crewTransportCategoryEntity.Total = total;
            crewTransportCategoryEntity.TotalIncVat = totalIncVat * (100 - crewTransportCategoryEntity.Discount) / 100;
        }

        private async Task UpdateTotalCategoryAndTotalIncVat(int projectId)
        {
            await CalculateTotalIncVat(projectId);
            await CalculateTotalFinancialCategory(projectId);
        }


        public async Task<decimal> CalculateTotalIncVat(int projectId, bool needSave = true)
        {
            List<ProjectFinancialCategoryEntity> listCategories = await _projectFinancialCategoryStorage.GetAllByProject(projectId);
            List<ProjectFinancialCategoryEntity> listWithoutTotalCategories = listCategories.Where(x => x.Category != ProjectFinancialCategoryEnum.TotalExcludeVat && x.ParentId == null).ToList();
            ProjectFinancialEntity entity = await _projectFinancialStorage.GetByProjectId(projectId);
            entity.TotalIncVat = Convert.ToDecimal(listWithoutTotalCategories.Sum(x => x.TotalIncVat));
            if (needSave)
            {
                ProjectFinancialEntity savEentity = await _projectFinancialStorage.Update(entity);
            }
            return entity.TotalIncVat;

        }


        public async Task<ProjectFinancialCategoryDto> CalculateTotalFinancialCategory(int projectId)
        {
            List<ProjectFinancialCategoryEntity> listCategories = await _projectFinancialCategoryStorage.GetAllByProject(projectId);
            ProjectFinancialCategoryEntity totalCategory = listCategories.Where(x => x.Category == ProjectFinancialCategoryEnum.TotalExcludeVat).FirstOrDefault();
            List<ProjectFinancialCategoryEntity> listWithoutTotalCategories = listCategories.Where(x => x.Category != ProjectFinancialCategoryEnum.TotalExcludeVat && x.ParentId == null).ToList();

            totalCategory.EstimatedCosts = Convert.ToDecimal(listWithoutTotalCategories.Sum(x => x.EstimatedCosts));
            totalCategory.PlannedCosts = Convert.ToDecimal(listWithoutTotalCategories.Sum(x => x.PlannedCosts));
            totalCategory.ActualCosts = Convert.ToDecimal(listWithoutTotalCategories.Sum(x => x.ActualCosts));
            totalCategory.Revenue = Convert.ToDecimal(listWithoutTotalCategories.Sum(x => x.Revenue));
            totalCategory.Profit = Convert.ToDecimal(listWithoutTotalCategories.Sum(x => x.Profit));
            totalCategory.Total = Convert.ToDecimal(listWithoutTotalCategories.Sum(x => x.Total));
            await _projectFinancialCategoryStorage.Update(totalCategory);

            return totalCategory.TransferToDto();
        }


        public async Task<decimal> ReCalculateTotalIncVat(int projectId, int vatSchemeId)
        {
            List<ProjectFinancialCategoryEntity> listEntities = await _projectFinancialCategoryStorage.GetAllByProject(projectId);
            List<ProjectFinancialCategoryEntity> listRentalSaleSubCategories = listEntities.Where(x => x.ParentId != null).ToList();
            List<ProjectFinancialCategoryEntity> listParentEntities = listEntities.Where(x => x.ParentId == null).ToList();

            VatSchemeRateDto vatScheme = await GetVatScheme(projectId, vatSchemeId);


            foreach (ProjectFinancialCategoryEntity el in listRentalSaleSubCategories)
            {
                if (el.TotalIncVat > 0)
                {
                    bool res = await CalculateRentalSaleTotalIncVat(projectId, el.ParentId, el, vatScheme);
                }
            }

            foreach (ProjectFinancialCategoryEntity el in listParentEntities)
            {
                if (el.TotalIncVat > 0)
                {
                    switch (el.Category)
                    {
                        case ProjectFinancialCategoryEnum.AdditionalCosts:
                            {
                                bool res = await CalculateAddCostTotalIncVat(projectId, vatScheme, el);
                                break;
                            }
                        case ProjectFinancialCategoryEnum.Crew:
                        case ProjectFinancialCategoryEnum.Transport:
                            {
                                bool res = await CalculateCrewTransportTotalIncVat(projectId, vatScheme, el);
                                break;
                            }
                        case ProjectFinancialCategoryEnum.Rental:
                        case ProjectFinancialCategoryEnum.Sale:
                            {
                                bool res = await CalculateRentalSaleFinanceCategory(projectId, el, el.Category);
                                break;
                            }
                    }
                }
            }
            decimal totalIncVat = await CalculateTotalIncVat(projectId, false);
            return totalIncVat;
        }

        private async Task<bool> CalculateRentalSaleTotalIncVat(int projectId, int? parentId, ProjectFinancialCategoryEntity categoryEntity, VatSchemeRateDto vatScheme = null)
        {
            int equipmentGroupId = categoryEntity.EquipmentGroupId.Value;
            ProjectEquipmentGroupEntity qroupEntity = await _projectEquipmentGroupStorage.GetById(equipmentGroupId);
            ProjectFinancialCategoryEnum category = categoryEntity.Category;

            if (category == ProjectFinancialCategoryEnum.Sale)
            {
                List<ProjectEquipmentEntity> saleList = qroupEntity?.Equipments?.Where(y => y.Equipment.SupplyType == SupplyTypeEnum.Sale).ToList();
                ProjectFinancialCategoryEntity saleCategoryEntity = await _projectFinancialCategoryStorage.GetByCategoryAndEquipmentGroup(projectId, equipmentGroupId, ProjectFinancialCategoryEnum.Sale);
                ProjectFinancialCategoryEntity parentSaleCategoryEntity = await _projectFinancialCategoryStorage.GetByCategory(projectId, ProjectFinancialCategoryEnum.Sale);
                if (saleList.Count > 0)
                {
                    bool resultSubCategory = await SaveSubCategory(projectId, equipmentGroupId, qroupEntity, saleList, saleCategoryEntity, parentSaleCategoryEntity, ProjectFinancialCategoryEnum.Sale, vatScheme);
                }
            }
            else
            {
                List<ProjectEquipmentEntity> rentalList = qroupEntity?.Equipments?.Where(y => y.Equipment.SupplyType == SupplyTypeEnum.Rental).ToList();
                ProjectFinancialCategoryEntity rentalCategoryEntity = await _projectFinancialCategoryStorage.GetByCategoryAndEquipmentGroup(projectId, equipmentGroupId, ProjectFinancialCategoryEnum.Rental);

                ProjectFinancialCategoryEntity parentRentalCategoryEntity = await _projectFinancialCategoryStorage.GetByCategory(projectId, ProjectFinancialCategoryEnum.Rental);


                if (rentalList.Count > 0)
                {
                    bool subCategory = await SaveSubCategory(projectId, equipmentGroupId, qroupEntity, rentalList, rentalCategoryEntity, parentRentalCategoryEntity, ProjectFinancialCategoryEnum.Rental, vatScheme);
                }
            }
            return true;
        }
        private async Task<bool> CalculateCrewTransportTotalIncVat(int projectId, VatSchemeRateDto vatScheme, ProjectFinancialCategoryEntity categoryEntity)
        {
            if (categoryEntity.Category == ProjectFinancialCategoryEnum.Crew)
            {
                List<ProjectPlanningEntity> listPlanningEntitiesForCrew = await _projectPlanningStorage.GetAllByProject(new ProjectPlanningFilterModel() { ProjectId = projectId, Type = ProjectFunctionTypeEnum.Crew });
                List<ProjectFunctionEntity> listEntitiesForCrew = await _projectFunctionStorage.GetAllByProject(projectId, ProjectFunctionTypeEnum.Crew);
                CalculateCrewTransportFinancialCategory(ref categoryEntity, listPlanningEntitiesForCrew, listEntitiesForCrew, vatScheme);

            }
            else
            {
                List<ProjectPlanningEntity> listPlanningEntitiesForTransport = await _projectPlanningStorage.GetAllByProject(new ProjectPlanningFilterModel() { ProjectId = projectId, Type = ProjectFunctionTypeEnum.Transport });
                List<ProjectFunctionEntity> listEntitiesForTransport = await _projectFunctionStorage.GetAllByProject(projectId, ProjectFunctionTypeEnum.Transport);
                CalculateCrewTransportFinancialCategory(ref categoryEntity, listPlanningEntitiesForTransport, listEntitiesForTransport, vatScheme);

            }
            var res = await _projectFinancialCategoryStorage.Update(categoryEntity);
            return true;
        }

        private async Task<bool> CalculateAddCostTotalIncVat(int projectId, VatSchemeRateDto vatScheme, ProjectFinancialCategoryEntity categoryAddCostEntity)
        {
            List<ProjectAdditionalCostEntity> listEntities = await _projectAdditionalCostStorage.GetAllByProject(projectId);
            //ProjectFinancialCategoryEntity categoryAddCostEntity = await _projectFinancialCategoryStorage.GetByCategory(projectId, ProjectFinancialCategoryEnum.AdditionalCosts);
            decimal totalIncVat = 0;


            if (vatScheme.Type == VatSchemeTypeEnum.Rates)
            {
                totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.SalePrice * (100 + (vatScheme.VatScheme.VatClassSchemeRates?.Where(y => y.VatClassId == x.VatClassId).FirstOrDefault()?.Rate ?? 0)) / 100));
            }
            else if (vatScheme.Type == VatSchemeTypeEnum.FixedRate)
            {
                totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.SalePrice) * (100 + (vatScheme?.Rate ?? 0)) / 100);
            }
            else if (vatScheme.Type == VatSchemeTypeEnum.VatReverseCharge)
            {
                totalIncVat = Convert.ToDecimal(listEntities.Sum(x => x.SalePrice) * 100 / (100 + (vatScheme?.Rate ?? 0)));

            }
            categoryAddCostEntity.TotalIncVat = totalIncVat * (100 - categoryAddCostEntity.Discount) / 100;
            var res = await _projectFinancialCategoryStorage.Update(categoryAddCostEntity);
            return true;
        }
    }
}