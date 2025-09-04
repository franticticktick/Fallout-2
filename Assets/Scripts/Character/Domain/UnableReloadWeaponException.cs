using System;

public class UnableReloadWeaponException : Exception
{
    public UnableReloadWeaponException(string message)
      : base(message)
    {
    }
}
