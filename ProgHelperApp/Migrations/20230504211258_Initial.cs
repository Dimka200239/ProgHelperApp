using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgHelperApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    id_Employee_F = table.Column<Guid>(nullable: false),
                    Name_F = table.Column<string>(maxLength: 50, nullable: false),
                    SerName_F = table.Column<string>(maxLength: 50, nullable: false),
                    Patronymic_F = table.Column<string>(maxLength: 50, nullable: false),
                    Login_F = table.Column<string>(maxLength: 50, nullable: false),
                    Password_F = table.Column<string>(maxLength: 50, nullable: false),
                    Phone_F = table.Column<string>(nullable: false),
                    Email_F = table.Column<string>(nullable: false),
                    Position_F = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.id_Employee_F);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    id_Task_F = table.Column<Guid>(nullable: false),
                    Title_F = table.Column<string>(maxLength: 50, nullable: false),
                    Description_F = table.Column<string>(maxLength: 50, nullable: false),
                    Deadline_F = table.Column<DateTime>(nullable: false),
                    Status_F = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.id_Task_F);
                });

            migrationBuilder.CreateTable(
                name: "CardProjects",
                columns: table => new
                {
                    id_CardProject_F = table.Column<Guid>(nullable: false),
                    Title_F = table.Column<string>(maxLength: 50, nullable: false),
                    Description_F = table.Column<string>(maxLength: 1000, nullable: false),
                    DateOfBegining_F = table.Column<string>(nullable: false),
                    Status_F = table.Column<string>(nullable: false),
                    id_Employee_F = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardProjects", x => x.id_CardProject_F);
                    table.ForeignKey(
                        name: "FK_CardProjects_Employees_id_Employee_F",
                        column: x => x.id_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardCompletes",
                columns: table => new
                {
                    id_CardComplete_F = table.Column<Guid>(nullable: false),
                    DateOfEnding_F = table.Column<DateTime>(nullable: false),
                    id_CardProject_F = table.Column<Guid>(nullable: false),
                    id_Task_F = table.Column<Guid>(nullable: false),
                    id_Employee_F = table.Column<Guid>(nullable: false),
                    CardProjectid_CardProject_F = table.Column<Guid>(nullable: true),
                    Employeeid_Employee_F = table.Column<Guid>(nullable: true),
                    Taskid_Task_F = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCompletes", x => x.id_CardComplete_F);
                    table.ForeignKey(
                        name: "FK_CardCompletes_CardProjects_CardProjectid_CardProject_F",
                        column: x => x.CardProjectid_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardCompletes_Employees_Employeeid_Employee_F",
                        column: x => x.Employeeid_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardCompletes_Tasks_Taskid_Task_F",
                        column: x => x.Taskid_Task_F,
                        principalTable: "Tasks",
                        principalColumn: "id_Task_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardCompletes_CardProjects_id_CardProject_F",
                        column: x => x.id_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardCompletes_Employees_id_Employee_F",
                        column: x => x.id_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardCompletes_Tasks_id_Task_F",
                        column: x => x.id_Task_F,
                        principalTable: "Tasks",
                        principalColumn: "id_Task_F",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardProjectEmployeeMaps",
                columns: table => new
                {
                    id_CardProject_F = table.Column<Guid>(nullable: false),
                    id_Employee_F = table.Column<Guid>(nullable: false),
                    CardProjectid_CardProject_F = table.Column<Guid>(nullable: true),
                    Employeeid_Employee_F = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardProjectEmployeeMaps", x => new { x.id_Employee_F, x.id_CardProject_F });
                    table.ForeignKey(
                        name: "FK_CardProjectEmployeeMaps_CardProjects_CardProjectid_CardProject_F",
                        column: x => x.CardProjectid_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardProjectEmployeeMaps_Employees_Employeeid_Employee_F",
                        column: x => x.Employeeid_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardProjectEmployeeMaps_CardProjects_id_CardProject_F",
                        column: x => x.id_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardProjectEmployeeMaps_Employees_id_Employee_F",
                        column: x => x.id_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTaskCardProjectMaps",
                columns: table => new
                {
                    id_CardProject_F = table.Column<Guid>(nullable: false),
                    id_Task_F = table.Column<Guid>(nullable: false),
                    id_Employee_F = table.Column<Guid>(nullable: false),
                    CardProjectid_CardProject_F = table.Column<Guid>(nullable: true),
                    Employeeid_Employee_F = table.Column<Guid>(nullable: true),
                    Taskid_Task_F = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTaskCardProjectMaps", x => new { x.id_Task_F, x.id_CardProject_F, x.id_Employee_F });
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_CardProjects_CardProjectid_CardProject_F",
                        column: x => x.CardProjectid_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_Employees_Employeeid_Employee_F",
                        column: x => x.Employeeid_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_Tasks_Taskid_Task_F",
                        column: x => x.Taskid_Task_F,
                        principalTable: "Tasks",
                        principalColumn: "id_Task_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_CardProjects_id_CardProject_F",
                        column: x => x.id_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_Employees_id_Employee_F",
                        column: x => x.id_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_Tasks_id_Task_F",
                        column: x => x.id_Task_F,
                        principalTable: "Tasks",
                        principalColumn: "id_Task_F",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskCardProjectMaps",
                columns: table => new
                {
                    id_CardProject_F = table.Column<Guid>(nullable: false),
                    id_Task_F = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCardProjectMaps", x => new { x.id_Task_F, x.id_CardProject_F });
                    table.ForeignKey(
                        name: "FK_TaskCardProjectMaps_CardProjects_id_CardProject_F",
                        column: x => x.id_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskCardProjectMaps_Tasks_id_Task_F",
                        column: x => x.id_Task_F,
                        principalTable: "Tasks",
                        principalColumn: "id_Task_F",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CardCompletes_id_CardProject_F",
                table: "CardCompletes",
                column: "id_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardCompletes_id_Employee_F",
                table: "CardCompletes",
                column: "id_Employee_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardCompletes_id_Task_F",
                table: "CardCompletes",
                column: "id_Task_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardProjectEmployeeMaps_CardProjectid_CardProject_F",
                table: "CardProjectEmployeeMaps",
                column: "CardProjectid_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardProjectEmployeeMaps_Employeeid_Employee_F",
                table: "CardProjectEmployeeMaps",
                column: "Employeeid_Employee_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardProjectEmployeeMaps_id_CardProject_F",
                table: "CardProjectEmployeeMaps",
                column: "id_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardProjects_id_Employee_F",
                table: "CardProjects",
                column: "id_Employee_F");

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
                name: "IX_EmployeeTaskCardProjectMaps_id_CardProject_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "id_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskCardProjectMaps_id_Employee_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "id_Employee_F");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCardProjectMaps_id_CardProject_F",
                table: "TaskCardProjectMaps",
                column: "id_CardProject_F");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardCompletes");

            migrationBuilder.DropTable(
                name: "CardProjectEmployeeMaps");

            migrationBuilder.DropTable(
                name: "EmployeeTaskCardProjectMaps");

            migrationBuilder.DropTable(
                name: "TaskCardProjectMaps");

            migrationBuilder.DropTable(
                name: "CardProjects");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
