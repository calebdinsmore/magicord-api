using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
    public partial class ChangePackDistribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "has_been_given_packs",
                schema: "dbo",
                table: "sealed_event_attendee",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "dbo",
                table: "sealed_event",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "has_been_given_packs",
                schema: "dbo",
                table: "sealed_event_attendee");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "dbo",
                table: "sealed_event");
        }
    }
}
