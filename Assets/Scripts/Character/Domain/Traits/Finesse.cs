namespace Traits
{
    public class Finesse : Trait
    {

        public override void Apply(Character character)
        {
            character.IncreaseCriticalChance(10);
            //на 30 меньше урона все атаки
        }

        public override void Cancel(Character character)
        {
            character.IncreaseCriticalChance(-10);
        }
    }
}