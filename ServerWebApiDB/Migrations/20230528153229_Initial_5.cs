using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerWebApiDB.Migrations
{
    public partial class Initial_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateOfEnding_F",
                table: "CardCompletes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfEnding_F",
                table: "CardCompletes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
