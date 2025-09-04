namespace Skills
{
    public class Repair : Skill
    {

        public override void Calculate(Character character)
        {
            value = 3 * character.Intelligence;
        }
    }
}