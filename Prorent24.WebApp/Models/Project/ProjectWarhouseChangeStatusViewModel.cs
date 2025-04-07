using Prorent24.Enum.Project;

namespace Prorent24.WebApp.Models.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectWarhouseChangeStatusViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProjectChangeStatusEnum Status { get; set; }
    }
}
