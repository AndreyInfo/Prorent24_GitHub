using Newtonsoft.Json;
using Prorent24.BLL.Models.Equipment;
using Prorent24.WebApp.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Equipment
{
    public static class EquipmentSerialNumberTransfer
    {
        /// <summary>
        /// Transfer from List<EquipmentSerialNumberViewModel> to List<EquipmentSerialNumberDto>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<EquipmentSerialNumberDto> TransferToListDto(this List<EquipmentSerialNumberViewModel> models)
        {
            List<EquipmentSerialNumberDto> EquipmentSerialNumberDtos = models.Select(x => x.TransferToDto()).ToList();

            return EquipmentSerialNumberDtos;
        }

        /// <summary>
        /// Transfer from EquipmentSerialNumberDto to EquipmentSerialNumberViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EquipmentSerialNumberDto TransferToDto(this EquipmentSerialNumberViewModel model)
        {
            EquipmentSerialNumberDto EquipmentSerialNumberDto = new EquipmentSerialNumberDto()
            {
                Id = model.Id,
                EquipmentId = model.EquipmentId,
                InternalReference = model.InternalReference,
                SerialNumber = model.SerialNumber,
                BookValue = model.BookValue,
                Active = model.Active,
                CalculateBookValueAutomatically = model.CalculateBookValueAutomatically,

                //PurchaseDate = model.PurchaseDate,
                //New
                PurchaseDate = model.PurchaseDate != null ? ((DateTime)model.PurchaseDate).ToString("dd.MM.yyyy") : null,

                PurchasePrice = model.PurchasePrice,
                DepreciationPerMonth = model.DepreciationPerMonth,
                Remark = model.Remark,
                SuppliersInfoJson = JsonConvert.SerializeObject(model.SuppliersInfo),
            };

            return EquipmentSerialNumberDto;
        }

        /// <summary>
        /// Transfer from EquipmentSerialNumberViewModel to EquipmentSerialNumberDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static EquipmentSerialNumberViewModel TransferToViewModel(this EquipmentSerialNumberDto dto)
        {
            EquipmentSerialNumberViewModel EquipmentSerialNumberViewModel = new EquipmentSerialNumberViewModel()
            {
                Id = dto.Id,
                EquipmentId = dto.EquipmentId,
                InternalReference = dto.InternalReference,
                SerialNumber = dto.SerialNumber,
                BookValue = dto.BookValue,
                Active = dto.Active,
                CalculateBookValueAutomatically = dto.CalculateBookValueAutomatically,

                //PurchaseDate = dto.PurchaseDate,
                //New
                PurchaseDate = DateTime.TryParse(dto.PurchaseDate, out DateTime resultPurchaseDate) ? (DateTime?)resultPurchaseDate : null,

                PurchasePrice = dto.PurchasePrice,
                DepreciationPerMonth = dto.DepreciationPerMonth,
                Remark = dto.Remark,
                SuppliersInfo = dto.SuppliersInfo?.TransferToViewModel()

            };
            return EquipmentSerialNumberViewModel;
        }

        /// <summary>
        /// Transfer from List<EquipmentSerialNumberDto> to List<EquipmentSerialNumberViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<EquipmentSerialNumberViewModel> TransferToViewModel(this List<EquipmentSerialNumberDto> dtos)
        {
            List<EquipmentSerialNumberViewModel> EquipmentSerialNumberViewModels = dtos.Select(x => x.TransferToViewModel()).ToList();
            return EquipmentSerialNumberViewModels;
        }
    }
}
