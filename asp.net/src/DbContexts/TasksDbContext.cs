
using Microsoft.EntityFrameworkCore;

using TaskService.Entities;

namespace TaskService.DbContexts
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
        {
        }

        public DbSet<TaskEntity> TaskEntities { get; set; }
    }
}
