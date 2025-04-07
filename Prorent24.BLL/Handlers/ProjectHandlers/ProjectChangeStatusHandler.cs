using MediatR;
using Prorent24.BLL.Models.Handler;
using Prorent24.BLL.Services.Project;
using Prorent24.BLL.Services.Project.Movement;
using System.Threading;
using System.Threading.Tasks;

namespace Prorent24.BLL.Handlers.ProjectHandlers
{
    public class ProjectChangeStatusHandler : INotificationHandler<ChangeStatusProjectModel>
    {
        private readonly IProjectService _projectService;
        private readonly IProjectEquipmentMovementService _projectEquipmentMovementService;

        public ProjectChangeStatusHandler(IProjectService projectService,
                                          IProjectEquipmentMovementService projectEquipmentMovementService)
        {
            _projectService = projectService;
            _projectEquipmentMovementService = projectEquipmentMovementService;
        }

        public async Task Handle(ChangeStatusProjectModel notification, CancellationToken cancellationToken)
        {
            await _projectService.ChangeStatus(notification.ProjectId, notification.Status);
        }
    }
}
