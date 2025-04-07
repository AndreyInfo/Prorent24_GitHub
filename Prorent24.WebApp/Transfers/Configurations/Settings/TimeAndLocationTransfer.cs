using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.WebApp.Models.Configuration.Settings;

namespace Prorent24.WebApp.Transfers.Configurations.Settings
{
    public static class TimeAndLocationTransfer
    {
        /// <summary>
        /// Transfer from  TimeAndLocationDto to  TimeAndLocationViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TimeAndLocationViewModel TransferToViewModel(this TimeAndLocationDto dto)
        {
            TimeAndLocationViewModel viewModel = new TimeAndLocationViewModel()
            {
               TimeZone = dto.TimeZone,

               CountryId = dto.CountryId,
               City = dto.City,
               Street = dto.Street,
               HouseNumber = dto.HouseNumber,
               Postcode = dto.Postcode,
               Lat = dto.Lat,
               Long = dto.Long,
               Alt = dto.Alt
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from  TimeAndLocationViewModel to  TimeAndLocationDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static TimeAndLocationDto TransferToDto(this TimeAndLocationViewModel view)
        {
            TimeAndLocationDto dto = new TimeAndLocationDto()
            {
                TimeZone = view.TimeZone,

                CountryId = view.CountryId,
                City = view.City,
                Street = view.Street,
                HouseNumber = view.HouseNumber,
                Postcode = view.Postcode,
                Lat = view.Lat,
                Long = view.Long,
                Alt = view.Alt
            };

            return dto;
        }
    }
}
