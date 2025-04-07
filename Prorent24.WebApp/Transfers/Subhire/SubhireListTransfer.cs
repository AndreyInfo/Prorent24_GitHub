using Prorent24.BLL.Models.Subhire;
using Prorent24.WebApp.Models.Subhire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Subhire
{
    public static class SubhireListTransfer
    {
       
        /// <summary>
        /// Transfer from  SubhireListDto to  SubhireListViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static SubhireListViewModel TransferToViewModel(this SubhireListDto dto)
        {
            SubhireListViewModel model = new SubhireListViewModel()
            {
                DateFrom = dto.DateFrom,
                DateTo = dto.DateTo,
                Subhires = dto.Subhires?.TransferToListViewModel()
            };

            return model;
        }
    }
}
