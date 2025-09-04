namespace Traits
{
    public class СhemReliant : Trait
    {
        public override void Apply(Character character)
        {
            //Эта особенность удваивает шансы возникновения зависимости от медицинских препаратов,
            //однако в два раза уменьшает время действия негативных эффектов от их приёма.
        }

        public override void Cancel(Character character)
        {
        }
    }
}