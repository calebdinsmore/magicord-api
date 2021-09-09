using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Magicord.Migrations
{
    public partial class AddStockMarket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_price_history_card_prices_card_price_id",
                schema: "dbo",
                table: "card_price_history");

            migrationBuilder.DropForeignKey(
                name: "fk_card_price_history_cards_card_id",
                schema: "dbo",
                table: "card_price_history");

            migrationBuilder.CreateTable(
                name: "user_share",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    card_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    cash_invested = table.Column<decimal>(type: "numeric", nullable: false),
                    is_foil = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_share", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_share_card_card_id",
                        column: x => x.card_id,
                        principalSchema: "dbo",
                        principalTable: "card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_share_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_short",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    card_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    reserved_cash = table.Column<decimal>(type: "numeric", nullable: false),
                    is_foil = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_short", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_short_card_card_id",
                        column: x => x.card_id,
                        principalSchema: "dbo",
                        principalTable: "card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_short_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_share_card_id",
                schema: "dbo",
                table: "user_share",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_share_user_id",
                schema: "dbo",
                table: "user_share",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_short_card_id",
                schema: "dbo",
                table: "user_short",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_short_user_id",
                schema: "dbo",
                table: "user_short",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_card_price_history_card_card_id",
                schema: "dbo",
                table: "card_price_history",
                column: "card_uuid",
                principalSchema: "dbo",
                principalTable: "card",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_card_price_history_card_price_card_price_id",
                schema: "dbo",
                table: "card_price_history",
                column: "card_price_id",
                principalSchema: "dbo",
                principalTable: "card_price",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_card_price_history_card_card_id",
                schema: "dbo",
                table: "card_price_history");

            migrationBuilder.DropForeignKey(
                name: "fk_card_price_history_card_price_card_price_id",
                schema: "dbo",
                table: "card_price_history");

            migrationBuilder.DropTable(
                name: "user_share",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user_short",
                schema: "dbo");

            migrationBuilder.AddForeignKey(
                name: "fk_card_price_history_card_prices_card_price_id",
                schema: "dbo",
                table: "card_price_history",
                column: "card_price_id",
                principalSchema: "dbo",
                principalTable: "card_price",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_card_price_history_cards_card_id",
                schema: "dbo",
                table: "card_price_history",
                column: "card_uuid",
                principalSchema: "dbo",
                principalTable: "card",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
