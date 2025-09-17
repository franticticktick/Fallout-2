namespace Skills
{
    public class Doctor : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 5 + (character.Perception + character.Intelligence);
        }
    }

}