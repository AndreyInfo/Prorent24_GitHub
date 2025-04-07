using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Equipment
{
    /// <summary>
    /// 
    /// </summary>
    public static class EquipmentTransfer
    {

        public static List<EquipmentGroupGridDto> TransferToListGroupDto(this List<EquipmentEntity> entities)
        {
            List<EquipmentGroupGridDto> equipmentGroupGridDtos = entities.Select(x => new EquipmentGroupGridDto()
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Folder == null ? "uknouwn" :  x.Folder.Name
            }).ToList();

            return equipmentGroupGridDtos;
        }

        /// <summary>
        /// Transfer from List<EquipmentEntity> to List<EquipmentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentDto> TransferToListDto(this List<EquipmentEntity> entities, FunctionPermissionDto permissions = null)
        {
            List<EquipmentDto> equipmentDtos = entities.Select(x => x.TransferToDto(null, permissions)).ToList();

            return equipmentDtos;
        }

        /// <summary>
        /// Transfer from List<EquipmentDto> to List<EquipmentEntity>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<EquipmentEntity> TransferToListEntity(this List<EquipmentDto> dtos)
        {
            List<EquipmentEntity> entities = dtos.Select(x=>x.TransferToEntity()).ToList();

            return entities;
        }

        /// <summary>
        /// Transfer from EquipmentDto to EquipmentEntity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentDto TransferToDto(this EquipmentEntity entity, string exclude = "", FunctionPermissionDto permissions = null)
        {

            EquipmentDto dto = new EquipmentDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,

                SetType = entity.SetType,
                SupplyType = entity.SupplyType,
                StockManagement = entity.StockManagement,
                CriticalStock = entity.CriticalStock,
                DisplayInPlanner = entity.DisplayInPlanner,

                InternalRemark = entity.InternalRemark,
                ExternalRemark = entity.ExternalRemark,
                FactorGroupId = entity.FactorGroupId,
                MeasuringUnit = entity.MeasuringUnit,
                VATClassId = entity.VATClassId,
                DiscountGroupId = entity.DiscountGroupId,
                VatClassSchemeRate = entity.VatClass?.VatClassSchemeRates?.FirstOrDefault(x => x.VatClassId == entity.VATClassId)?.Rate,


                QuantityMode = entity.QuantityMode,
                Quantity = entity.CalculateQuantity(),
                LocationInWarehouse = entity.LocationInWarehouse,

                FolderId = entity.FolderId,
                Folder = entity.Folder?.TransferToDto(),

                RentalPrice = entity.RentalPrice.HasValue ? entity.RentalPrice.Value : 0,
                MarginPrice = entity.MarginPrice.HasValue ? entity.MarginPrice.Value : 0,
                NewPrice = entity.NewPrice.HasValue ? entity.NewPrice.Value : 0,
                SubhirePrice = entity.SubhirePrice.HasValue ? entity.SubhirePrice.Value : 0,

                SalePrice = entity.SalePrice.HasValue ? entity.SalePrice.Value : 0,
                PurchasePrice = entity.PurchasePrice.HasValue ? entity.PurchasePrice.Value : 0,

                // 
                HeightDim = entity.Height.HasValue ? entity.Height.Value :0, //.CalculateViewLengthUnit(entity.HeightUnit ?? LenghtUnitEnum.Cm)
                LengthDim = entity.Length.HasValue ? entity.Length.Value: 0, //.CalculateViewLengthUnit(entity.LengthUnit ?? LenghtUnitEnum.Cm) 
                WidthDim = entity.Width.HasValue ? entity.Width.Value : 0, //.CalculateViewLengthUnit(entity.WidthUnit ?? LenghtUnitEnum.Cm)
                WeightDim = entity.Weight.HasValue ? entity.Weight.Value : 0, //.CalculateViewWeightUnit(entity.WeightUnit ?? WeightUnitEnum.Kg)
                Volume = entity.Volume.HasValue ? entity.Volume.Value : 0,
                Current = entity.Current.HasValue ? entity.Current.Value : 0,
                Power = entity.Power.HasValue ? entity.Power.Value : 0,
                SurfaceItem = entity.SurfaceItem,

                HeightUnit = entity.HeightUnit, // ?? LenghtUnitEnum.Cm
                LengthUnit = entity.LengthUnit, // ?? LenghtUnitEnum.Cm
                WidthUnit = entity.WidthUnit, // ?? LenghtUnitEnum.Cm
                WeightUnit = entity.WeightUnit, // ?? WeightUnitEnum.Kg
                VolumeUnit = entity.VolumeUnit, // ?? VolumeUnitEnum.Cm3
                CurrentUnit = entity.CurrentUnit, // ?? CurrentUtitEnum.Ampere
                PowerUnit = entity.PowerUnit, // ?? PowerUnitEnum.Watt


                Tasks = exclude == "Equipment" ? null : entity.Tasks?.ToList().TransferToListDto("Equipment"),
                Notes = entity.Notes?.ToList().TransferToListDto(),
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Files = entity.Files?.ToList().TransferToListDto(),

                Colour = entity.Colour,
                Brand = entity.Brand,
                ProducerCode = entity.ProducerCode,
                Model = entity.Model,

                MaintenanceRequired = entity.MaintenanceRequired.HasValue ? entity.MaintenanceRequired.Value : false,

                KitType = entity.KitType,
                Storage = entity.Storage,
                TransportationType = entity.TransportationType,
                PowerConnector = entity.PowerConnector,
                SuppliersInfoJson = entity.SuppliersInfo
            };

            if (permissions != null)
            {
                dto.Delete = permissions.Delete;
                dto.Edit = permissions.Edit;
                dto.View = permissions.View;
            }

            return dto;
        }

        /// <summary>
        /// Transfer from EquipmentEntity to EquipmentDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentEntity TransferToEntity(this EquipmentDto dto)
        {
            decimal? height = dto.HeightDim;  //dto.HeightDim?.CalculateDbLengtUnit(dto.HeightUnit ?? LenghtUnitEnum.Cm);
            decimal? length = dto.LengthDim; //?.CalculateDbLengtUnit(dto.LengthUnit ?? LenghtUnitEnum.Cm);
            decimal? width = dto.WidthDim; //?.CalculateDbLengtUnit(dto.WidthUnit ?? LenghtUnitEnum.Cm);
            EquipmentEntity EquipmentEntity = new EquipmentEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Code = dto.Code,

                SetType = dto.SetType,
                SupplyType = dto.SupplyType,
                StockManagement = dto.StockManagement,
                CriticalStock = dto.CriticalStock.HasValue ? dto.CriticalStock.Value : 0,
                DisplayInPlanner = dto.DisplayInPlanner,

                InternalRemark = dto.InternalRemark,
                ExternalRemark = dto.ExternalRemark,
                FactorGroupId = dto.FactorGroupId,
                MeasuringUnit = dto.MeasuringUnit,
                VATClassId = dto.VATClassId,


                QuantityMode = dto.QuantityMode,
                Quantity = dto.Quantity,
                LocationInWarehouse = dto.LocationInWarehouse,

                FolderId = dto.FolderId,

                RentalPrice = dto.RentalPrice,
                MarginPrice = dto.MarginPrice,
                NewPrice = dto.NewPrice,
                SubhirePrice = dto.SubhirePrice,

                SalePrice = dto.SalePrice,
                PurchasePrice = dto.PurchasePrice,

                DiscountGroupId = dto.DiscountGroupId,

                // 
                Height = height,
                Length =length,
                Width = width,
                Weight = dto.WeightDim?.CalculateDbWeightUnit(dto.WeightUnit ?? WeightUnitEnum.Kg),
                Volume = length?.CalculateDbVolume(width, height),
                Current = dto.Current,
                Power = dto.Power,
                SurfaceItem = dto.SurfaceItem,

                HeightUnit = dto.HeightUnit ?? LenghtUnitEnum.Cm,
                LengthUnit = dto.LengthUnit ?? LenghtUnitEnum.Cm,
                WidthUnit = dto.WidthUnit ?? LenghtUnitEnum.Cm,
                WeightUnit = dto.WeightUnit ?? WeightUnitEnum.Kg,
                VolumeUnit = dto.VolumeUnit ?? VolumeUnitEnum.M3,
                CurrentUnit = dto.CurrentUnit ?? CurrentUtitEnum.Ampere,
                PowerUnit = dto.PowerUnit ?? PowerUnitEnum.Watt,

                Colour = dto.Colour,
                Brand = dto.Brand,
                ProducerCode = dto.ProducerCode,
                Model = dto.Model,

                MaintenanceRequired = dto.MaintenanceRequired,
                KitType = dto.KitType,
                Storage = dto.Storage,
                TransportationType = dto.TransportationType,
                PowerConnector = dto.PowerConnector,
                SuppliersInfo = dto.SuppliersInfoJson

            };
            return EquipmentEntity;
        }

        /// <summary>
        /// Transfer from List<EquipmentDto> to List<EquipmentEntity>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentEntity> TransferToEntitiesList(this List<EquipmentDto> dto)
        {
            List<EquipmentEntity> EquipmentEntitys = dto.Select(x => x.TransferToEntity()).ToList();
            return EquipmentEntitys;
        }

        public static int CalculateQuantity(this EquipmentEntity entity)
        {
            int quantity = 0;
            if (entity.SetType == SetTypeEnum.Kit)
            {
                if (entity.EquipmentContents?.Count > 0)
                {
                    quantity = entity.EquipmentContents?.Select(x => (x.Content?.Quantity > 0 ? (int)x.Content.Quantity : 0) / (x.Quantity != 0 ? x.Quantity : 1))?.ToList()?.Min() ?? 0;

                }
                else
                {
                    quantity = 0;
                }
            }
            else
            {
                quantity = entity.QuantityMode == QuantityModeEnum.CalculateQuantityCountingSerialNumbers ? (entity.EquipmentSerialNumbers != null ? entity.EquipmentSerialNumbers.Count : 0) : (entity.Quantity.HasValue ? entity.Quantity.Value : 0);
            }
            return quantity;
        }

        public static decimal CalculateDbLengtUnit(this decimal value, LenghtUnitEnum lenghtUnit)
        {
            switch (lenghtUnit)
            {
                case LenghtUnitEnum.Cm:
                    {
                        return value;
                    }
                case LenghtUnitEnum.Km:
                    {
                        return value * 100000;
                    }
                case LenghtUnitEnum.M:
                    {
                        return value * 100;
                    }
                case LenghtUnitEnum.Mm:
                    {
                        return value * 0.01m;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        public static decimal CalculateDbWeightUnit(this decimal value, WeightUnitEnum weightUnit)
        {
            switch (weightUnit)
            {
                case WeightUnitEnum.G:
                    {
                        return value * 0.001m;
                    }
                case WeightUnitEnum.Kg:
                    {
                        return value;
                    }
                case WeightUnitEnum.Ton:
                    {
                        return value * 1000;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        public static decimal CalculateDbVolume(this decimal lenght, decimal? width, decimal? height)
        {
            if (width == null || height == null)
                return 0;
            return lenght * width.Value * height.Value * 0.000001m;
        }

        public static decimal CalculateViewLengthUnit(this decimal value, LenghtUnitEnum lenghtUnit)
        {
            switch (lenghtUnit)
            {
                case LenghtUnitEnum.Cm:
                    {
                        return value;
                    }
                case LenghtUnitEnum.Km:
                    {
                        return value * 0.000001m;
                    }
                case LenghtUnitEnum.M:
                    {
                        return value * 0.01m;
                    }
                case LenghtUnitEnum.Mm:
                    {
                        return value * 10;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        public static decimal CalculateViewWeightUnit(this decimal value, WeightUnitEnum weightUnit)
        {
            switch (weightUnit)
            {
                case WeightUnitEnum.G:
                    {
                        return value * 1000;
                    }
                case WeightUnitEnum.Kg:
                    {
                        return value;
                    }
                case WeightUnitEnum.Ton:
                    {
                        return value * 0.001m;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

    }
}
