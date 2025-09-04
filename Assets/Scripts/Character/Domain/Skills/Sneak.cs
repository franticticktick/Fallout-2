namespace Skills
{
    public class Sneak : Skill
    {

        public override void Calculate(Character character)
        {
            value = 5 + (3 * character.Agility);
        }
    }
}
