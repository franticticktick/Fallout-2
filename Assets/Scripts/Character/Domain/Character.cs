using System;
using System.Collections.Generic;
using UnityEngine;
using Skills;
using Traits;
using Weapons;
using Armors;
using System.Linq;

[CreateAssetMenu(fileName = "New Character", menuName = "Character", order = 1)]
public class Character : ScriptableObject
{
    public const int RANGE_WEAPON_COMBAT_ACTION_POINTS = 5;
    public const int BURST_FIRE_COMBAT_ACTION_POINTS = 6;
    public const int MELE_WEAPON_COMBAT_ACTION_POINTS = 4;
    public const int NO_WEAPON_COMBAT_ACTION_POINTS = 3;
    public const int RELOAD_WEAPON_COMBAT_ACTION_POINTS = 2;
    public const int MELLE_ATTACK_DISTANCE = 2;

    public event Action<string> ActionDone;

    [SerializeField]
    private new string name;

    [SerializeField]
    private bool chosenOne;

    [SerializeField]
    private CharacterState state = CharacterState.Traveling;

    [SerializeField]
    private int strenght = 1;

    [SerializeField]
    private int perception = 1;

    [SerializeField]
    private int endurance = 1;

    [SerializeField]
    private int charisma = 1;

    [SerializeField]
    private int intelligence = 1;

    [SerializeField]
    private int agility = 1;

    [SerializeField]
    private int luck = 1;

    [SerializeField]
    private double hitPoints;

    [SerializeField]
    private double currentHitPoints;

    [SerializeField]
    private int armorClass;

    [SerializeField]
    private int additionalArmorClass;

    [SerializeField]
    private int carryWeight;

    [SerializeField]
    private int actionPoints;

    [SerializeField]
    private int combatActionPoints;

    [SerializeField]
    private int sequence;

    [SerializeField]
    private int healingRate;

    [SerializeField]
    private int perkRate = 3;

    [SerializeField]
    private int fireResistance = 0;

    [SerializeField]
    private int radiationResistance;

    [SerializeField]
    private int damageResistance = 0;

    [SerializeField]
    private int poisonResistance;

    [SerializeField]
    private int skillRate;

    [SerializeField]
    private int criticalChance;

    [SerializeField]
    private int meleeDamage;

    [SerializeField]
    private List<Skill> skills = new();

    [SerializeField]
    private List<Trait> traits = new();

    [SerializeField]
    private int skillPoints;

    private int level = 1;

    private CarryWeightDelegate calcCarryWeight = (strenght) => 25 + (25 * strenght);

    public delegate int CarryWeightDelegate(int strenght);

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Armor armor;

    private void OnEnable()
    {
        InitCombatActionPoints();
    }

    private void Awake()
    {
        InitCombatActionPoints();
        InitStats();
    }

    public void InitStats()
    {
        CalculateStrenhghtBasedDerivedStatistic();
        CalculateAgilityBasedDerivedStatistic();
        CalculatePerceptionBasedDerivedStatistic();
        CalculateEnduranceBasedDerivedStatistic();
        CalculateIntelligenceBasedDerivedStatistic();
        CalculateLuckBasedDerivedStatistic();
        InitSkills();
    }

    public void InitSkills(List<Skill> skills)
    {
        this.skills = skills;
        skills?.ForEach(skill => skill.InitBaseValue(this));
    }

    public void InitSkills()
    {
        skills?.ForEach(skill => skill.InitBaseValue(this));
    }

    public void ChoseTrait(string name)
    {
        Trait trait = traits.Find(t => t.TraitName.Equals(name));
        if (!trait.IsChosen())
        {
            trait?.Chose(this);
        }
    }

    public void CancelTrait(string name)
    {
        Trait trait = traits.Find(t => t.TraitName.Equals(name));
        trait?.UnChose(this);
    }

    public void IncrementStrenght(int value)
    {
        strenght = IncrementCharacteristic(strenght, value);
        CalculateStrenhghtBasedDerivedStatistic();
        skills.ForEach(skill => skill.InitBaseValue(this));
    }

