using System;

namespace Magicord.Core.Exceptions
{
  public class ValidationFailureException : Exception
  {
    public ValidationFailureException() { }

    public ValidationFailureException(string message)
      : base(message)
    {
    }
  }
}