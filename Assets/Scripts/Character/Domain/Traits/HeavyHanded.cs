namespace Traits
{
    public class HeavyHanded : Trait
    {
        public override void Apply(Character character)
        {
            character.IncreaseMeleeDamage(4);
            //от таблицы критических эффектов вычитается 30
        }

        public override void Cancel(Character character)
        {
            character.IncreaseMeleeDamage(-4);
        }
    }
}