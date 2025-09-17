using Skills;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Traits;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWindow : MonoBehaviour
{
    [SerializeField]
    private Image skillWarningBox;

    [SerializeField]
    private TMP_Text skillWarningField;

    private Image skillPointFirstDigit;

    private Image skillPointSecondDigit;

    private Image firstDigitSt;

    private Image secondDigitSt;

    private Image firstDigitPe;

    private Image secondDigitPe;

    private Image firstDigitEn;

    private Image secondDigitEn;

    private Image firstDigitCh;

    private Image secondDigitCh;

    private Image firstDigitIn;

    private Image secondDigitIn;

    private Image firstDigitAg;

    private Image secondDigitAg;

    private Image firstDigitLk;

    private Image secondDigitLk;

    private TMP_Text stDescription;

    private TMP_Text peDescription;

    private TMP_Text enDescription;

    private TMP_Text chDescription;

    private TMP_Text inDescription;

    private TMP_Text agDescription;

    private TMP_Text lkDescription;

    [SerializeField]
    private Sprite[] nums;

    private TMP_Text smallGuns;

    private TMP_Text bigGuns;

    private TMP_Text energyWeapon;

    private TMP_Text unarmed;

    private TMP_Text meleeWeapon;

    private TMP_Text throwing;

    private TMP_Text firstAid;

    private TMP_Text doctor;

    private TMP_Text sneak;

    private TMP_Text lockpick;

    private TMP_Text steal;

    private TMP_Text traps;

    private TMP_Text science;

    private TMP_Text repair;

    private TMP_Text speech;

    private TMP_Text barter;

    private TMP_Text gumbling;

    private TMP_Text outdoorsman;

    private TMP_Text smallGunsName;

    private TMP_Text bigGunsName;

    private TMP_Text energyWeaponName;

    private TMP_Text unarmedName;

    private TMP_Text meleeWeaponName;

    private TMP_Text throwingName;

    private TMP_Text firstAidName;

    private TMP_Text doctorName;

    private TMP_Text sneakName;

    private TMP_Text lockpickName;

    private TMP_Text stealName;

    private TMP_Text trapsName;

    private TMP_Text scienceName;

    private TMP_Text repairName;

    private TMP_Text speechName;

    private TMP_Text barterName;

    private TMP_Text gumblingName;

    private TMP_Text outdoorsmanName;

    private TMP_Text armorClass;

    private TMP_Text actionPoins;

    private TMP_Text carryWeight;

    private TMP_Text melleDamage;

    private TMP_Text damageRes;

    private TMP_Text poisonRes;

    private TMP_Text radRes;

    private TMP_Text sequence;

    private TMP_Text healingRate;

    private TMP_Text critChance;

    private TMP_Text armorClassName;

    private TMP_Text actionPoinsName;

    private TMP_Text carryWeightName;

    private TMP_Text melleDamageName;

    private TMP_Text damageResName;

    private TMP_Text poisonResName;

    private TMP_Text radResName;

    private TMP_Text sequenceName;

    private TMP_Text healingRateName;

    private TMP_Text critChanceName;

    private TMP_Text hp;

    [SerializeField]
    private Image image;

    private TMP_Text label;

    private TMP_Text description;

    private TMP_Text poisoned;

    private TMP_Text radiated;

    private TMP_Text eyeDamage;

    private TMP_Text crippledRightArm;

    private TMP_Text crippledLeftArm;

    private TMP_Text crippledRightLeg;

    private TMP_Text crippledLeftLeg;

    private TMP_Text level;

    private TMP_Text nextLevel;

    private TMP_Text exp;

    private TMP_Text traitsHeader;

    [SerializeField]
    private List<TMP_Text> traits = new();

    private string[] encodingsToTry = { "utf-8", "windows-1251", "koi8-r" };

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip open;

    [SerializeField]
    private AudioClip pressButton;

    [SerializeField]
    private AudioClip done;

    private Image smallGunsSlider;

    private Image bigGunsSlider;

    private Image energyWeaponSlider;

    private Image unarmedSlider;

    private Image meleeWeaponSlider;

    private Image throwingSlider;

    private Image firstAidSlider;

    private Image doctorSlider;

    private Image sneakSlider;

    private Image lockpickSlider;

    private Image stealSlider;

    private Image trapsSlider;

    private Image scienceSlider;

    private Image repairSlider;

    private Image speechSlider;

    private Image barterSlider;

    private Image gumblingSlider;

    private Image outdoorsmanSlider;


    [SerializeField]
    private ChosenOneController chosenOneController;

    private Dictionary<string, string> traitNames = new()
    {
        ["Тяжёлая рука"] = "HEAVYHND",
        ["Одарённый"] = "GIFTED"
    };

    void Start()
    {
        var character = chosenOneController.Character;

        InitLevels();
        InitLabelAndSecription();
        GetMainStatisticsDescriptions();
        GetSkillPoints(character);
        SelectSkills();
        GetDerivedStatistics();
        GetDerivedStatisticsNames();
        InitDerivedStatistics(character);
        InitHp(character);
        InitConditions();
        GetSkillFields();
        GetDigits();
        GetSkillNameFields();
        InitBaseCharacterisrics(character);
        InitSkills(character);
        InitTraits(character);
        MarkMainSkills(character);
        InitSliders();
        UnselectAllSliders();
    }

    private void InitTraits(Character character)
    {
        traitsHeader = GetTextFieldByName("Traits");

        List<Trait> chosenTraits = character.GetChosenTraits();
        for (int i = 0; i < chosenTraits.Count; i++)
        {
            traits[i].text = chosenTraits[i].Description;
        }
    }

    private void InitLevels()
    {
        level = GetTextFieldByName("Level");
        nextLevel = GetTextFieldByName("NextLevel");
        exp = GetTextFieldByName("Exp");
    }

    private void InitLabelAndSecription()
    {
        label = GetTextFieldByName("Label");
        description = GetTextFieldByName("Description");
    }

    private void GetSkillPoints(Character character)
    {
        skillPointFirstDigit = GetImageByName("skillPointFirstDigit");
        skillPointSecondDigit = GetImageByName("skillPointSecondDigit");

        var skillPoints = character.SkillPoints;
        char[] skillPointsDigits = skillPoints.ToString().ToCharArray();

        InitSkillPointsImages(skillPointsDigits);
    }

    private void InitSkillPointsImages(char[] digits)
    {
        if (digits.Length == 1)
        {
            var num = ConvertCharToInt(digits[0]);

            skillPointFirstDigit.sprite = nums[0];
            skillPointSecondDigit.sprite = nums[num];
        }
        if (digits.Length == 2)
        {
            var num1 = ConvertCharToInt(digits[0]);
            var num2 = ConvertCharToInt(digits[1]);

            skillPointFirstDigit.sprite = nums[num1];
            skillPointSecondDigit.sprite = nums[num2];
        }
    }

    private void GetDigits()
    {
        firstDigitSt = GetImageByTag("firstDigitSt");
        secondDigitSt = GetImageByTag("secondDigitSt");
        firstDigitPe = GetImageByTag("firstDigitPe");
        secondDigitPe = GetImageByTag("secondDigitPe");
        firstDigitEn = GetImageByTag("firstDigitEn");
        secondDigitEn = GetImageByTag("secondDigitEn");
        firstDigitCh = GetImageByTag("firstDigitCh");
        secondDigitCh = GetImageByTag("secondDigitCh");
        firstDigitIn = GetImageByTag("firstDigitIn");
        secondDigitIn = GetImageByTag("secondDigitIn");
        firstDigitAg = GetImageByTag("firstDigitAg");
        secondDigitAg = GetImageByTag("secondDigitAg");
        firstDigitLk = GetImageByTag("firstDigitLk");
        secondDigitLk = GetImageByTag("secondDigitLk");
    }

    private void GetMainStatisticsDescriptions()
    {
        stDescription = GetTextFieldByName("stDescription");
        peDescription = GetTextFieldByName("peDescription");
        enDescription = GetTextFieldByName("enDescription");
        chDescription = GetTextFieldByName("chDescription");
        inDescription = GetTextFieldByName("inDescription");
        agDescription = GetTextFieldByName("agDescription");
        lkDescription = GetTextFieldByName("lkDescription");
    }

    private Image GetImageByTag(string tag)
    {
        return GetComponentsInChildren<Image>()
            .Where(gameObject => gameObject.gameObject.CompareTag(tag))
            .FirstOrDefault();
    }

    private Image GetImageByName(string name)
    {
        return GetComponentsInChildren<Image>(true)
            .Where(gameObject => gameObject.gameObject.name.Equals(name))
            .FirstOrDefault();
    }

    private void GetSkillFields()
    {
        smallGuns = GetTextFieldByName("smallGuns");
        bigGuns = GetTextFieldByName("bigGuns");
        energyWeapon = GetTextFieldByName("energyWeapon");
        unarmed = GetTextFieldByName("unarmed");
        meleeWeapon = GetTextFieldByName("meleeWeapons");
        throwing = GetTextFieldByName("throwing");
        firstAid = GetTextFieldByName("firstAid");
        doctor = GetTextFieldByName("doctor");
        sneak = GetTextFieldByName("sneak");
        lockpick = GetTextFieldByName("lockpick");
        steal = GetTextFieldByName("steal");
        traps = GetTextFieldByName("traps");
        science = GetTextFieldByName("science");
        repair = GetTextFieldByName("repair");
        speech = GetTextFieldByName("speech");
        barter = GetTextFieldByName("barter");
        gumbling = GetTextFieldByName("gumbling");
        outdoorsman = GetTextFieldByName("outdoorsman");
    }

    private void GetSkillNameFields()
    {
        smallGunsName = GetTextFieldByName("smallGunsName");
        bigGunsName = GetTextFieldByName("bigGunsName");
        energyWeaponName = GetTextFieldByName("energyWeaponName");
        unarmedName = GetTextFieldByName("unarmedName");
        meleeWeaponName = GetTextFieldByName("meleeWeaponName");
        throwingName = GetTextFieldByName("throwingName");
        firstAidName = GetTextFieldByName("firstAidName");
        doctorName = GetTextFieldByName("doctorName");
        sneakName = GetTextFieldByName("sneakName");
        lockpickName = GetTextFieldByName("lockpickName");
        stealName = GetTextFieldByName("stealName");
        trapsName = GetTextFieldByName("trapsName");
        scienceName = GetTextFieldByName("scienceName");
        repairName = GetTextFieldByName("repairName");
        speechName = GetTextFieldByName("speechName");
        barterName = GetTextFieldByName("barterName");
        gumblingName = GetTextFieldByName("gumblingName");
        outdoorsmanName = GetTextFieldByName("outdoorsmanName");
    }

    private void InitSliders()
    {
        smallGunsSlider = GetImageByName("smallGunsSlider");
        bigGunsSlider = GetImageByName("bigGunsSlider");
        energyWeaponSlider = GetImageByName("energyWeaponSlider");
        unarmedSlider = GetImageByName("unarmedSlider");
        meleeWeaponSlider = GetImageByName("meleeWeaponSlider");
        throwingSlider = GetImageByName("throwingSlider");
        firstAidSlider = GetImageByName("firstAidSlider");
        doctorSlider = GetImageByName("doctorSlider");
        lockpickSlider = GetImageByName("lockpickSlider");
        stealSlider = GetImageByName("stealSlider");
        trapsSlider = GetImageByName("trapsSlider");
        scienceSlider = GetImageByName("scienceSlider");
        repairSlider = GetImageByName("repairSlider");
        speechSlider = GetImageByName("speechSlider");
        barterSlider = GetImageByName("barterSlider");
        gumblingSlider = GetImageByName("gumblingSlider");
        outdoorsmanSlider = GetImageByName("outdoorsmanSlider");
        sneakSlider = GetImageByName("sneakSlider");
    }

    private void MarkMainSkills(Character character)
    {
        List<Skill> mainSkills = character.GetMainSkills();

        foreach (Skill skill in mainSkills)
        {
            var name = skill.SkillName;

            TMP_Text skillField = GetTextFieldByName(name);
            TMP_Text skillNameField = GetTextFieldByName(name + "Name");

            if (ColorUtility.TryParseHtmlString("#E9F6ED", out Color parsedColor))
            {
                if (skillField != null)
                {
                    skillField.color = parsedColor;
                    skillNameField.color = parsedColor;
                }
            }
        }
    }

    private void MarkAllSkillsExpectSelected(string selectedSkillName, Character character)
    {
        List<Skill> mainSkills = character.GetMainSkills();

        foreach (Skill skill in mainSkills)
        {
            var name = skill.SkillName;

            if (!selectedSkillName.Equals(name))
            {
                TMP_Text skillField = GetTextFieldByName(name);
                TMP_Text skillNameField = GetTextFieldByName(name + "Name");

                if (ColorUtility.TryParseHtmlString("#E9F6ED", out Color parsedColor))
                {
                    if (skillField != null)
                    {
                        skillField.color = parsedColor;
                        skillNameField.color = parsedColor;
                    }
                }
            }
        }
    }

    private void InitField(int value, Image firstDigit, Image secondDigit, TMP_Text description)
    {
        char[] digits = value.ToString().ToCharArray();
        if (digits.Length == 1)
        {
            var num = ConvertCharToInt(digits[0]);

            firstDigit.sprite = nums[0];
            secondDigit.sprite = nums[num];
        }
        if (digits.Length == 2)
        {
            var num1 = ConvertCharToInt(digits[0]);
            var num2 = ConvertCharToInt(digits[1]);

            firstDigit.sprite = nums[num1];
            secondDigit.sprite = nums[num2];
        }
        description.text = DefineMainMainStatisticsDescriptionByValue(value);
    }

    private string DefineMainMainStatisticsDescriptionByValue(int value)
    {
        switch (value)
        {
            case 1: return "Труп";
            case 2: return "Плохо";
            case 3: return "Средне";
            case 4: return "Удовл.";
            case 5: return "Обычно";
            case 6: return "Хорошо";
            case 7: return "Очень хор.";
            case 8: return "Круто";
            case 9: return "Герой";
            case 10: return "Бог";
        }
        return null;
    }

    private void InitHp(Character character)
    {
        hp = GetTextFieldByName("HP");
        if (hp != null)
        {
            hp.text += " " + character.HitPoints + "/" + character.CurrentHitPoints;
        }
    }

    private void InitConditions()
    {
        poisoned = GetTextFieldByName("Poisoned");
        radiated = GetTextFieldByName("Radiated");
        eyeDamage = GetTextFieldByName("Eye Damage");
        crippledLeftArm = GetTextFieldByName("Crippled Left Arm");
        crippledRightArm = GetTextFieldByName("Crippled Right Arm");
        crippledRightLeg = GetTextFieldByName("Crippled Right Leg");
        crippledLeftLeg = GetTextFieldByName("Crippled Left Leg");
    }

    private void InitBaseCharacterisrics(Character character)
    {
        var st = character.Strenght;
        var pe = character.Perception;
        var en = character.Endurance;
        var ch = character.Charisma;
        var it = character.Intelligence;
        var ag = character.Agility;
        var lk = character.Luck;

        InitField(st, firstDigitSt, secondDigitSt, stDescription);
        InitField(pe, firstDigitPe, secondDigitPe, peDescription);
        InitField(en, firstDigitEn, secondDigitEn, enDescription);
        InitField(ch, firstDigitCh, secondDigitCh, chDescription);
        InitField(it, firstDigitIn, secondDigitIn, inDescription);
        InitField(ag, firstDigitAg, secondDigitAg, agDescription);
        InitField(lk, firstDigitLk, secondDigitLk, lkDescription);
    }

    private int ConvertCharToInt(char digit)
    {
        return digit - '0';
    }

    private void InitSkills(Character character)
    {
        var smallGuns = character.GetSkillByType(typeof(SmallGuns));
        var bigGuns = character.GetSkillByType(typeof(BigGuns));
        var energyWeapon = character.GetSkillByType(typeof(EnergyWeapons));
        var unarmed = character.GetSkillByType(typeof(Unarmed));
        var meleeWeapons = character.GetSkillByType(typeof(MeleeWeapons));
        var throwing = character.GetSkillByType(typeof(Throwing));
        var firstAid = character.GetSkillByType(typeof(FirstAid));
        var doctor = character.GetSkillByType(typeof(Doctor));
        var sneak = character.GetSkillByType(typeof(Sneak));
        var lockpick = character.GetSkillByType(typeof(Lockpick));
        var steal = character.GetSkillByType(typeof(Steal));
        var traps = character.GetSkillByType(typeof(Traps));
        var science = character.GetSkillByType(typeof(Science));
        var repair = character.GetSkillByType(typeof(Repair));
        var speech = character.GetSkillByType(typeof(Speech));
        var gumbling = character.GetSkillByType(typeof(Gumbling));
        var outdoorsman = character.GetSkillByType(typeof(Outdoorsman));

        SetSkillValue(this.smallGuns, smallGuns.GetValue());
        SetSkillValue(this.bigGuns, bigGuns.GetValue());
        SetSkillValue(this.energyWeapon, energyWeapon.GetValue());
        SetSkillValue(this.unarmed, unarmed.GetValue());
        SetSkillValue(this.meleeWeapon, meleeWeapons.GetValue());
        SetSkillValue(this.throwing, throwing.GetValue());
        SetSkillValue(this.firstAid, firstAid.GetValue());
        SetSkillValue(this.doctor, doctor.GetValue());
        SetSkillValue(this.sneak, sneak.GetValue());
        SetSkillValue(this.lockpick, lockpick.GetValue());
        SetSkillValue(this.steal, steal.GetValue());
        SetSkillValue(this.traps, traps.GetValue());
        SetSkillValue(this.science, science.GetValue());
        SetSkillValue(this.repair, repair.GetValue());
        SetSkillValue(this.speech, speech.GetValue());
        SetSkillValue(this.gumbling, gumbling.GetValue());
        SetSkillValue(this.bigGuns, bigGuns.GetValue());
        SetSkillValue(this.outdoorsman, outdoorsman.GetValue());
    }

    private void SetSkillValue(TMP_Text field, int value)
    {
        if (field != null)
        {
            field.text = value.ToString() + "%";
        }
    }

    private void GetDerivedStatistics()
    {
        armorClass = GetTextFieldByTag("armorClass");
        actionPoins = GetTextFieldByTag("actionPoins");
        carryWeight = GetTextFieldByTag("carryWeight");
        melleDamage = GetTextFieldByTag("melleDamage");
        damageRes = GetTextFieldByTag("damageRes");
        poisonRes = GetTextFieldByTag("poisonRes");
        sequence = GetTextFieldByTag("sequence");
        radRes = GetTextFieldByTag("radRes");
        healingRate = GetTextFieldByTag("healingRate");
        critChance = GetTextFieldByTag("critChance");
    }

    private void GetDerivedStatisticsNames()
    {
        armorClassName = GetTextFieldByTag("armorClassName");
        actionPoinsName = GetTextFieldByTag("actionPoinsName");
        carryWeightName = GetTextFieldByTag("carryWeightName");
        melleDamageName = GetTextFieldByTag("melleDamageName");
        damageResName = GetTextFieldByTag("damageResName");
        poisonResName = GetTextFieldByTag("poisonResName");
        sequenceName = GetTextFieldByTag("sequenceName");
        radResName = GetTextFieldByTag("radResName");
        healingRateName = GetTextFieldByTag("healingRateName");
        critChanceName = GetTextFieldByTag("critChanceName");
    }

    private TMP_Text GetTextFieldByTag(string tag)
    {
        return GetComponentsInChildren<TMP_Text>()
            .Where(gameObject => gameObject.gameObject.CompareTag(tag))
            .FirstOrDefault();
    }

    private TMP_Text GetTextFieldByName(string name)
    {
        return GetComponentsInChildren<TMP_Text>()
            .Where(gameObject => gameObject.gameObject.name.Equals(name))
            .FirstOrDefault();
    }

    private void InitDerivedStatistics(Character character)
    {
        armorClass.text = character.ArmorClass.ToString();
        actionPoins.text = character.ActionPoints.ToString();
        carryWeight.text = character.CarryWeight.ToString();
        melleDamage.text = character.MeleeDamage.ToString();
        damageRes.text = character.GetArmorDamageresistance(Weapons.DamageType.Common).ToString();
        poisonRes.text = character.PoisonResistance.ToString() + "%";
        radRes.text = character.RadiationResistance.ToString() + "%";
        sequence.text = character.Sequence.ToString();
        healingRate.text = character.HealingRate.ToString();
        critChance.text = character.CriticalChance.ToString();
    }

    public void ShowStrenghtDescription()
    {
        UnselectAll();
        InitTextAndImage("Сила",
            "Skilldex/Description/ST",
            "Skilldex/ST");
    }

    public void ShowPerceptionDescription()
    {
        UnselectAll();
        InitTextAndImage("Восприятие",
            "Skilldex/Description/PE",
            "Skilldex/PE");
    }

    public void ShowEnduranceDescription()
    {
        UnselectAll();
        InitTextAndImage("Выносливость",
            "Skilldex/Description/EN",
            "Skilldex/EN");
    }

    public void ShowCharismaDescription()
    {
        UnselectAll();
        InitTextAndImage("Привлекательность",
            "Skilldex/Description/CH",
            "Skilldex/CH");
    }

    public void ShowIntelligenceDescription()
    {
        UnselectAll();
        InitTextAndImage("Интеллект",
            "Skilldex/Description/IN",
            "Skilldex/IN");
    }

    public void ShowAgilityDescription()
    {
        UnselectAll();
        InitTextAndImage("Ловкость",
            "Skilldex/Description/AG",
            "Skilldex/AG");
    }

    public void ShowLuckDescription()
    {
        if (label != null)
        {
            label.text = "Удача";
        }
        if (description != null)
        {
            description.text = LoadTxt("Skilldex/Description/LK");
        }
        if (image != null)
        {
            var sprite = LoadSprite("Skilldex/LK");
            image.sprite = sprite;
        }

        SelectFilelds(bigGuns, bigGunsName);
        InitTextAndImage("Удача",
            "Skilldex/Description/LK",
            "Skilldex/LK");
    }

    private Sprite LoadSprite(string name)
    {
        Texture2D texture = Resources.Load<Texture2D>(name);

        if (texture != null)
        {
            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

        }
        return null;
    }

    public void SelectSmallGuns()
    {
        UnselectAllSliders();
        SelectFilelds(smallGuns, smallGunsName);
        InitTextAndImage("Легкое оружие",
            "Skills/Description/GUNSML",
            "Skills/GUNSML");
        MarkAllSkillsExpectSelected("smallGuns", chosenOneController.Character);
        smallGunsSlider.gameObject.SetActive(true);
    }

    public void SelectBigGuns()
    {
        UnselectAllSliders();
        SelectFilelds(bigGuns, bigGunsName);
        InitTextAndImage("Тяжёлое оружие",
            "Skills/Description/GUNBIG",
            "Skills/GUNBIG");
        MarkAllSkillsExpectSelected("bigGuns", chosenOneController.Character);
        bigGunsSlider.gameObject.SetActive(true);
    }

    public void SelectEnergyWeapon()
    {
        UnselectAllSliders();
        SelectFilelds(energyWeapon, energyWeaponName);
        InitTextAndImage("Энергетическое оружие",
            "Skills/Description/ENERGYWEAPON",
            "Skills/ENERGYWEAPON");
        MarkAllSkillsExpectSelected("energyWeapon", chosenOneController.Character);
        energyWeaponSlider.gameObject.SetActive(true);
    }

    public void SelectUnarmed()
    {
        UnselectAllSliders();
        SelectFilelds(unarmed, unarmedName);
        InitTextAndImage("Без оружия",
            "Skills/Description/UNARMED",
            "Skills/UNARMED");
        MarkAllSkillsExpectSelected("unarmed", chosenOneController.Character);
        unarmedSlider.gameObject.SetActive(true);
    }

    public void SelectMeleeWeapons()
    {
        UnselectAllSliders();
        SelectFilelds(meleeWeapon, meleeWeaponName);
        InitTextAndImage("Холодное оружие",
            "Skills/Description/MELEE",
            "Skills/MELEE");
        MarkAllSkillsExpectSelected("meleeWeapon", chosenOneController.Character);
        meleeWeaponSlider.gameObject.SetActive(true);
    }

    public void SelectThrowing()
    {
        UnselectAllSliders();
        SelectFilelds(throwing, throwingName);
        InitTextAndImage("Метание",
            "Skills/Description/THROWING",
            "Skills/THROWING");
        MarkAllSkillsExpectSelected("throwing", chosenOneController.Character);
        throwingSlider.gameObject.SetActive(true);
    }

    public void SelectFirstAid()
    {
        UnselectAllSliders();
        SelectFilelds(firstAid, firstAidName);
        InitTextAndImage("Первая помощь",
            "Skills/Description/FIRSTAID",
            "Skills/FIRSTAID");
        MarkAllSkillsExpectSelected("firstAid", chosenOneController.Character);
        firstAidSlider.gameObject.SetActive(true);
    }

    public void SelectDoctor()
    {
        UnselectAllSliders();
        SelectFilelds(doctor, doctorName);
        InitTextAndImage("Доктор",
            "Skills/Description/DOCTOR",
            "Skills/DOCTOR");
        MarkAllSkillsExpectSelected("doctor", chosenOneController.Character);
        doctorSlider.gameObject.SetActive(true);
    }

    public void SelectSneak()
    {
        UnselectAllSliders();
        SelectFilelds(sneak, sneakName);
        InitTextAndImage("Скрытность",
            "Skills/Description/SNEAK",
            "Skills/SNEAK");
        MarkAllSkillsExpectSelected("sneak", chosenOneController.Character);
        sneakSlider.gameObject.SetActive(true);
    }

    public void SelectLockpick()
    {
        UnselectAllSliders();
        SelectFilelds(lockpick, lockpickName);
        InitTextAndImage("Взлом",
            "Skills/Description/LOCKPICK",
            "Skills/LOCKPICK");
        MarkAllSkillsExpectSelected("lockpick", chosenOneController.Character);
        lockpickSlider.gameObject.SetActive(true);
    }

    public void SelectSteal()
    {
        UnselectAllSliders();
        SelectFilelds(steal, stealName);
        InitTextAndImage("Кража",
            "Skills/Description/STEAL",
            "Skills/STEAL");
        MarkAllSkillsExpectSelected("steal", chosenOneController.Character);
        stealSlider.gameObject.SetActive(true);
    }

    public void SelectTraps()
    {
        UnselectAllSliders();
        SelectFilelds(traps, trapsName);
        InitTextAndImage("Ловушки",
            "Skills/Description/TRAPS",
            "Skills/TRAPS");
        MarkAllSkillsExpectSelected("traps", chosenOneController.Character);
        trapsSlider.gameObject.SetActive(true);
    }

    public void SelectScience()
    {
        UnselectAllSliders();
        SelectFilelds(science, scienceName);
        InitTextAndImage("Наука",
            "Skills/Description/REPAIR",
            "Skills/SCIENCE");
        MarkAllSkillsExpectSelected("science", chosenOneController.Character);
        scienceSlider.gameObject.SetActive(true);
    }
    public void SelectRepair()
    {
        UnselectAllSliders();
        SelectFilelds(repair, repairName);
        InitTextAndImage("Ремонт",
            "Skills/Description/REPAIR",
            "Skills/REPAIR");
        MarkAllSkillsExpectSelected("repair", chosenOneController.Character);
        repairSlider.gameObject.SetActive(true);
    }

    public void SelectSpeech()
    {
        UnselectAllSliders();
        SelectFilelds(speech, speechName);
        InitTextAndImage("Красноречие",
            "Skills/Description/SPEECH",
            "Skills/SPEECH");
        MarkAllSkillsExpectSelected("speech", chosenOneController.Character);
        speechSlider.gameObject.SetActive(true);
    }

    public void SelectBarter()
    {
        UnselectAllSliders();
        SelectFilelds(barter, barterName);
        InitTextAndImage("Бартер",
            "Skills/Description/BARTER",
            "Skills/BARTER");
        MarkAllSkillsExpectSelected("barter", chosenOneController.Character);
        barterSlider.gameObject.SetActive(true);
    }

    public void SelectGumbling()
    {
        UnselectAllSliders();
        SelectFilelds(gumbling, gumblingName);
        InitTextAndImage("Азартные игры",
            "Skills/Description/GAMBLING",
            "Skills/GAMBLING");
        MarkAllSkillsExpectSelected("gumbling", chosenOneController.Character);
        gumblingSlider.gameObject.SetActive(true);
    }

    public void SelectOutdoorsman()
    {
        UnselectAllSliders();
        SelectFilelds(outdoorsman, outdoorsmanName);
        InitTextAndImage("Натуралист",
            "Skills/Description/OUTDOORS",
            "Skills/OUTDOORS");
        MarkAllSkillsExpectSelected("outdoorsman", chosenOneController.Character);
        outdoorsmanSlider.gameObject.SetActive(true);
    }

    public void SelectSkills()
    {
        InitTextAndImage("Навыки",
            "Skills/Description/SKILLS",
            "Skills/SKILLS");
    }

    public void SelectArmorClass()
    {
        SelectFilelds(armorClass, armorClassName);
        InitTextAndImage("Класс брони",
            "DerivedStatistics/Description/ARMORCLS",
            "DerivedStatistics/ARMORCLS");
    }

    public void SelectActionPoints()
    {
        SelectFilelds(actionPoins, actionPoinsName);
        InitTextAndImage("Очки действий",
            "DerivedStatistics/Description/ACTIONPT",
            "DerivedStatistics/ACTIONPT");
    }

    public void SelectCarryWeigh()
    {
        SelectFilelds(carryWeight, carryWeightName);
        InitTextAndImage("Максимальный груз",
            "DerivedStatistics/Description/CARRYAMT",
            "DerivedStatistics/CARRYAMT");
    }

    public void SelectDamageRes()
    {
        SelectFilelds(damageRes, damageResName);
        InitTextAndImage("Сопротивляемость урону",
            "DerivedStatistics/Description/DAMRESIS",
            "DerivedStatistics/DAMRESIS");
    }

    public void SelectMelleDamage()
    {
        SelectFilelds(melleDamage, melleDamageName);
        InitTextAndImage("Урон в ближнем бою",
            "DerivedStatistics/Description/MELEEDAM",
            "DerivedStatistics/MELEEDAM");
    }

    public void SelectPoisonRes()
    {
        SelectFilelds(poisonRes, poisonResName);
        InitTextAndImage("Сопротивляемость ядам",
            "DerivedStatistics/Description/POISNRES",
            "DerivedStatistics/POISNRES");
    }

    public void SelectRadRes()
    {
        SelectFilelds(radRes, radResName);
        InitTextAndImage("Сопротивляемость радиации",
            "DerivedStatistics/Description/RADRESIS",
            "DerivedStatistics/RADRESIS");
    }

    public void SelectSkillPoints()
    {
        UnselectAll();
        InitTextAndImage("Очки навыков",
            "Skills/Description/SKILLPNT",
            "Skills/SKILLPOINTS");
    }

    private void SelectFilelds(TMP_Text field, TMP_Text nameField)
    {
        UnselectAll();

        SetSelected(field);
        SetSelected(nameField);
    }

    private void SelectFilelds(TMP_Text field)
    {
        UnselectAll();
        SetSelected(field);
    }

    private void UnselectAll()
    {
        UnselectTraits();
        UnselectSkills();
        UnselectDerivedStatistics();
        UnselectConditions();
        SetUnSelected(hp);
        UnselectLevels();
    }

    private void UnselectTraits()
    {
        SetUnSelected(traitsHeader);
        foreach (TMP_Text trait in traits)
        {
            SetUnSelected(trait);
        }
    }

    private void InitTextAndImage(string labelText, string descritionPath, string spritePath)
    {
        if (label != null)
        {
            label.text = labelText;
        }
        if (description != null)
        {
            description.text = LoadTxt(descritionPath);
        }
        if (image != null)
        {
            var sprite = LoadSprite(spritePath);
            image.sprite = sprite;
        }
    }

    public void SelectSequence()
    {
        SelectFilelds(sequence, sequenceName);
        InitTextAndImage("Реакция",
            "DerivedStatistics/Description/SEQUENCE",
            "DerivedStatistics/SEQUENCE");
    }

    public void SelectHpRate()
    {
        SelectFilelds(healingRate, healingRateName);
        InitTextAndImage("Скорость восстановления",
            "DerivedStatistics/Description/HEALRATE",
            "DerivedStatistics/HEALRATE");
    }

    public void SelectCritChance()
    {
        SelectFilelds(critChance, critChanceName);
        InitTextAndImage("Шанс критического попадания",
            "DerivedStatistics/Description/CRITCHNC",
            "DerivedStatistics/CRITCHNC");
    }

    private void SetSelected(TMP_Text field)
    {
        if (field != null)
        {
            if (ColorUtility.TryParseHtmlString("#E3E195", out Color parsedColor))
            {
                field.color = parsedColor;
            }
        }
    }

    private void SetUnSelected(TMP_Text field)
    {
        if (field != null)
        {
            if (ColorUtility.TryParseHtmlString("#08FF4E", out Color parsedColor))
            {
                field.color = parsedColor;
            }
        }
    }

    private void UnselectLevels()
    {
        SetUnSelected(level);
        SetUnSelected(nextLevel);
        SetUnSelected(exp);
    }

    private void UnselectAllSliders()
    {
        smallGunsSlider.gameObject.SetActive(false);
        bigGunsSlider.gameObject.SetActive(false);
        energyWeaponSlider.gameObject.SetActive(false);
        unarmedSlider.gameObject.SetActive(false);
        meleeWeaponSlider.gameObject.SetActive(false);
        throwingSlider.gameObject.SetActive(false);
        firstAidSlider.gameObject.SetActive(false);
        doctorSlider.gameObject.SetActive(false);
        sneakSlider.gameObject.SetActive(false);
        lockpickSlider.gameObject.SetActive(false);
        stealSlider.gameObject.SetActive(false);
        trapsSlider.gameObject.SetActive(false);
        scienceSlider.gameObject.SetActive(false);
        repairSlider.gameObject.SetActive(false);
        speechSlider.gameObject.SetActive(false);
        barterSlider.gameObject.SetActive(false);
        outdoorsmanSlider.gameObject.SetActive(false);
        gumblingSlider.gameObject.SetActive(false);
    }

    private void UnselectSkills()
    {
        SetUnSelected(smallGuns);
        SetUnSelected(smallGunsName);

        SetUnSelected(bigGuns);
        SetUnSelected(bigGunsName);

        SetUnSelected(energyWeapon);
        SetUnSelected(energyWeaponName);

        SetUnSelected(unarmed);
        SetUnSelected(unarmedName);

        SetUnSelected(meleeWeapon);
        SetUnSelected(meleeWeaponName);

        SetUnSelected(throwing);
        SetUnSelected(throwingName);

        SetUnSelected(firstAid);
        SetUnSelected(firstAidName);

        SetUnSelected(doctor);
        SetUnSelected(doctorName);

        SetUnSelected(sneak);
        SetUnSelected(sneakName);

        SetUnSelected(lockpick);
        SetUnSelected(lockpickName);

        SetUnSelected(steal);
        SetUnSelected(stealName);

        SetUnSelected(traps);
        SetUnSelected(trapsName);

        SetUnSelected(science);
        SetUnSelected(scienceName);

        SetUnSelected(repair);
        SetUnSelected(repairName);

        SetUnSelected(speech);
        SetUnSelected(speechName);

        SetUnSelected(barter);
        SetUnSelected(barterName);

        SetUnSelected(gumbling);
        SetUnSelected(gumblingName);

        SetUnSelected(outdoorsman);
        SetUnSelected(outdoorsmanName);
    }

    private void UnselectDerivedStatistics()
    {
        SetUnSelected(armorClass);
        SetUnSelected(armorClassName);

        SetUnSelected(actionPoins);
        SetUnSelected(actionPoinsName);

        SetUnSelected(carryWeight);
        SetUnSelected(carryWeightName);

        SetUnSelected(melleDamage);
        SetUnSelected(melleDamageName);

        SetUnSelected(armorClass);
        SetUnSelected(armorClassName);

        SetUnSelected(poisonRes);
        SetUnSelected(poisonResName);

        SetUnSelected(radRes);
        SetUnSelected(radResName);

        SetUnSelected(healingRate);
        SetUnSelected(healingRateName);

        SetUnSelected(sequence);
        SetUnSelected(sequenceName);

        SetUnSelected(critChance);
        SetUnSelected(critChanceName);

        SetUnSelected(damageRes);
        SetUnSelected(damageResName);
    }

    private void UnselectConditions()
    {
        SetConditionUnSelected(poisoned);
        SetConditionUnSelected(radiated);
        SetConditionUnSelected(eyeDamage);
        SetConditionUnSelected(crippledRightArm);
        SetConditionUnSelected(crippledLeftArm);
        SetConditionUnSelected(crippledRightLeg);
        SetConditionUnSelected(crippledLeftLeg);
    }

    private void SetConditionUnSelected(TMP_Text field)
    {
        if (field != null)
        {
            if (ColorUtility.TryParseHtmlString("#346A43", out Color parsedColor))
            {
                field.color = parsedColor;
            }
        }
    }

    private string LoadTxt(string name)
    {
        UnityEngine.TextAsset textAsset = Resources.Load<UnityEngine.TextAsset>(name);

        if (textAsset != null)
        {
            foreach (string encodingName in encodingsToTry)
            {
                try
                {
                    Encoding encoding = Encoding.GetEncoding(encodingName);
                    string content = encoding.GetString(textAsset.bytes);

                    if (IsCyrillicTextValid(content))
                    {
                        return content;
                    }
                }
                catch
                {
                    // Пропускаем невалидные кодировки
                }
            }
        }
        return null;
    }

    private bool IsCyrillicTextValid(string text)
    {
        foreach (char c in text)
        {
            if (c >= 'А' && c <= 'я') return true;
            if (c == 'ё' || c == 'Ё') return true;
        }
        return false;
    }

    public void SelectHp()
    {
        SelectFilelds(hp);
        InitTextAndImage("Очки здоровья",
            "Conditions/Description/HITPOINT",
            "Conditions/HITPOINT");
    }

    public void SelectPoisoned()
    {
        SelectFilelds(poisoned);
        InitTextAndImage("Отравление",
            "Conditions/Description/POISONED",
            "Conditions/POISONED");
    }

    public void SelectRadiated()
    {
        SelectFilelds(radiated);
        InitTextAndImage("Лучевая болезнь",
            "Conditions/Description/RADIATED",
            "Conditions/RADIATED");
    }

    public void SelectEyeDamage()
    {
        SelectFilelds(eyeDamage);
        InitTextAndImage("Повреждение глаз",
            "Conditions/Description/EYEDAMAG",
            "Conditions/EYEDAMAG");
    }
    public void SelectCrippledLeftArm()
    {
        SelectFilelds(crippledLeftArm);
        InitTextAndImage("Сломана левая рука",
            "Conditions/Description/ARMLEFT",
            "Conditions/ARMLEFT");
    }

    public void SelectCrippledRighttArm()
    {
        SelectFilelds(crippledRightArm);
        InitTextAndImage("Сломана правая рука",
            "Conditions/Description/ARMRIGHT",
            "Conditions/ARMRIGHT");
    }

    public void SelectCrippledRightLeg()
    {
        SelectFilelds(crippledRightLeg);
        InitTextAndImage("Сломана правая нога",
            "Conditions/Description/LEGRIGHT",
            "Conditions/LEGRIGHT");
    }

    public void SelectCrippledLeftLeg()
    {
        SelectFilelds(crippledLeftLeg);
        InitTextAndImage("Сломана левая нога",
            "Conditions/Description/LEGLEFT",
            "Conditions/LEGLEFT");
    }

    public void SelectLevel()
    {
        SelectFilelds(level);
        InitTextAndImage("Уровень персонажа",
            "Level/Description/LEVEL",
            "Level/LEVEL");
    }

    public void SelectExp()
    {
        SelectFilelds(exp);
        InitTextAndImage("Очки опыта",
            "Level/Description/EXPER",
            "Level/EXPER");
    }

    public void SelectNextLevel()
    {
        SelectFilelds(nextLevel);
        InitTextAndImage("Следующий уровень",
            "Level/Description/LEVELNXT",
            "Level/LEVELNXT");
    }

    public void SelectTraits()
    {
        SelectFilelds(traitsHeader);
        InitTextAndImage("Особенности",
            "Traits/Description/TRAITS",
            "Traits/TRAITS");
    }

    public void SelectTrait(TMP_Text trait)
    {
        SelectFilelds(trait);
        var traitName = traitNames[trait.text];
        InitTextAndImage(trait.text,
            "Traits/Description/" + traitName,
            "Traits/" + traitName);
    }

    public void SelectPerks()
    {
        InitTextAndImage("Способности",
            "Perks/Description/PERKS",
            "Perks/PERKS");
    }

    public void Enable()
    {
        gameObject.SetActive(true);

        if (audioSource != null)
        {
            if (pressButton != null)
            {
                PlayAudioClipOneShot(pressButton);
            }
            if (open != null)
            {
                PlayAudioClipOneShot(open);
            }
        }
    }

    public void Disable()
    {
        if (open != null)
        {
            PlayAudioClipOneShot(done);
        }
        gameObject.SetActive(false);
    }

    private void PlayAudioClipOneShot(AudioClip audioClip)
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void IncreaseSmallGuns()
    {
        IncreaseSkill("smallGuns");
        var character = chosenOneController.Character;
        var smallGuns = character.GetSkillByType(typeof(SmallGuns));
        SetSkillValue(this.smallGuns, smallGuns.GetValue());
    }

    public void IncreaseBarter()
    {
        IncreaseSkill("barter");
        var character = chosenOneController.Character;
        var barter = character.GetSkillByType(typeof(Barter));
        SetSkillValue(this.barter, barter.GetValue());
    }

    public void IncreaseBigGuns()
    {
        IncreaseSkill("bigGuns");
        var character = chosenOneController.Character;
        var bigGuns = character.GetSkillByType(typeof(BigGuns));
        SetSkillValue(this.bigGuns, bigGuns.GetValue());
    }

    public void IncreaseDoctor()
    {
        IncreaseSkill("doctor");
        var character = chosenOneController.Character;
        var doctor = character.GetSkillByType(typeof(Doctor));
        SetSkillValue(this.doctor, doctor.GetValue());
    }

    public void IncreaseEnergyWeapon()
    {
        IncreaseSkill("energyWeapon");
        var character = chosenOneController.Character;
        var energyWeapon = character.GetSkillByType(typeof(EnergyWeapons));
        SetSkillValue(this.energyWeapon, energyWeapon.GetValue());
    }

    public void IncreaseFirstAid()
    {
        IncreaseSkill("firstAid");
        var character = chosenOneController.Character;
        var firstAid = character.GetSkillByType(typeof(FirstAid));
        SetSkillValue(this.firstAid, firstAid.GetValue());
    }

    public void IncreaseGumbling()
    {
        IncreaseSkill("gumbling");
        var character = chosenOneController.Character;
        var gumbling = character.GetSkillByType(typeof(Gumbling));
        SetSkillValue(this.gumbling, gumbling.GetValue());
    }

    public void IncreaseLockpick()
    {
        IncreaseSkill("lockpick");
        var character = chosenOneController.Character;
        var lockpick = character.GetSkillByType(typeof(Lockpick));
        SetSkillValue(this.lockpick, lockpick.GetValue());
    }

    public void IncreaseMeleeWeapon()
    {
        IncreaseSkill("meleeWeapon");
        var character = chosenOneController.Character;
        var meleeWeapon = character.GetSkillByType(typeof(MeleeWeapons));
        SetSkillValue(this.meleeWeapon, meleeWeapon.GetValue());
    }

    public void IncreaseOutdoorsman()
    {
        IncreaseSkill("outdoorsman");
        var character = chosenOneController.Character;
        var outdoorsman = character.GetSkillByType(typeof(Outdoorsman));
        SetSkillValue(this.outdoorsman, outdoorsman.GetValue());
    }

    public void IncreaseRepair()
    {
        IncreaseSkill("repair");
        var character = chosenOneController.Character;
        var repair = character.GetSkillByType(typeof(Repair));
        SetSkillValue(this.repair, repair.GetValue());
    }

    public void IncreaseScience()
    {
        IncreaseSkill("science");
        var character = chosenOneController.Character;
        var science = character.GetSkillByType(typeof(Science));
        SetSkillValue(this.science, science.GetValue());
    }

    public void IncreaseSneak()
    {
        IncreaseSkill("sneak");
        var character = chosenOneController.Character;
        var sneak = character.GetSkillByType(typeof(Sneak));
        SetSkillValue(this.sneak, sneak.GetValue());
    }

    public void IncreaseSpeech()
    {
        IncreaseSkill("speech");
        var character = chosenOneController.Character;
        var speech = character.GetSkillByType(typeof(Speech));
        SetSkillValue(this.speech, speech.GetValue());
    }

    public void IncreasSteal()
    {
        IncreaseSkill("steal");
        var character = chosenOneController.Character;
        var steal = character.GetSkillByType(typeof(Steal));
        SetSkillValue(this.steal, steal.GetValue());
    }

    public void IncreaseThrowing()
    {
        IncreaseSkill("throwing");
        var character = chosenOneController.Character;
        var throwing = character.GetSkillByType(typeof(Throwing));
        SetSkillValue(this.throwing, throwing.GetValue());
    }

    public void IncreaseTraps()
    {
        IncreaseSkill("traps");
        var character = chosenOneController.Character;
        var traps = character.GetSkillByType(typeof(Traps));
        SetSkillValue(this.traps, traps.GetValue());
    }

    public void IncreaseUnarmed()
    {
        IncreaseSkill("unarmed");
        var character = chosenOneController.Character;
        var unarmed = character.GetSkillByType(typeof(Unarmed));
        SetSkillValue(this.unarmed, unarmed.GetValue());
    }

    public void DecreaseSmallGuns()
    {
        DecreaseSkill("smallGuns");
        var character = chosenOneController.Character;
        var smallGuns = character.GetSkillByType(typeof(SmallGuns));
        SetSkillValue(this.smallGuns, smallGuns.GetValue());
    }

    public void DecreaseBarter()
    {
        DecreaseSkill("barter");
        var character = chosenOneController.Character;
        var barter = character.GetSkillByType(typeof(Barter));
        SetSkillValue(this.barter, barter.GetValue());
    }

    public void DecreaseBigGuns()
    {
        DecreaseSkill("bigGuns");
        var character = chosenOneController.Character;
        var bigGuns = character.GetSkillByType(typeof(BigGuns));
        SetSkillValue(this.bigGuns, bigGuns.GetValue());
    }

    public void DecreaseDoctor()
    {
        DecreaseSkill("doctor");
        var character = chosenOneController.Character;
        var doctor = character.GetSkillByType(typeof(Doctor));
        SetSkillValue(this.doctor, doctor.GetValue());
    }

    public void DecreaseEnergyWeapon()
    {
        DecreaseSkill("energyWeapon");
        var character = chosenOneController.Character;
        var energyWeapon = character.GetSkillByType(typeof(EnergyWeapons));
        SetSkillValue(this.energyWeapon, energyWeapon.GetValue());
    }

    public void DecreaseFirstAid()
    {
        DecreaseSkill("firstAid");
        var character = chosenOneController.Character;
        var firstAid = character.GetSkillByType(typeof(FirstAid));
        SetSkillValue(this.firstAid, firstAid.GetValue());
    }

    public void DecreaseGumbling()
    {
        DecreaseSkill("gumbling");
        var character = chosenOneController.Character;
        var gumbling = character.GetSkillByType(typeof(Gumbling));
        SetSkillValue(this.gumbling, gumbling.GetValue());
    }

    public void DecreaseLockpick()
    {
        DecreaseSkill("lockpick");
        var character = chosenOneController.Character;
        var lockpick = character.GetSkillByType(typeof(Lockpick));
        SetSkillValue(this.lockpick, lockpick.GetValue());
    }

    public void DecreaseMeleeWeapon()
    {
        DecreaseSkill("meleeWeapon");
        var character = chosenOneController.Character;
        var meleeWeapon = character.GetSkillByType(typeof(MeleeWeapons));
        SetSkillValue(this.meleeWeapon, meleeWeapon.GetValue());
    }

    public void DecreaseOutdoorsman()
    {
        DecreaseSkill("outdoorsman");
        var character = chosenOneController.Character;
        var outdoorsman = character.GetSkillByType(typeof(Outdoorsman));
        SetSkillValue(this.outdoorsman, outdoorsman.GetValue());
    }

    public void DecreaseRepair()
    {
        DecreaseSkill("repair");
        var character = chosenOneController.Character;
        var repair = character.GetSkillByType(typeof(Repair));
        SetSkillValue(this.repair, repair.GetValue());
    }

    public void DecreaseScience()
    {
        DecreaseSkill("science");
        var character = chosenOneController.Character;
        var science = character.GetSkillByType(typeof(Science));
        SetSkillValue(this.science, science.GetValue());
    }

    public void DecreaseSneak()
    {
        DecreaseSkill("sneak");
        var character = chosenOneController.Character;
        var sneak = character.GetSkillByType(typeof(Sneak));
        SetSkillValue(this.sneak, sneak.GetValue());
    }

    public void DecreaseSpeech()
    {
        DecreaseSkill("speech");
        var character = chosenOneController.Character;
        var speech = character.GetSkillByType(typeof(Speech));
        SetSkillValue(this.speech, speech.GetValue());
    }

    public void DecreaseSteal()
    {
        DecreaseSkill("steal");
        var character = chosenOneController.Character;
        var steal = character.GetSkillByType(typeof(Steal));
        SetSkillValue(this.steal, steal.GetValue());
    }

    public void DecreaseThrowing()
    {
        DecreaseSkill("throwing");
        var character = chosenOneController.Character;
        var throwing = character.GetSkillByType(typeof(Throwing));
        SetSkillValue(this.throwing, throwing.GetValue());
    }

    public void DecreaseTraps()
    {
        DecreaseSkill("traps");
        var character = chosenOneController.Character;
        var traps = character.GetSkillByType(typeof(Traps));
        SetSkillValue(this.traps, traps.GetValue());
    }

    public void DecreaseUnarmed()
    {
        DecreaseSkill("unarmed");
        var character = chosenOneController.Character;
        var unarmed = character.GetSkillByType(typeof(Unarmed));
        SetSkillValue(this.unarmed, unarmed.GetValue());
    }

    private void IncreaseSkill(string skillName)
    {
        PlayAudioClipOneShot(done);

        var character = chosenOneController.Character;
        if (character.IsSkillPointsEmpty())
        {
            if (skillWarningBox != null)
            {
                skillWarningBox.gameObject.SetActive(true);
            }
            if (skillWarningField != null)
            {
                skillWarningField.text = "Нет свободных очков навыков.";
            }
        }
        else
        {
            character.IncreaseSkill(skillName);
            GetSkillPoints(character);
        }
    }

    private void DecreaseSkill(string skillName)
    {
        PlayAudioClipOneShot(done);

        var character = chosenOneController.Character;

        if (character.SkillHasEmptyValue(skillName))
        {
            if (skillWarningBox != null)
            {
                skillWarningBox.gameObject.SetActive(true);
            }
            if (skillWarningField != null)
            {
                skillWarningField.text = "Навык на минимальном уровне.";
            }
        }
        else
        {
            character.DecreaseSkill(skillName);
            GetSkillPoints(character);
        }
    }

    public void DisableSkillWarning()
    {
        PlayAudioClipOneShot(done);
        if (skillWarningBox != null)
        {
            skillWarningBox.gameObject.SetActive(false);
        }
    }
}
