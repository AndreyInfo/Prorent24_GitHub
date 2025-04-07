using Prorent24.BLL.Models.CrewMember;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.CrewMember
{
    public static class SocialNetworkTransfer
    {

        public static List<SocialNetworkViewModel> TransferToViewModels(this List<SocialNetworkDto> dtos)
        {
            return dtos.Select(x => x.TransferToViewModel()).ToList();
        }

        public static List<SocialNetworkDto> TransferToDtoModels(this List<SocialNetworkViewModel> dtos)
        {
            return dtos.Select(x => x.TransferToDtoModel()).ToList();
        }

        /// <summary>
        /// Transfer from SocialNetworkDto to SocialNetworkViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        public static SocialNetworkViewModel TransferToViewModel(this SocialNetworkDto dto)
        {
            SocialNetworkViewModel viewModel = new SocialNetworkViewModel()
            {
                Type = dto.Type,
                Name = dto.Name

            };

            return viewModel;
        }

        /// <summary>
        /// Transfer from SocialNetworkViewModel to SocialNetworkDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static SocialNetworkDto TransferToDtoModel(this SocialNetworkViewModel view)
        {
            SocialNetworkDto dto = new SocialNetworkDto()
            {
                Type = view.Type,
                Name = view.Name
            };

            return dto;
        }
    }
}
