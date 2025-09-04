using System;

public class PerkAlreadyExistsException : Exception
{
    public PerkAlreadyExistsException(string message)
       : base(message)
    {
    }
}
