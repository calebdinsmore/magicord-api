using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Magicord.Migrations
{
    public partial class AddBoosterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "store_booster_listing",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    set_code = table.Column<string>(nullable: false),
                    booster_type = table.Column<string>(nullable: true, defaultValue: "default"),
                    retail_price = table.Column<decimal>(nullable: false),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_store_booster_listings", x => x.id);
                    table.ForeignKey(
                        name: "fk_store_booster_listings_sets_set_id",
                        column: x => x.set_code,
                        principalSchema: "dbo",
                        principalTable: "set",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_booster",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(nullable: false),
                    set_code = table.Column<string>(nullable: true),
                    buy_price = table.Column<decimal>(nullable: false),
                    is_opened = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_boosters", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_boosters_sets_set_id",
                        column: x => x.set_code,
                        principalSchema: "dbo",
                        principalTable: "set",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_boosters_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_booster_card",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_booster_id = table.Column<long>(nullable: false),
                    card_uuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_booster_card", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_booster_card_cards_card_id",
                        column: x => x.card_uuid,
                        principalSchema: "dbo",
                        principalTable: "card",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_booster_card_user_boosters_user_booster_id",
                        column: x => x.user_booster_id,
                        principalSchema: "dbo",
                        principalTable: "user_booster",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_store_booster_listing_set_code",
                schema: "dbo",
                table: "store_booster_listing",
                column: "set_code");

            migrationBuilder.CreateIndex(
                name: "ix_user_booster_set_code",
                schema: "dbo",
                table: "user_booster",
                column: "set_code");

            migrationBuilder.CreateIndex(
                name: "ix_user_boosters_user_id",
                schema: "dbo",
                table: "user_booster",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_booster_card_card_uuid",
                schema: "dbo",
                table: "user_booster_card",
                column: "card_uuid");

            migrationBuilder.CreateIndex(
                name: "ix_user_booster_card_user_booster_id",
                schema: "dbo",
                table: "user_booster_card",
                column: "user_booster_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "store_booster_listing",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user_booster_card",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user_booster",
                schema: "dbo");
        }
    }
}
