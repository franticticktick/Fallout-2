namespace Skills
{
    public class EnergyWeapons : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 2 * character.Agility;
        }
    }
}

