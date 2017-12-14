using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.DataAccess.Projects;
using TasksManager.Db;
using TasksManager.ViewModel.Projects;
using TaskStatus = TasksManager.Entities.TaskStatus;

namespace TasksManager.DataAccess.DbImplementation.Projects
{
    public class UpdateProjectCommand : IUpdateProjectCommand
    {
        private TasksContext Context { get; }

        public UpdateProjectCommand(TasksContext context)
        {
            Context = context;
        }

        public async Task<ProjectResponse> ExecuteAsync(int projectId, UpdateProjectRequest request)
        {
            var project = await Context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return null;
            }

            // TODO: Use Automapper here.
            project.Name = request.Name;
            project.Description = request.Description;
            for (int i = 0; i < request.RowVersion.Length; i++)
            {
                project.RowVersion[i] = request.RowVersion[i];
            }

            await Context.SaveChangesAsync();

            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                RowVersion = project.RowVersion,
                OpenTasksCount = Context.Tasks.Count(t => t.ProjectId == projectId && t.Status != TaskStatus.Completed)
            };
        }
    }
}
