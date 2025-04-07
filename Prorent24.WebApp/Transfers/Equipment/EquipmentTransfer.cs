using Newtonsoft.Json;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.Equipment.Supplier;
using Prorent24.Enum.Equipment;
using Prorent24.WebApp.Models.Equipment;
using Prorent24.WebApp.Models.Equipment.Supplier;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.Equipment
{
    /// <summary>
    /// 
    /// </summary>
    public static class EquipmentTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentViewModel> to List<EquipmentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentDto> TransferToListDto(this List<EquipmentViewModel> entities)
        {
            List<EquipmentDto> equipmentDtos = entities.Select(x => x.TransferToDto()).ToList();

            return equipmentDtos;
        }

        /// <summary>
        /// Transfer from EquipmentDto to EquipmentViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentDto TransferToDto(this EquipmentViewModel model)
        {
            EquipmentDto equipmentDto = new EquipmentDto()
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,

                SetType = model.SetType,
                SupplyType = model.SupplyType,
                StockManagement = model.StockManagement,
                CriticalStock = model.CriticalStock,
                DisplayInPlanner = model.DisplayInPlanner,

                InternalRemark = model.InternalRemark,
                ExternalRemark = model.ExternalRemark,
                FactorGroupId = model.FactorGroupId,
                MeasuringUnit = model.MeasuringUnit,
                VATClassId = model.VATClassId,
                DiscountGroupId = model.DiscountGroupId,

                QuantityMode = model.QuantityMode,
                Quantity = model.Quantity,
                LocationInWarehouse = model.LocationInWarehouse,


                FolderId = model.FolderId,

                RentalPrice = model.RentalPrice,
                NewPrice = model.NewPrice,
                SubhirePrice = model.SubhirePrice,

                SalePrice = model.SalePrice,
                PurchasePrice = model.PurchasePrice,

                MarginPrice = model.MarginPrice,

                // 
                HeightDim =  model.Height,
                LengthDim = model.Length,
                WidthDim = model.Width,
                WeightDim = model.Weight,
                Volume = model.Volume,
                Current = model.Current,
                Power = model.Power,
                SurfaceItem = model.SurfaceItem,

                HeightUnit = model.HeightUnit ?? LenghtUnitEnum.Cm,
                LengthUnit = model.LengthUnit ?? LenghtUnitEnum.Cm,
                WidthUnit = model.WidthUnit ?? LenghtUnitEnum.Cm,
                WeightUnit = model.WeightUnit ?? WeightUnitEnum.Kg,
                VolumeUnit = model.VolumeUnit ?? VolumeUnitEnum.Cm3,
                CurrentUnit = model.CurrentUnit ?? CurrentUtitEnum.Ampere,
                PowerUnit = model.PowerUnit ?? PowerUnitEnum.Watt,

                Colour = model.Colour,
                Brand = model.Brand,
                ProducerCode = model.ProducerCode,
                Model = model.Model,
                MaintenanceRequired = model.MaintenanceRequired,
                KitType = model.KitType,
                Storage = model.Storage,
                TransportationType = model.TransportationType,
                PowerConnector = model.PowerConnector,
                SuppliersInfoJson = JsonConvert.SerializeObject(model.SuppliersInfo)
            };

            return equipmentDto;
        }

        /// <summary>
        /// Transfer from EquipmentViewModel to EquipmentDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentViewModel TransferToViewModel(this EquipmentDto dto)
        {
            EquipmentViewModel equipmentViewModel = new EquipmentViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Code = dto.Code,

                SetType = dto.SetType,
                SupplyType = dto.SupplyType,
                StockManagement = dto.StockManagement,
                CriticalStock = dto.CriticalStock,
                DisplayInPlanner = dto.DisplayInPlanner,

                InternalRemark = dto.InternalRemark,
                ExternalRemark = dto.ExternalRemark,
                FactorGroupId = dto.FactorGroupId,
                MeasuringUnit = dto.MeasuringUnit,
                VATClassId = dto.VATClassId,
                DiscountGroupId = dto.DiscountGroupId,

                QuantityMode = dto.QuantityMode,
                Quantity = dto.Quantity,
                LocationInWarehouse = dto.LocationInWarehouse,

                FolderId = dto.FolderId,
                FolderName = dto.Folder?.Name,

                RentalPrice = dto.RentalPrice,
                NewPrice = dto.NewPrice,
                SubhirePrice = dto.SubhirePrice,

                SalePrice = dto.SalePrice,
                PurchasePrice = dto.PurchasePrice,

                MarginPrice = dto.MarginPrice,

                // 
                Height = dto.HeightDim,
                Length = dto.LengthDim,
                Width = dto.WidthDim,
                Weight = dto.WeightDim,
                Volume = dto.Volume,
                Current = dto.Current,
                Power = dto.Power,
                SurfaceItem = dto.SurfaceItem,

                HeightUnit = dto.HeightUnit,
                LengthUnit = dto.LengthUnit,
                WidthUnit = dto.WidthUnit,
                WeightUnit = dto.WeightUnit,
                VolumeUnit = dto.VolumeUnit,
                CurrentUnit = dto.CurrentUnit,
                PowerUnit = dto.PowerUnit,

                Colour = dto.Colour,
                Brand = dto.Brand,
                ProducerCode = dto.ProducerCode,
                Model = dto.Model,

                MaintenanceRequired = dto.MaintenanceRequired,
                KitType = dto.KitType,
                Storage = dto.Storage,
                TransportationType = dto.TransportationType,
                PowerConnector = dto.PowerConnector,
                SuppliersInfo = dto.SuppliersInfo?.TransferToViewModel()
                
            };
            return equipmentViewModel;
        }

        /// <summary>
        /// Transfer from List<EquipmentDto> to List<EquipmentViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentViewModel> TransferToViewModel(this List<EquipmentDto> dto)
        {
            List<EquipmentViewModel> equipmentViewModels = dto.Select(x => x.TransferToViewModel()).ToList();
            return equipmentViewModels;
        }

        public static SupplierInfoViewModel TransferToViewModel(this SupplierInfoDto dto)
        {
            SupplierInfoViewModel model = new SupplierInfoViewModel()
            {
                CompanyName = dto.CompanyName,
                ContactPerson = dto.ContactPerson,
                Email = dto.Email,
                Phone = dto.Phone,
                Website = dto.Website
            };

            return model;
        }

        public static List<SupplierInfoViewModel> TransferToViewModel(this List<SupplierInfoDto> dtos) {
            List<SupplierInfoViewModel> suppliers = dtos.Select(x => x.TransferToViewModel()).ToList();
            return suppliers;
        }

    }
}
