namespace Skills
{
    public class Gumbling : Skill
    {

        public override void Calculate(Character character)
        {
            value = 5 * character.Luck;
        }
    }
}