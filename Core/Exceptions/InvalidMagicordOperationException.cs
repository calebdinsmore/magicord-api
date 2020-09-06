using System;

namespace Magicord.Core.Exceptions
{
  public class InvalidMagicordOperationException : Exception
  {
    public InvalidMagicordOperationException() { }

    public InvalidMagicordOperationException(string message)
      : base(message)
    {
    }
  }
}