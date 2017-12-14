using System.Linq;
using System.Threading.Tasks;
using TasksManager.DataAccess.Projects;
using TasksManager.Db;
using TasksManager.ViewModel.Projects;
using TaskStatus = TasksManager.Entities.TaskStatus;

namespace TasksManager.DataAccess.DbImplementation.Projects
{
    public class DeleteProjectCommand : IDeleteProjectCommand
    {
        private TasksContext Context { get; }

        public DeleteProjectCommand(TasksContext context)
        {
            Context = context;
        }

        public async Task<bool> ExecuteAsync(int projectId, DeleteProjectRequest request)
        {
            var project = await Context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return false;
            }

            //TODO: check project task status

            // TODO: Use Automapper here.
            for (int i = 0; i < request.RowVersion.Length; i++)
            {
                project.RowVersion[i] = request.RowVersion[i];
            }

            Context.Projects.Remove(project);

            await Context.SaveChangesAsync();

            return true;
        }
    }
}
