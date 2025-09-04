namespace Skills
{
    public class Outdoorsman : Skill
    {

        public override void Calculate(Character character)
        {
            value = 2 * (character.Endurance + character.Intelligence);
        }
    }
}
