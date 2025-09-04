namespace Skills
{
    public class FirstAid : Skill
    {
        public override void Calculate(Character character)
        {
            value = 2 * (character.Perception + character.Intelligence);
        }
    }
}
