using Prorent24.BLL.Models.Notification;
using Prorent24.Enum.General;
using Prorent24.Enum.Notification;
using Prorent24.WebApp.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Notification
{
    public static class NotificationTransfer
    {/// <summary>
     /// Transfer from List<CrewMemberShortDto> to List<CrewMemberShortVieweModel>
     /// </summary>
     /// <param name="entities"></param>
     /// <returns></returns>
        public static List<NotificationShortModel> TransferToListModel(this List<NotificationDto> entities)
        {
            List<NotificationShortModel> crewMemberViewModels = entities.Select(x => x.TransferToViewModel()).ToList();
            return crewMemberViewModels;
        }

        /// <summary>
        /// Transfer from CrewMemberShortVieweModel to CrewMemberShortDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static NotificationShortModel TransferToViewModel(this NotificationDto model)
        {
            NotificationShortModel view = new NotificationShortModel()
            {
                Id = model.Id,
                Message = model.Message,
                ModuleType = model.ModuleType,
                IsRead = model.IsRead,
                Theme = model.Theme,
                CreationDate = model.CreationDate
            };

            return view;
        }

       
    }
}