    private void CalculateStrenhghtBasedDerivedStatistic()
    {
        CalculateHp();

        carryWeight = calcCarryWeight(strenght);

        int value = strenght - 5;
        if (value <= 0)
        {
            meleeDamage = 1;
        }
        else
        {
            meleeDamage = value;
        }
    }

    private void CalculateHp()
    {
        var needUpdateCurrentHp = hitPoints <= currentHitPoints;
        hitPoints = 15 + strenght + (endurance * 2);
        if (needUpdateCurrentHp)
        {
            currentHitPoints = hitPoints;
        }
    }

    public void IncrementAgility(int value)
    {
        agility = IncrementCharacteristic(agility, value);
        CalculateAgilityBasedDerivedStatistic();
        skills.ForEach(skill => skill.InitBaseValue(this));
    }

    private void CalculateAgilityBasedDerivedStatistic()
    {
        ArmorClass = agility;
        actionPoints = 5 + agility / 2;
    }

    public void IncrementEndurance(int value)
    {
        endurance = IncrementCharacteristic(endurance, value);
        CalculateEnduranceBasedDerivedStatistic();
        skills.ForEach(skill => skill.InitBaseValue(this));
    }

    private void CalculateEnduranceBasedDerivedStatistic()
    {
        CalculateHp();
        healingRate = endurance / 3;
        radiationResistance = endurance * 2;
        PoisonResistance = endurance * 5;
    }

    public void IncrementLuck(int value)
    {
        luck = IncrementCharacteristic(luck, value);
        CalculateLuckBasedDerivedStatistic();
        skills.ForEach(skill => skill.InitBaseValue(this));
    }

    private void CalculateLuckBasedDerivedStatistic()
    {
        criticalChance = luck;
    }

    public void IncrementIntelligence(int value)
    {
        intelligence = IncrementCharacteristic(intelligence, value);
        CalculateIntelligenceBasedDerivedStatistic();
        skills.ForEach(skill => skill.InitBaseValue(this));
    }

    private void CalculateIntelligenceBasedDerivedStatistic()
    {
        skillRate = 5 + intelligence * 2;
    }

    public void IncrementPerception(int value)
    {
        perception = IncrementCharacteristic(perception, value);
        CalculatePerceptionBasedDerivedStatistic();
        skills.ForEach(skill => skill.InitBaseValue(this));
    }

    private void CalculatePerceptionBasedDerivedStatistic()
    {
        sequence = 2 * perception;
    }

    public void IncrementCharisma(int value)
    {
        charisma = IncrementCharacteristic(charisma, value);
        skills.ForEach(skill => skill.InitBaseValue(this));
    }

    public void HitDamageWithBurstFire(Character character, int distance)
    {
        if (weapon == null)
        {
            throw new ArgumentException("Weapon cant be null");
        }
        if (!weapon.IsRange())
        {
            throw new ArgumentException("Weapon should be range");
        }
        if (weapon != null && weapon.HasStore())
        {
            if (!weapon.IsStoreNotEmpty())
            {
                Debug.Log("Weapon store is empty");
                return;
            }
        }
        if (combatActionPoints == 0)
        {
            Debug.Log("No action points");
            return;
        }
        int damage = 0;
        ReduceCombatActionPoints(BURST_FIRE_COMBAT_ACTION_POINTS);
        for (var i = 0; i < weapon.BurstSize; i++)
        {
            damage += CalculateDamageWithBurstFire(character, distance);
        }
        Debug.Log("Damage is " + damage);
        character.ReduceHitPoints(damage);
        weapon.ReduceStoreForBurstFire();
    }

