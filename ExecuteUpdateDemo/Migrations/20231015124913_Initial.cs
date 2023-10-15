using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExecuteUpdateDemo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Declarations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Declarations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    DeclarationReference = table.Column<string>(type: "text", nullable: false),
                    Period_StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Period_EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeclarationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credits_Declarations_DeclarationId",
                        column: x => x.DeclarationId,
                        principalTable: "Declarations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contestations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    DeclarationReference = table.Column<string>(type: "text", nullable: false),
                    CreditReference = table.Column<string>(type: "text", nullable: false),
                    CreditId = table.Column<long>(type: "bigint", nullable: true),
                    DeclarationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contestations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contestations_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contestations_Declarations_DeclarationId",
                        column: x => x.DeclarationId,
                        principalTable: "Declarations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contestations_CreditId_CreditReference",
                table: "Contestations",
                columns: new[] { "CreditId", "CreditReference" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contestations_DeclarationId_DeclarationReference",
                table: "Contestations",
                columns: new[] { "DeclarationId", "DeclarationReference" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contestations_Reference",
                table: "Contestations",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credits_DeclarationId_DeclarationReference",
                table: "Credits",
                columns: new[] { "DeclarationId", "DeclarationReference" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credits_Reference",
                table: "Credits",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Declarations_Reference",
                table: "Declarations",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contestations");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Declarations");
        }
    }
}
