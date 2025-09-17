namespace Skills
{
    public class Science : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 4 * character.Intelligence;
        }

    }
}
