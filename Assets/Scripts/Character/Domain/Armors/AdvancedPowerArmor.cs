using UnityEngine;

namespace Armors
{

    [CreateAssetMenu(fileName = "AdvancedPowerArmor", menuName = "AdvancedPowerArmor")]
    public class AdvancedPowerArmor : Armor
    {

        public override void ApplEffects(Character character)
        {
          //  character.IncreaseRadiationResistance(60);
          //  character.IncrementStrenght(4);
        }

        public override void CancellEffects(Character character)
        {
           // character.IncreaseRadiationResistance(-60);
          //  character.IncrementStrenght(-4);
        }
    }
}
