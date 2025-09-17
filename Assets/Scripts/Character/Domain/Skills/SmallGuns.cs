namespace Skills
{

    public class SmallGuns : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 5 + (4 * character.Agility);
        }
    }
}
