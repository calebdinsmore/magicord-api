using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
    public partial class AddAverageInvestedValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "average_invested_value",
                schema: "dbo",
                table: "user_share",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "average_invested_value",
                schema: "dbo",
                table: "user_share");
        }
    }
}
