namespace Skills
{
    public class Repair : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 3 * character.Intelligence;
        }
    }
}