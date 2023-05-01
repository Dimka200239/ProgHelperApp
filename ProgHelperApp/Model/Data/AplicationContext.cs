using Microsoft.EntityFrameworkCore;

namespace ProgHelperApp.Model.Data
{
    public class AplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; private set; }
        public DbSet<CardProject> CardProjects { get; private set; }
        public DbSet<Task> Tasks { get; private set; }
        public DbSet<CardProjectEmployeeMap> CardProjectEmployeeMaps { get; private set; }
        public DbSet<CardComplete> CardCompletes { get; private set; }
        public DbSet<EmployeeTaskCardProjectMap> EmployeeTaskCardProjectMaps { get; private set; }
        public DbSet<TaskCardProjectMap> TaskCardProjectMaps { get; private set; }

        public AplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTaskCardProjectMap>().HasKey(u => new { u.id_Employee_F, u.id_Task_F, u.id_CardProject_F });
            modelBuilder.Entity<CardProjectEmployeeMap>().HasKey(u => new { u.id_CardProject_F, u.id_Employee_F });
            modelBuilder.Entity<TaskCardProjectMap>().HasKey(u => new { u.id_Task_F, u.id_CardProject_F });
            modelBuilder.Entity<CardComplete>().HasKey(u => new { u.id_Employee_F, u.id_Task_F, u.id_CardProject_F });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProgHelperAppDB;Trusted_Connection=True;");
        }
    }
}
