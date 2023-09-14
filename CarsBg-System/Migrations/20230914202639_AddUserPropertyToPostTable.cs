using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsBg_System.Migrations
{
    public partial class AddUserPropertyToPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Posts");
        }
    }
}
