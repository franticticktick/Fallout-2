namespace Traits
{
    public class Kamikaze : Trait
    {
        private int armorClass;

        public override void Apply(Character character)
        {
            armorClass = character.ArmorClass;

            character.IncreaseSequencee(5);
            character.SetArmorClass(0);
        }

        public override void Cancel(Character character)
        {
            character.IncreaseSequencee(-5);
            character.SetArmorClass(armorClass);

            armorClass = 0;
        }
    }
}