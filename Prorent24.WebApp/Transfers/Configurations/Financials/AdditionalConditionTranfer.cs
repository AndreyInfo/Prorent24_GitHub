using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials.AdditionalCondition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    internal static class AdditionalConditionTranfer
    {
        /// <summary>
        /// Transfer from AdditionalConditionDto to AdditionalConditionViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static AdditionalConditionViewModel TransferToViewModel(this AdditionalConditionDto dto)
        {
            AdditionalConditionViewModel viewModel = new AdditionalConditionViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Text = dto.Text
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from AdditionalConditionViewModel to AdditionalConditionDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static AdditionalConditionDto TransferToDtoModel(this AdditionalConditionViewModel view)
        {
            AdditionalConditionDto dto = new AdditionalConditionDto()
            {
                Id = view.Id,
                Name = view.Name,
                Text = view.Text
            };

            return dto;
        }
    }
}
