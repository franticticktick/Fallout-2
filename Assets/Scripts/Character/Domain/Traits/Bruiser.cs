namespace Traits
{
    public class Bruiser : Trait
    {

        public override void Apply(Character character)
        {
            character.IncreaseActionPoints(-2);
            character.IncrementStrenght(2);
        }

        public override void Cancel(Character character)
        {
            character.IncreaseActionPoints(2);
            character.IncrementStrenght(-2);
        }
    }
}