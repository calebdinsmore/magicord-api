using Magicord.Models.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
  public partial class AddUserCardTable : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "user_card",
          schema: "dbo",
          columns: table => new
          {
            user_id = table.Column<long>(nullable: false),
            card_id = table.Column<long>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_user_card", x => new { x.card_id, x.user_id });
            table.ForeignKey(
                      name: "fk_user_card_cards_card_id",
                      column: x => x.card_id,
                      principalSchema: "dbo",
                      principalTable: "card",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "fk_user_card_users_user_id",
                      column: x => x.user_id,
                      principalSchema: "dbo",
                      principalTable: "user",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "ix_user_card_user_id",
          schema: "dbo",
          table: "user_card",
          column: "user_id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "user_card",
          schema: "dbo");
    }
  }
}
