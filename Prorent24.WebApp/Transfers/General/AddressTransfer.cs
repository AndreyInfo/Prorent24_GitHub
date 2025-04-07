using Prorent24.BLL.Models.General.Address;
using Prorent24.WebApp.Models.General.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.General
{
    public static class AddressTransfer
    {
        /// <summary>
        /// Transfer from AddressViewModel to AddressDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static AddressDto TransferToDto(this AddressViewModel view)
        {
            AddressDto dto = new AddressDto()
            {
                Region = view.Region,
                CountryId = view.CountryId,
                City = view.City,
                AdditionalAddress = view.AdditionalAddress,
                AddressId = view.AddressId,
                Number = view.Number,
                PostalCode = view.PostalCode,
                Address = view.Address,

                Alt = view.Alt,
                Lat = view.Lat,
                Long = view.Long
            };

            return dto;
        }

        /// <summary>
        /// Transfer from AddressDto to AddressViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static AddressViewModel TransferToView(this AddressDto dto)
        {
            AddressViewModel view = new AddressViewModel()
            {
                Region = dto.Region,
                CountryId = dto.CountryId,
                City = dto.City,
                AdditionalAddress = dto.AdditionalAddress,
                AddressId = dto.AddressId,
                Number = dto.Number,
                PostalCode = dto.PostalCode,
                Address = dto.Address,

                Alt = dto.Alt,
                Lat = dto.Lat,
                Long = dto.Long
                //Path = dto.Path
            };

            return view;
        }

        /// <summary>
        /// Transfer from List<AddressDto> to List<AddressViewModel>
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<AddressViewModel> TransferToListView(this List<AddressDto> dtos)
        {
            List<AddressViewModel> views = dtos.Select(x => x.TransferToView()).ToList();
            return views;
        }
    }
}
