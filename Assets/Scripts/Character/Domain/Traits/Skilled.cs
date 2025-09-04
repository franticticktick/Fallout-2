namespace Traits
{
    public class Skilled : Trait
    {
        public override void Apply(Character character)
        {
            //+5 очков навыков каждый уровень;
            character.IncreasePerkRate(1);
        }

        public override void Cancel(Character character)
        {
            character.IncreasePerkRate(-1);
        }
    }
}