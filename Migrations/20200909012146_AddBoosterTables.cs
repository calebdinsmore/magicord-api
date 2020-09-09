using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Magicord.Migrations
{
    public partial class AddBoosterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "booster",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    set_code = table.Column<string>(nullable: true),
                    buy_price = table.Column<decimal>(nullable: false),
                    is_opened = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_boosters", x => x.id);
                    table.ForeignKey(
                        name: "fk_boosters_sets_set_id",
                        column: x => x.set_code,
                        principalSchema: "dbo",
                        principalTable: "set",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "booster_card",
                schema: "dbo",
                columns: table => new
                {
                    booster_id = table.Column<long>(nullable: false),
                    card_uuid = table.Column<string>(nullable: false),
                    id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booster_card", x => new { x.booster_id, x.card_uuid });
                    table.ForeignKey(
                        name: "fk_booster_card_boosters_booster_id",
                        column: x => x.booster_id,
                        principalSchema: "dbo",
                        principalTable: "booster",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_booster_card_cards_card_id",
                        column: x => x.card_uuid,
                        principalSchema: "dbo",
                        principalTable: "card",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_booster",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<long>(nullable: false),
                    booster_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_booster", x => new { x.user_id, x.booster_id });
                    table.ForeignKey(
                        name: "fk_user_booster_boosters_booster_id",
                        column: x => x.booster_id,
                        principalSchema: "dbo",
                        principalTable: "booster",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_booster_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_booster_set_code",
                schema: "dbo",
                table: "booster",
                column: "set_code");

            migrationBuilder.CreateIndex(
                name: "ix_booster_card_card_uuid",
                schema: "dbo",
                table: "booster_card",
                column: "card_uuid");

            migrationBuilder.CreateIndex(
                name: "ix_user_booster_booster_id",
                schema: "dbo",
                table: "user_booster",
                column: "booster_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booster_card",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user_booster",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "booster",
                schema: "dbo");
        }
    }
}
