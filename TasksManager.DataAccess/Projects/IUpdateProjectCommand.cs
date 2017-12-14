using System.Threading.Tasks;
using TasksManager.ViewModel.Projects;

namespace TasksManager.DataAccess.Projects
{
    public interface IUpdateProjectCommand
    {
        Task<ProjectResponse> ExecuteAsync(int projectId, UpdateProjectRequest request);
    }
}
