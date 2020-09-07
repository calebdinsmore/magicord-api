using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Magicord.Migrations
{
  public partial class AddCardPriceTable : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "card_price",
          schema: "dbo",
          columns: table => new
          {
            id = table.Column<long>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            card_uuid = table.Column<string>(nullable: false),
            current_buylist_foil = table.Column<decimal>(nullable: false),
            current_buylist_non_foil = table.Column<decimal>(nullable: false),
            current_retail_foil = table.Column<decimal>(nullable: false),
            current_retail_non_foil = table.Column<decimal>(nullable: false),
            buylist_foil_history = table.Column<Dictionary<DateTime, decimal>>(type: "jsonb", nullable: true),
            buylist_non_foil_history = table.Column<Dictionary<DateTime, decimal>>(type: "jsonb", nullable: true),
            retail_foil_history = table.Column<Dictionary<DateTime, decimal>>(type: "jsonb", nullable: true),
            retail_non_foil_history = table.Column<Dictionary<DateTime, decimal>>(type: "jsonb", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_card_prices", x => x.id);
            table.ForeignKey(
                      name: "fk_card_prices_cards_card_uuid",
                      column: x => x.card_uuid,
                      principalSchema: "dbo",
                      principalTable: "card",
                      principalColumn: "uuid",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "ix_card_price_card_uuid",
          schema: "dbo",
          table: "card_price",
          column: "card_uuid",
          unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "card_price",
          schema: "dbo");
    }
  }
}
