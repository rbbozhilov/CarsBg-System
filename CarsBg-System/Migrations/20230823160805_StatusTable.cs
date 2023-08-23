using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsBg_System.Migrations
{
    public partial class StatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_StatusId",
                table: "Cars",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Statuses_StatusId",
                table: "Cars",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Statuses_StatusId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Cars_StatusId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Cars");
        }
    }
}
