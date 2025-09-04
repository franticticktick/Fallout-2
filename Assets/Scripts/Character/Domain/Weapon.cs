using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    public enum WeaponType
    {
        Melee,
        Unarmed,
        SmallGuns,
        BigGuns,
        Throwing,
        Energy
    }

    public enum DamageType
    {
        Common,
        Laser,
        Fire,
        Plasma,
        Explosion,
        Electricity,
        ElectromagneticPulse
    }

    public enum Mode
    {
        Single,
        Burst,
        Punch,
        Reload
    }

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
    public class Weapon : ScriptableObject, Item, ActiveItem
    {
        public const string PUNCH_ANIMATION = "Punch";
        public const string FIRE_ANIMATION = "Fire";
        public const string BURST_FIRE_ANIMATION = "BurstFire";

        [SerializeField]
        private AudioClip hitSound;

        [SerializeField]
        private WeaponType type;

        [SerializeField]
        private DamageType damageType;

        [SerializeField]
        private new string name;

        [SerializeField]
        private string description;

        [SerializeField]
        private int minDamage;

        [SerializeField]
        private int maxDamage;

        [SerializeField]
        private int range;

        [SerializeField]
        private int burstRange;

        [SerializeField]
        private int strenght;

        [SerializeField]
        private int ctiticalChance;

        [SerializeField]
        private int weight;

        [SerializeField]
        private int price;

        [SerializeField]
        private int size;

        [SerializeField]
        private WeaponStore store;

        [SerializeField]
        private int burstSize;

        [SerializeField]
        private string idleAnimationName;

        [SerializeField]
        private string attackAnimationName = FIRE_ANIMATION;

        [SerializeField]
        private string burstAnimationName = BURST_FIRE_ANIMATION;

        [SerializeField]
        private Mode mode;

        [SerializeField]
        private List<Mode> availableModes;

        [SerializeField]
        private Texture2D image;

        [SerializeField]
        private List<AmmunitionType> ammunitionTypes = new();

        public WeaponType Type { get => type; set => type = value; }
        public int Weight { get => weight; set => weight = value; }
        public int MinDamage { get => minDamage; set => minDamage = value; }
        public int MaxDamage { get => maxDamage; set => maxDamage = value; }
        public DamageType DamageType { get => damageType; set => damageType = value; }
        public WeaponStore Store { set => store = value; }
        public AudioClip HitSound { get => hitSound; }
        public string Name { get => name; }
        public string IdleAnimationName { get => idleAnimationName; }
        public int BurstSize { get => burstSize; }
        public Mode Mode { get => mode; }
        public Texture2D Image { get => image; }
        public string Description { get => description; }

        public bool IsBurst() => mode == Mode.Burst;

        public int GetRange()
        {
            switch (mode)
            {
                case Mode.Burst: return burstRange;
                case Mode.Single: return range;
            }
            return 1;
        }

        public bool IsRange() => type == WeaponType.SmallGuns ||
                type == WeaponType.BigGuns ||
                type == WeaponType.Throwing ||
                type == WeaponType.Energy;

        public int CalculateDamage(int meleeDamage)
        {
            int damage = new System.Random().Next(minDamage, maxDamage + meleeDamage);
            if (meleeDamage == 0 && HasStore())
            {
                damage *= store.GetDamageModifier();
            }
            return damage;
        }

        public bool HasStore() => store != null;

        public void ReloadStore(Ammunition ammunition)
        {
            if (store != null)
            {
                store.Reload(ammunition);
            }
        }

        public bool IsStoreNotEmpty()
        {
            if (store != null)
            {
                return store.IsNotEmpty();
            }
            return false;
        }

        public bool IsStoreNotEmptyForBurstFire()
        {
            if (store != null)
            {
                return store.IsNotEmpty(burstSize) && mode == Mode.Burst;
            }
            return false;
        }

        public void ReduceStore(int value)
        {
            if (HasStore())
            {
                store.Reduce(value);
            }
        }

        public void ReduceStoreForBurstFire()
        {
            ReduceStore(burstSize);
        }

        public int GetArmorClassModifier()
        {
            if (store != null)
            {
                return store.GetArmorClassModifier();
            }
            return 0;
        }

        public int GetDamageResistanceModifier()
        {
            if (store != null)
            {
                return store.GetDamageResistanceModifier();
            }
            return 0;
        }

        public bool IsStoreAlmostEmpty()
        {
            if (store == null)
            {
                return false;
            }
            return store.AlmostEmpty();
        }

        public string GetAttackAnimationName() =>
             mode switch
             {
                 Mode.Burst => burstAnimationName,
                 Mode.Single => attackAnimationName,
                 _ => PUNCH_ANIMATION,
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

        public bool SupportAmmunitionType(AmmunitionType ammunitionType)
        {
            return ammunitionTypes.Contains(ammunitionType);
        }

        public Mode ChangeWeaponMode()
        {
            if (availableModes != null && availableModes.Count > 1)
            {
                var nextWeaponMode = availableModes.Where(m => m != mode)
                    .FirstOrDefault();
                mode = nextWeaponMode;
                return Mode;
            }
            return Mode.Single;
        }

        public bool HasWeaponModes()
        {
            return availableModes != null && availableModes.Count > 1;
        }

        public string GetStoreCharge()
        {
            if (store != null)
            {
                return store.GetCharge();
            }
            return "";
        }
    }
}
