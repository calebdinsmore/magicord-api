using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Magicord.Migrations
{
    public partial class AddSealedPromos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sealed_event_promo",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sealed_event_id = table.Column<long>(type: "bigint", nullable: false),
                    promo_type = table.Column<string>(type: "text", nullable: true),
                    set_code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sealed_event_promo", x => x.id);
                    table.ForeignKey(
                        name: "fk_sealed_event_promo_sealed_events_sealed_event_id",
                        column: x => x.sealed_event_id,
                        principalSchema: "dbo",
                        principalTable: "sealed_event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_sealed_event_promo_sealed_event_id",
                schema: "dbo",
                table: "sealed_event_promo",
                column: "sealed_event_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sealed_event_promo",
                schema: "dbo");
        }
    }
}
