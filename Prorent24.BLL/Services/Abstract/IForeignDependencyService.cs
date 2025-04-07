using Prorent24.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Abstract
{
    public interface IForeignDependencyService<T>
    {
        Task<BaseListDto> GetPagedList(int id);
    }
}
