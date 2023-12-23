using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurrance.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccidentsTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carschussis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentsTemp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarDetailsTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cartype = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    carschussis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    ACost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Minstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDetailsTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarDetailsTemp_Customers_CustomerNumber",
                        column: x => x.CustomerNumber,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarDetailsTemp_InsuranceTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "InsuranceTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarFirstChecksTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carschussis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarFirstChecksTemp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomersTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Nat = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneN1 = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    PhoneN2 = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomersTemp_CustomerStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "CustomerStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersTemp_Nationalites_Nat",
                        column: x => x.Nat,
                        principalTable: "Nationalites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarDetailsTemp_CustomerNumber",
                table: "CarDetailsTemp",
                column: "CustomerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CarDetailsTemp_TypeID",
                table: "CarDetailsTemp",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersTemp_Nat",
                table: "CustomersTemp",
                column: "Nat");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersTemp_StatusID",
                table: "CustomersTemp",
                column: "StatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentsTemp");

            migrationBuilder.DropTable(
                name: "CarDetailsTemp");

            migrationBuilder.DropTable(
                name: "CarFirstChecksTemp");

            migrationBuilder.DropTable(
                name: "CustomersTemp");
        }
    }
}
