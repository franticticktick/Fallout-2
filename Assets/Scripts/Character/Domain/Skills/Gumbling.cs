namespace Skills
{
    public class Gumbling : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 5 * character.Luck;
        }
    }
}