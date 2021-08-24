using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Magicord.Migrations
{
    public partial class AddPriceHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "card_price_history",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    card_uuid = table.Column<string>(type: "character(36)", nullable: true),
                    buylist_foil = table.Column<decimal>(type: "numeric", nullable: false),
                    buylist_non_foil = table.Column<decimal>(type: "numeric", nullable: false),
                    retail_foil = table.Column<decimal>(type: "numeric", nullable: false),
                    retail_non_foil = table.Column<decimal>(type: "numeric", nullable: false),
                    date_recorded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    card_price_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_card_price_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_card_price_history_card_prices_card_price_id",
                        column: x => x.card_price_id,
                        principalSchema: "dbo",
                        principalTable: "card_price",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_card_price_history_cards_card_id",
                        column: x => x.card_uuid,
                        principalSchema: "dbo",
                        principalTable: "card",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_card_price_history_card_price_id",
                schema: "dbo",
                table: "card_price_history",
                column: "card_price_id");

            migrationBuilder.CreateIndex(
                name: "ix_card_price_history_card_uuid",
                schema: "dbo",
                table: "card_price_history",
                column: "card_uuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card_price_history",
                schema: "dbo");
        }
    }
}
