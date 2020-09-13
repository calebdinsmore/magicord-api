using System;
using System.Configuration;
using Magicord.Models.Enums;
using Magicord.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;

namespace Magicord.Models
{
  public partial class MagicordContext : DbContext
  {
    static MagicordContext()
    {
      NpgsqlConnection.GlobalTypeMapper.MapEnum<SetsType>();
      NpgsqlConnection.GlobalTypeMapper.MapEnum<CardsRarity>();
      NpgsqlConnection.GlobalTypeMapper.MapEnum<CardsFrameVersion>();
      NpgsqlConnection.GlobalTypeMapper.MapEnum<CardsBorderColor>();
    }

    public MagicordContext()
    {
    }

    public MagicordContext(DbContextOptions<MagicordContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }
    public virtual DbSet<ForeignData> ForeignDatas { get; set; }
    public virtual DbSet<Legality> Legalities { get; set; }
    public virtual DbSet<Meta> Metas { get; set; }
    public virtual DbSet<Ruling> Rulings { get; set; }
    public virtual DbSet<Set> Sets { get; set; }
    public virtual DbSet<SetTranslation> SetTranslations { get; set; }
    public virtual DbSet<Token> Tokens { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<CardPrice> CardPrices { get; set; }
    public virtual DbSet<UserBooster> UserBoosters { get; set; }
    public virtual DbSet<StoreBoosterListing> StoreBoosterListings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSnakeCaseNamingConvention();
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("dbo");

      modelBuilder.RemovePluralizingTableNameConvention();

      modelBuilder.HasPostgresEnum("dbo", "cards_border_color", new[] { "black", "white", "borderless", "silver", "gold" })
          .HasPostgresEnum("dbo", "cards_frame_version", new[] { "2003", "1993", "2015", "1997", "future" })
          .HasPostgresEnum("dbo", "cards_layout", new[] { "normal", "aftermath", "split", "flip", "leveler", "saga", "vanguard", "transform", "adventure", "meld", "scheme", "planar", "host", "augment" })
          .HasPostgresEnum("dbo", "cards_rarity", new[] { "rare", "uncommon", "common", "mythic" })
          .HasPostgresEnum("dbo", "foreign_data_language", new[] { "German", "Spanish", "French", "Italian", "Japanese", "Portuguese (Brazil)", "Russian", "Chinese Simplified", "Korean", "Chinese Traditional", "Phyrexian", "Sanskrit", "Hebrew", "Ancient Greek", "Latin", "Arabic" })
          .HasPostgresEnum("dbo", "legalities_format", new[] { "commander", "duel", "legacy", "modern", "vintage", "pauper", "penny", "historic", "pioneer", "brawl", "future", "standard", "oldschool" })
          .HasPostgresEnum("dbo", "legalities_status", new[] { "Legal", "Banned", "Restricted" })
          .HasPostgresEnum("dbo", "set_translations_language", new[] { "Chinese Simplified", "Chinese Traditional", "French", "German", "Italian", "Japanese", "Korean", "Portuguese (Brazil)", "Russian", "Spanish" })
          .HasPostgresEnum("dbo", "sets_type", new[] { "core", "masters", "expansion", "starter", "memorabilia", "archenemy", "box", "draft_innovation", "commander", "funny", "duel_deck", "from_the_vault", "masterpiece", "promo", "premium_deck", "planechase", "token", "vanguard", "treasure_chest", "spellbook" })
          .HasPostgresEnum("dbo", "tokens_border_color", new[] { "black", "borderless", "silver", "gold" })
          .HasPostgresEnum("dbo", "tokens_layout", new[] { "token", "double_faced_token", "emblem", "art_series", "normal" });

      modelBuilder.Entity<Card>(entity =>
      {
        entity.ToTable("card", "dbo");

        entity.HasIndex(e => e.SetCode)
                  .HasName("idx_24799_set_code");

        entity.HasIndex(e => e.Uuid)
                  .HasName("idx_24799_uuid")
                  .IsUnique();

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("nextval('dbo.cards_id_seq'::regclass)");

        entity.Property(e => e.Artist).HasColumnName("artist");

        entity.Property(e => e.AsciiName).HasColumnName("ascii_name");

        entity.Property(e => e.Availability).HasColumnName("availability");

        entity.Property(e => e.CardKingdomFoilId).HasColumnName("card_kingdom_foil_id");

        entity.Property(e => e.CardKingdomId).HasColumnName("card_kingdom_id");

        entity.Property(e => e.ColorIdentity).HasColumnName("color_identity");

        entity.Property(e => e.ColorIndicator).HasColumnName("color_indicator");

        entity.Property(e => e.Colors).HasColumnName("colors");

        entity.Property(e => e.ConvertedManaCost).HasColumnName("converted_mana_cost");

        entity.Property(e => e.DuelDeck).HasColumnName("duel_deck");

        entity.Property(e => e.EdhrecRank).HasColumnName("edhrec_rank");

        entity.Property(e => e.FaceConvertedManaCost).HasColumnName("face_converted_mana_cost");

        entity.Property(e => e.FaceName).HasColumnName("face_name");

        entity.Property(e => e.FlavorName).HasColumnName("flavor_name");

        entity.Property(e => e.FlavorText).HasColumnName("flavor_text");

        entity.Property(e => e.FrameEffects).HasColumnName("frame_effects");

        entity.Property(e => e.Hand).HasColumnName("hand");

        entity.Property(e => e.HasAlternativeDeckLimit).HasColumnName("has_alternative_deck_limit");

        entity.Property(e => e.HasContentWarning).HasColumnName("has_content_warning");

        entity.Property(e => e.HasFoil).HasColumnName("has_foil");

        entity.Property(e => e.HasNonFoil).HasColumnName("has_non_foil");

        entity.Property(e => e.IsAlternative).HasColumnName("is_alternative");

        entity.Property(e => e.IsFullArt).HasColumnName("is_full_art");

        entity.Property(e => e.IsOnlineOnly).HasColumnName("is_online_only");

        entity.Property(e => e.IsOversized).HasColumnName("is_oversized");

        entity.Property(e => e.IsPromo).HasColumnName("is_promo");

        entity.Property(e => e.IsReprint).HasColumnName("is_reprint");

        entity.Property(e => e.IsReserved).HasColumnName("is_reserved");

        entity.Property(e => e.IsStarter).HasColumnName("is_starter");

        entity.Property(e => e.IsStorySpotlight).HasColumnName("is_story_spotlight");

        entity.Property(e => e.IsTextless).HasColumnName("is_textless");

        entity.Property(e => e.IsTimeshifted).HasColumnName("is_timeshifted");

        entity.Property(e => e.Keywords).HasColumnName("keywords");

        entity.Property(e => e.LeadershipSkills).HasColumnName("leadership_skills");

        entity.Property(e => e.Life).HasColumnName("life");

        entity.Property(e => e.Loyalty).HasColumnName("loyalty");

        entity.Property(e => e.ManaCost).HasColumnName("mana_cost");

        entity.Property(e => e.McmId).HasColumnName("mcm_id");

        entity.Property(e => e.McmMetaId).HasColumnName("mcm_meta_id");

        entity.Property(e => e.MtgArenaId).HasColumnName("mtg_arena_id");

        entity.Property(e => e.MtgjsonV4Id).HasColumnName("mtgjson_v4_id");

        entity.Property(e => e.MtgoFoilId).HasColumnName("mtgo_foil_id");

        entity.Property(e => e.MtgoId).HasColumnName("mtgo_id");

        entity.Property(e => e.MultiverseId).HasColumnName("multiverse_id");

        entity.Property(e => e.Name).HasColumnName("name");

        entity.Property(e => e.Number).HasColumnName("number");

        entity.Property(e => e.OriginalText).HasColumnName("original_text");

        entity.Property(e => e.OriginalType).HasColumnName("original_type");

        entity.Property(e => e.OtherFaceIds).HasColumnName("other_face_ids");

        entity.Property(e => e.Power).HasColumnName("power");

        entity.Property(e => e.Printings).HasColumnName("printings");

        entity.Property(e => e.PromoTypes).HasColumnName("promo_types");

        entity.Property(e => e.PurchaseUrls).HasColumnName("purchase_urls");

        entity.Property(e => e.ScryfallId).HasColumnName("scryfall_id");

        entity.Property(e => e.ScryfallIllustrationId).HasColumnName("scryfall_illustration_id");

        entity.Property(e => e.ScryfallOracleId).HasColumnName("scryfall_oracle_id");

        entity.Property(e => e.SetCode)
                  .IsRequired()
                  .HasColumnName("set_code")
                  .HasMaxLength(8);

        entity.Property(e => e.Side).HasColumnName("side");

        entity.Property(e => e.Subtypes).HasColumnName("subtypes");

        entity.Property(e => e.Supertypes).HasColumnName("supertypes");

        entity.Property(e => e.TcgplayerProductId).HasColumnName("tcgplayer_product_id");

        entity.Property(e => e.Text).HasColumnName("text");

        entity.Property(e => e.Toughness).HasColumnName("toughness");

        entity.Property(e => e.Type).HasColumnName("type");

        entity.Property(e => e.Types).HasColumnName("types");

        entity.Property(e => e.Uuid)
                  .IsRequired()
                  .HasColumnName("uuid")
                  .HasMaxLength(36)
                  .IsFixedLength();

        entity.Property(e => e.Variations).HasColumnName("variations");

        entity.Property(e => e.Watermark).HasColumnName("watermark");

        entity.HasOne(d => d.SetCodeNavigation)
                  .WithMany(p => p.Cards)
                  .HasPrincipalKey(p => p.Code)
                  .HasForeignKey(d => d.SetCode)
                  .HasConstraintName("cards_ibfk_1");

        entity.HasOne(c => c.CardPrice)
              .WithOne(cp => cp.Card)
              .HasPrincipalKey<Card>(c => c.Uuid)
              .HasForeignKey<CardPrice>(cp => cp.CardUuid)
              .IsRequired();
      });

      modelBuilder.Entity<CardPrice>()
        .HasIndex(cp => cp.CardUuid)
        .IsUnique();

      modelBuilder.Entity<ForeignData>(entity =>
      {
        entity.ToTable("foreign_data", "dbo");

        entity.HasIndex(e => e.Uuid)
                  .HasName("idx_24823_uuid");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.FlavorText).HasColumnName("flavor_text");

        entity.Property(e => e.Multiverseid).HasColumnName("multiverseid");

        entity.Property(e => e.Name).HasColumnName("name");

        entity.Property(e => e.Text).HasColumnName("text");

        entity.Property(e => e.Type).HasColumnName("type");

        entity.Property(e => e.Uuid)
                  .IsRequired()
                  .HasColumnName("uuid")
                  .HasMaxLength(36)
                  .IsFixedLength();

        entity.HasOne(d => d.Uu)
                  .WithMany(p => p.ForeignDatas)
                  .HasPrincipalKey(p => p.Uuid)
                  .HasForeignKey(d => d.Uuid)
                  .HasConstraintName("foreign_data_ibfk_1");
      });

