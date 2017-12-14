using System;

namespace TasksManager.DataAccess.Projects
{
    public class ProjectLockedException : Exception
    {
        public ProjectLockedException()
            : base("TODO: exception message") { }
    }
}
