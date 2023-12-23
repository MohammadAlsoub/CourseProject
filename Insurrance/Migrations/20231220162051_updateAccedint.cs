using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurrance.Migrations
{
    /// <inheritdoc />
    public partial class updateAccedint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Accidents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Accidents",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Accidents");
        }
    }
}
