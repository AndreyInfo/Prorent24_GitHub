using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials.Vat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class VatClassTransfer
    {
        /// <summary>
        /// Transger from List<VatClassDto>  to List<VatClassViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<VatClassViewModel> TransferToListViewModel(this List<VatClassDto> dtos)
        {
            List<VatClassViewModel> viewModels = dtos.Select(x => x.TransferToViewModel()).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from VatClassDto to VatClassViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static VatClassViewModel TransferToViewModel(this VatClassDto dto)
        {
            VatClassViewModel viewModel = new VatClassViewModel()
            {
                Id = dto.Id,
                Name = dto.Name
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from VatClassViewModel to VatClassDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static VatClassDto TransferToDtoModel(this VatClassViewModel view)
        {
            VatClassDto dto = new VatClassDto()
            {
                Id = view.Id,
                Name = view.Name
            };

            return dto;
        }

    }
}
