namespace Skills
{
    public class Traps : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 10 + (character.Perception + character.Agility);
        }
    }
}
