using Prorent24.BLL.Models;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.CrewMember
{
    public interface ICrewMemberRateService : IBaseService<CrewMemberRateDto, int>
    {
        Task<BaseListDto> GetPagedList(string crewMemberId);
    }
}

