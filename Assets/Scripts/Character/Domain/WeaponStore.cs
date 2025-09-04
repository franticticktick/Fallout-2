using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "WeaponStore", menuName = "WeaponStore")]
public class WeaponStore : ScriptableObject
{
    [SerializeField]
    private Ammunition ammunition;

    [SerializeField]
    private int size;

    public void Reload(Ammunition ammunition)
    {
        if (this.ammunition.Size != size && ammunition.Size != 0)
        {
            int remainder = size - this.ammunition.Size;
            int reloadSize = remainder;
            if (ammunition.Size < remainder)
            {
                reloadSize = ammunition.Size;
            }

            this.ammunition.Add(reloadSize);
            ammunition.Remove(reloadSize);
        }
    }

    public void Reduce(int value)
    {
        if (ammunition != null)
        {
            ammunition.Remove(value);
        }
    }

    public bool IsNotEmpty()
    {
        if (ammunition == null)
        {
            return false;
        }
        return ammunition.Size > 0;
    }
    public bool IsNotEmpty(int size)
    {
        if (this.ammunition != null)
        {
            return this.ammunition.Size - size >= 0;
        }
        return false;
    }

    public int GetDamageModifier()
    {
        if (ammunition != null)
        {
            return ammunition.DamageModifier;
        }
        return 1;
    }

    public int GetArmorClassModifier()
    {
        if (ammunition != null)
        {
            return ammunition.ArmorClassModifier;
        }
        return 0;
    }

    public int GetDamageResistanceModifier()
    {
        if (ammunition != null)
        {
            return ammunition.DamageResistanceModifier;
        }
        return 0;
    }

    public bool AlmostEmpty()
    {
        if (ammunition == null)
        {
            return false;
        }
        int percentage = (int)(((double)ammunition.Size / (double)size) * 100);
        return percentage < 30;
    }

    public string GetCharge()
    {
        return ammunition.GetQuantity() + "/" + size;
    }
}
