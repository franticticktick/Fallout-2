namespace Skills
{

    public class SmallGuns : Skill
    {
        public override void Calculate(Character character)
        {
            value = 5 + (4 * character.Agility);
        }
    }
}
