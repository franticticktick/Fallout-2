namespace Skills
{
    public class Doctor : Skill
    {

        public override void Calculate(Character character)
        {
            value = 5 + (character.Perception + character.Intelligence);
        }
    }

}