namespace Skills
{
    public class BigGuns : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 2 * character.Agility;
        }
    }
}
