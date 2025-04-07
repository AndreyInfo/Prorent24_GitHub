using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Transfers.Configuration.Financial;
using Prorent24.Entity.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.Contact
{
    public static class ContactPaymentTransfer
    {
        /// <summary>
        /// Transfer from List<ContactPaymentEntity> to List<ContactPaymentDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ContactPaymentDto> TransferToListDto(this List<ContactPaymentEntity> entities)
        {
            List<ContactPaymentDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
            return dtos;
        }

        /// <summary>
        /// Transfer from ContactPaymentEntity to ContactPaymentDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ContactPaymentDto TransferToDto(this ContactPaymentEntity entity)
        {
            ContactPaymentDto dto = new ContactPaymentDto()
            {
                ContactId = entity.ContactId,
                Id = entity.Id,
                InvoiceMomentId = entity.InvoiceMomentId,
                InvoiceMoment = entity.InvoiceMoment?.TransferToInvoiceMomentDto(),
                PaymentConditionId = entity.PaymentConditionId,
                PaymentCondition = entity.PaymentCondition?.TransferToConditionDto(),
                VatSchemeId = entity.VatSchemeId,
                VatScheme = entity.VatScheme?.TransferToVatSchemeDto(),
                ContactInsurancePercentage = entity.ContactInsurancePercentage,
                InsurancePercentage = entity.InsurancePercentage,
                Rental = entity.Rental,
                Sales = entity.Sales,
                DiscountRentalEquipment = entity.DiscountRentalEquipment,
                DiscountSaleEquipment = entity.DiscountSaleEquipment,
                CrewDiscount = entity.CrewDiscount,
                TransportDiscount = entity.TransportDiscount,
                TotalDiscount = entity.TotalDiscount,
                SubhireDiscount = entity.SubhireDiscount,
                CreationDate = entity.CreationDate,
                LastModifiedDate = entity.LastModifiedDate
            };
            return dto;
        }

        /// <summary>
        /// Transfer from ContactPaymentDto to ContactPaymentEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactPaymentEntity TransferToEntity(this ContactPaymentDto dto)
        {
            ContactPaymentEntity entity = new ContactPaymentEntity()
            {
                ContactId = dto.ContactId,
                Id = dto.Id,
                InvoiceMomentId = dto.InvoiceMomentId,
                PaymentConditionId = dto.PaymentConditionId,
                VatSchemeId = dto.VatSchemeId,
                ContactInsurancePercentage = dto.ContactInsurancePercentage,
                InsurancePercentage = dto.InsurancePercentage,
                Rental = dto.Rental,
                Sales = dto.Sales,
                DiscountRentalEquipment = dto.DiscountRentalEquipment,
                DiscountSaleEquipment = dto.DiscountSaleEquipment,
                CrewDiscount = dto.CrewDiscount,
                TransportDiscount = dto.TransportDiscount,
                TotalDiscount = dto.TotalDiscount,
                SubhireDiscount = dto.SubhireDiscount,
                CreationDate = dto.CreationDate,
                LastModifiedDate = dto.LastModifiedDate
            };

            return entity;
        }
    }
}
