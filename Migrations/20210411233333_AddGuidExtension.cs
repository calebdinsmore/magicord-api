using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
  public partial class AddGuidExtension : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DROP EXTENSION IF EXISTS \"uuid-ossp\";");
    }
  }
}
