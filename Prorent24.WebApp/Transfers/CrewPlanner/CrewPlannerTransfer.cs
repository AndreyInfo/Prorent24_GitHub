using Prorent24.BLL.Models.CrewPlanner;
using Prorent24.WebApp.Models.CrewPlanner;
using Prorent24.WebApp.Transfers.CrewMember;
using Prorent24.WebApp.Transfers.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.CrewPlanner
{
    public static class CrewPlannerTransfer
    {
        /// <summary>
      /// Transfer from List<CrewPlannerViewModel> to List<CrewPlannerDto>
      /// </summary>
      /// <param name="viewmodels"></param>
      /// <returns></returns>
        public static List<CrewPlannerDto> TransferToListDto(this List<CrewPlannerViewModel> viewmodels)
        {
            List<CrewPlannerDto> CrewPlannerDtos = viewmodels.Select(x => x.TransferToDto()).ToList();

            return CrewPlannerDtos;
        }

        /// <summary>
        /// Transfer from List<CrewPlannerDto> to List<CrewPlannerViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<CrewPlannerViewModel> TransferToListViewModel(this List<CrewPlannerDto> dtos)
        {
            List<CrewPlannerViewModel> viewModels = dtos.Select(x => x.TransferToViewModel()).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from CrewPlannerDto to CrewPlannerViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CrewPlannerDto TransferToDto(this CrewPlannerViewModel model)
        {
            CrewPlannerDto dto = new CrewPlannerDto()
            {
                //Id = model.Id,
                Action = model.Action,
                From = model.From,
                Until = model.Until,
                Type = model.Type,
                FunctionIds = model.FunctionIds,
                Comment = model.Comment
                //Vehicle = model.Vehicle?.TransferToDto(),
                //CrewMemberId = model.CrewMemberId,
                //CrewMember = model.CrewMember?.TransferToDto()

            };

            return dto;
        }

        /// <summary>
        /// Transfer from CrewPlannerViewModel to CrewPlannerDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static CrewPlannerViewModel TransferToViewModel(this CrewPlannerDto dto)
        {
            CrewPlannerViewModel viewModel = new CrewPlannerViewModel()
            {
                Id = dto.Id,
                Action = dto.Action,
                From = dto.From,
                Until = dto.Until,
                Type = dto.Type,
                FunctionIds = dto.FunctionIds,
                Comment = dto.Comment
                //VehicleId = dto.VehicleId,
                //CrewMemberId = dto.CrewMemberId,
            
            };
            return viewModel;
        }
    }
}
