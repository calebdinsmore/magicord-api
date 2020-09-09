using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
    public partial class UpdateUserCardToUseUuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_card_cards_card_id",
                schema: "dbo",
                table: "user_card");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_card",
                schema: "dbo",
                table: "user_card");

            migrationBuilder.DropColumn(
                name: "card_id",
                schema: "dbo",
                table: "user_card");

            migrationBuilder.AddColumn<string>(
                name: "card_uuid",
                schema: "dbo",
                table: "user_card",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_card",
                schema: "dbo",
                table: "user_card",
                columns: new[] { "card_uuid", "user_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_user_card_cards_card_id",
                schema: "dbo",
                table: "user_card",
                column: "card_uuid",
                principalSchema: "dbo",
                principalTable: "card",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_card_cards_card_id",
                schema: "dbo",
                table: "user_card");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_card",
                schema: "dbo",
                table: "user_card");

            migrationBuilder.DropColumn(
                name: "card_uuid",
                schema: "dbo",
                table: "user_card");

            migrationBuilder.AddColumn<long>(
                name: "card_id",
                schema: "dbo",
                table: "user_card",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_card",
                schema: "dbo",
                table: "user_card",
                columns: new[] { "card_id", "user_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_user_card_cards_card_id",
                schema: "dbo",
                table: "user_card",
                column: "card_id",
                principalSchema: "dbo",
                principalTable: "card",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
