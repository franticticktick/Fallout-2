using System;

public class UnableHitDamageException : Exception
{
    public UnableHitDamageException(string message)
    : base(message)
    {
    }
}
