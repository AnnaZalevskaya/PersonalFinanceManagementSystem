using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Accounts.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.CreateTable(
                name: "currency",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    abbreviation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "financial_account_type",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_account_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "financial_account",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    financial_account_type_id = table.Column<int>(type: "integer", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_account", x => x.id);
                    table.ForeignKey(
                        name: "account_account_type_id_fkey",
                        column: x => x.financial_account_type_id,
                        principalSchema: "accounts",
                        principalTable: "financial_account_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "financial_account_currency_id_fkey",
                        column: x => x.currency_id,
                        principalSchema: "accounts",
                        principalTable: "currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_currency_id",
                schema: "accounts",
                table: "currency",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_financial_account_currency_id",
                schema: "accounts",
                table: "financial_account",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_financial_account_financial_account_type_id",
                schema: "accounts",
                table: "financial_account",
                column: "financial_account_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_financial_account_id",
                schema: "accounts",
                table: "financial_account",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_financial_account_type_id",
                schema: "accounts",
                table: "financial_account_type",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "financial_account",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "financial_account_type",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "currency",
                schema: "accounts");
        }
    }
}
