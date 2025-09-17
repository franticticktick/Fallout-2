namespace Skills
{
    public class FirstAid : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 2 * (character.Perception + character.Intelligence);
        }
    }
}
