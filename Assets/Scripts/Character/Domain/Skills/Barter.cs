namespace Skills
{
    public class Barter : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 4 * character.Charisma;
        }
    }
}
