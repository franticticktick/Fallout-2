namespace Traits
{
    public class SmallFrame : Trait
    {

        public override void Apply(Character character)
        {
            character.IncrementAgility(1);
            character.SetCalcCarryWeight((strenght) => 25 + (15 * strenght));
        }

        public override void Cancel(Character character)
        {
            character.IncrementAgility(-1);
            character.SetCalcCarryWeight((strenght) => 25 + (25 * strenght));
        }
    }
}