using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Magicord.Migrations
{
    public partial class AddSealedEventEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sealed_event",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    entry_fee = table.Column<decimal>(type: "numeric", nullable: false),
                    packs_are_distributed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sealed_event", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sealed_event_attendee",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    sealed_event_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sealed_event_attendee", x => new { x.user_id, x.sealed_event_id });
                    table.ForeignKey(
                        name: "fk_sealed_event_attendee_sealed_events_sealed_event_id",
                        column: x => x.sealed_event_id,
                        principalSchema: "dbo",
                        principalTable: "sealed_event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sealed_event_attendee_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sealed_event_pack",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    set_code = table.Column<string>(type: "character varying(8)", nullable: true),
                    booster_type = table.Column<string>(type: "text", nullable: true),
                    pack_count = table.Column<int>(type: "integer", nullable: false),
                    sealed_event_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sealed_event_pack", x => x.id);
                    table.ForeignKey(
                        name: "fk_sealed_event_pack_sealed_events_sealed_event_id",
                        column: x => x.sealed_event_id,
                        principalSchema: "dbo",
                        principalTable: "sealed_event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sealed_event_pack_sets_set_id",
                        column: x => x.set_code,
                        principalSchema: "dbo",
                        principalTable: "set",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_sealed_event_attendee_sealed_event_id",
                schema: "dbo",
                table: "sealed_event_attendee",
                column: "sealed_event_id");

            migrationBuilder.CreateIndex(
                name: "ix_sealed_event_pack_sealed_event_id",
                schema: "dbo",
                table: "sealed_event_pack",
                column: "sealed_event_id");

            migrationBuilder.CreateIndex(
                name: "ix_sealed_event_pack_set_code",
                schema: "dbo",
                table: "sealed_event_pack",
                column: "set_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sealed_event_attendee",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "sealed_event_pack",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "sealed_event",
                schema: "dbo");
        }
    }
}
