using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class DiscountGroupTransfer
    {
        /// <summary>
        /// Transfer from DiscountGroupDto to DiscountGroupViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static DiscountGroupViewModel TransferToViewModel(this DiscountGroupDto dto)
        {
            DiscountGroupViewModel viewModel = new DiscountGroupViewModel()
            {
                Id = dto.Id,
                Name = dto.Name
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from DiscountGroupViewModel to DiscountGroupDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static DiscountGroupDto TransferToDtoModel(this DiscountGroupViewModel view)
        {
            DiscountGroupDto dto = new DiscountGroupDto()
            {
                Id = view.Id,
                Name = view.Name
            };

            return dto;
        }
    }
}
