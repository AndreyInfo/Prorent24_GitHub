using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.WebApp.Models.Vehicle;
using Prorent24.WebApp.Transfers.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prorent24.WebApp.Transfers.CrewMember;
using Prorent24.Enum.Equipment;

namespace Prorent24.WebApp.Transfers.Vehicle
{
    public static class VehicleTransfer
    {
        /// <summary>
        /// Transfer from List<VehicleViewModel> to List<VehicleDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<VehicleDto> TransferToListDto(this List<VehicleViewModel> entities)
        {
            List<VehicleDto> vehicleDtos = entities.Select(x => x.TransferToDto()).ToList();

            return vehicleDtos;
        }

        /// <summary>
        /// Transfer from VehicleDto to VehicleViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static VehicleDto TransferToDto(this VehicleViewModel model)
        {
            VehicleDto vehicleDto = new VehicleDto()
            {
                Id = model.Id,
                Name = model.Name,
                FolderId = model.FolderId,
                DeployedMultipleTimesSimultaneously = model.DeployedMultipleTimesSimultaneously,
                RegistrationNumber = model.RegistrationNumber,
                LoadCapacity = model.LoadCapacity,

                //New
                //MOTDate = model.MOTDate,
                MOTDate = model.MOTDate != null ? ((DateTime)model.MOTDate).ToString("dd.MM.yyyy") : null,

                DayilCosts = model.DayilCosts,
                VariableCosts = model.VariableCosts,
                DisplayInPlanner = model.DisplayInPlanner,
                LoadingSurface = model.LoadingSurface,
                LengthDim = model.Length,
                WidthDim = model.Width,
                HeightDim = model.Height,
                Seats = model.Seats,
                Description = model.Description,
                Note = model.Note?.TransferToDtoModel(),

                //New
                //InsuranceDate = model.InsuranceDate,
                InsuranceDate = model.InsuranceDate != null ? ((DateTime)model.InsuranceDate).ToString("dd.MM.yyyy") : null,

                IsPublic = model.IsPublic,
                CrewMembers = model.CrewMembers != null ? model.CrewMembers.TransferToListDto() : new List<CrewMemberShortDto>(),
                HeightUnit = model.HeightUnit ?? LenghtUnitEnum.Cm,
                LengthUnit = model.LengthUnit ?? LenghtUnitEnum.Cm,
                WidthUnit = model.WidthUnit ?? LenghtUnitEnum.Cm,
                LoadCapacityUnit = model.LoadCapacityUnit ?? WeightUnitEnum.Kg,

            };

            return vehicleDto;
        }

        /// <summary>
        /// Transfer from VehicleViewModel to VehicleDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static VehicleViewModel TransferToViewModel(this VehicleDto dto)
        {
            VehicleViewModel vehicleViewModel = new VehicleViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                FolderId = dto.FolderId,
                FolderName = dto.Folder?.Name,
                DeployedMultipleTimesSimultaneously = dto.DeployedMultipleTimesSimultaneously,
                RegistrationNumber = dto.RegistrationNumber,
                LoadCapacity = dto.LoadCapacity,

                //New
                //MOTDate = dto.MOTDate,
                MOTDate = DateTime.TryParse(dto.MOTDate, out DateTime resultMOTDate) ? (DateTime?)resultMOTDate : null,

                DayilCosts = dto.DayilCosts,
                VariableCosts = dto.VariableCosts,
                DisplayInPlanner = dto.DisplayInPlanner,
                LoadingSurface = dto.LoadingSurface,
                Length = dto.LengthDim,
                Width = dto.WidthDim,
                Height = dto.HeightDim,
                Seats = dto.Seats,
                Description = dto.Description,
                Note = dto.Note?.TransferToViewModel(),

                //New
                //InsuranceDate = dto.InsuranceDate,
                InsuranceDate = DateTime.TryParse(dto.InsuranceDate, out DateTime resultInsuranceDate) ? (DateTime?)resultInsuranceDate : null,

                IsPublic = dto.IsPublic,
                CrewMembers = dto.CrewMembers?.TransferToListModel(),
                HeightUnit = dto.HeightUnit,
                LengthUnit = dto.LengthUnit,
                WidthUnit = dto.WidthUnit,
                LoadCapacityUnit = dto.LoadCapacityUnit,

            };
            return vehicleViewModel;
        }
    }
}