      modelBuilder.Entity<Legality>(entity =>
      {
        entity.ToTable("legality", "dbo");

        entity.HasIndex(e => e.Uuid)
                  .HasName("idx_24832_uuid");

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("nextval('dbo.legalities_id_seq'::regclass)");

        entity.Property(e => e.Uuid)
                  .IsRequired()
                  .HasColumnName("uuid")
                  .HasMaxLength(36)
                  .IsFixedLength();

        entity.HasOne(d => d.Uu)
                  .WithMany(p => p.Legalities)
                  .HasPrincipalKey(p => p.Uuid)
                  .HasForeignKey(d => d.Uuid)
                  .HasConstraintName("legalities_ibfk_1");
      });

      modelBuilder.Entity<Meta>(entity =>
      {
        entity.ToTable("meta", "dbo");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Date)
                  .HasColumnName("date")
                  .HasColumnType("date");

        entity.Property(e => e.Version).HasColumnName("version");
      });

      modelBuilder.Entity<Ruling>(entity =>
      {
        entity.ToTable("ruling", "dbo");

        entity.HasIndex(e => e.Uuid)
                  .HasName("idx_24847_uuid");

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("nextval('dbo.rulings_id_seq'::regclass)");

        entity.Property(e => e.Date)
                  .HasColumnName("date")
                  .HasColumnType("date");

        entity.Property(e => e.Text).HasColumnName("text");

        entity.Property(e => e.Uuid)
                  .IsRequired()
                  .HasColumnName("uuid")
                  .HasMaxLength(36)
                  .IsFixedLength();

        entity.HasOne(d => d.Uu)
                  .WithMany(p => p.Rulings)
                  .HasPrincipalKey(p => p.Uuid)
                  .HasForeignKey(d => d.Uuid)
                  .HasConstraintName("rulings_ibfk_1");
      });

