using System;

public class NoActionPointsException : Exception
{
    public NoActionPointsException(string message)
      : base(message)
    {
    }
}
