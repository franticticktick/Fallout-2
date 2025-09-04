namespace Traits
{
    public class СhemResistant : Trait
    {
        public override void Apply(Character character)
        {
            //Шанс впасть в зависимость от того или иного препарата вдвое ниже обычного,
            //но и воздействие лекарств ослаблено на 50 %.
        }

        public override void Cancel(Character character)
        {
        }
    }
}
