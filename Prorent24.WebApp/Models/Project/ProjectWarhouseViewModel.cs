using System;
using System.Collections.Generic;

namespace Prorent24.WebApp.Models.Project
{
    /// <summary>
    /// List Project Warhouse by statuses
    /// </summary>
    public class ProjectWarhouseListViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ProjectWarhouseListViewModel()
        {
            Pack = new List<ProjectWarhouseViewModel>();
            Prepped = new List<ProjectWarhouseViewModel>();
            OnLocation = new List<ProjectWarhouseViewModel>();
            InUse = new List<ProjectWarhouseViewModel>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<ProjectWarhouseViewModel> Pack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProjectWarhouseViewModel> Prepped { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProjectWarhouseViewModel> OnLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProjectWarhouseViewModel> InUse { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProjectWarhouseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartPlaning { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndPlaning { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExpectedBack { get; set; }
    }


}
