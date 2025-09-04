using UnityEngine;
using Weapons;

namespace Armors
{

    public class Armor : ScriptableObject, Item
    {
        [SerializeField]
        private new string name;

        private int armorClass;

        [SerializeField]
        private int commonDamageResistance;

        [SerializeField]
        private int laserDamageResistance;

        [SerializeField]
        private int fireDamageResistance;

        [SerializeField]
        private int plasmaDamageResistance;

        [SerializeField]
        private int explosionDamageResistance;

        [SerializeField]
        private int electricityDamageResistance;

        [SerializeField]
        private int electromagneticPulseDamageResistance;

        [SerializeField]
        private int commonDamageThreshold;

        [SerializeField]
        private int laserDamageThreshold;

        [SerializeField]
        private int fireDamageThreshold;

        [SerializeField]
        private int plasmaDamageThreshold;

        [SerializeField]
        private int explosionDamageThreshold;

        [SerializeField]
        private int electricityDamageThreshold;

        [SerializeField]
        private int electromagneticPulseDamageThreshold;

        [SerializeField]
        private int weight;

        [SerializeField]
        private int size;

        [SerializeField]
        private AudioClip hitSound;

        [SerializeField]
        private Texture2D image;

        public int ArmorClass { get => armorClass; set => armorClass = value; }
        public AudioClip HitSound { get => hitSound; }
        public int CommonDamageResistance { get => commonDamageResistance; }
        public int LaserDamageResistance { get => laserDamageResistance; }
        public int FireDamageResistance { get => fireDamageResistance; }
        public int PlasmaDamageResistance { get => plasmaDamageResistance; }
        public int ExplosionDamageResistance { get => explosionDamageResistance; }
        public int CommonDamageThreshold { get => commonDamageThreshold; }
        public int LaserDamageThreshold { get => laserDamageThreshold; }
        public int FireDamageThreshold { get => fireDamageThreshold; }
        public int PlasmaDamageThreshold { get => plasmaDamageThreshold; }
        public int ExplosionDamageThreshold { get => explosionDamageThreshold; }
        public Texture2D Image { get => image; }

        public virtual void ApplEffects(Character character)
        {
        }

        public virtual void CancellEffects(Character character)
        {
        }

        public int DefinDamageThreshold(DamageType damageType) =>
             damageType switch
             {
                 DamageType.Common => commonDamageThreshold,
                 DamageType.Laser => laserDamageThreshold,
                 DamageType.Plasma => plasmaDamageThreshold,
                 DamageType.Fire => fireDamageThreshold,
                 DamageType.Explosion => explosionDamageThreshold,
                 DamageType.Electricity => electricityDamageThreshold,
                 DamageType.ElectromagneticPulse => electromagneticPulseDamageThreshold,
                 _ => 0,
             };

        public int DefinDamageResistancee(DamageType damageType) =>
         damageType switch
         {
             DamageType.Common => commonDamageResistance,
             DamageType.Laser => laserDamageResistance,
             DamageType.Plasma => plasmaDamageResistance,
             DamageType.Fire => fireDamageResistance,
             DamageType.Explosion => explosionDamageResistance,
             DamageType.Electricity => electricityDamageResistance,
             DamageType.ElectromagneticPulse => electromagneticPulseDamageResistance,
             _ => 0,
         };

        public Texture2D GetImage()
        {
            return image;
        }

        public int GetQuantity()
        {
            return 1;
        }

        public bool IsQuantitative()
        {
            return false;
        }
    }
}
