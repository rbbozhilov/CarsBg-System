using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsBg_System.Migrations
{
    public partial class RemoveCountPropertyFromReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Reports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
