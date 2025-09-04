namespace Skills
{
    public class Speech : Skill
    {

        public override void Calculate(Character character)
        {
            value = 5 * character.Charisma;
        }
    }
}
