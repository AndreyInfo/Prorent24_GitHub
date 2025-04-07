using Prorent24.BLL.Models.Project;
using Prorent24.WebApp.Models.Project;
using Prorent24.WebApp.Transfers.Configurations.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Project
{
    public static class ProjectFinancialTransfer
    {
        /// <summary>
        /// Transfer from ProjectFinancialDto to ProjectFinancialViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ProjectFinancialDto TransferToDto(this ProjectFinancialViewModel model)
        {
            ProjectFinancialDto dto = new ProjectFinancialDto()
            {
                Id = model.Id,
                Deposit = model.Deposit,
                //DepositStatus = model.DepositStatus,
                AdditionalConditionId = model.AdditionalConditionId,
                InvoiceMomentId = model.InvoiceMomentId,
                ProjectId = model.ProjectId,
                VatSchemeId = model.VatSchemeId,
                TotalIncVat = model.TotalIncVat

            };

            return dto;
        }

        /// <summary>
        /// Transfer from ProjectFinancialViewModel to ProjectFinancialDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectFinancialViewModel TransferToViewModel(this ProjectFinancialDto dto)
        {
            ProjectFinancialViewModel model = new ProjectFinancialViewModel()
            {
                Id = dto.Id,
                Deposit = dto.Deposit,
                //DepositStatus = dto.DepositStatus,
                AdditionalConditionId = dto.AdditionalConditionId,
                AdditionalCondition = dto.AdditionalCondition?.TransferToViewModel(),
                InvoiceMomentId = dto.InvoiceMomentId,
                InvoiceMoment = dto.InvoiceMoment?.TransferToViewModel(),
                ProjectId = dto.ProjectId,
                VatSchemeId = dto.VatSchemeId,
                TotalIncVat = dto.TotalIncVat,
                VatScheme = dto.VatScheme?.TransferToViewModel()
            };
            return model;
        }
    }
}
