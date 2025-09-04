namespace Traits
{
    public class Gifted : Trait
    {

        public override void Apply(Character character)
        {
            character.IncrementAgility(1);
            character.IncrementEndurance(1);
            character.IncrementStrenght(1);
            character.IncrementLuck(1);
            character.IncrementPerception(1);
            character.IncrementIntelligence(1);
            character.IncrementCharisma(1);

            character.Skills.ForEach(skill => { skill.IncreaseValue(-10); });
        }

        public override void Cancel(Character character)
        {
            character.IncrementAgility(-1);
            character.IncrementEndurance(-1);
            character.IncrementStrenght(-1);
            character.IncrementLuck(-1);
            character.IncrementPerception(-1);
            character.IncrementIntelligence(-1);
            character.IncrementCharisma(-1);

            character.Skills.ForEach(skill => { skill.IncreaseValue(10); });
        }
    }
}