      modelBuilder.Entity<Set>(entity =>
      {
        entity.ToTable("set", "dbo");

        entity.HasIndex(e => e.Code)
                  .HasName("idx_24856_code")
                  .IsUnique();

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("nextval('dbo.sets_id_seq'::regclass)");

        entity.Property(e => e.BaseSetSize).HasColumnName("base_set_size");

        entity.Property(e => e.Block).HasColumnName("block");

        entity.Property(e => e.Booster)
                  .HasColumnName("booster")
                  .HasColumnType("jsonb");

        entity.Property(e => e.Code)
                  .IsRequired()
                  .HasColumnName("code")
                  .HasMaxLength(8);

        entity.Property(e => e.IsFoilOnly).HasColumnName("is_foil_only");

        entity.Property(e => e.IsForeignOnly).HasColumnName("is_foreign_only");

        entity.Property(e => e.IsNonFoilOnly).HasColumnName("is_non_foil_only");

        entity.Property(e => e.IsOnlineOnly).HasColumnName("is_online_only");

        entity.Property(e => e.IsPartialPreview).HasColumnName("is_partial_preview");

        entity.Property(e => e.KeyruneCode).HasColumnName("keyrune_code");

        entity.Property(e => e.McmId).HasColumnName("mcm_id");

        entity.Property(e => e.McmName).HasColumnName("mcm_name");

        entity.Property(e => e.MtgoCode).HasColumnName("mtgo_code");

        entity.Property(e => e.Name).HasColumnName("name");

        entity.Property(e => e.ParentCode).HasColumnName("parent_code");

        entity.Property(e => e.ReleaseDate)
                  .HasColumnName("release_date")
                  .HasColumnType("date");

        entity.Property(e => e.TcgplayerGroupId).HasColumnName("tcgplayer_group_id");

        entity.Property(e => e.TotalSetSize).HasColumnName("total_set_size");
      });

