using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurrance.Migrations
{
    /// <inheritdoc />
    public partial class updateCarsTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<int>(
                name: "CustomerNumber",
                table: "CarDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarDetails_CustomerNumber",
                table: "CarDetails",
                column: "CustomerNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDetails_Customers_CustomerNumber",
                table: "CarDetails",
                column: "CustomerNumber",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDetails_Customers_CustomerNumber",
                table: "CarDetails");

            migrationBuilder.DropIndex(
                name: "IX_CarDetails_CustomerNumber",
                table: "CarDetails");

            migrationBuilder.DropColumn(
                name: "CustomerNumber",
                table: "CarDetails");

          
        }
    }
}
