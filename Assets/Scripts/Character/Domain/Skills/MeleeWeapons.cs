namespace Skills
{
    public class MeleeWeapons : Skill
    {
        public override void Calculate(Character character)
        {
            value = 20 + (2 * (character.Agility + character.Strenght));
        }
    }
}
