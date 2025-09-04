namespace Skills
{
    public class Barter : Skill
    {

        public override void Calculate(Character character)
        {
            value = 4 * character.Charisma;
        }
    }
}
