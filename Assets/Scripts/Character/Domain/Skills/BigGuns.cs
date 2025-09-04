namespace Skills
{
    public class BigGuns : Skill
    {
        public override void Calculate(Character character)
        {
            value = 2 * character.Agility;
        }
    }
}
