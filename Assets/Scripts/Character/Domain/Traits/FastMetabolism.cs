namespace Traits
{
    public class FastMetabolism : Trait
    {
        private int poisonResistance;
        private int radiationResistance;

        public override void Apply(Character character)
        {
            Trait trait = character.GetTrait(this);
            if (trait == null)
            {
                poisonResistance = character.PoisonResistance;
                radiationResistance = character.RadiationResistance;

                character.IncreaseHealingRate(2);
                character.SetPoisonResistance(0);
                character.SetRadiationResistance(0);
            }
        }

        public override void Cancel(Character character)
        {
            Trait trait = character.GetTrait(this);
            if (trait != null)
            {
                character.IncreaseHealingRate(-2);
                character.SetPoisonResistance(poisonResistance);
                character.SetRadiationResistance(radiationResistance);

                poisonResistance = 0;
                radiationResistance = 0;
            }
        }
    }
}
