using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
    public partial class AddAmountNonFoilField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "amount_non_foil",
                schema: "dbo",
                table: "user_card",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount_non_foil",
                schema: "dbo",
                table: "user_card");
        }
    }
}