      modelBuilder.Entity<SetTranslation>(entity =>
      {
        entity.ToTable("set_translation", "dbo");

        entity.HasIndex(e => e.SetCode)
                  .HasName("idx_24870_set_code");

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("nextval('dbo.set_translations_id_seq'::regclass)");

        entity.Property(e => e.SetCode)
                  .IsRequired()
                  .HasColumnName("set_code")
                  .HasMaxLength(8);

        entity.Property(e => e.Translation).HasColumnName("translation");

        entity.HasOne(d => d.SetCodeNavigation)
                  .WithMany(p => p.SetTranslations)
                  .HasPrincipalKey(p => p.Code)
                  .HasForeignKey(d => d.SetCode)
                  .HasConstraintName("set_translations_ibfk_1");
      });

      modelBuilder.Entity<Token>(entity =>
      {
        entity.ToTable("token", "dbo");

        entity.HasIndex(e => e.SetCode)
                  .HasName("idx_24879_set_code");

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("nextval('dbo.tokens_id_seq'::regclass)");

        entity.Property(e => e.Artist).HasColumnName("artist");

        entity.Property(e => e.AsciiName).HasColumnName("ascii_name");

        entity.Property(e => e.Availability).HasColumnName("availability");

        entity.Property(e => e.ColorIdentity).HasColumnName("color_identity");

        entity.Property(e => e.Colors).HasColumnName("colors");

        entity.Property(e => e.EdhrecRank).HasColumnName("edhrec_rank");

        entity.Property(e => e.FaceName).HasColumnName("face_name");

        entity.Property(e => e.FlavorText).HasColumnName("flavor_text");

        entity.Property(e => e.FrameEffects).HasColumnName("frame_effects");

        entity.Property(e => e.FrameVersion).HasColumnName("frame_version");

        entity.Property(e => e.HasFoil).HasColumnName("has_foil");

        entity.Property(e => e.HasNonFoil).HasColumnName("has_non_foil");

        entity.Property(e => e.IsFullArt).HasColumnName("is_full_art");

        entity.Property(e => e.IsPromo).HasColumnName("is_promo");

        entity.Property(e => e.IsReprint).HasColumnName("is_reprint");

        entity.Property(e => e.Keywords).HasColumnName("keywords");

        entity.Property(e => e.MtgArenaId).HasColumnName("mtg_arena_id");

        entity.Property(e => e.MtgjsonV4Id).HasColumnName("mtgjson_v4_id");

        entity.Property(e => e.MultiverseId).HasColumnName("multiverse_id");

        entity.Property(e => e.Name).HasColumnName("name");

        entity.Property(e => e.Number).HasColumnName("number");

        entity.Property(e => e.OriginalText).HasColumnName("original_text");

        entity.Property(e => e.OriginalType).HasColumnName("original_type");

        entity.Property(e => e.Power).HasColumnName("power");

        entity.Property(e => e.PromoTypes).HasColumnName("promo_types");

        entity.Property(e => e.ReverseRelated).HasColumnName("reverse_related");

        entity.Property(e => e.ScryfallId).HasColumnName("scryfall_id");

        entity.Property(e => e.ScryfallIllustrationId).HasColumnName("scryfall_illustration_id");

        entity.Property(e => e.ScryfallOracleId).HasColumnName("scryfall_oracle_id");

        entity.Property(e => e.SetCode)
                  .IsRequired()
                  .HasColumnName("set_code")
                  .HasMaxLength(8);

        entity.Property(e => e.Side).HasColumnName("side");

        entity.Property(e => e.Subtypes).HasColumnName("subtypes");

        entity.Property(e => e.Supertypes).HasColumnName("supertypes");

        entity.Property(e => e.TcgplayerProductId).HasColumnName("tcgplayer_product_id");

        entity.Property(e => e.Text).HasColumnName("text");

        entity.Property(e => e.Toughness).HasColumnName("toughness");

        entity.Property(e => e.Type).HasColumnName("type");

        entity.Property(e => e.Types).HasColumnName("types");

        entity.Property(e => e.Uuid)
                  .IsRequired()
                  .HasColumnName("uuid")
                  .HasMaxLength(36)
                  .IsFixedLength();

        entity.Property(e => e.Watermark).HasColumnName("watermark");

        entity.HasOne(d => d.SetCodeNavigation)
                  .WithMany(p => p.Tokens)
                  .HasPrincipalKey(p => p.Code)
                  .HasForeignKey(d => d.SetCode)
                  .HasConstraintName("tokens_ibfk_1");
      });

