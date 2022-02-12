using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magicord.Migrations
{
    public partial class AddAlchemyToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date_recorded",
                schema: "dbo",
                table: "card_price_history",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
            migrationBuilder.Sql("ALTER TYPE dbo.legalities_format ADD VALUE 'alchemy'", suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date_recorded",
                schema: "dbo",
                table: "card_price_history",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
            migrationBuilder.Sql("ALTER TYPE dbo.legalities_format REMOVE VALUE 'alchemy';", suppressTransaction: true);
        }
    }
}
