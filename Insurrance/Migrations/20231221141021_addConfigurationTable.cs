using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurrance.Migrations
{
    /// <inheritdoc />
    public partial class addConfigurationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Send = table.Column<bool>(type: "bit", nullable: false),
                    Wellcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedCar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Update = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accident = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    compensation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");
        }
    }
}
