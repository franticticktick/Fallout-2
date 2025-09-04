using Skills;
namespace Traits
{
    public class GoodNatured : Trait
    {
        public override void Apply(Character character)
        {
            Speech speech = (Speech)character.GetSkillByType(typeof(Speech));
            FirstAid firstAid = (FirstAid)character.GetSkillByType(typeof(FirstAid));
            Doctor doctor = (Doctor)character.GetSkillByType(typeof(Doctor));
            Barter barter = (Barter)character.GetSkillByType(typeof(Barter));

            speech.IncreaseValue(15);
            firstAid.IncreaseValue(15);
            doctor.IncreaseValue(15);
            barter.IncreaseValue(15);

            MeleeWeapons meleeWeapons = (MeleeWeapons)character.GetSkillByType(typeof(MeleeWeapons));
            EnergyWeapons energyWeapons = (EnergyWeapons)character.GetSkillByType(typeof(EnergyWeapons));
            BigGuns bigGuns = (BigGuns)character.GetSkillByType(typeof(BigGuns));
            SmallGuns smallGuns = (SmallGuns)character.GetSkillByType(typeof(SmallGuns));
            Unarmed unarmed = (Unarmed)character.GetSkillByType(typeof(Unarmed));
            Throwing throwing = (Throwing)character.GetSkillByType(typeof(Throwing));

            meleeWeapons.IncreaseValue(-10);
            energyWeapons.IncreaseValue(-10);
            bigGuns.IncreaseValue(-10);
            smallGuns.IncreaseValue(-10);
            unarmed.IncreaseValue(-10);
            throwing.IncreaseValue(-10);
        }

        public override void Cancel(Character character)
        {
            Speech speech = (Speech)character.GetSkillByType(typeof(Speech));
            FirstAid firstAid = (FirstAid)character.GetSkillByType(typeof(FirstAid));
            Doctor doctor = (Doctor)character.GetSkillByType(typeof(Doctor));
            Barter barter = (Barter)character.GetSkillByType(typeof(Barter));

            speech.IncreaseValue(-15);
            firstAid.IncreaseValue(-15);
            doctor.IncreaseValue(-15);
            barter.IncreaseValue(-15);

            MeleeWeapons meleeWeapons = (MeleeWeapons)character.GetSkillByType(typeof(MeleeWeapons));
            EnergyWeapons energyWeapons = (EnergyWeapons)character.GetSkillByType(typeof(EnergyWeapons));
            BigGuns bigGuns = (BigGuns)character.GetSkillByType(typeof(BigGuns));
            SmallGuns smallGuns = (SmallGuns)character.GetSkillByType(typeof(SmallGuns));
            Unarmed unarmed = (Unarmed)character.GetSkillByType(typeof(Unarmed));
            Throwing throwing = (Throwing)character.GetSkillByType(typeof(Throwing));

            meleeWeapons.IncreaseValue(10);
            energyWeapons.IncreaseValue(10);
            bigGuns.IncreaseValue(10);
            smallGuns.IncreaseValue(10);
            unarmed.IncreaseValue(10);
            throwing.IncreaseValue(10);
        }
    }
}