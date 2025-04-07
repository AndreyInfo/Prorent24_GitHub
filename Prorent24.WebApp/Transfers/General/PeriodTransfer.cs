using Prorent24.BLL.Models.General.Filter;
using Prorent24.BLL.Models.General.Period;
using Prorent24.BLL.Models.General.Preset;
using Prorent24.Enum.General;
using Prorent24.WebApp.Models.General.Filter;
using Prorent24.WebApp.Models.General.Period;
using Prorent24.WebApp.Models.General.Perset;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.General
{
    internal static class PeriodTransfer
    {
        /// <summary>
        /// Transfer from List<PeriodDto> to List<PeriodViewModel>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<PeriodViewModel> TransferToViewModel(this List<PeriodDto> list)
        {
            List<PeriodViewModel> viewModels = list.Select(x => new PeriodViewModel()
            {
               PeriodType = x.PeriodType,
               Duration = x.Duration,
               DurationTime = x.DurationTime,
               FromDate = x.FromDate,
               TimeUnit = x.TimeUnit,
               ToDate = x.ToDate
            }).ToList();

            return viewModels;
        }

        /// <summary>
        /// Transfer from PeriodDto to PeriodViewModel
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static PeriodViewModel TransferToViewModel(this PeriodDto dto)
        {
            PeriodViewModel view = new PeriodViewModel()
            {
                PeriodType = dto.PeriodType,
                Duration = dto.Duration,
                DurationTime = dto.DurationTime,
                FromDate = dto.FromDate,
                TimeUnit = dto.TimeUnit,
                ToDate = dto.ToDate
            };

            return view;
        }

        /// <summary>
        /// Transfer from PeriodViewModel to PeriodDto
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static PeriodDto TransferToDtoModel(this PeriodViewModel view)
        {
            PeriodDto dto = new PeriodDto()
            {
                PeriodType = view.PeriodType,
                Duration = view.Duration,
                DurationTime = view.DurationTime,
                FromDate = view.FromDate,
                TimeUnit = view.TimeUnit,
                ToDate = view.ToDate
            };

            return dto;
        }

    }
}
