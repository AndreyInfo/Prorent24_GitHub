using Prorent24.BLL.Models.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.CustomerCommunication;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.BLL.Transfers.Configuration.CustomerCommunication
{
    public static class LetterheadTransfer
    {
        /// <summary>
        /// Transfer from List<LetterheadEntity> to List<LetterheadDto>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<LetterheadDto> TransferToListDto(this List<LetterheadEntity> entities)
        {
            List<LetterheadDto> dtos = entities.Select(x => x.TransferToDto()).ToList();

            return dtos;
        }

        /// <summary>
        /// Transfer from LetterheadEntity to LetterheadDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static LetterheadDto TransferToDto(this LetterheadEntity entity)
        {
            LetterheadDto dto = new LetterheadDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                PageSize = entity.PageSize,
                PageWidth = entity.PageWidth,
                PageHeight = entity.PageHeight,
                TopMargin = entity.TopMargin,
                RightMargin = entity.RightMargin,
                BottomMargin = entity.BottomMargin,
                LeftMargin = entity.LeftMargin,
                PageNumbers = entity.PageNumbers,
                ShowAtInvoices = entity.ShowAtInvoices,
                ShowAtQuotations = entity.ShowAtQuotations,
                DisplayAtContracts = entity.DisplayAtContracts,
                ShowAtSubhireSlips = entity.ShowAtSubhireSlips,
                ShowAtReminders = entity.ShowAtReminders,
                ShowAtCrewMemberSlips = entity.ShowAtCrewMemberSlips,
                ShowAtTransportSlips = entity.ShowAtTransportSlips,
                ShowOnEquipmentSlips = entity.ShowOnEquipmentSlips,
                ShowAtRepairSlips = entity.ShowAtRepairSlips,
                ShowAtOtherSlips = entity.ShowAtOtherSlips
            };

            return dto;
        }

        /// <summary>
        /// Transfer from LetterheadDto to LetterheadEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static LetterheadEntity TransferToEntity(this LetterheadDto dto)
        {
            LetterheadEntity entity= new LetterheadEntity()
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
