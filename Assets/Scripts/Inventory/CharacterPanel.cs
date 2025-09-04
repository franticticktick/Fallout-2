using TMPro;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField]
    private ChosenOneController chosenOne;

    [SerializeField]
    private TMP_Text strenght;

    [SerializeField]
    private TMP_Text perception;

    [SerializeField]
    private TMP_Text endurance;

    [SerializeField]
    private TMP_Text charisma;

    [SerializeField]
    private TMP_Text intelligence;

    [SerializeField]
    private TMP_Text agility;

    [SerializeField]
    private TMP_Text luck;

    [SerializeField]
    private TMP_Text hp;

    [SerializeField]
    private TMP_Text armorClass;

    [SerializeField]
    private TMP_Text normal;

    [SerializeField]
    private TMP_Text laser;

    [SerializeField]
    private TMP_Text fire;

    [SerializeField]
    private TMP_Text plasma;

    [SerializeField]
    private TMP_Text explode;

    [SerializeField]
    private TMP_Text range;

    [SerializeField]
    private TMP_Text damage;

    [SerializeField]
    private TMP_Text weapoName;

    [SerializeField]
    private TMP_Text ammo;

    void Start()
    {
        UpdateCharacterValues();
    }

    private void OnEnable()
    {
        UpdateCharacterValues();
    }

    public void UpdateCharacterValues()
    {
        var character = chosenOne.Character;
        var weapon = character.Weapon;

        strenght.text = character.Strenght.ToString();
        perception.text = character.Perception.ToString();
        endurance.text = character.Endurance.ToString();
        charisma.text = character.Charisma.ToString();
        intelligence.text = character.Intelligence.ToString();
        agility.text = character.Agility.ToString();
        luck.text = character.Luck.ToString();
        hp.text = character.HitPoints + "/" + character.CurrentHitPoints;
        armorClass.text = character.ArmorClass.ToString();
        normal.text = character.CommonDamageThreshold() + "/" + character.CommonDamageResistance() + "%";
        laser.text = character.LaserDamageThreshold() + "/" + character.LaserDamageResistance() + "%";
        fire.text = character.FireDamageThreshold() + "/" + character.FireDamageResistance() + "%";
        plasma.text = character.PlasmaDamageThreshold() + "/" + character.PlasmaDamageResistance() + "%";
        explode.text = character.ExplosionDamageThreshold() + "/" + character.ExplosionDamageResistance() + "%";
        ammo.text = character.GetStoreCharge();

        if (weapon != null)
        {
            weapoName.text = weapon.Description;
            damage.text = weapon.MinDamage + "-" + weapon.MaxDamage;
            if (weapon.IsRange())
            {
                range.text = weapon.GetRange().ToString();
            }
        }
        else
        {
            weapoName.text = "No Item";
            damage.text = 1 + "-" + character.MeleeDamage;
        }
    }
}
