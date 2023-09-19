using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Models;
using ToDo.Persistence.EntityConfigurations;

namespace ToDo.Persistence.Data
{


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        //data logging to get more detailed information in the logs
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .EnableSensitiveDataLogging()
        //        .UseSqlServer("server=Nati-Ti; database=ToDo; Integrated Security=true; Encrypt=false");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, constraints, and other configurations

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());
            modelBuilder.ApplyConfiguration(new ToDoListConfiguration());
        }
    }
}
