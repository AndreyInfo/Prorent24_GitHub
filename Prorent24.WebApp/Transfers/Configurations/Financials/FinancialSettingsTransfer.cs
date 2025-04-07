using Prorent24.BLL.Models.Configuration.Financial;
using Prorent24.WebApp.Models.Configuration.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.Financials
{
    public static class FinancialSettingsTransfer
    {
        /// <summary>
        /// Transfer from FinancialSettingDto to FinancialSettingViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static FinancialSettingViewModel TransferToViewModel(this FinancialSettingDto dto)
        {
            FinancialSettingViewModel viewModel = new FinancialSettingViewModel()
            {
                CurrencySign = dto.CurrencySign,
                DefaultExpirationPeriodQuotationContract = dto.DefaultExpirationPeriodQuotationContract,
                DefaultDueTermInvoiceReminders = dto.DefaultDueTermInvoiceReminders,
                DefaultVatSchemeId = dto.DefaultVatSchemeId,
                VatScheme = dto.VatScheme?.TransferToViewModel(),
                DefaultVatClassId = dto.DefaultVatClassId,
                VatClass = dto.VatClass?.TransferToViewModel(),
                DefaultTaxClassCrewFunctionId = dto.DefaultTaxClassCrewFunctionId,
                DefaultTaxClassTransportFunctionId = dto.DefaultTaxClassTransportFunctionId,
                VatClassInsuranceId = dto.VatClassInsuranceId,
                BankName = dto.BankName,
                BankAccountNumber = dto.BankAccountNumber,
                BankNumberBic = dto.BankNumberBic,
                PrintOnLetterhead = dto.PrintOnLetterhead,
                SendTermsConditionsWithQuotations = dto.SendTermsConditionsWithQuotations,
                SendTermsConditionsWithContracts = dto.SendTermsConditionsWithContracts,
                SendTermsConditionsWithInvoices = dto.SendTermsConditionsWithInvoices,
                CompanyTypeId = dto.CompanyTypeId

            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from FinancialSettingViewModel to FinancialSettingDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static FinancialSettingDto TransferToDtoModel(this FinancialSettingViewModel view)
        {
            FinancialSettingDto dto = new FinancialSettingDto()
            {
                CurrencySign = view.CurrencySign,
                DefaultExpirationPeriodQuotationContract = view.DefaultExpirationPeriodQuotationContract,
                DefaultDueTermInvoiceReminders = view.DefaultDueTermInvoiceReminders,
                DefaultVatSchemeId = view.DefaultVatSchemeId,
                VatScheme = view.VatScheme?.TransferToDtoModel(),
                DefaultVatClassId = view.DefaultVatClassId,
                VatClass = view.VatClass?.TransferToDtoModel(),
                DefaultTaxClassCrewFunctionId = view.DefaultTaxClassCrewFunctionId,
                DefaultTaxClassTransportFunctionId = view.DefaultTaxClassTransportFunctionId,
                VatClassInsuranceId = view.VatClassInsuranceId,
                BankName = view.BankName,
                BankAccountNumber = view.BankAccountNumber,
                BankNumberBic = view.BankNumberBic,
                PrintOnLetterhead = view.PrintOnLetterhead,
                SendTermsConditionsWithQuotations = view.SendTermsConditionsWithQuotations,
                SendTermsConditionsWithContracts = view.SendTermsConditionsWithContracts,
                SendTermsConditionsWithInvoices = view.SendTermsConditionsWithInvoices,
                CompanyTypeId = view.CompanyTypeId
            };

            return dto;
        }
    }
}
