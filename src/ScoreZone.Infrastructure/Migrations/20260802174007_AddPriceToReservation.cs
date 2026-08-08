using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PricePerMatch",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeeEntityFootballCourtEntity",
                columns: table => new
                {
                    EmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FootballCourtsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEntityFootballCourtEntity", x => new { x.EmployeesId, x.FootballCourtsId });
                    table.ForeignKey(
                        name: "FK_EmployeeEntityFootballCourtEntity_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEntityFootballCourtEntity_FootballCourts_FootballCourtsId",
                        column: x => x.FootballCourtsId,
                        principalTable: "FootballCourts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEntityFootballCourtEntity_FootballCourtsId",
                table: "EmployeeEntityFootballCourtEntity",
                column: "FootballCourtsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEntityFootballCourtEntity");

            migrationBuilder.DropColumn(
                name: "PricePerMatch",
                table: "Reservations");
        }
    }
}
