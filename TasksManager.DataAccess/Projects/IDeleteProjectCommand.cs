using System.Threading.Tasks;
using TasksManager.ViewModel.Projects;

namespace TasksManager.DataAccess.Projects
{
    public interface IDeleteProjectCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="CannotDeleteProjectWithTasksException"></exception>
        Task<bool> ExecuteAsync(int projectId, DeleteProjectRequest request);
    }
}
