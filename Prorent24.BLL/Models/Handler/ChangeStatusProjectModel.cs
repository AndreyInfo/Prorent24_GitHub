using MediatR;
using Prorent24.Enum.Project;

namespace Prorent24.BLL.Models.Handler
{
    public class ChangeStatusProjectModel : INotification
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
