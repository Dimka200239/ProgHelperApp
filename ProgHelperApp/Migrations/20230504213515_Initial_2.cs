using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgHelperApp.Migrations
{
    public partial class Initial_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardCompletes_CardProjects_CardProjectid_CardProject_F",
                table: "CardCompletes");

            migrationBuilder.DropForeignKey(
                name: "FK_CardCompletes_Employees_Employeeid_Employee_F",
                table: "CardCompletes");

            migrationBuilder.DropForeignKey(
                name: "FK_CardCompletes_Tasks_Taskid_Task_F",
                table: "CardCompletes");

            migrationBuilder.DropForeignKey(
                name: "FK_CardProjectEmployeeMaps_CardProjects_CardProjectid_CardProject_F",
                table: "CardProjectEmployeeMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_CardProjectEmployeeMaps_Employees_Employeeid_Employee_F",
                table: "CardProjectEmployeeMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskCardProjectMaps_CardProjects_CardProjectid_CardProject_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskCardProjectMaps_Employees_Employeeid_Employee_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskCardProjectMaps_Tasks_Taskid_Task_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTaskCardProjectMaps_CardProjectid_CardProject_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTaskCardProjectMaps_Employeeid_Employee_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTaskCardProjectMaps_Taskid_Task_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropIndex(
                name: "IX_CardProjectEmployeeMaps_CardProjectid_CardProject_F",
                table: "CardProjectEmployeeMaps");

            migrationBuilder.DropIndex(
                name: "IX_CardProjectEmployeeMaps_Employeeid_Employee_F",
                table: "CardProjectEmployeeMaps");

            migrationBuilder.DropIndex(
                name: "IX_CardCompletes_CardProjectid_CardProject_F",
                table: "CardCompletes");

            migrationBuilder.DropIndex(
                name: "IX_CardCompletes_Employeeid_Employee_F",
                table: "CardCompletes");

            migrationBuilder.DropIndex(
                name: "IX_CardCompletes_Taskid_Task_F",
                table: "CardCompletes");

            migrationBuilder.DropColumn(
                name: "CardProjectid_CardProject_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropColumn(
                name: "Employeeid_Employee_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropColumn(
                name: "Taskid_Task_F",
                table: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropColumn(
                name: "CardProjectid_CardProject_F",
                table: "CardProjectEmployeeMaps");

            migrationBuilder.DropColumn(
                name: "Employeeid_Employee_F",
                table: "CardProjectEmployeeMaps");

            migrationBuilder.DropColumn(
                name: "CardProjectid_CardProject_F",
                table: "CardCompletes");

            migrationBuilder.DropColumn(
                name: "Employeeid_Employee_F",
                table: "CardCompletes");

            migrationBuilder.DropColumn(
                name: "Taskid_Task_F",
                table: "CardCompletes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CardProjectid_CardProject_F",
                table: "EmployeeTaskCardProjectMaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Employeeid_Employee_F",
                table: "EmployeeTaskCardProjectMaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Taskid_Task_F",
                table: "EmployeeTaskCardProjectMaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardProjectid_CardProject_F",
                table: "CardProjectEmployeeMaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Employeeid_Employee_F",
                table: "CardProjectEmployeeMaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardProjectid_CardProject_F",
                table: "CardCompletes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Employeeid_Employee_F",
                table: "CardCompletes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Taskid_Task_F",
                table: "CardCompletes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskCardProjectMaps_CardProjectid_CardProject_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "CardProjectid_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskCardProjectMaps_Employeeid_Employee_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "Employeeid_Employee_F");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskCardProjectMaps_Taskid_Task_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "Taskid_Task_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardProjectEmployeeMaps_CardProjectid_CardProject_F",
                table: "CardProjectEmployeeMaps",
                column: "CardProjectid_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardProjectEmployeeMaps_Employeeid_Employee_F",
                table: "CardProjectEmployeeMaps",
                column: "Employeeid_Employee_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardCompletes_CardProjectid_CardProject_F",
                table: "CardCompletes",
                column: "CardProjectid_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardCompletes_Employeeid_Employee_F",
                table: "CardCompletes",
                column: "Employeeid_Employee_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardCompletes_Taskid_Task_F",
                table: "CardCompletes",
                column: "Taskid_Task_F");

            migrationBuilder.AddForeignKey(
                name: "FK_CardCompletes_CardProjects_CardProjectid_CardProject_F",
                table: "CardCompletes",
                column: "CardProjectid_CardProject_F",
                principalTable: "CardProjects",
                principalColumn: "id_CardProject_F",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardCompletes_Employees_Employeeid_Employee_F",
                table: "CardCompletes",
                column: "Employeeid_Employee_F",
                principalTable: "Employees",
                principalColumn: "id_Employee_F",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardCompletes_Tasks_Taskid_Task_F",
                table: "CardCompletes",
                column: "Taskid_Task_F",
                principalTable: "Tasks",
                principalColumn: "id_Task_F",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardProjectEmployeeMaps_CardProjects_CardProjectid_CardProject_F",
                table: "CardProjectEmployeeMaps",
                column: "CardProjectid_CardProject_F",
                principalTable: "CardProjects",
                principalColumn: "id_CardProject_F",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardProjectEmployeeMaps_Employees_Employeeid_Employee_F",
                table: "CardProjectEmployeeMaps",
                column: "Employeeid_Employee_F",
                principalTable: "Employees",
                principalColumn: "id_Employee_F",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskCardProjectMaps_CardProjects_CardProjectid_CardProject_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "CardProjectid_CardProject_F",
                principalTable: "CardProjects",
                principalColumn: "id_CardProject_F",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskCardProjectMaps_Employees_Employeeid_Employee_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "Employeeid_Employee_F",
                principalTable: "Employees",
                principalColumn: "id_Employee_F",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskCardProjectMaps_Tasks_Taskid_Task_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "Taskid_Task_F",
                principalTable: "Tasks",
                principalColumn: "id_Task_F",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
