namespace Skills
{
    public class Traps : Skill
    {

        public override void Calculate(Character character)
        {
            value = 10 + (character.Perception + character.Agility);
        }
    }
}
