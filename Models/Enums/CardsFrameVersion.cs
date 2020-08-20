using NpgsqlTypes;

namespace Magicord.Models.Enums
{
  public enum CardsFrameVersion
  {
    [PgName("2003")]
    TwoThousandThree,
    [PgName("1993")]
    NineteenNinetyThree,
    [PgName("2015")]
    TwoThousandFifteen,
    [PgName("1997")]
    NineteenNinetySeven
  }
}