using Microsoft.EntityFrameworkCore.Migrations;

namespace Magicord.Migrations
{
  public partial class AddNewRarities : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("ALTER TYPE dbo.cards_rarity ADD VALUE 'special'", suppressTransaction: true);
      migrationBuilder.Sql("ALTER TYPE dbo.cards_rarity ADD VALUE 'bonus'", suppressTransaction: true);
      // migrationBuilder.AlterDatabase()
      //     .Annotation("Npgsql:Enum:dbo.cards_border_color", "black,white,borderless,silver,gold")
      //     .Annotation("Npgsql:Enum:dbo.cards_frame_version", "2003,1993,2015,1997,future")
      //     .Annotation("Npgsql:Enum:dbo.cards_layout", "normal,aftermath,split,flip,leveler,saga,vanguard,transform,adventure,meld,scheme,planar,host,augment")
      //     .Annotation("Npgsql:Enum:dbo.cards_rarity", "rare,uncommon,common,mythic,special,bonus")
      //     .Annotation("Npgsql:Enum:dbo.foreign_data_language", "German,Spanish,French,Italian,Japanese,Portuguese (Brazil),Russian,Chinese Simplified,Korean,Chinese Traditional,Phyrexian,Sanskrit,Hebrew,Ancient Greek,Latin,Arabic")
      //     .Annotation("Npgsql:Enum:dbo.legalities_format", "commander,duel,legacy,modern,vintage,pauper,penny,historic,pioneer,brawl,future,standard,oldschool")
      //     .Annotation("Npgsql:Enum:dbo.legalities_status", "Legal,Banned,Restricted")
      //     .Annotation("Npgsql:Enum:dbo.set_translations_language", "Chinese Simplified,Chinese Traditional,French,German,Italian,Japanese,Korean,Portuguese (Brazil),Russian,Spanish")
      //     .Annotation("Npgsql:Enum:dbo.sets_type", "core,masters,expansion,starter,memorabilia,archenemy,box,draft_innovation,commander,funny,duel_deck,from_the_vault,masterpiece,promo,premium_deck,planechase,token,vanguard,treasure_chest,spellbook")
      //     .Annotation("Npgsql:Enum:dbo.tokens_border_color", "black,borderless,silver,gold")
      //     .Annotation("Npgsql:Enum:dbo.tokens_layout", "token,double_faced_token,emblem,art_series,normal")
      //     .OldAnnotation("Npgsql:Enum:dbo.cards_border_color", "black,white,borderless,silver,gold")
      //     .OldAnnotation("Npgsql:Enum:dbo.cards_frame_version", "2003,1993,2015,1997,future")
      //     .OldAnnotation("Npgsql:Enum:dbo.cards_layout", "normal,aftermath,split,flip,leveler,saga,vanguard,transform,adventure,meld,scheme,planar,host,augment")
      //     .OldAnnotation("Npgsql:Enum:dbo.cards_rarity", "rare,uncommon,common,mythic")
      //     .OldAnnotation("Npgsql:Enum:dbo.foreign_data_language", "German,Spanish,French,Italian,Japanese,Portuguese (Brazil),Russian,Chinese Simplified,Korean,Chinese Traditional,Phyrexian,Sanskrit,Hebrew,Ancient Greek,Latin,Arabic")
      //     .OldAnnotation("Npgsql:Enum:dbo.legalities_format", "commander,duel,legacy,modern,vintage,pauper,penny,historic,pioneer,brawl,future,standard,oldschool")
      //     .OldAnnotation("Npgsql:Enum:dbo.legalities_status", "Legal,Banned,Restricted")
      //     .OldAnnotation("Npgsql:Enum:dbo.set_translations_language", "Chinese Simplified,Chinese Traditional,French,German,Italian,Japanese,Korean,Portuguese (Brazil),Russian,Spanish")
      //     .OldAnnotation("Npgsql:Enum:dbo.sets_type", "core,masters,expansion,starter,memorabilia,archenemy,box,draft_innovation,commander,funny,duel_deck,from_the_vault,masterpiece,promo,premium_deck,planechase,token,vanguard,treasure_chest,spellbook")
      //     .OldAnnotation("Npgsql:Enum:dbo.tokens_border_color", "black,borderless,silver,gold")
      //     .OldAnnotation("Npgsql:Enum:dbo.tokens_layout", "token,double_faced_token,emblem,art_series,normal");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("ALTER TYPE dbo.cards_rarity REMOVE VALUE 'special';", suppressTransaction: true);
      migrationBuilder.Sql("ALTER TYPE dbo.cards_rarity REMOVE VALUE 'bonus';", suppressTransaction: true);
      // migrationBuilder.AlterDatabase()
      //     .Annotation("Npgsql:Enum:dbo.cards_border_color", "black,white,borderless,silver,gold")
      //     .Annotation("Npgsql:Enum:dbo.cards_frame_version", "2003,1993,2015,1997,future")
      //     .Annotation("Npgsql:Enum:dbo.cards_layout", "normal,aftermath,split,flip,leveler,saga,vanguard,transform,adventure,meld,scheme,planar,host,augment")
      //     .Annotation("Npgsql:Enum:dbo.cards_rarity", "rare,uncommon,common,mythic")
      //     .Annotation("Npgsql:Enum:dbo.foreign_data_language", "German,Spanish,French,Italian,Japanese,Portuguese (Brazil),Russian,Chinese Simplified,Korean,Chinese Traditional,Phyrexian,Sanskrit,Hebrew,Ancient Greek,Latin,Arabic")
      //     .Annotation("Npgsql:Enum:dbo.legalities_format", "commander,duel,legacy,modern,vintage,pauper,penny,historic,pioneer,brawl,future,standard,oldschool")
      //     .Annotation("Npgsql:Enum:dbo.legalities_status", "Legal,Banned,Restricted")
      //     .Annotation("Npgsql:Enum:dbo.set_translations_language", "Chinese Simplified,Chinese Traditional,French,German,Italian,Japanese,Korean,Portuguese (Brazil),Russian,Spanish")
      //     .Annotation("Npgsql:Enum:dbo.sets_type", "core,masters,expansion,starter,memorabilia,archenemy,box,draft_innovation,commander,funny,duel_deck,from_the_vault,masterpiece,promo,premium_deck,planechase,token,vanguard,treasure_chest,spellbook")
      //     .Annotation("Npgsql:Enum:dbo.tokens_border_color", "black,borderless,silver,gold")
      //     .Annotation("Npgsql:Enum:dbo.tokens_layout", "token,double_faced_token,emblem,art_series,normal")
      //     .OldAnnotation("Npgsql:Enum:dbo.cards_border_color", "black,white,borderless,silver,gold")
      //     .OldAnnotation("Npgsql:Enum:dbo.cards_frame_version", "2003,1993,2015,1997,future")
      //     .OldAnnotation("Npgsql:Enum:dbo.cards_layout", "normal,aftermath,split,flip,leveler,saga,vanguard,transform,adventure,meld,scheme,planar,host,augment")
      //     .OldAnnotation("Npgsql:Enum:dbo.cards_rarity", "rare,uncommon,common,mythic,special,bonus")
      //     .OldAnnotation("Npgsql:Enum:dbo.foreign_data_language", "German,Spanish,French,Italian,Japanese,Portuguese (Brazil),Russian,Chinese Simplified,Korean,Chinese Traditional,Phyrexian,Sanskrit,Hebrew,Ancient Greek,Latin,Arabic")
      //     .OldAnnotation("Npgsql:Enum:dbo.legalities_format", "commander,duel,legacy,modern,vintage,pauper,penny,historic,pioneer,brawl,future,standard,oldschool")
      //     .OldAnnotation("Npgsql:Enum:dbo.legalities_status", "Legal,Banned,Restricted")
      //     .OldAnnotation("Npgsql:Enum:dbo.set_translations_language", "Chinese Simplified,Chinese Traditional,French,German,Italian,Japanese,Korean,Portuguese (Brazil),Russian,Spanish")
      //     .OldAnnotation("Npgsql:Enum:dbo.sets_type", "core,masters,expansion,starter,memorabilia,archenemy,box,draft_innovation,commander,funny,duel_deck,from_the_vault,masterpiece,promo,premium_deck,planechase,token,vanguard,treasure_chest,spellbook")
      //     .OldAnnotation("Npgsql:Enum:dbo.tokens_border_color", "black,borderless,silver,gold")
      //     .OldAnnotation("Npgsql:Enum:dbo.tokens_layout", "token,double_faced_token,emblem,art_series,normal");
    }
  }
}
