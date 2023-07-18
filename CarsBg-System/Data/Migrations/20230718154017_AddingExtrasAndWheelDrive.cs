using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsBg_System.Data.Migrations
{
    public partial class AddingExtrasAndWheelDrive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cars",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WheelDriveId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WheelDrive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WheelDrive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarExtra",
                columns: table => new
                {
                    CarsId = table.Column<int>(type: "int", nullable: false),
                    ExtrasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarExtra", x => new { x.CarsId, x.ExtrasId });
                    table.ForeignKey(
                        name: "FK_CarExtra_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarExtra_Extras_ExtrasId",
                        column: x => x.ExtrasId,
                        principalTable: "Extras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_WheelDriveId",
                table: "Cars",
                column: "WheelDriveId");

            migrationBuilder.CreateIndex(
                name: "IX_CarExtra_ExtrasId",
                table: "CarExtra",
                column: "ExtrasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_WheelDrive_WheelDriveId",
                table: "Cars",
                column: "WheelDriveId",
                principalTable: "WheelDrive",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_WheelDrive_WheelDriveId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarExtra");

            migrationBuilder.DropTable(
                name: "WheelDrive");

            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DropIndex(
                name: "IX_Cars_WheelDriveId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "WheelDriveId",
                table: "Cars");
        }
    }
}
