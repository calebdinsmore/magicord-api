using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
    public partial class AddUserCardXmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "dbo",
                table: "user_card",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "dbo",
                table: "user_card");
        }
    }
}
