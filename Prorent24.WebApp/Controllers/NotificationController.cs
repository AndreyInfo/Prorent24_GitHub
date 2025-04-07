using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Notification;
using Prorent24.BLL.Services.Notification;
using Prorent24.WebApp.Transfers;
using Prorent24.WebApp.Transfers.Notification;

namespace Prorent24.WebApp.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get list Notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            BaseListDto dto = await _notificationService.GetPagedList();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Get short list Notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet("short")]
        public async Task<IActionResult> GetShortNotifications()
        {
            List<NotificationDto> dto = await _notificationService.GetShortList();
            return Ok(dto.TransferToListModel());
        }


        /// <summary>
        /// Get Notification by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificarionById(int id)
        {
            NotificationDto dto = await _notificationService.GetById(id);
            return Ok(dto);
        }

        /// <summary>
        ///  Read Notification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> ReadNotificarion(int id)
        {
            try
            {
                NotificationDto dto = await _notificationService.ReadNotification(id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}