    public void HitDamage(Character character, int distance)
    {
        if (state != CharacterState.CombatTurn)
        {
            throw new UnableHitDamageException("Unable to hit damage");
        }
        if (combatActionPoints == 0)
        {
            Debug.Log("No action points");
            throw new NoActionPointsException("Weapon store is empty");
        }
        if (weapon != null && weapon.HasStore())
        {
            if (!weapon.IsStoreNotEmpty())
            {
                Debug.Log("Weapon store is empty");
                throw new UnableHitDamageException("Weapon store is empty");
            }
        }
        int damage = CalculateDamage(character, distance);
        Debug.Log("Damage is " + damage);
        character.ReduceHitPoints(damage);
    }

    private int CalculateDamage(Character character, int distance)
    {
        int damage;
        if (weapon == null)
        {
            damage = new System.Random().Next(1, meleeDamage);
            ReduceCombatActionPoints(NO_WEAPON_COMBAT_ACTION_POINTS);
        }
        else
        {
            if (weapon.IsRange())
            {
                ReduceCombatActionPoints(RANGE_WEAPON_COMBAT_ACTION_POINTS);
                weapon.ReduceStore(1);
                int hitChance = CalculateHitChance(distance, weapon, character.CalculateArmorClass());

                Debug.Log("Hit chanse is " + hitChance);
                if (new System.Random().Next(1, 100) > hitChance)
                {
                    return 0;
                }
                damage = weapon.CalculateDamage(0);
            }
            else
            {
                damage = weapon.CalculateDamage(meleeDamage);
                ReduceCombatActionPoints(MELE_WEAPON_COMBAT_ACTION_POINTS);
            }
        }

        Trait finesse = GetTraitByType(typeof(Finesse));
        if (finesse != null)
        {
            damage = (int)(damage - (damage * 0.3));
            damage = damage < 0 ? 1 : damage;
        }
        return ApplyDamageModifiers(character, damage);
    }

    private int ApplyDamageModifiers(Character character, int damage)
    {
        DamageType damageType = weapon == null ? DamageType.Common : weapon.DamageType;
        var ammunitionDamageReistance = weapon == null ? 0 : weapon.GetDamageResistanceModifier();
        var damageWihoutThreshold = damage - character.GetArmorDamageThreshold(damageType);
        var finalDamageResistance = character.GetArmorDamageresistance(damageType) + ammunitionDamageReistance;

        var finaldamage = damageWihoutThreshold - damageWihoutThreshold
            * finalDamageResistance / 100;
        return finaldamage < 0 ? 0 : finaldamage;
    }

    private int CalculateDamageWithBurstFire(Character character, int distance)
    {
        int hitChance = CalculateHitChance(distance, weapon, character.CalculateArmorClass());

        Debug.Log("Hit chanse is " + hitChance);
        var realChance = new System.Random().Next(1, 100);
        Debug.Log("Real hit chanse is " + realChance);
        if (realChance > hitChance)
        {
            return 0;
        }
        int damage = weapon.CalculateDamage(0);
        return ApplyDamageModifiers(character, damage);
    }

    public void ReloadWeapon(Ammunition ammunition)
    {
        if (weapon == null || !weapon.HasStore()
            || state == CharacterState.WaitingForTurn
            || state == CharacterState.CombatMoving)
        {
            throw new UnableReloadWeaponException("Unable to reload weapon");
        }
        if (weapon != null && weapon.HasStore() && ammunition != null)
        {
            if (state == CharacterState.CombatTurn
                && combatActionPoints >= RELOAD_WEAPON_COMBAT_ACTION_POINTS
                && ammunition.IsNotEmpty())
            {
                weapon.ReloadStore(ammunition);
                ReduceCombatActionPoints(RELOAD_WEAPON_COMBAT_ACTION_POINTS);
                Debug.Log("Weapon has been reloaded");
                ActionDone?.Invoke(ActionLogger.LogReloadWeapon(chosenOne, name));
            }
            if (state == CharacterState.Traveling && ammunition.IsNotEmpty())
            {
                weapon.ReloadStore(ammunition);
                Debug.Log("Weapon has been reloaded");
            }
        }
    }

