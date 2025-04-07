using Prorent24.BLL.Models.Contact;
using Prorent24.WebApp.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Contact
{
    public static class ContactPaymentTransfer
    {
        /// <summary>
        /// Transfer from List<ContactPaymentViewModel> to List<ContactPaymentDto>
        /// </summary>
        /// <param name="viewmodels"></param>
        /// <returns></returns>
        public static List<ContactPaymentDto> TransferToListDto(this List<ContactPaymentViewModel> viewmodels)
        {
            List<ContactPaymentDto> ContactPaymentDtos = viewmodels.Select(x => x.TransferToDto()).ToList();

            return ContactPaymentDtos;
        }

        /// <summary>
        /// Transfer from List<ContactPaymentDto> to List<ContactPaymentViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ContactPaymentViewModel> TransferToListViewModel(this List<ContactPaymentDto> dtos)
        {
            List<ContactPaymentViewModel> viewModels = dtos.Select(x => x.TransferToViewModel()).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from ContactPaymentDto to ContactPaymentViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ContactPaymentDto TransferToDto(this ContactPaymentViewModel model)
        {
            ContactPaymentDto ContactPaymentDto = new ContactPaymentDto()
            {
                Id = model.Id,
                ContactId = model.ContactId,
                DiscountRentalEquipment = model.DiscountRentalEquipment,
                DiscountSaleEquipment = model.DiscountSaleEquipment,
                InvoiceMomentId = model.InvoiceMomentId,
                PaymentConditionId = model.PaymentConditionId,
                VatSchemeId = model.VatSchemeId,

                InsurancePercentage = model.InsurancePercentage,
                ContactInsurancePercentage = model.ContactInsurancePercentage,

                Rental = model.Rental,
                Sales = model.Sales,
                CrewDiscount = model.CrewDiscount,
                SubhireDiscount = model.SubhireDiscount,
                TotalDiscount = model.TotalDiscount,
                TransportDiscount = model.TransportDiscount
            };

            return ContactPaymentDto;
        }

        /// <summary>
        /// Transfer from ContactPaymentViewModel to ContactPaymentDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static ContactPaymentViewModel TransferToViewModel(this ContactPaymentDto dto)
        {
            ContactPaymentViewModel ContactPaymentViewModel = new ContactPaymentViewModel();

            if (dto != null)
            {
                ContactPaymentViewModel = new ContactPaymentViewModel()
                {
                    Id = dto.Id,
                    ContactId = dto.ContactId,
                    DiscountRentalEquipment = dto.DiscountRentalEquipment,
                    DiscountSaleEquipment = dto.DiscountSaleEquipment,
                    InvoiceMomentId = dto.InvoiceMomentId,
                    PaymentConditionId = dto.PaymentConditionId,
                    VatSchemeId = dto.VatSchemeId,

                    InsurancePercentage = dto.InsurancePercentage,
                    ContactInsurancePercentage = dto.ContactInsurancePercentage,

                    Rental = dto.Rental,
                    Sales = dto.Sales,
                    CrewDiscount = dto.CrewDiscount,
                    SubhireDiscount = dto.SubhireDiscount,
                    TotalDiscount = dto.TotalDiscount,
                    TransportDiscount = dto.TransportDiscount
                };
            }

            return ContactPaymentViewModel;
        }
    }
}
