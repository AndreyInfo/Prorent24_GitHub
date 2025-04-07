using Prorent24.BLL.Models.Subhire;
using Prorent24.WebApp.Models.Subhire;
using Prorent24.WebApp.Transfers.Contact;
using Prorent24.WebApp.Transfers.ContactPerson;
using Prorent24.WebApp.Transfers.CrewMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Subhire
{
    public static class SubhireShortTransfer
    {
        /// <summary>
        /// Transfer from List<SubhireShortViewModel> to List<SubhireShortDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<SubhireShortViewModel> TransferToListViewModel(this List<SubhireShortDto> models)
        {
            List<SubhireShortViewModel> dtos = models.Select(x => x.TransferToViewModel()).ToList();
            return dtos;
        }

     
        /// <summary>
        /// Transfer from SubhireShortDto to SubhireShortViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SubhireShortViewModel TransferToViewModel(this SubhireShortDto dto)
        {
            SubhireShortViewModel model = new SubhireShortViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Status = dto.Status,
                SupplierName = dto.SupplierName,
                IsAlmostInPeriod = dto.IsAlmostInPeriod,
                PlanningPeriodEnd = dto.PlanningPeriodEnd,
                PlanningPeriodStart = dto.PlanningPeriodStart

            };

            return model;
        }
    }
}
