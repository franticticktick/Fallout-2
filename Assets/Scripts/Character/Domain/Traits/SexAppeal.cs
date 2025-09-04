namespace Traits
{
    public class SexAppeal : Trait
    {
        public override void Apply(Character character)
        {
            //‘актически способность увеличивает Ђѕривлекательностьї на 1 и отношение на 20 при общении
            //с персонажами противоположного пола и соответственно уменьшает при общении с персонажами своего пола.
        }

        public override void Cancel(Character character)
        {
        }
    }
}