using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Entity.Project;
using Prorent24.Enum.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Project
{
   public static class ProjectFinancialTransfer
    {
        

        /// <summary>
        /// Transfer from ProjectFinancialEntity to ProjectFinancialDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ProjectFinancialEntity TransferToEntity(this ProjectFinancialDto dto)
        {
            ProjectFinancialEntity entity = new ProjectFinancialEntity()
            {
                Id = dto.Id,
                Deposit = dto.Deposit,
                //DepositStatus = dto.DepositStatus,
                AdditionalConditionId = dto.AdditionalConditionId,
                InvoiceMomentId = dto.InvoiceMomentId,
                ProjectId = dto.ProjectId,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate,
                VatSchemeId = dto.VatSchemeId,
                TotalIncVat = dto.TotalIncVat
                
            };
            return entity;
        }


        /// <summary>
        /// Transfer from ProjectFinancialEntity to ProjectFinancialDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ProjectFinancialDto TransferToDto(this ProjectFinancialEntity entity, string exclude = null)
        {
            ProjectFinancialDto dto = new ProjectFinancialDto()
            {
                Id = entity.Id,
                Deposit = entity.Deposit,
                //DepositStatus = entity.DepositStatus,
                AdditionalConditionId = entity.AdditionalConditionId,
                AdditionalCondition = entity.AdditionalCondition?.TransferToDto(),
                InvoiceMomentId = entity.InvoiceMomentId,
                InvoiceMoment = entity.InvoiceMoment?.TransferToInvoiceMomentDto(),
                ProjectId = entity.ProjectId,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate,
                VatSchemeId = entity.VatSchemeId,
                TotalIncVat = entity.TotalIncVat,
                VatScheme = entity.VatScheme?.TransferToVatSchemeDto()
            };

            return dto;
        }


    }
}
