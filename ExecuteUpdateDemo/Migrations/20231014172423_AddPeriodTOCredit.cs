using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExecuteUpdateDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddPeriodTOCredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Period_EndDate",
                table: "Credits",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Period_StartDate",
                table: "Credits",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period_EndDate",
                table: "Credits");

            migrationBuilder.DropColumn(
                name: "Period_StartDate",
                table: "Credits");
        }
    }
}
