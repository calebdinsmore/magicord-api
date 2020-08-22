﻿// <auto-generated />
using System;
using Magicord.Models;
using Magicord.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Magicord.Migrations
{
    [DbContext(typeof(MagicordContext))]
    [Migration("20200821000448_AddUserCardTable")]
    partial class AddUserCardTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Npgsql:Enum:dbo.cards_border_color", "black,white,borderless,silver,gold")
                .HasAnnotation("Npgsql:Enum:dbo.cards_frame_version", "2003,1993,2015,1997,future")
                .HasAnnotation("Npgsql:Enum:dbo.cards_layout", "normal,aftermath,split,flip,leveler,saga,vanguard,transform,adventure,meld,scheme,planar,host,augment")
                .HasAnnotation("Npgsql:Enum:dbo.cards_rarity", "rare,uncommon,common,mythic")
                .HasAnnotation("Npgsql:Enum:dbo.foreign_data_language", "German,Spanish,French,Italian,Japanese,Portuguese (Brazil),Russian,Chinese Simplified,Korean,Chinese Traditional,Phyrexian,Sanskrit,Hebrew,Ancient Greek,Latin,Arabic")
                .HasAnnotation("Npgsql:Enum:dbo.legalities_format", "commander,duel,legacy,modern,vintage,pauper,penny,historic,pioneer,brawl,future,standard,oldschool")
                .HasAnnotation("Npgsql:Enum:dbo.legalities_status", "Legal,Banned,Restricted")
                .HasAnnotation("Npgsql:Enum:dbo.set_translations_language", "Chinese Simplified,Chinese Traditional,French,German,Italian,Japanese,Korean,Portuguese (Brazil),Russian,Spanish")
                .HasAnnotation("Npgsql:Enum:dbo.sets_type", "core,masters,expansion,starter,memorabilia,archenemy,box,draft_innovation,commander,funny,duel_deck,from_the_vault,masterpiece,promo,premium_deck,planechase,token,vanguard,treasure_chest,spellbook")
                .HasAnnotation("Npgsql:Enum:dbo.tokens_border_color", "black,borderless,silver,gold")
                .HasAnnotation("Npgsql:Enum:dbo.tokens_layout", "token,double_faced_token,emblem,art_series,normal")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Magicord.Models.Card", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('dbo.cards_id_seq'::regclass)");

                    b.Property<string>("Artist")
                        .HasColumnName("artist")
                        .HasColumnType("text");

                    b.Property<string>("AsciiName")
                        .HasColumnName("ascii_name")
                        .HasColumnType("text");

                    b.Property<string>("Availability")
                        .HasColumnName("availability")
                        .HasColumnType("text");

                    b.Property<string>("CardKingdomFoilId")
                        .HasColumnName("card_kingdom_foil_id")
                        .HasColumnType("text");

                    b.Property<string>("CardKingdomId")
                        .HasColumnName("card_kingdom_id")
                        .HasColumnType("text");

                    b.Property<string>("ColorIdentity")
                        .HasColumnName("color_identity")
                        .HasColumnType("text");

                    b.Property<string>("ColorIndicator")
                        .HasColumnName("color_indicator")
                        .HasColumnType("text");

                    b.Property<string>("Colors")
                        .HasColumnName("colors")
                        .HasColumnType("text");

                    b.Property<double?>("ConvertedManaCost")
                        .HasColumnName("converted_mana_cost")
                        .HasColumnType("double precision");

                    b.Property<string>("DuelDeck")
                        .HasColumnName("duel_deck")
                        .HasColumnType("text");

                    b.Property<long?>("EdhrecRank")
                        .HasColumnName("edhrec_rank")
                        .HasColumnType("bigint");

                    b.Property<double?>("FaceConvertedManaCost")
                        .HasColumnName("face_converted_mana_cost")
                        .HasColumnType("double precision");

                    b.Property<string>("FaceName")
                        .HasColumnName("face_name")
                        .HasColumnType("text");

                    b.Property<string>("FlavorName")
                        .HasColumnName("flavor_name")
                        .HasColumnType("text");

                    b.Property<string>("FlavorText")
                        .HasColumnName("flavor_text")
                        .HasColumnType("text");

                    b.Property<string>("FrameEffects")
                        .HasColumnName("frame_effects")
                        .HasColumnType("text");

                    b.Property<string>("Hand")
                        .HasColumnName("hand")
                        .HasColumnType("text");

                    b.Property<bool>("HasAlternativeDeckLimit")
                        .HasColumnName("has_alternative_deck_limit")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasContentWarning")
                        .HasColumnName("has_content_warning")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasFoil")
                        .HasColumnName("has_foil")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasNonFoil")
                        .HasColumnName("has_non_foil")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAlternative")
                        .HasColumnName("is_alternative")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFullArt")
                        .HasColumnName("is_full_art")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOnlineOnly")
                        .HasColumnName("is_online_only")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOversized")
                        .HasColumnName("is_oversized")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPromo")
                        .HasColumnName("is_promo")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReprint")
                        .HasColumnName("is_reprint")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReserved")
                        .HasColumnName("is_reserved")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStarter")
                        .HasColumnName("is_starter")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStorySpotlight")
                        .HasColumnName("is_story_spotlight")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsTextless")
                        .HasColumnName("is_textless")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsTimeshifted")
                        .HasColumnName("is_timeshifted")
                        .HasColumnType("boolean");

                    b.Property<string>("Keywords")
                        .HasColumnName("keywords")
                        .HasColumnType("text");

                    b.Property<string>("LeadershipSkills")
                        .HasColumnName("leadership_skills")
                        .HasColumnType("text");

                    b.Property<string>("Life")
                        .HasColumnName("life")
                        .HasColumnType("text");

                    b.Property<string>("Loyalty")
                        .HasColumnName("loyalty")
                        .HasColumnType("text");

                    b.Property<string>("ManaCost")
                        .HasColumnName("mana_cost")
                        .HasColumnType("text");

                    b.Property<string>("McmId")
                        .HasColumnName("mcm_id")
                        .HasColumnType("text");

                    b.Property<string>("McmMetaId")
                        .HasColumnName("mcm_meta_id")
                        .HasColumnType("text");

                    b.Property<string>("MtgArenaId")
                        .HasColumnName("mtg_arena_id")
                        .HasColumnType("text");

                    b.Property<string>("MtgjsonV4Id")
                        .HasColumnName("mtgjson_v4_id")
                        .HasColumnType("text");

                    b.Property<string>("MtgoFoilId")
                        .HasColumnName("mtgo_foil_id")
                        .HasColumnType("text");

                    b.Property<string>("MtgoId")
                        .HasColumnName("mtgo_id")
                        .HasColumnType("text");

                    b.Property<string>("MultiverseId")
                        .HasColumnName("multiverse_id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .HasColumnName("number")
                        .HasColumnType("text");

                    b.Property<string>("OriginalText")
                        .HasColumnName("original_text")
                        .HasColumnType("text");

                    b.Property<string>("OriginalType")
                        .HasColumnName("original_type")
                        .HasColumnType("text");

                    b.Property<string>("OtherFaceIds")
                        .HasColumnName("other_face_ids")
                        .HasColumnType("text");

                    b.Property<string>("Power")
                        .HasColumnName("power")
                        .HasColumnType("text");

                    b.Property<string>("Printings")
                        .HasColumnName("printings")
                        .HasColumnType("text");

                    b.Property<string>("PromoTypes")
                        .HasColumnName("promo_types")
                        .HasColumnType("text");

                    b.Property<string>("PurchaseUrls")
                        .HasColumnName("purchase_urls")
                        .HasColumnType("text");

                    b.Property<CardsRarity>("Rarity")
                        .HasColumnName("rarity")
                        .HasColumnType("cards_rarity");

                    b.Property<string>("ScryfallId")
                        .HasColumnName("scryfall_id")
                        .HasColumnType("text");

                    b.Property<string>("ScryfallIllustrationId")
                        .HasColumnName("scryfall_illustration_id")
                        .HasColumnType("text");

                    b.Property<string>("ScryfallOracleId")
                        .HasColumnName("scryfall_oracle_id")
                        .HasColumnType("text");

                    b.Property<string>("SetCode")
                        .IsRequired()
                        .HasColumnName("set_code")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<string>("Side")
                        .HasColumnName("side")
                        .HasColumnType("text");

                    b.Property<string>("Subtypes")
                        .HasColumnName("subtypes")
                        .HasColumnType("text");

                    b.Property<string>("Supertypes")
                        .HasColumnName("supertypes")
                        .HasColumnType("text");

                    b.Property<string>("TcgplayerProductId")
                        .HasColumnName("tcgplayer_product_id")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnName("text")
                        .HasColumnType("text");

                    b.Property<string>("Toughness")
                        .HasColumnName("toughness")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnName("type")
                        .HasColumnType("text");

                    b.Property<string>("Types")
                        .HasColumnName("types")
                        .HasColumnType("text");

                    b.Property<string>("Uuid")
                        .IsRequired()
                        .HasColumnName("uuid")
                        .HasColumnType("character(36)")
                        .IsFixedLength(true)
                        .HasMaxLength(36);

                    b.Property<string>("Variations")
                        .HasColumnName("variations")
                        .HasColumnType("text");

                    b.Property<string>("Watermark")
                        .HasColumnName("watermark")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_cards");

                    b.HasIndex("SetCode")
                        .HasName("idx_24799_set_code");

                    b.HasIndex("Uuid")
                        .IsUnique()
                        .HasName("idx_24799_uuid");

                    b.ToTable("card","dbo");
                });

            modelBuilder.Entity("Magicord.Models.ForeignData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FlavorText")
                        .HasColumnName("flavor_text")
                        .HasColumnType("text");

                    b.Property<long?>("Multiverseid")
                        .HasColumnName("multiverseid")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnName("text")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnName("type")
                        .HasColumnType("text");

                    b.Property<string>("Uuid")
                        .IsRequired()
                        .HasColumnName("uuid")
                        .HasColumnType("character(36)")
                        .IsFixedLength(true)
                        .HasMaxLength(36);

                    b.HasKey("Id")
                        .HasName("pk_foreign_datas");

                    b.HasIndex("Uuid")
                        .HasName("idx_24823_uuid");

                    b.ToTable("foreign_data","dbo");
                });

            modelBuilder.Entity("Magicord.Models.Legality", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('dbo.legalities_id_seq'::regclass)");

                    b.Property<string>("Uuid")
                        .IsRequired()
                        .HasColumnName("uuid")
                        .HasColumnType("character(36)")
                        .IsFixedLength(true)
                        .HasMaxLength(36);

                    b.HasKey("Id")
                        .HasName("pk_legalities");

                    b.HasIndex("Uuid")
                        .HasName("idx_24832_uuid");

                    b.ToTable("legality","dbo");
                });

            modelBuilder.Entity("Magicord.Models.Meta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("date");

                    b.Property<string>("Version")
                        .HasColumnName("version")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_metas");

                    b.ToTable("meta","dbo");
                });

            modelBuilder.Entity("Magicord.Models.Ruling", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('dbo.rulings_id_seq'::regclass)");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("date");

                    b.Property<string>("Text")
                        .HasColumnName("text")
                        .HasColumnType("text");

                    b.Property<string>("Uuid")
                        .IsRequired()
                        .HasColumnName("uuid")
                        .HasColumnType("character(36)")
                        .IsFixedLength(true)
                        .HasMaxLength(36);

                    b.HasKey("Id")
                        .HasName("pk_rulings");

                    b.HasIndex("Uuid")
                        .HasName("idx_24847_uuid");

                    b.ToTable("ruling","dbo");
                });

            modelBuilder.Entity("Magicord.Models.Set", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('dbo.sets_id_seq'::regclass)");

                    b.Property<long?>("BaseSetSize")
                        .HasColumnName("base_set_size")
                        .HasColumnType("bigint");

                    b.Property<string>("Block")
                        .HasColumnName("block")
                        .HasColumnType("text");

                    b.Property<string>("Booster")
                        .HasColumnName("booster")
                        .HasColumnType("jsonb");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<bool>("IsFoilOnly")
                        .HasColumnName("is_foil_only")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsForeignOnly")
                        .HasColumnName("is_foreign_only")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsNonFoilOnly")
                        .HasColumnName("is_non_foil_only")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOnlineOnly")
                        .HasColumnName("is_online_only")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPartialPreview")
                        .HasColumnName("is_partial_preview")
                        .HasColumnType("boolean");

                    b.Property<string>("KeyruneCode")
                        .HasColumnName("keyrune_code")
                        .HasColumnType("text");

                    b.Property<long?>("McmId")
                        .HasColumnName("mcm_id")
                        .HasColumnType("bigint");

                    b.Property<string>("McmName")
                        .HasColumnName("mcm_name")
                        .HasColumnType("text");

                    b.Property<string>("MtgoCode")
                        .HasColumnName("mtgo_code")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("ParentCode")
                        .HasColumnName("parent_code")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnName("release_date")
                        .HasColumnType("date");

                    b.Property<long?>("TcgplayerGroupId")
                        .HasColumnName("tcgplayer_group_id")
                        .HasColumnType("bigint");

                    b.Property<long?>("TotalSetSize")
                        .HasColumnName("total_set_size")
                        .HasColumnType("bigint");

                    b.Property<SetsType>("Type")
                        .HasColumnName("type")
                        .HasColumnType("sets_type");

                    b.HasKey("Id")
                        .HasName("pk_sets");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasName("idx_24856_code");

                    b.ToTable("set","dbo");
                });

            modelBuilder.Entity("Magicord.Models.SetTranslation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('dbo.set_translations_id_seq'::regclass)");

                    b.Property<string>("SetCode")
                        .IsRequired()
                        .HasColumnName("set_code")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<string>("Translation")
                        .HasColumnName("translation")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_set_translations");

                    b.HasIndex("SetCode")
                        .HasName("idx_24870_set_code");

                    b.ToTable("set_translation","dbo");
                });

            modelBuilder.Entity("Magicord.Models.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('dbo.tokens_id_seq'::regclass)");

                    b.Property<string>("Artist")
                        .HasColumnName("artist")
                        .HasColumnType("text");

                    b.Property<string>("AsciiName")
                        .HasColumnName("ascii_name")
                        .HasColumnType("text");

                    b.Property<string>("Availability")
                        .HasColumnName("availability")
                        .HasColumnType("text");

                    b.Property<string>("ColorIdentity")
                        .HasColumnName("color_identity")
                        .HasColumnType("text");

                    b.Property<string>("Colors")
                        .HasColumnName("colors")
                        .HasColumnType("text");

                    b.Property<long?>("EdhrecRank")
                        .HasColumnName("edhrec_rank")
                        .HasColumnType("bigint");

                    b.Property<string>("FaceName")
                        .HasColumnName("face_name")
                        .HasColumnType("text");

                    b.Property<string>("FlavorText")
                        .HasColumnName("flavor_text")
                        .HasColumnType("text");

                    b.Property<string>("FrameEffects")
                        .HasColumnName("frame_effects")
                        .HasColumnType("text");

                    b.Property<string>("FrameVersion")
                        .HasColumnName("frame_version")
                        .HasColumnType("text");

                    b.Property<bool>("HasFoil")
                        .HasColumnName("has_foil")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasNonFoil")
                        .HasColumnName("has_non_foil")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFullArt")
                        .HasColumnName("is_full_art")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPromo")
                        .HasColumnName("is_promo")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReprint")
                        .HasColumnName("is_reprint")
                        .HasColumnType("boolean");

                    b.Property<string>("Keywords")
                        .HasColumnName("keywords")
                        .HasColumnType("text");

                    b.Property<string>("MtgArenaId")
                        .HasColumnName("mtg_arena_id")
                        .HasColumnType("text");

                    b.Property<string>("MtgjsonV4Id")
                        .HasColumnName("mtgjson_v4_id")
                        .HasColumnType("text");

                    b.Property<string>("MultiverseId")
                        .HasColumnName("multiverse_id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .HasColumnName("number")
                        .HasColumnType("text");

                    b.Property<string>("OriginalText")
                        .HasColumnName("original_text")
                        .HasColumnType("text");

                    b.Property<string>("OriginalType")
                        .HasColumnName("original_type")
                        .HasColumnType("text");

                    b.Property<string>("Power")
                        .HasColumnName("power")
                        .HasColumnType("text");

                    b.Property<string>("PromoTypes")
                        .HasColumnName("promo_types")
                        .HasColumnType("text");

                    b.Property<string>("ReverseRelated")
                        .HasColumnName("reverse_related")
                        .HasColumnType("text");

                    b.Property<string>("ScryfallId")
                        .HasColumnName("scryfall_id")
                        .HasColumnType("text");

                    b.Property<string>("ScryfallIllustrationId")
                        .HasColumnName("scryfall_illustration_id")
                        .HasColumnType("text");

                    b.Property<string>("ScryfallOracleId")
                        .HasColumnName("scryfall_oracle_id")
                        .HasColumnType("text");

                    b.Property<string>("SetCode")
                        .IsRequired()
                        .HasColumnName("set_code")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<string>("Side")
                        .HasColumnName("side")
                        .HasColumnType("text");

                    b.Property<string>("Subtypes")
                        .HasColumnName("subtypes")
                        .HasColumnType("text");

                    b.Property<string>("Supertypes")
                        .HasColumnName("supertypes")
                        .HasColumnType("text");

                    b.Property<string>("TcgplayerProductId")
                        .HasColumnName("tcgplayer_product_id")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnName("text")
                        .HasColumnType("text");

                    b.Property<string>("Toughness")
                        .HasColumnName("toughness")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnName("type")
                        .HasColumnType("text");

                    b.Property<string>("Types")
                        .HasColumnName("types")
                        .HasColumnType("text");

                    b.Property<string>("Uuid")
                        .IsRequired()
                        .HasColumnName("uuid")
                        .HasColumnType("character(36)")
                        .IsFixedLength(true)
                        .HasMaxLength(36);

                    b.Property<string>("Watermark")
                        .HasColumnName("watermark")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_tokens");

                    b.HasIndex("SetCode")
                        .HasName("idx_24879_set_code");

                    b.ToTable("token","dbo");
                });

            modelBuilder.Entity("Magicord.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnName("balance")
                        .HasColumnType("numeric");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Magicord.Models.UserCard", b =>
                {
                    b.Property<long>("CardId")
                        .HasColumnName("card_id")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("CardId", "UserId")
                        .HasName("pk_user_card");

                    b.HasIndex("UserId")
                        .HasName("ix_user_card_user_id");

                    b.ToTable("user_card");
                });

            modelBuilder.Entity("Magicord.Models.Card", b =>
                {
                    b.HasOne("Magicord.Models.Set", "SetCodeNavigation")
                        .WithMany("Cards")
                        .HasForeignKey("SetCode")
                        .HasConstraintName("cards_ibfk_1")
                        .HasPrincipalKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Magicord.Models.ForeignData", b =>
                {
                    b.HasOne("Magicord.Models.Card", "Uu")
                        .WithMany("ForeignDatas")
                        .HasForeignKey("Uuid")
                        .HasConstraintName("foreign_data_ibfk_1")
                        .HasPrincipalKey("Uuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Magicord.Models.Legality", b =>
                {
                    b.HasOne("Magicord.Models.Card", "Uu")
                        .WithMany("Legalities")
                        .HasForeignKey("Uuid")
                        .HasConstraintName("legalities_ibfk_1")
                        .HasPrincipalKey("Uuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Magicord.Models.Ruling", b =>
                {
                    b.HasOne("Magicord.Models.Card", "Uu")
                        .WithMany("Rulings")
                        .HasForeignKey("Uuid")
                        .HasConstraintName("rulings_ibfk_1")
                        .HasPrincipalKey("Uuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Magicord.Models.SetTranslation", b =>
                {
                    b.HasOne("Magicord.Models.Set", "SetCodeNavigation")
                        .WithMany("SetTranslations")
                        .HasForeignKey("SetCode")
                        .HasConstraintName("set_translations_ibfk_1")
                        .HasPrincipalKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Magicord.Models.Token", b =>
                {
                    b.HasOne("Magicord.Models.Set", "SetCodeNavigation")
                        .WithMany("Tokens")
                        .HasForeignKey("SetCode")
                        .HasConstraintName("tokens_ibfk_1")
                        .HasPrincipalKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Magicord.Models.UserCard", b =>
                {
                    b.HasOne("Magicord.Models.Card", "Card")
                        .WithMany("UserCards")
                        .HasForeignKey("CardId")
                        .HasConstraintName("fk_user_card_cards_card_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Magicord.Models.User", "User")
                        .WithMany("UserCards")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_card_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
