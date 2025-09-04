namespace Skills
{
    public class Science : Skill
    {

        public override void Calculate(Character character)
        {
            value = 4 * character.Intelligence;
        }
    }
}
