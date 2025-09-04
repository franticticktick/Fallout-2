using UnityEngine;

namespace Weapons
{

    public enum AmmunitionType
    {
        MicrofusionCell
    }

    [CreateAssetMenu(fileName = "Ammunition", menuName = "Ammunition")]
    public class Ammunition : ScriptableObject, Item
    {
        [SerializeField]
        private WeaponType weaponType;

        [SerializeField]
        private int armorClassModifier;

        [SerializeField]
        private int damageModifier;

        [SerializeField]
        private int damageResistanceModifier;

        [SerializeField]
        private int weight;

        [SerializeField]
        private int size;

        [SerializeField]
        private new string name;

        [SerializeField]
        private Texture2D image;

        [SerializeField]
        private AmmunitionType ammunitionType;

        public WeaponType WeaponType { get => weaponType; }
        public int ArmorClassModifier { get => armorClassModifier; }
        public int DamageModifier { get => damageModifier; }
        public int DamageResistanceModifier { get => damageResistanceModifier; }
        public int Size { get => size; }
        public AmmunitionType AmmunitionType { get => ammunitionType; }

        public void Remove(int value)
        {
            if (size != 0)
            {
                size -= value;
                size = size < 0 ? 0 : size;
            }
        }

        public void Add(int value)
        {
            if (value > 0)
            {
                size += value;
            }
        }

        public bool IsNotEmpty() => size > 0;

        public Texture2D GetImage()
        {
            return image;
        }

        public int GetQuantity()
        {
            return size;
        }

        public bool IsQuantitative()
        {
            return true;
        }
    }
}

