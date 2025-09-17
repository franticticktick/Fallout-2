namespace Skills
{
    public class Sneak : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 5 + (3 * character.Agility);
        }
    }
}
