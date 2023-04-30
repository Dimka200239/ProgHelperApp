using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgHelperApp.Migrations
{
    public partial class Initial_Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardCompletes",
                columns: table => new
                {
                    id_Employee_F = table.Column<Guid>(nullable: false),
                    id_Task_F = table.Column<Guid>(nullable: false),
                    id_CardProject_F = table.Column<Guid>(nullable: false),
                    DateOfEnding_F = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCompletes", x => new { x.id_Employee_F, x.id_Task_F, x.id_CardProject_F });
                    table.ForeignKey(
                        name: "FK_CardCompletes_CardProjects_id_CardProject_F",
                        column: x => x.id_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardCompletes_Employees_id_Employee_F",
                        column: x => x.id_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardCompletes_Tasks_id_Task_F",
                        column: x => x.id_Task_F,
                        principalTable: "Tasks",
                        principalColumn: "id_Task_F",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardProjectEmployeeMaps",
                columns: table => new
                {
                    id_CardProject_F = table.Column<Guid>(nullable: false),
                    id_Employee_F = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardProjectEmployeeMaps", x => new { x.id_CardProject_F, x.id_Employee_F });
                    table.ForeignKey(
                        name: "FK_CardProjectEmployeeMaps_CardProjects_id_CardProject_F",
                        column: x => x.id_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardProjectEmployeeMaps_Employees_id_Employee_F",
                        column: x => x.id_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTaskCardProjectMaps",
                columns: table => new
                {
                    id_Employee_F = table.Column<Guid>(nullable: false),
                    id_Task_F = table.Column<Guid>(nullable: false),
                    id_CardProject_F = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTaskCardProjectMaps", x => new { x.id_Employee_F, x.id_Task_F, x.id_CardProject_F });
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_CardProjects_id_CardProject_F",
                        column: x => x.id_CardProject_F,
                        principalTable: "CardProjects",
                        principalColumn: "id_CardProject_F",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_Employees_id_Employee_F",
                        column: x => x.id_Employee_F,
                        principalTable: "Employees",
                        principalColumn: "id_Employee_F",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTaskCardProjectMaps_Tasks_id_Task_F",
                        column: x => x.id_Task_F,
                        principalTable: "Tasks",
                        principalColumn: "id_Task_F",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskCardProjectMaps",
                columns: table => new
                {
                    id_Task_F = table.Column<Guid>(nullable: false),
                    id_CardProject_F = table.Column<Guid>(nullable: false)
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
                name: "IX_CardCompletes_id_CardProject_F",
                table: "CardCompletes",
                column: "id_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardCompletes_id_Task_F",
                table: "CardCompletes",
                column: "id_Task_F");

            migrationBuilder.CreateIndex(
                name: "IX_CardProjectEmployeeMaps_id_Employee_F",
                table: "CardProjectEmployeeMaps",
                column: "id_Employee_F");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskCardProjectMaps_id_CardProject_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "id_CardProject_F");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTaskCardProjectMaps_id_Task_F",
                table: "EmployeeTaskCardProjectMaps",
                column: "id_Task_F");

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
        }
    }
}
