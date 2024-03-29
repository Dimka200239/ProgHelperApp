﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ServerWebApiDB.Model.Data
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
            modelBuilder.Entity<CardProjectEmployeeMap>().HasKey(u => new { u.id_Employee_F, u.id_CardProject_F });
            modelBuilder.Entity<TaskCardProjectMap>().HasKey(u => new { u.id_Task_F, u.id_CardProject_F });
            modelBuilder.Entity<EmployeeTaskCardProjectMap>().HasKey(u => new { u.id_Task_F, u.id_CardProject_F, u.id_Employee_F, u.id_Forwarded_Employee_F});

            modelBuilder.Entity<CardComplete>()
                .HasOne(cc => cc.Employee)
                .WithMany(cc => cc.CardCompletes)
                .HasForeignKey(cc => cc.id_Employee_F)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CardComplete>()
                .HasOne(cc => cc.Task)
                .WithMany(cc => cc.CardCompletes)
                .HasForeignKey(cc => cc.id_Task_F)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CardComplete>()
                .HasOne(cc => cc.CardProject)
                .WithMany(cc => cc.CardCompletes)
                .HasForeignKey(cc => cc.id_CardProject_F)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CardProjectEmployeeMap>()
                .HasOne(cc => cc.Employee)
                .WithMany(cc => cc.CardProjectEmployeeMaps)
                .HasForeignKey(cc => cc.id_Employee_F)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CardProjectEmployeeMap>()
                .HasOne(cc => cc.CardProject)
                .WithMany(cc => cc.CardProjectEmployeeMaps)
                .HasForeignKey(cc => cc.id_CardProject_F)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeTaskCardProjectMap>()
                .HasOne(cc => cc.CardProject)
                .WithMany(cc => cc.EmployeeTaskCardProjectMaps)
                .HasForeignKey(cc => cc.id_CardProject_F)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeTaskCardProjectMap>()
                .HasOne(cc => cc.Employee)
                .WithMany(cc => cc.EmployeeTaskCardProjectMaps)
                .HasForeignKey(cc => cc.id_Employee_F)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeTaskCardProjectMap>()
                .HasOne(cc => cc.Task)
                .WithMany(cc => cc.EmployeeTaskCardProjectMaps)
                .HasForeignKey(cc => cc.id_Task_F)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeTaskCardProjectMap>()
                .HasOne(cc => cc.Employee)
                .WithMany(cc => cc.EmployeeTaskCardProjectMaps)
                .HasForeignKey(cc => cc.id_Forwarded_Employee_F)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskCardProjectMap>()
                .HasOne(cc => cc.CardProject)
                .WithMany(cc => cc.TaskCardProjectMaps)
                .HasForeignKey(cc => cc.id_CardProject_F)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TaskCardProjectMap>()
                .HasOne(cc => cc.Task)
                .WithMany(cc => cc.TaskCardProjectMaps)
                .HasForeignKey(cc => cc.id_Task_F)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProgHelperAppDB;Trusted_Connection=True;");
        }
    }
}
