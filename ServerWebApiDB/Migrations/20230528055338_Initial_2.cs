using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerWebApiDB.Migrations
{
    public partial class Initial_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description_F",
                table: "CardCompletes",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_F",
                table: "CardCompletes");
        }
    }
}
