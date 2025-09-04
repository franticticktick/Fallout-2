namespace Traits
{
    public class Jinxed : Trait
    {
        public override void Apply(Character character)
        {
            //Увеличивается вероятность того, что если персонаж игрока или неигровой персонаж
            //допускает во время боя промах, этот промах будет критическим.
        }

        public override void Cancel(Character character)
        {
        }
    }
}