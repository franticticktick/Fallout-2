public class ActionLogger
{
    public static string LogGetDamage(int damage, bool chosenOne, string characterName)
    {
        if (chosenOne)
        {
            return "●Вы получили " + damage + " урона<br>";
        }
        return "●" + characterName + " получает " + damage + " урона<br>";
    }

    public static string LogReloadWeapon(bool chosenOne, string characterName)
    {
        if (chosenOne)
        {
            return "●Вы перезарядили оружие<br>";
        }
        return "●" + characterName + " перезаряжает оружие<br>";
    }
}
