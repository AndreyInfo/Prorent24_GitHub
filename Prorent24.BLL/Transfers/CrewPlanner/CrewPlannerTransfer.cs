using Prorent24.BLL.Models.CrewPlanner;
using Prorent24.BLL.Transfers.CrewMember;
using Prorent24.BLL.Transfers.Vehicle;
using Prorent24.Entity.CrewPlanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Transfers.CrewPlanner
{
    public static class CrewPlannerTransfer
    {/// <summary>
     /// Transfer from List<CrewPlannerEntity> to List<CrewPlannerDto>
     /// </summary>
     /// <param name="entities"></param>
     /// <returns></returns>
        //public static List<CrewPlannerDto> TransferToListDto(this List<CrewPlannerEntity> entities)
        //{
        //    List<CrewPlannerDto> dtos = entities.Select(x => x.TransferToDto()).ToList();
        //    return dtos;
        //}

        /// <summary>
        /// Transfer from CrewPlannerEntity to CrewPlannerDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public static CrewPlannerDto TransferToDto(this CrewPlannerEntity entity, string exclude = null)
        //{
        //    CrewPlannerDto dto = new CrewPlannerDto()
        //    {
        //        Id = entity.Id,
        //        Action = entity.Action,
        //        From = entity.From,
        //        Until = entity.Until,
        //        VehicleId = entity.VehicleId,
        //        Vehicle  = entity.Vehicle?.TransferToDto(),
        //        CrewMemberId = entity.CrewMemberId,
        //        CrewMember = entity.CrewMember?.TransferToDto(),
        //        CreationDate = entity.CreationDate,
        //        LastModifiedDate = entity.LastModifiedDate
        //    };

        //    return dto;
        //}

        /// <summary>
        /// Transfer from CrewPlannerDto to CrewPlannerEntity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public static CrewPlannerEntity TransferToEntity(this CrewPlannerDto dto)
        //{
        //    CrewPlannerEntity entity = new CrewPlannerEntity()
        //    {
        //        Id = dto.Id,
        //        Action = dto.Action,
        //        From = dto.From,
        //        Until = dto.Until,
        //        VehicleId = dto.VehicleId,
        //        CrewMemberId = dto.CrewMemberId,
        //        CreationDate = dto.CreationDate,
        //        LastModifiedDate = dto.LastModifiedDate
        //    };

        //    return entity;
        //}
    }
}
