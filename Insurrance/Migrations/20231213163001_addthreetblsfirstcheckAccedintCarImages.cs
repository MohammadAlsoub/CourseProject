using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurrance.Migrations
{
    /// <inheritdoc />
    public partial class addthreetblsfirstcheckAccedintCarImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carschussis = table.Column<int>(type: "int", nullable: false),
                    FL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accidents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarFirstChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carschussis = table.Column<int>(type: "int", nullable: false),
                    FL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarFirstChecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carschussis = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accidents");

            migrationBuilder.DropTable(
                name: "CarFirstChecks");

            migrationBuilder.DropTable(
                name: "CarImages");
        }
    }
}
