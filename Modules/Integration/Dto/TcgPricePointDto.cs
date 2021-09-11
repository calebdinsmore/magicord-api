namespace Magicord.Modules.Integration.TcgPlayer
{
  public class TcgPricePointDto
  {
    public PrintingTypeEnum PrintingType { get; set; }
    public decimal? MarketPrice { get; set; }
  }

  public enum PrintingTypeEnum
  {
    Normal,
    Foil
  }
}