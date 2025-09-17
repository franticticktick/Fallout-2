namespace Skills
{

    public class Unarmed : Skill
    {

        protected override int CalculateValue(Character character)
        {
            return 30 + (2 * (character.Agility + character.Strenght));
        }
    }
}
