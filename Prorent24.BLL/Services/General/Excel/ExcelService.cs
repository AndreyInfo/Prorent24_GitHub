using Newtonsoft.Json;
using Prorent24.BLL.Models.Equipment;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.BLL.Services.Equipment;
using Prorent24.BLL.Services.Vehicle;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.Common.ApplicationSettings;
using Prorent24.Common.Extentions;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Excels;
using Prorent24.Entity.Equipment;
using Prorent24.Enum.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.General.Excel
{
    internal class ExcelService : IExcelService
    {
        private readonly IVehicleService _vehicleService;
        private readonly IEquipmentService _equipmentService;

        public ExcelService(IVehicleService vehicleService, IEquipmentService equipmentService)
        {
            _vehicleService = vehicleService;
            _equipmentService = equipmentService;
        }

        public async Task<ExcelCustomWorksheet> GetWorksheet(ModuleTypeEnum moduleType, MemoryStream memoryStream)
        {
            return await Task.Run<ExcelCustomWorksheet>(() =>
             {
                 ExcelCustomWorksheet worksheet = memoryStream.ParceToExcelWorksheet();
                 worksheet.CreateEntitiesForExcel(moduleType);
                 return worksheet;
             });
        }

        /// <summary>
        /// Import data from excel
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        public async Task Import(ModuleTypeEnum moduleType, ExcelCustomWorksheet worksheet)
        {
            switch (moduleType)
            {
                case ModuleTypeEnum.Vehicles:
                    {

                        List<VehicleDto> vehicles = new List<VehicleDto>();
                        List<Column> entityCells = worksheet.Rows[0].Cells;

                        int indexRow = 0;

                        foreach (ExcelCustomRow row in worksheet.Rows)
                        {
                            if (!worksheet.ContainsHeader || (worksheet.ContainsHeader && indexRow != 0))
                            {
                                int indexCell = 0;

                                row.Cells.ForEach(x =>
                                {
                                    x.Key = worksheet.Rows[0].Cells[indexCell].Key;
                                    indexCell = indexCell + 1;
                                });

                                VehicleDto model = new VehicleDto()
                                {
                                    MOTDate = null,
                                    FolderId = worksheet.FolderId
                                };

                                model.MoveDataToModel<VehicleDto>(row.Cells);
                                vehicles.Add(model);
                            }

                            indexRow = indexRow + 1;

                        }

                        await _vehicleService.Import(vehicles);

                        break;
                    }
                case ModuleTypeEnum.Equipment:
                    {

                        using (var wc = new System.Net.WebClient())
                        {
                            var data = wc.DownloadData(worksheet.FilePath);

                            using (var memoryStream = new MemoryStream(data))
                            {
                                int startRow = !worksheet.ContainsHeader ? 0 : 1;

                                List<Column> cells = worksheet.Rows[startRow].Cells;

                                cells = cells.Where(x => x.Value != null).ToList();

                                PropertyInfo[] modelProperies = typeof(EquipmentDto).GetProperties();

                                modelProperies = modelProperies.Where(x => cells.Any(c => c.Key!=null && c.Key.ToLower() == x.Name.ToLower())).ToArray();

                                ExcelCustomWorksheet excelWorksheet = memoryStream.ParceToExcelWorksheet();

                                if (startRow > 0)
                                {
                                    excelWorksheet.Rows.RemoveAt(0);
                                }


                                List<EquipmentEntity> entities = new List<EquipmentEntity>();

                                for (int i = 0; i < excelWorksheet.Rows.Count; i++)
                                {
                                    List<Column> excelCells = excelWorksheet.Rows[i].Cells.Where(x => x.Value != null).ToList();

                                    EquipmentDto dto =new EquipmentDto().MoveDataToModel<EquipmentDto>(modelProperies, excelCells, cells);
                                    dto.FolderId = worksheet.FolderId;


                                    if (dto != null && dto.Name!=null && dto.Name.Length > 0)
                                    {
                                        EquipmentEntity equipment = dto.TransferToEntity();
                                        entities.Add(dto.TransferToEntity());
                                    }
                                }

                                await _equipmentService.Import(entities);
                            }
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Download tamplate 
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task<string> DownloadTemplate(ModuleTypeEnum moduleType, string path)
        {
            return await Task.Run<string>(() =>
            {
                List<Column> columns = new List<Column>();
                switch (moduleType)
                {
                    case ModuleTypeEnum.Equipment:
                        columns.AddColumns<EquipmentDto>(null, true);
                        break;
                    case ModuleTypeEnum.Vehicles:
                        columns.AddColumns<VehicleDto>(null, true);
                        break;
                    default:
                        break;
                }

                string urlFile = columns.CreateEmptyExcelWorksheetWithHeader(moduleType.ToString(), path); ;
                return urlFile;
            });


            //switch (moduleType)
            //{
            //    case ModuleTypeEnum.Vehicles:
            //        {

            //            List<VehicleDto> vehicles = new List<VehicleDto>();
            //            List<Column> entityCells = worksheet.Rows[0].Cells;

            //            int indexRow = 0;

            //            foreach (ExcelCustomRow row in worksheet.Rows)
            //            {
            //                if (!worksheet.ContainsHeader || (worksheet.ContainsHeader && indexRow != 0))
            //                {
            //                    int indexCell = 0;

            //                    row.Cells.ForEach(x =>
            //                    {
            //                        x.Key = worksheet.Rows[0].Cells[indexCell].Key;
            //                        indexCell = indexCell + 1;
            //                    });

            //                    VehicleDto model = new VehicleDto()
            //                    {
            //                        MOTDate = null,
            //                        FolderId = worksheet.FolderId
            //                    };

            //                    model.MoveDataToModel<VehicleDto>(row.Cells);
            //                    vehicles.Add(model);
            //                }

            //                indexRow = indexRow + 1;

            //            }

            //            await _vehicleService.Import(vehicles);

            //            break;
            //        }
            //}
        }
    }
}
