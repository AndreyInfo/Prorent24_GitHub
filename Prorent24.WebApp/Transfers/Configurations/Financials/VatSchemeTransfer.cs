using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials.Vat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class VatSchemeTransfer
    {
        /// <summary>
        /// Transfer from VatSchemeDto to VatSchemeViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static VatSchemeViewModel TransferToViewModel(this VatSchemeDto dto)
        {
            VatSchemeViewModel viewModel = new VatSchemeViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                VatClassSchemeRates = dto.VatClassSchemeRates?.Select(x => x.TransferToViewModel()).ToList()

            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from VatSchemeViewModel to VatSchemeDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static VatSchemeDto TransferToDtoModel(this VatSchemeViewModel view)
        {
            VatSchemeDto dto = new VatSchemeDto()
            {
                Id = view.Id,
                Name = view.Name,
                Type = view.Type,
                VatClassSchemeRates = view.VatClassSchemeRates?.Select(x=>x.TransferToDtoModel()).ToList()
            };

            return dto;
        }


        /// <summary>
        /// Transfer from VatSchemeDto to VatSchemeViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static VatClassSchemeRateViewModel TransferToViewModel(this VatClassSchemeRateDto dto)
        {
            VatClassSchemeRateViewModel viewModel = new VatClassSchemeRateViewModel()
            {
                Id = dto.Id,
                Type = dto.Type,
                VatClassId = dto.VatClassId,
                VatSchemeId = dto.VatSchemeId,
                VatScheme = dto.VatScheme?.TransferToViewModel(),
                Rate = dto.Rate,
                AccountingCode = dto.AccountingCode,
                EdifactCode = dto.EdifactCode
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from VatSchemeViewModel to VatSchemeDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static VatClassSchemeRateDto TransferToDtoModel(this VatClassSchemeRateViewModel view)
        {
            VatClassSchemeRateDto dto = new VatClassSchemeRateDto()
            {
                Id = view.Id,
                Type = view.Type,
                VatClassId = view.VatClassId,
                VatSchemeId = view.VatSchemeId,
                //VatScheme = view.VatScheme.TransferToViewModel(),
                Rate = view.Rate,
                AccountingCode = view.AccountingCode,
                EdifactCode = view.EdifactCode
            };

            return dto;
        }
    }
}
