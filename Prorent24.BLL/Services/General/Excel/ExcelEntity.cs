using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Excels;
using Prorent24.Common.Models.Grids;
using Prorent24.Enum.Entity;
using Prorent24.Enum.General;
using System.Collections.Generic;

namespace Prorent24.BLL.Services.General.Excel
{
    public static class ExcelEntity
    {
        public static ExcelCustomWorksheet CreateEntitiesForExcel(this ExcelCustomWorksheet excelCustomWorksheet, ModuleTypeEnum moduleType)
        {
            switch (moduleType)
            {
                case ModuleTypeEnum.Vehicles:
                    {
                        excelCustomWorksheet.Entities.AddEntities(EntityEnum.VehicleEntity);
                        excelCustomWorksheet.SubEntities.AddTreeColumnsGroup<VehicleDto>();
                        excelCustomWorksheet.RequiredFields.AddRequiredFields<VehicleDto>();
                        excelCustomWorksheet.ExctractColumns<VehicleDto>();

                        //excelCustomWorksheet.RequiredFields.Add("Name");
                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {
                        excelCustomWorksheet.Entities.AddEntities(EntityEnum.EquipmentEntity);
                        excelCustomWorksheet.SubEntities.AddTreeColumnsGroup<EquipmentDto>();
                        excelCustomWorksheet.RequiredFields.AddRequiredFields<EquipmentDto>();
                        excelCustomWorksheet.ExctractColumns<EquipmentDto>();

                        //excelCustomWorksheet.RequiredFields.Add("Name");
                        //excelCustomWorksheet.RequiredFields.Add("SupplyType");
                        //excelCustomWorksheet.RequiredFields.Add("SetType");
                        //excelCustomWorksheet.RequiredFields.Add("StockManagement");
                        //excelCustomWorksheet.RequiredFields.Add("QuantityMode");
                        break;
                    }
            }

            return excelCustomWorksheet;
        }

        private static List<EntityGrid> AddEntities(this List<EntityGrid> entities, params EntityEnum[] entituEnums)
        {
            for (int i = 0; i < entituEnums.Length; i++)
            {
                entities.Add(new EntityGrid()
                {
                    Entity = entituEnums[0].ToString()
                });
            }

            return entities;
        }

        
    }
}
