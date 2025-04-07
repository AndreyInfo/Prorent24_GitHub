using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.WebApp.Models.Configuration.CustomerCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prorent24.WebApp.Transfers.Configurations.CustomerCommunication
{

    public static class LetterheadTransfer
    {

        /// <summary>
        /// Transfer from List<LetterheadDto> to List<LetterheadViewModel> 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<LetterheadViewModel> TransferToListDto(this List<LetterheadDto> entities)
        {
            List<LetterheadViewModel> models = entities.Select(x => x.TransferToViewModel()).ToList();
            return models;
        }

        /// <summary>
        /// Transfer from LetterheadViewModel to LetterheadDto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static LetterheadDto TransferToDto(this LetterheadViewModel model)
        {
            LetterheadDto dto = new LetterheadDto()
            {
                Id = model.Id,
                Name = model.Name,
                PageSize = model.PageSize,
                PageWidth = model.PageWidth,
                PageHeight = model.PageHeight,
                TopMargin = model.TopMargin,
                RightMargin = model.RightMargin,
                BottomMargin = model.BottomMargin,
                LeftMargin = model.LeftMargin,
                PageNumbers = model.PageNumbers,
                ShowAtInvoices = model.ShowAtInvoices,
                ShowAtQuotations = model.ShowAtQuotations,
                DisplayAtContracts = model.DisplayAtContracts,
                ShowAtSubhireSlips = model.ShowAtSubhireSlips,
                ShowAtReminders = model.ShowAtReminders,
                ShowAtCrewMemberSlips = model.ShowAtCrewMemberSlips,
                ShowAtTransportSlips = model.ShowAtTransportSlips,
                ShowOnEquipmentSlips = model.ShowOnEquipmentSlips,
                ShowAtRepairSlips = model.ShowAtRepairSlips,
                ShowAtOtherSlips = model.ShowAtOtherSlips

            };

            return dto;
        }

        /// <summary>
        /// Transfer from LetterheadDto to LetterheadDto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static LetterheadViewModel TransferToViewModel(this LetterheadDto dto)
        {
            LetterheadViewModel entity = new LetterheadViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                PageSize = dto.PageSize,
                PageWidth = dto.PageWidth,
                PageHeight = dto.PageHeight,
                TopMargin = dto.TopMargin,
                RightMargin = dto.RightMargin,
                BottomMargin = dto.BottomMargin,
                LeftMargin = dto.LeftMargin,
                PageNumbers = dto.PageNumbers,
                ShowAtInvoices = dto.ShowAtInvoices,
                ShowAtQuotations = dto.ShowAtQuotations,
                DisplayAtContracts = dto.DisplayAtContracts,
                ShowAtSubhireSlips = dto.ShowAtSubhireSlips,
                ShowAtReminders = dto.ShowAtReminders,
                ShowAtCrewMemberSlips = dto.ShowAtCrewMemberSlips,
                ShowAtTransportSlips = dto.ShowAtTransportSlips,
                ShowOnEquipmentSlips = dto.ShowOnEquipmentSlips,
                ShowAtRepairSlips = dto.ShowAtRepairSlips,
                ShowAtOtherSlips = dto.ShowAtOtherSlips
            };

            return entity;
        }
    }
}
