namespace Skills
{
    public class Steal : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 3 * character.Agility;
        }
    }

}