      modelBuilder.Entity<UserCard>()
        .HasKey(uc => new { uc.CardUuid, uc.UserId });
      modelBuilder.Entity<UserCard>()
          .HasOne(uc => uc.Card)
          .WithMany(c => c.UserCards)
          .HasPrincipalKey(c => c.Uuid)
          .HasForeignKey(uc => uc.CardUuid);
      modelBuilder.Entity<UserCard>()
          .HasOne(uc => uc.User)
          .WithMany(u => u.UserCards)
          .HasForeignKey(uc => uc.UserId);

      modelBuilder.Entity<UserBooster>()
        .HasOne(ub => ub.User)
        .WithMany(u => u.UserBoosters)
        .HasForeignKey(ub => ub.UserId);
      modelBuilder.Entity<UserBooster>()
        .HasOne(ub => ub.Set)
        .WithMany(b => b.UserBoosters)
        .HasPrincipalKey(s => s.Code)
        .HasForeignKey(ub => ub.SetCode);

      modelBuilder.Entity<UserBoosterCard>(entity =>
      {
        entity.HasOne(bc => bc.Card)
              .WithMany(c => c.UserBoosterCards)
              .HasPrincipalKey(c => c.Uuid)
              .HasForeignKey(bc => bc.CardUuid);

        entity.HasOne(bc => bc.UserBooster)
              .WithMany(b => b.UserBoosterCards)
              .HasForeignKey(bc => bc.UserBoosterId);
      });

      modelBuilder.Entity<StoreBoosterListing>(entity =>
      {
        entity.HasOne(sbl => sbl.Set)
          .WithMany(s => s.StoreBoosterListings)
          .HasPrincipalKey(s => s.Code)
          .HasForeignKey(sbl => sbl.SetCode);

        entity.Property(sbl => sbl.BoosterType).HasDefaultValue("default");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
