using UnityEngine;

public class Level : ScriptableObject
{

    private int value = 1;

    public int NextlevelExpirience()
    {
        return ((value + 1) * (value) / 2) * 1000;
    }
}
