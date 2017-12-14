using Microsoft.EntityFrameworkCore;
using TasksManager.Entities;

namespace TasksManager.Db
{
    public class TasksContext : DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagsInTask> TagsInTasks { get; set; }
        public DbSet<ProjectLock> ProjectLocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Lock)
                .WithOne(pl => pl.Project)
                .HasForeignKey<ProjectLock>(pl => pl.ProjectId);
        }
    }
}
