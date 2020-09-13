using System;
using Newtonsoft.Json;

namespace Magicord.Modules.Core.Extension
{
  public static class StringExtensions
  {
    public static T ParseJson<T>(this string value)
    {
      try
      {
        return JsonConvert.DeserializeObject<T>(value);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}