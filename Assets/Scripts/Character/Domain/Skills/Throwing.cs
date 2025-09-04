namespace Skills
{
    public class Throwing : Skill
    {
        public override void Calculate(Character character)
        {
            value = 4 * character.Agility;
        }
    }
}