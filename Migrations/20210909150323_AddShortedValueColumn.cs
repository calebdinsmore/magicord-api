using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
    public partial class AddShortedValueColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "shorted_value",
                schema: "dbo",
                table: "user_short",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shorted_value",
                schema: "dbo",
                table: "user_short");
        }
    }
}
