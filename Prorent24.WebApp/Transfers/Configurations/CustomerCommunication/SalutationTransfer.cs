using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;

namespace Prorent24.WebApp.Transfers.Configurations.CustomerCommunication
{
    public static class SalutationTransfer
    {
        /// <summary>
        /// Transfer from SalutationDto to SalutationViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SalutationViewModel TransferToViewModel(this SalutationDto dto)
        {
            SalutationViewModel viewModel = new SalutationViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                View = dto.DisplayView
            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from SalutationViewModel to SalutationDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static SalutationDto TransferToViewModel(this SalutationViewModel view)
        {
            SalutationDto dto = new SalutationDto()
            {
                Id = view.Id,
                Name = view.Name,
                DisplayView = view.View
            };

            return dto;
        }
    }
}
