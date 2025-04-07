using Prorent24.Enum.General;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.General.Folder
{
    /// <summary>
    /// Folder View Model
    /// </summary>
    public class FolderViewModel
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Module type
        /// </summary>
        public ModuleTypeEnum ModuleType { get; set; }

        /// <summary>
        /// Folder name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parent Id folder
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// List Parents
        /// </summary>
        public List<FolderViewModel> Childs { get; set; }

        /// <summary>
        /// Selected current item
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Order number
        /// </summary>
        public int Order { get; set; }
    }
}
