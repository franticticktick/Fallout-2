namespace Skills
{

    public class Unarmed : Skill
    {
        public override void Calculate(Character character)
        {
            value = 30 + (2 * (character.Agility + character.Strenght));
        }
    }
}
