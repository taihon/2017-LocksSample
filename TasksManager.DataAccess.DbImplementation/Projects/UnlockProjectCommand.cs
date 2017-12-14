using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasksManager.DataAccess.Projects;
using TasksManager.Db;

namespace TasksManager.DataAccess.DbImplementation.Projects
{
    public class UnlockProjectCommand : IUnlockProjectCommand
    {
        private TasksContext Context { get; }

        public UnlockProjectCommand(TasksContext context)
        {
            Context = context;
        }

        public async Task ExecuteAsync(int projectId, int userId)
        {
            var projectLock = await Context.ProjectLocks.SingleOrDefaultAsync(pl => pl.ProjectId == projectId && pl.UserId == userId);
            if (projectLock == null)
            {
                return;
            }

            Context.ProjectLocks.Remove(projectLock);

            await Context.SaveChangesAsync();
        }
    }
}
