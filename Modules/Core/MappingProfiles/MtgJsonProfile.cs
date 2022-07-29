using System.Collections.Generic;
using AutoMapper;
using Magicord.Models;
using Magicord.Modules.AdminProcess;
using Newtonsoft.Json;

namespace Magicord.Modules.Core.MappingProfiles
{
  public class MtgJsonProfile : Profile
  {
    public MtgJsonProfile()
    {
      CreateMap<SetJson, Set>()
        .ForMember(dest => dest.Booster, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Booster)));

      CreateMap<CardJson, Card>()
        .ForMember(dest => dest.Availability, opt => opt.MapFrom(src => string.Join(',', src.Availability)))
        .ForMember(dest => dest.ColorIdentity, opt => opt.MapFrom(src => string.Join(',', src.ColorIdentity)))
        .ForMember(dest => dest.ColorIndicator, opt => opt.MapFrom(src => string.Join(',', src.ColorIndicator)))
        .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => string.Join(',', src.Colors)))
        .ForMember(dest => dest.FrameEffects, opt => opt.MapFrom(src => string.Join(',', src.FrameEffects)))
        .ForMember(dest => dest.Keywords, opt => opt.MapFrom(src => string.Join(',', src.Keywords)))
        .ForMember(dest => dest.LeadershipSkills, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.LeadershipSkills)))
        .ForMember(dest => dest.OtherFaceIds, opt => opt.MapFrom(src => string.Join(',', src.OtherFaceIds)))
        .ForMember(dest => dest.Printings, opt => opt.MapFrom(src => string.Join(',', src.Printings)))
        .ForMember(dest => dest.PromoTypes, opt => opt.MapFrom(src => string.Join(',', src.PromoTypes)))
        .ForMember(dest => dest.PurchaseUrls, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.PurchaseUrls)))
        .ForMember(dest => dest.Subtypes, opt => opt.MapFrom(src => string.Join(',', src.Subtypes)))
        .ForMember(dest => dest.Supertypes, opt => opt.MapFrom(src => string.Join(',', src.Supertypes)))
        .ForMember(dest => dest.Types, opt => opt.MapFrom(src => string.Join(',', src.Types)))
        .ForMember(dest => dest.Variations, opt => opt.MapFrom(src => string.Join(',', src.Variations)))
        .ForMember(dest => dest.CardKingdomId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("cardKingdomId")))
        .ForMember(dest => dest.McmId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("mcmId")))
        .ForMember(dest => dest.McmMetaId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("mcmMetaId")))
        .ForMember(dest => dest.MtgjsonV4Id, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("mtgjsonV4Id")))
        .ForMember(dest => dest.MultiverseId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("multiverseId")))
        .ForMember(dest => dest.ScryfallId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("scryfallId")))
        .ForMember(dest => dest.ScryfallIllustrationId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("scryfallIllustrationId")))
        .ForMember(dest => dest.ScryfallOracleId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("scryfallOracleId")))
        .ForMember(dest => dest.TcgplayerProductId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("tcgplayerProductId")));

      CreateMap<TokenJson, Token>()
        .ForMember(dest => dest.Availability, opt => opt.MapFrom(src => string.Join(',', src.Availability)))
        .ForMember(dest => dest.ColorIdentity, opt => opt.MapFrom(src => string.Join(',', src.ColorIdentity)))
        .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => string.Join(',', src.Colors)))
        .ForMember(dest => dest.FrameEffects, opt => opt.MapFrom(src => string.Join(',', src.FrameEffects)))
        .ForMember(dest => dest.Keywords, opt => opt.MapFrom(src => string.Join(',', src.Keywords)))
        .ForMember(dest => dest.PromoTypes, opt => opt.MapFrom(src => string.Join(',', src.PromoTypes)))
        .ForMember(dest => dest.ReverseRelated, opt => opt.MapFrom(src => ";" + string.Join(';', src.ReverseRelated) + ";"))
        .ForMember(dest => dest.Subtypes, opt => opt.MapFrom(src => string.Join(',', src.Subtypes)))
        .ForMember(dest => dest.Supertypes, opt => opt.MapFrom(src => string.Join(',', src.Supertypes)))
        .ForMember(dest => dest.ScryfallId, opt => opt.MapFrom(src => src.Identifiers.GetValueOrDefault("scryfallId")))
        .ForMember(dest => dest.Types, opt => opt.MapFrom(src => string.Join(',', src.Types)));

      CreateMap<ForeignDataJson, ForeignData>();
      CreateMap<LegalityJson, Legality>();
      CreateMap<RulingJson, Ruling>();
      CreateMap<SetTranslationJson, SetTranslation>();
    }
  }
}