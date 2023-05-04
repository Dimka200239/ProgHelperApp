using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgHelperApp.Migrations
{
    public partial class Initial_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCardProjectMaps_CardProjects_id_CardProject_F",
                table: "TaskCardProjectMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskCardProjectMaps_Tasks_id_Task_F",
                table: "TaskCardProjectMaps");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCardProjectMaps_CardProjects_id_CardProject_F",
                table: "TaskCardProjectMaps",
                column: "id_CardProject_F",
                principalTable: "CardProjects",
                principalColumn: "id_CardProject_F",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCardProjectMaps_Tasks_id_Task_F",
                table: "TaskCardProjectMaps",
                column: "id_Task_F",
                principalTable: "Tasks",
                principalColumn: "id_Task_F",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCardProjectMaps_CardProjects_id_CardProject_F",
                table: "TaskCardProjectMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskCardProjectMaps_Tasks_id_Task_F",
                table: "TaskCardProjectMaps");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCardProjectMaps_CardProjects_id_CardProject_F",
                table: "TaskCardProjectMaps",
                column: "id_CardProject_F",
                principalTable: "CardProjects",
                principalColumn: "id_CardProject_F",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCardProjectMaps_Tasks_id_Task_F",
                table: "TaskCardProjectMaps",
                column: "id_Task_F",
                principalTable: "Tasks",
                principalColumn: "id_Task_F",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
