using Microsoft.EntityFrameworkCore;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Enum.Configuration;
using Prorent24.UnitOfWork;
using Prorent24.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prorent24.DAL.Storages.Configuration.CustomerCommunication.DocumentTemplate
{
    internal class DocumentTemplateStorage : BaseStorage<DocumentTemplateEntity>, IDocumentTemplateStorage
    {
        public DocumentTemplateStorage(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Get list DocumentTemplates
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<IPagedList<DocumentTemplateEntity>> GetAll(List<Predicate<DocumentTemplateEntity>> conditions = null)
        {
            List<int> list = await _repos.TableNoTracking.Select(x => x.LanguageId).ToListAsync();
            int lang = list.Min();

            /*var result = await _repos.TableNoTracking.Where(x => x.LanguageId == lang).Include(contry => contry.Country.Locs).Include(language => language.Language.Locs)
               .Select(x => x).ToPagedListAsync(1, 100);*/


            return await _repos.GetPagedListAsync(predicate: x => x.LanguageId == lang,
                include: c => c.Include(contry => contry.Country.Locs).Include(language => language.Language.Locs), pageSize: 100);
        }

        public async Task<List<DocumentTemplateEntity>> GetAllByType(DocumentTemplateTypeEnum directoryType)
        {
            List<DocumentTemplateEntity> result = await _repos.TableNoTracking.Where(x => x.Type == directoryType)
                .Select(x => x).ToAsyncEnumerable().ToList();

            int languageMin = result.Select(x => x.LanguageId).Min();

            return result.Where(x => x.LanguageId == languageMin).ToList();
        }

        public async override Task<DocumentTemplateEntity> GetById(object id)
        {

            var result = await _repos.TableNoTracking
                            .Include(x => x.Blocks)
                            .FirstOrDefaultAsync(x => x.Id == (int)id);


            return result;
        }
    }
}
