using BE_TaskManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_TaskManager.Context
{
    public class TaskManager : DbContext
    {
        public TaskManager(DbContextOptions<TaskManager> options) : base(options)
        {
            options = options ?? throw new ArgumentNullException();
        }

        //public DbSet<Models.Task>? Tasks { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}
