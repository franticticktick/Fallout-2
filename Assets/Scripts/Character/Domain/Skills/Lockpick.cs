namespace Skills
{
    public class Lockpick : Skill
    {

        public override void Calculate(Character character)
        {
            value = 10 + (character.Perception + character.Agility);
        }
    }
}
