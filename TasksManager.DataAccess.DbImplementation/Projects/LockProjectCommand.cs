using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasksManager.DataAccess.Projects;
using TasksManager.Db;
using TasksManager.Entities;

namespace TasksManager.DataAccess.DbImplementation.Projects
{
    public class LockProjectCommand : ILockProjectCommand
    {
        private TasksContext Context { get; }

        public LockProjectCommand(TasksContext context)
        {
            Context = context;
        }

        public async Task<bool> ExecuteAsync(int projectId, int userId)
        {
            var project = await Context.Projects.Include(p => p.Lock).SingleOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                return false;
            }

            if (project.Lock != null)
            {
                throw new ProjectLockedException();
            }

            var projectLock = new ProjectLock {ProjectId = projectId, UserId = userId, LockDateTime = DateTime.UtcNow};
            Context.ProjectLocks.Add(projectLock);

            await Context.SaveChangesAsync();

            return true;
        }
    }
}
