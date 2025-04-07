using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class FactorGroupTransfer
    {
        /// <summary>
        /// Transfer from FactorGroupDto to FactorGroupViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static FactorGroupViewModel TransferToViewModel(this FactorGroupDto dto)
        {
            FactorGroupViewModel viewModel = new FactorGroupViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                IsSystem = dto.IsSystem,
                FactorGroupOptions = dto.FactorGroupOptions?.Select(x => x.TransferToViewModel()).ToList()

            };
            return viewModel;
        }

        /// <summary>
        /// Transfer from FactorGroupViewModel to FactorGroupDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static FactorGroupDto TransferToDtoModel(this FactorGroupViewModel view)
        {
            FactorGroupDto dto = new FactorGroupDto()
            {
                Id = view.Id,
                Name = view.Name,
                IsSystem = view.IsSystem
            };

            return dto;
        }
    }
}