    private void ReduceCombatActionPoints(int value)
    {
        combatActionPoints = combatActionPoints - value <= 0 ? 0 : combatActionPoints - value;
    }

    public int GetArmorDamageThreshold(DamageType damageType)
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.DefinDamageThreshold(damageType);
    }

    public int GetArmorDamageresistance(DamageType damageType)
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.DefinDamageResistancee(damageType);
    }

    private int CalculateHitChance(int distance, Weapon weapon, int armorClass)
    {
        Skill skill = DefineSkillByWeaponType(weapon);
        var basicHitChance = skill.GetValue() - 30;
        var perceptionBonus = perception - 2 <= 0 ? 1 : perception - 2;
        var finalArmorClass = armorClass + weapon.GetArmorClassModifier();

        basicHitChance = basicHitChance < 0 ? 1 : basicHitChance;
        return basicHitChance + (perceptionBonus * 16) - (distance * 4) - finalArmorClass;
    }

    public int CalculateArmorClass()
    {
        if (armor == null)
        {
            return armorClass;
        }
        return armorClass + armor.ArmorClass;
    }

    public void ReduceHitPoints(int damage)
    {
        if (currentHitPoints > 0)
        {
            ActionDone?.Invoke(ActionLogger.LogGetDamage(damage, chosenOne, name));
            var diff = currentHitPoints - damage;
            currentHitPoints = diff < 0 ? 0 : diff;
        }
    }

    private Skill DefineSkillByWeaponType(Weapon weapon)
    {
        if (weapon == null)
        {
            return GetSkillByType(typeof(Unarmed));
        }
        switch (weapon.Type)
        {
            case WeaponType.Melee: return GetSkillByType(typeof(MeleeWeapons));
            case WeaponType.SmallGuns: return GetSkillByType(typeof(SmallGuns));
            case WeaponType.BigGuns: return GetSkillByType(typeof(BigGuns));
            case WeaponType.Throwing: return GetSkillByType(typeof(Throwing));
            case WeaponType.Energy: return GetSkillByType(typeof(EnergyWeapons));
        }
        return GetSkillByType(typeof(Unarmed));
    }

    private int IncrementCharacteristic(int characteristic, int value)
    {
        characteristic += value;
        if (characteristic > 10)
        {
            return 10;
        }
        if (characteristic <= 0)
        {
            return 1;
        }
        return characteristic;
    }

    public bool CanAttack(int distance)
    {
        if (weapon == null)
        {
            return combatActionPoints - NO_WEAPON_COMBAT_ACTION_POINTS >= 0 && distance <= MELLE_ATTACK_DISTANCE;
        }
        if (weapon.Type == WeaponType.Melee)
        {
            return combatActionPoints - MELE_WEAPON_COMBAT_ACTION_POINTS >= 0 && distance <= MELLE_ATTACK_DISTANCE;
        }
        return distance <= weapon.GetRange() && combatActionPoints - RANGE_WEAPON_COMBAT_ACTION_POINTS >= 0
            && weapon.IsStoreNotEmpty();
    }

    public bool CanAttackWithBurstFire(int distance)
    {
        if (weapon != null)
        {
            return combatActionPoints - BURST_FIRE_COMBAT_ACTION_POINTS >= 0
                && distance <= weapon.GetRange()
                && weapon.IsBurst()
                && weapon.IsStoreNotEmptyForBurstFire();
        }
        return false;
    }

    public void TryReloadWeapon(Ammunition ammunition)
    {
        if (weapon != null && weapon.IsStoreAlmostEmpty())
        {
            ReloadWeapon(ammunition);
        }
    }

    public bool IsWeaponRange()
    {
        if (weapon == null)
        {
            return false;
        }
        return weapon.IsRange();
    }

    public int Strenght { get => strenght; }

    public int Perception { get => perception; }

    public int Intelligence { get => intelligence; }

    public int Luck { get => luck; set => luck = value; }

    public int Agility { get => agility; }

    public int Charisma { get => charisma; }

    public int Endurance { get => endurance; }

    public List<Skill> Skills { set => skills = value; get => skills; }

    public int PoisonResistance { get => poisonResistance; set => poisonResistance = value; }

    public int RadiationResistance { get => radiationResistance; }

    public int ArmorClass { get => armorClass; set => armorClass = value; }

    public int MeleeDamage { get => meleeDamage; set => meleeDamage = value; }

    public int SkillRate { get => skillRate; set => skillRate = value; }

    public int Level { get => level; set => level = value; }

    public List<Trait> Traits { get => traits; set => traits = value; }

    public Weapon Weapon { get => weapon; set => weapon = value; }

    public int ActionPoints { get => actionPoints; }

    public double CurrentHitPoints { get => currentHitPoints; }
    public double HitPoints { get => hitPoints; }
    public Armor Armor { get => armor; }
    public int CarryWeight { get => carryWeight; set => carryWeight = value; }
    public int CriticalChance { get => criticalChance; set => criticalChance = value; }
    public int Sequence { get => sequence; set => sequence = value; }
    public int HealingRate { get => healingRate; set => healingRate = value; }
    public int SkillPoints { get => skillPoints; }

    public bool IsCombatTurn() => state == CharacterState.CombatTurn;

    public bool IsTraveling() => state == CharacterState.Traveling;

    public void ChangeStateToCombatMoving()
    {
        state = CharacterState.CombatMoving;
    }

    public void ChangeStateToCombatTurn()
    {
        state = CharacterState.CombatTurn;
    }
    public void ChangeStateToWaitingForTurn()
    {
        state = CharacterState.WaitingForTurn;
    }

    public void ChangeStateToTraveling()
    {
        state = CharacterState.Traveling;
    }

    public int CalculateMovementActionPoints(int distance)
    {
        if (weapon == null || !weapon.IsRange())
        {
            var melleDistance = distance - MELLE_ATTACK_DISTANCE;
            if (distance == MELLE_ATTACK_DISTANCE)
            {
                return 0;
            }
            if (combatActionPoints >= melleDistance)
            {
                return melleDistance;
            }
            else
            {
                return combatActionPoints;
            }
        }
        if (weapon != null && weapon.IsRange())
        {
            if (distance < weapon.GetRange())
            {
                return 0;
            }
            if (distance > weapon.GetRange())
            {
                var diff = distance - weapon.GetRange();
                if (combatActionPoints > diff)
                {
                    return diff;
                }
                else
                {
                    return combatActionPoints;
                }
            }
        }
        return MELLE_ATTACK_DISTANCE;
    }

    public void SetRadiationResistance(int value)
    {
        radiationResistance = value;
    }

    public void SetPoisonResistance(int value)
    {
        PoisonResistance = value;
    }

    public void IncreaseHealingRate(int value)
    {
        healingRate += value;
    }

    public void IncreaseActionPoints(int value)
    {
        actionPoints += value;
    }

    public void IncreaseCriticalChance(int value)
    {
        criticalChance += value;
    }

    public void IncreaseSequencee(int value)
    {
        sequence += value;
    }

    public void SetArmorClass(int value)
    {
        ArmorClass = value;
    }

    public void SetCalcCarryWeight(CarryWeightDelegate @delegate) => calcCarryWeight = @delegate;

    public void IncreaseMeleeDamage(int value)
    {
        this.meleeDamage += value;
    }

    public void IncreasePerkRate(int value)
    {
        this.perkRate += value;
    }

    public Trait GetTrait(Trait trait)
    {
        return traits.Find(t => t.GetType().Equals(trait.GetType()));
    }

    public Skill GetSkillByType(Type type)
    {
        return skills.Find(s => s.GetType().Equals(type));
    }

    private Trait GetTraitByType(Type type)
    {
        return traits.Find(t => t.GetType().Equals(type));
    }

    public void IncreaseLevel()
    {
        level++;
        skillPoints = 5 + (intelligence * 2);
        //Каждый третий уровень вы сможете получить одну новую способность.
        IncreaseHitPoints((int)(2 + (0.5 * endurance)));
    }

    private void IncreaseHitPoints(int value)
    {
        if (currentHitPoints == hitPoints)
        {
            currentHitPoints += value;
        }
        hitPoints += value;
    }

    public void IncreaseSkill(string skillName)
    {
        if (skillPoints == 0)
        {
            return;
        }
        skillPoints--;

        var skill = skills.Where(skill => skill.SkillName.Equals(skillName))
            .FirstOrDefault();
        if (skill != null)
        {
            skill.IncrementValue();
        }
    }

    public void DecreaseSkill(string skillName)
    {
        var skill = skills.Where(skill => skill.SkillName.Equals(skillName))
            .FirstOrDefault();
        if (skill != null)
        {
            skill.DicrementValue();
            skillPoints++;
        }
    }

    public bool SkillHasEmptyValue(string skillName)
    {
        var skill = skills.Where(skill => skill.SkillName.Equals(skillName))
            .FirstOrDefault();
        if (skill == null)
        {
            return false;
        }
        return skill.IsEmptyValue();
    }

    public bool IsSkillPointsEmpty() => skillPoints == 0;

    public void GetTurnInCombat()
    {
        state = CharacterState.CombatTurn;
        armorClass -= additionalArmorClass;
        additionalArmorClass = 0;
        InitCombatActionPoints();
    }

    public void InitCombatActionPoints()
    {
        combatActionPoints = ActionPoints;
    }

    public bool ReduceActionPointsForMovementIfPossible(int value)
    {
        if (value <= combatActionPoints)
        {
            combatActionPoints -= value;
            return true;
        }
        return false;
    }

    public void ReduceActionPointsForMovement(int value)
    {
        if (state != CharacterState.CombatMoving)
        {
            var actionPoints = CalculateMovementActionPoints(value);
            combatActionPoints -= actionPoints;
            combatActionPoints = combatActionPoints < 0 ? 0 : combatActionPoints;
        }
    }

    public bool IsDead() => currentHitPoints <= 0;

    public void ChangeWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public bool IsWeaponStoreEmpty()
    {
        if (weapon == null)
        {
            return false;
        }
        else
        {
            return !weapon.IsStoreNotEmpty();
        }
    }

    public string GetWeaponName()
    {
        if (weapon != null)
        {
            return weapon.Name;
        }
        return null;
    }

    public AudioClip GetHitSound() =>
         weapon != null ? weapon.HitSound : null;

    public int GetCombatActionPoints() => combatActionPoints;

    public bool HasMelleWeapon()
    {
        if (weapon == null)
        {
            return true;
        }
        if (weapon.Type == WeaponType.Melee)
        {
            return true;
        }
        return false;
    }

    public AudioClip GetArmorHitSound()
    {
        if (armor != null)
        {
            return armor.HitSound;
        }
        return null;
    }

    public bool IsWeaponStoreNotEmptyForBurstFire()
    {
        if (weapon != null && weapon.HasStore() && weapon.IsBurst())
        {
            return weapon.IsStoreNotEmptyForBurstFire();
        }
        return false;
    }

    public void PussTurn()
    {
        armorClass += combatActionPoints;
        additionalArmorClass = combatActionPoints;

        ChangeStateToWaitingForTurn();
    }

    public void InitArmorClass()
    {
        armorClass -= additionalArmorClass;
        additionalArmorClass = 0;
    }

    public string GetAttackAnimationName()
    {
        if (weapon == null)
        {
            return Weapon.PUNCH_ANIMATION;
        }
        return weapon.GetAttackAnimationName();
    }

    public void IncreaseRadiationResistance(int value)
    {
        radiationResistance += value;
    }

    public int CommonDamageResistance()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.CommonDamageResistance;
    }

    public int LaserDamageResistance()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.LaserDamageResistance;
    }

    public int FireDamageResistance()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.FireDamageResistance;
    }

    public int PlasmaDamageResistance()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.PlasmaDamageResistance;
    }

    public int ExplosionDamageResistance()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.ExplosionDamageResistance;
    }

    public int CommonDamageThreshold()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.CommonDamageThreshold;
    }

    public int LaserDamageThreshold()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.LaserDamageThreshold;
    }

    public int FireDamageThreshold()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.FireDamageThreshold;
    }

    public int PlasmaDamageThreshold()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.PlasmaDamageThreshold;
    }

    public int ExplosionDamageThreshold()
    {
        if (armor == null)
        {
            return 0;
        }
        return armor.ExplosionDamageThreshold;
    }

    public void ChangeArmor(Armor armor)
    {
        if (this.armor != null)
        {
            this.armor.CancellEffects(this);
        }
        this.armor = armor;
        if (this.armor != null)
        {
            this.armor.ApplEffects(this);
        }
    }

    public bool WeaponSupportAmmunitionType(AmmunitionType ammunitionType)
    {
        if (weapon != null)
        {
            return weapon.SupportAmmunitionType(ammunitionType);
        }
        return false;
    }

    public Mode ChangeWeaponMode()
    {
        if (weapon != null)
        {
            return weapon.ChangeWeaponMode();
        }
        return Mode.Single;
    }

    public bool HasWeaponModes()
    {
        if (weapon != null)
        {
            weapon.HasWeaponModes();
        }
        return false;
    }

    public string GetWeaponActionPoints()
    {
        var points = 0;
        if (weapon == null)
        {
            points = MELE_WEAPON_COMBAT_ACTION_POINTS;
        }
        if (weapon.IsRange() && !weapon.IsBurst())
        {
            points = RANGE_WEAPON_COMBAT_ACTION_POINTS;
        }
        if (weapon.IsBurst())
        {
            points = BURST_FIRE_COMBAT_ACTION_POINTS;
        }
        return "ОД " + points;
    }

    public Mode GetWeaponMode()
    {
        if (weapon == null)
        {
            return Mode.Punch;
        }
        return weapon.Mode;
    }

    public string GetMelleAttackAtionPoints()
    {
        return "ОД " + MELE_WEAPON_COMBAT_ACTION_POINTS.ToString();
    }

    public string GetReloadWeaponAtionPoints()
    {
        return "ОД " + RELOAD_WEAPON_COMBAT_ACTION_POINTS.ToString();
    }

    public void Heal(int hp)
    {
        currentHitPoints += hp;
        if (currentHitPoints > hitPoints)
        {
            currentHitPoints = hitPoints;
        }
    }

    public string GetStoreCharge()
    {
        if (weapon != null)
        {
            return weapon.GetStoreCharge();
        }
        return "";
    }

    public List<Trait> GetChosenTraits()
    {
        return traits.Where(trait => trait.IsChosen())
            .ToList();
    }

    public List<Skill> GetMainSkills()
    {
        return skills.Where(skill => skill.IsMain())
            .ToList();
    }

    public void MarkSkillAsMain(string skillName)
    {
        List<Skill> mainSkills = skills.Where(skill => skill.IsMain())
            .ToList();
        if (mainSkills.Count == 3)
        {
            throw new UnableMarksSkillAsMainException("Can't mark skill as main");
        }
        var skill = skills.Where(skill => skill.SkillName.Equals(skillName))
            .FirstOrDefault();
        if (skill != null)
        {
            if (!skill.IsMain())
            {
                skill.MarkAsMain();
            }
        }
    }

    public void MarkSkillAsNotMain(string skillName)
    {
        var skill = skills.Where(skill => skill.SkillName.Equals(skillName))
            .FirstOrDefault();
        if (skill != null)
        {
            if (skill.IsMain())
            {
                skill.MarkAsNotMain();
            }
        }
    }

    public void IncrementSkillPoints()
    {
        skillPoints++;
    }
}
