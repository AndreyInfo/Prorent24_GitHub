using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.Vehicle;
using Prorent24.BLL.Transfers.Equipment;
using Prorent24.BLL.Transfers.General;
using Prorent24.BLL.Transfers.Tasks;
using Prorent24.Entity.Vehicle;
using Prorent24.Enum.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Vehicle
{
    public static class VehicleTransfer
    {
        /// <summary>
        /// Transfer from List<VehicleEntity> to List<VehicleDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<VehicleDto> TransferToListDto(this List<VehicleEntity> entities, FunctionPermissionDto permissions = null)
        {
            List<VehicleDto> vehicleDtos = entities.Select(x => x.TransferToDto("", permissions)).ToList();

            return vehicleDtos;
        }

        /// <summary>
        /// Transfer from List<VehicleDto> to List<VehicleEntity>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<VehicleEntity> TransferToListEntity(this List<VehicleDto> dtos)
        {
            List<VehicleEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();

            return entities;
        }

        /// <summary>
        /// Transfer from VehicleDto to VehicleEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static VehicleDto TransferToDto(this VehicleEntity entity, string exclude = "", FunctionPermissionDto permissions = null)
        {
            VehicleDto vehicleDto = new VehicleDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                FolderId = entity.FolderId,
                Folder = entity.Folder?.TransferToDto(),
                DeployedMultipleTimesSimultaneously = entity.DeployedMultipleTimesSimultaneously,
                RegistrationNumber = entity.RegistrationNumber,

                //New
                //MOTDate = entity.MOTDate,
                MOTDate = entity.MOTDate != null ? ((DateTime)entity.MOTDate).ToString("dd.MM.yyyy") : null,

                DayilCosts = entity.DayilCosts,
                VariableCosts = entity.VariableCosts,
                DisplayInPlanner = entity.DisplayInPlanner,
                LoadingSurface = entity.LoadingSurface,
                Seats = entity.Seats,
                Description = entity.Description,
                Notes = entity.Notes?.ToList().TransferToListDto(),
                Tags = entity.Tags?.ToList().TransferToListTagDto(),
                Tasks = exclude == "Vehicle" ? null : entity.Tasks?.ToList().TransferToListDto("Vehicle"),
                Files = entity.Files?.ToList().TransferToListDto(),

                //New
                //InsuranceDate = entity.InsuranceDate,
                InsuranceDate = entity.InsuranceDate != null ? ((DateTime)entity.InsuranceDate).ToString("dd.MM.yyyy") : null,


                IsPublic = entity.IsPublic ?? true,
                CrewMembers = entity.CrewMembers != null ? entity.CrewMembers.ToList().TransferToDto() : new List<CrewMemberShortDto>(),
                HeightUnit = entity.HeightUnit ?? LenghtUnitEnum.Cm,
                LengthUnit = entity.LengthUnit ?? LenghtUnitEnum.Cm,
                WidthUnit = entity.WidthUnit ?? LenghtUnitEnum.Cm,
                LoadCapacityUnit = entity.LoadCapacityUnit ?? WeightUnitEnum.Kg,
                HeightDim = entity.Height.CalculateViewLengthUnit(entity.HeightUnit ?? LenghtUnitEnum.Cm),
                LengthDim = entity.Length.CalculateViewLengthUnit(entity.LengthUnit ?? LenghtUnitEnum.Cm),
                WidthDim = entity.Width.CalculateViewLengthUnit(entity.WidthUnit ?? LenghtUnitEnum.Cm),
                LoadCapacity = entity.LoadCapacity.CalculateViewWeightUnit(entity.LoadCapacityUnit ?? WeightUnitEnum.Kg),

            };

            if (permissions != null)
            {
                vehicleDto.Edit = permissions.Edit;
                vehicleDto.Delete = permissions.Delete;
                vehicleDto.View = permissions.View;
            }

            return vehicleDto;
        }

        /// <summary>
        /// Transfer from VehicleEntity to VehicleDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static VehicleEntity TransferToEntity(this VehicleDto dto)
        {
            VehicleEntity vehicleEntity = new VehicleEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                FolderId = dto.FolderId,
                DeployedMultipleTimesSimultaneously = dto.DeployedMultipleTimesSimultaneously,
                RegistrationNumber = dto.RegistrationNumber,

                //New
                //MOTDate = dto.MOTDate,
                MOTDate = DateTime.TryParse(dto.MOTDate, out DateTime resultMOTDate) ? (DateTime?)resultMOTDate : null,


                DayilCosts = dto.DayilCosts,
                VariableCosts = dto.VariableCosts,
                DisplayInPlanner = dto.DisplayInPlanner,
                LoadingSurface = dto.LoadingSurface,
                Seats = dto.Seats,
                Description = dto.Description,

                //New
                //InsuranceDate = dto.InsuranceDate,
                InsuranceDate = DateTime.TryParse(dto.InsuranceDate, out DateTime resultInsuranceDate) ? (DateTime?)resultInsuranceDate : null,

                IsPublic = dto.IsPublic,
                HeightUnit = dto.HeightUnit ?? LenghtUnitEnum.Cm,
                LengthUnit = dto.LengthUnit ?? LenghtUnitEnum.Cm,
                WidthUnit = dto.WidthUnit ?? LenghtUnitEnum.Cm,
                LoadCapacityUnit = dto.LoadCapacityUnit ?? WeightUnitEnum.Kg,
                Height = dto.HeightDim.CalculateDbLengtUnit(dto.HeightUnit ?? LenghtUnitEnum.Cm),
                Length = dto.LengthDim.CalculateDbLengtUnit(dto.LengthUnit ?? LenghtUnitEnum.Cm),
                Width = dto.WidthDim.CalculateDbLengtUnit(dto.WidthUnit ?? LenghtUnitEnum.Cm),
                LoadCapacity =  dto.LoadCapacity.CalculateDbWeightUnit(dto.LoadCapacityUnit ?? WeightUnitEnum.Kg)
            };
            return vehicleEntity;
        }


        /// <summary>
        /// Transfer from List<VehicleEntity> to List<VehiclePlanningDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<VehiclePlanningDto> TransferToListForPlanningDto(this List<VehicleEntity> entities)
        {
            List<VehiclePlanningDto> vehicleDtos = entities.Select(x => x.TransferToPlanningDto()).ToList();

            return vehicleDtos;
        }

        public static VehiclePlanningDto TransferToPlanningDto(this VehicleEntity entity, string exclude = "")
        {
            VehiclePlanningDto planDto = new VehiclePlanningDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                FolderName = entity.Folder?.Name
            };

            return planDto;
        }

        public static List<CrewMemberShortDto> TransferToDto(this List<VehicleVisibilityCrewMemberEntity> entities)
        {
            List<CrewMemberShortDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        public static CrewMemberShortDto TransferToDto(this VehicleVisibilityCrewMemberEntity entity)
        {
            CrewMemberShortDto dto = new CrewMemberShortDto()
            {
                Id = entity.CrewMemberId,
                FirstName = entity.CrewMember?.FirstName,
                LastName = entity.CrewMember?.LastName,
                MiddleName = entity.CrewMember?.MiddleName
            };
            return dto;
        }

        public static List<VehicleVisibilityCrewMemberEntity> TransferToVehicleVisibilityEntity(this List<CrewMemberShortDto> dtos)
        {
            List<VehicleVisibilityCrewMemberEntity> entities = dtos.Select(x => x.TransferToEntity()).ToList();
            return entities;
        }

        public static VehicleVisibilityCrewMemberEntity TransferToEntity(this CrewMemberShortDto dto)
        {
            VehicleVisibilityCrewMemberEntity entity = new VehicleVisibilityCrewMemberEntity()
            {
                CrewMemberId = dto.Id
            };
            return entity;
        }

    }
}
