using Microsoft.EntityFrameworkCore;

namespace ProgHelperApp.Model.Data
{
    public class AplicationContext : DbContext
    {
        private DbSet<Employee> Employees { get; set; }
        private DbSet<CardProject> CardProjects { get; set; }
        private DbSet<Task> Tasks { get; set; }
        private DbSet<CardProjectEmployeeMap> CardProjectEmployeeMaps { get; set; }
        private DbSet<CardComplete> CardCompletes { get; set; }
        private DbSet<EmployeeTaskCardProjectMap> EmployeeTaskCardProjectMaps { get; set; }
        private DbSet<TaskCardProjectMap> TaskCardProjectMaps { get; set; }

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
