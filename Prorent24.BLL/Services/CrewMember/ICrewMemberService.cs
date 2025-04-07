using Prorent24.BLL.Models;
using Prorent24.BLL.Models.CrewMember;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.Common.Models.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.CrewMember
{
    public interface ICrewMemberService
    {
        Task<List<ModuleDetailDto>> GetDetails(string id);
        Task<bool> UpdateDefaultRate(string id, int rateId);
        Task<BaseListDto> GetCrewMwmberForProjectPlanning();

        Task<CrewMemberDto> Update(CrewMemberDto model, string userId = null);

        Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList);

        Task<CrewMemberDto> GetById(string id);

        Task<CrewMemberDto> Create(CrewMemberDto model);

        Task<bool> Delete(string id);

        Task<List<CrewMemberDto>> GetAll();

    }
}
