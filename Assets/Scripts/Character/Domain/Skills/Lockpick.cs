namespace Skills
{
    public class Lockpick : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 10 + (character.Perception + character.Agility);
        }
    }
}
