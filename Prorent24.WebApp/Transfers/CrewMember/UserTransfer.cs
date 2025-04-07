using Prorent24.BLL.Models.CrewMember;
using Prorent24.Entity;
using Prorent24.WebApp.Models.Account;
using Prorent24.WebApp.Models.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.CrewMember
{
    public static class UserTransfer
    {
        /// <summary>
        /// Transfer from List<CrewMemberViewModel> to List<CrewMemberDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<CrewMemberViewModel> TransferToModelView(this List<User> entities)
        {
            List<CrewMemberViewModel> crewMemberDtos = entities.Select(x => x.TransferToModelView()).ToList();

            return crewMemberDtos;
        }

        /// <summary>
        /// Transfer from CrewMemberViewModel to CrewMemberDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CrewMemberViewModel TransferToModelView(this User model)
        {
            CrewMemberViewModel crewMemberDto = new CrewMemberViewModel()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.UserName,
                Email = model.Email,
                IsSystemUser = model.IsSystemUser
            };

            return crewMemberDto;
        }

        public static CrewMemberViewModel TransferToModelView(this RegisterViewModel model) {
            CrewMemberViewModel crewMemberDto = new CrewMemberViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Email,
                Email = model.Email,
                CompanyName = model.CompanyName
                
            };

            return crewMemberDto;
        }
    }
}
