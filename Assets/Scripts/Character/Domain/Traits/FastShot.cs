namespace Traits
{
    public class FastShot : Trait
    {
        public override void Apply(Character character)
        {
            //Использование стрелкового и метательного оружия стоит на одно
            //очко действия меньше за счёт невозможности сделать прицельный выстрел.
        }

        public override void Cancel(Character character)
        {
        }
    }
}