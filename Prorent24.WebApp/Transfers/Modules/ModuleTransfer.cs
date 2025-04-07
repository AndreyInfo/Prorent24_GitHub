using Prorent24.BLL.Models.General.File;
using Prorent24.BLL.Models.General.Modules;
using Prorent24.BLL.Models.General.Note;
using Prorent24.BLL.Models.General.Tag;
using Prorent24.BLL.Models.Tasks;
using Prorent24.BLL.Transfers.General;
using Prorent24.Enum.Entity;
using Prorent24.WebApp.Models.General.File;
using Prorent24.WebApp.Models.General.Note;
using Prorent24.WebApp.Models.General.Tag;
using Prorent24.WebApp.Models.Modules;
using Prorent24.WebApp.Models.Tasks;
using Prorent24.WebApp.Transfers.General;
using Prorent24.WebApp.Transfers.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Prorent24.WebApp.Transfers.Modules
{
    /// <summary>
    /// Module transfers
    /// </summary>
    public static class ModuleTransfer
    {
        /// <summary>
        /// Transfer to ViewModel
        /// </summary>
        /// <param name="moduleDtos"></param>
        /// <returns></returns>
        public static List<ModuleViewModel> TransferToModuleViewModel(this List<ModuleDto> moduleDtos)
        {
            List<ModuleViewModel> modules = moduleDtos.Select(x => new ModuleViewModel()
            {
                Id = x.Id,
                ModuleType = x.ModuleType,
                Name = x.Name,
                Order = x.Order
            }).ToList();

            return modules;
        }

        /// <summary>
        /// Transfer to model detail
        /// </summary>
        /// <param name="moduleDtos"></param>
        /// <returns></returns>
        public static List<ModuleDetailViewModel> TransferToModuleDetailViewModel(this List<ModuleDetailDto> moduleDtos)
        {
            List<ModuleDetailViewModel> modules = moduleDtos.Select(x => new ModuleDetailViewModel()
            {
                Entity = x.Entity,
                Data = x.Data.GetDataByEntityType(x.Entity)
            }).ToList();

            return modules;
        }

        #region Private

        private static object GetDataByEntityType(this object data, DetailsEntityEnum entityType)
        {
            switch (entityType)
            {
                case DetailsEntityEnum.DetailEntity:
                    {
                        List<DetailDto> details = data != null ? (List<DetailDto>)data : null;
                        List<DetailViewModel> detailView = details != null ? details.TransferToDetailViewModel() : null;
                        return detailView;
                    }
                case DetailsEntityEnum.Project:
                case DetailsEntityEnum.Equipment:
                case DetailsEntityEnum.Subhire:
                    {
                        return data;
                    }
                case DetailsEntityEnum.ProjectList:
                    {
                        List<string> details = data != null ? (List<string>)data : null;
                        return data;

                    }
                case DetailsEntityEnum.TagEntity:
                    {
                        List<TagDto> tags = data != null ? (List<TagDto>)data : null;
                        List<TagViewModel> tagVieweModels = tags != null ? tags.TransferToViewModel() : null;
                        return tagVieweModels;
                    }
                case DetailsEntityEnum.TaskEntity:
                    {
                        List<TaskDto> tasks = data != null ? (List<TaskDto>)data : null;
                        List<TaskViewModel> taskVieweModels = tasks != null ? tasks.TransferToViewModel() : null;
                        return taskVieweModels;
                    }
                case DetailsEntityEnum.NoteEntity:
                    {
                        List<NoteDto> notes = data != null ? (List<NoteDto>)data : null;
                        List<NoteViewModel> noteVieweModels = notes != null ? notes.TransferToViewModel() : null;
                        return noteVieweModels;
                    }
                case DetailsEntityEnum.FileEntity:
                    {
                        List<FileDto> files = data != null ? (List<FileDto>)data : null;
                        List<FileViewModel> fileVieweModels = files != null ? files.TransferToListView() : null;
                        return fileVieweModels;
                    }
                case DetailsEntityEnum.DetailsOfLinkedItem:
                    {
                        List<DetailDto> details = data != null ? (List<DetailDto>)data : null;
                        List<DetailViewModel> detailView = details != null ? details.TransferToDetailViewModel() : null;
                        return detailView;
                    }
                case DetailsEntityEnum.LastNotesLinkedToThisItem:
                    {
                        List<NoteDto> notes = data != null ? (List<NoteDto>)data : null;
                        List<NoteViewModel> noteVieweModels = notes != null ? notes.TransferToViewModel() : null;
                        return noteVieweModels;
                    }
                case DetailsEntityEnum.LastTasksLinkedToThisItem:
                    {
                        List<TaskDto> tasks = data != null ? (List<TaskDto>)data : null;
                        List<TaskViewModel> taskVieweModels = tasks != null ? tasks.TransferToViewModel() : null;
                        return taskVieweModels;
                    }
                case DetailsEntityEnum.ViewTimeLine:
                    {
                        List<DetailDto> details = data != null ? (List<DetailDto>)data : null;
                        List<DetailViewModel> detailView = details != null ? details.TransferToDetailViewModel() : null;
                        return detailView;
                        //Dictionary<string, object> viewTimeLine = data != null ? (Dictionary<string, object>)data : null;
                        //return viewTimeLine;
                    }
            }

            return null;
        }

        public static List<DetailViewModel> TransferToDetailViewModel(this List<DetailDto> dtos)
        {
            List<DetailViewModel> views = dtos.Select(x => new DetailViewModel()
            {
                Key = x.Key,
                Type = x.Type,
                Value = x.Value
            }).ToList();

            return views;
        }


        #endregion
    }
}
