using System.Threading.Tasks;

namespace TasksManager.DataAccess.Projects
{
    public interface IUnlockProjectCommand
    {
        Task ExecuteAsync(int projectId, int userId);
    }
}
