namespace Skills
{
    public class EnergyWeapons : Skill
    {
        public override void Calculate(Character character)
        {
            value = 2 * character.Agility;
        }
    }
}

