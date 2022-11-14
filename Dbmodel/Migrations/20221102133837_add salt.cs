using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dbmodel.Migrations
{
    public partial class addsalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "RegisteredUsers",
                type: "nvarchar(1000)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "RegisteredUsers");
        }
    }
}
