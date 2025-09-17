namespace Skills
{
    public class Speech : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 5 * character.Charisma;
        }
    }
}
