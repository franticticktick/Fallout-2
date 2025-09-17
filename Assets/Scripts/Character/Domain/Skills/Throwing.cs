namespace Skills
{
    public class Throwing : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 4 * character.Agility;
        }
    }
}