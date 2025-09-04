namespace Skills
{
    public class Steal : Skill
    {

        public override void Calculate(Character character)
        {
            value = 3 * character.Agility;
        }
    }

}