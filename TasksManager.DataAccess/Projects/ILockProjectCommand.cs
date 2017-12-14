using System.Threading.Tasks;

namespace TasksManager.DataAccess.Projects
{
    public interface ILockProjectCommand
    {
        Task<bool> ExecuteAsync(int projectId, int userId);
    }
}
