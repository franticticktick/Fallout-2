namespace Skills
{
    public class MeleeWeapons : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 20 + (2 * (character.Agility + character.Strenght));
        }
    }
}
