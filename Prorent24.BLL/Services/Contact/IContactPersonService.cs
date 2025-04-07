using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.BLL.Services.Contact
{
    public interface IContactPersonService
    {
        /// <summary>
        /// Create ContactPerson
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ContactPersonDto> Create(ContactPersonDto model);

        /// <summary>
        /// Update ContactPerson
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ContactPersonDto> Update(ContactPersonDto model);


        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ContactPersonDto> GetById(int id);


        /// <summary>
        /// Delete ContactPerson
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(int contactId, int id);

        /// <summary>
        /// Delete ContactPerson multiple
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        Task<bool> DeleteMultiple(List<int> values);

        /// <summary>
        /// Get list ContactPersons by contactId
        /// </summary>
        /// <returns></returns>
        Task<BaseListDto> GetPagedList(int contactId);

        Task<List<ContactPersonDto>> SearchContactPersons(string likeString);
    }
}
