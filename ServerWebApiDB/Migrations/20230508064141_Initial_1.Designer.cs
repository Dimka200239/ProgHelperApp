﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerWebApiDB.Model.Data;

namespace ServerWebApiDB.Migrations
{
    [DbContext(typeof(AplicationContext))]
    [Migration("20230508064141_Initial_1")]
    partial class Initial_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServerWebApiDB.Model.CardComplete", b =>
                {
                    b.Property<Guid>("id_CardComplete_F")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfEnding_F")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("id_CardProject_F")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("id_Employee_F")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("id_Task_F")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id_CardComplete_F");

                    b.HasIndex("id_CardProject_F");

                    b.HasIndex("id_Employee_F");

                    b.HasIndex("id_Task_F");

                    b.ToTable("CardCompletes");
                });

            modelBuilder.Entity("ServerWebApiDB.Model.CardProject", b =>
                {
                    b.Property<Guid>("id_CardProject_F")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DateOfBegining_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Status_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("id_Employee_F")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id_CardProject_F");

                    b.HasIndex("id_Employee_F");

                    b.ToTable("CardProjects");
                });

            modelBuilder.Entity("ServerWebApiDB.Model.CardProjectEmployeeMap", b =>
                {
                    b.Property<Guid>("id_Employee_F")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("id_CardProject_F")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id_Employee_F", "id_CardProject_F");

                    b.HasIndex("id_CardProject_F");

                    b.ToTable("CardProjectEmployeeMaps");
                });

            modelBuilder.Entity("ServerWebApiDB.Model.Employee", b =>
                {
                    b.Property<Guid>("id_Employee_F")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Patronymic_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phone_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerName_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("id_Employee_F");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ServerWebApiDB.Model.EmployeeTaskCardProjectMap", b =>
                {
                    b.Property<Guid>("id_Task_F")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("id_CardProject_F")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("id_Employee_F")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("id_Forwarded_Employee_F")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id_Task_F", "id_CardProject_F", "id_Employee_F", "id_Forwarded_Employee_F");

                    b.HasIndex("id_CardProject_F");

                    b.HasIndex("id_Employee_F");

                    b.HasIndex("id_Forwarded_Employee_F");

                    b.ToTable("EmployeeTaskCardProjectMaps");
                });

            modelBuilder.Entity("ServerWebApiDB.Model.Task", b =>
                {
                    b.Property<Guid>("id_Task_F")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Deadline_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Status_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title_F")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("id_Task_F");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ServerWebApiDB.Model.TaskCardProjectMap", b =>
                {
                    b.Property<Guid>("id_Task_F")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("id_CardProject_F")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id_Task_F", "id_CardProject_F");

                    b.HasIndex("id_CardProject_F");

                    b.ToTable("TaskCardProjectMaps");
                });

            modelBuilder.Entity("ServerWebApiDB.Model.CardComplete", b =>
                {
                    b.HasOne("ServerWebApiDB.Model.CardProject", "CardProject")
                        .WithMany("CardCompletes")
                        .HasForeignKey("id_CardProject_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServerWebApiDB.Model.Employee", "Employee")
                        .WithMany("CardCompletes")
                        .HasForeignKey("id_Employee_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServerWebApiDB.Model.Task", "Task")
                        .WithMany("CardCompletes")
                        .HasForeignKey("id_Task_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerWebApiDB.Model.CardProject", b =>
                {
                    b.HasOne("ServerWebApiDB.Model.Employee", "Employee")
                        .WithMany("CardProjects")
                        .HasForeignKey("id_Employee_F")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerWebApiDB.Model.CardProjectEmployeeMap", b =>
                {
                    b.HasOne("ServerWebApiDB.Model.CardProject", "CardProject")
                        .WithMany("CardProjectEmployeeMaps")
                        .HasForeignKey("id_CardProject_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServerWebApiDB.Model.Employee", "Employee")
                        .WithMany("CardProjectEmployeeMaps")
                        .HasForeignKey("id_Employee_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerWebApiDB.Model.EmployeeTaskCardProjectMap", b =>
                {
                    b.HasOne("ServerWebApiDB.Model.CardProject", "CardProject")
                        .WithMany("EmployeeTaskCardProjectMaps")
                        .HasForeignKey("id_CardProject_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServerWebApiDB.Model.Employee", "Forwarded_Employee")
                        .WithMany()
                        .HasForeignKey("id_Employee_F")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerWebApiDB.Model.Employee", "Employee")
                        .WithMany("EmployeeTaskCardProjectMaps")
                        .HasForeignKey("id_Forwarded_Employee_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServerWebApiDB.Model.Task", "Task")
                        .WithMany("EmployeeTaskCardProjectMaps")
                        .HasForeignKey("id_Task_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerWebApiDB.Model.TaskCardProjectMap", b =>
                {
                    b.HasOne("ServerWebApiDB.Model.CardProject", "CardProject")
                        .WithMany("TaskCardProjectMaps")
                        .HasForeignKey("id_CardProject_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServerWebApiDB.Model.Task", "Task")
                        .WithMany("TaskCardProjectMaps")
                        .HasForeignKey("id_Task_F")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}