using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class FactorGroupOptionTransfer
    {
        /// <summary>
        /// Transfer from FactorGroupOptionDto to FactorGroupOptionViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static FactorGroupOptionViewModel TransferToViewModel(this FactorGroupOptionDto dto)
        {
            FactorGroupOptionViewModel viewModel = new FactorGroupOptionViewModel()
            {
                Id = dto.Id,
                NumberOfDaysFrom = dto.NumberOfDaysFrom,
                NumberOfDaysTo = dto.NumberOfDaysTo,
                Factor = dto.Factor,
                FactorGroupId = dto.FactorGroupId
            };
            return viewModel;
        }

        /// <summary>
        /// Transfer from FactorGroupOptionViewModel to FactorGroupOptionDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static FactorGroupOptionDto TransferToDtoModel(this FactorGroupOptionViewModel view)
        {
            FactorGroupOptionDto dto = new FactorGroupOptionDto()
            {
                Id = view.Id,
                NumberOfDaysFrom = view.NumberOfDaysFrom,
                NumberOfDaysTo = view.NumberOfDaysTo,
                Factor = view.Factor,
                FactorGroupId = view.FactorGroupId
            };

            return dto;
        }
    }
}
