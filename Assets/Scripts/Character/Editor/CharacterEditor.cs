using Skills;
using System.Collections.Generic;
using Traits;
using UnityEditor;
using UnityEngine;

public class CharacterEditor : EditorWindow
{
    private string skillsFolder = "Assets";
    private string traitsFolder = "Assets";
    private string characterFolder = "Assets";

    [MenuItem("Window/CharacterEditor")]
    public static void ShowWindow()
    {
        GetWindow<CharacterEditor>("CharacterEditor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Enter Skills folder:");
        skillsFolder = EditorGUILayout.TextField(skillsFolder);

        GUILayout.Label("Enter Traits folder:");
        traitsFolder = EditorGUILayout.TextField(traitsFolder);

        GUILayout.Label("Enter Character folder:");
        characterFolder = EditorGUILayout.TextField(characterFolder);
        if (GUILayout.Button("Create New"))
        {
            List<Skill> skills = CreateSkills();
            List<Trait> traits = CreateTraits();

            Character character = ScriptableObject.CreateInstance<Character>();

            character.InitStats();
            character.InitSkills(skills);
            character.Traits = traits;

            AssetDatabase.CreateAsset(character, characterFolder + "/Character.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private List<Skill> CreateSkills()
    {
        var meleeWeapons = ScriptableObject.CreateInstance<MeleeWeapons>();
        var smallGuns = ScriptableObject.CreateInstance<SmallGuns>();
        var bigGuns = ScriptableObject.CreateInstance<BigGuns>();
        var energyWeapons = ScriptableObject.CreateInstance<EnergyWeapons>();
        var unarmed = ScriptableObject.CreateInstance<Unarmed>();
        var throwing = ScriptableObject.CreateInstance<Throwing>();
        var firstAid = ScriptableObject.CreateInstance<FirstAid>();
        var doctor = ScriptableObject.CreateInstance<Doctor>();
        var sneak = ScriptableObject.CreateInstance<Sneak>();
        var lockpick = ScriptableObject.CreateInstance<Lockpick>();
        var steal = ScriptableObject.CreateInstance<Steal>();
        var traps = ScriptableObject.CreateInstance<Traps>();
        var science = ScriptableObject.CreateInstance<Science>();
        var repair = ScriptableObject.CreateInstance<Repair>();
        var speech = ScriptableObject.CreateInstance<Speech>();
        var barter = ScriptableObject.CreateInstance<Barter>();
        var gumbling = ScriptableObject.CreateInstance<Gumbling>();
        var outdoorsman = ScriptableObject.CreateInstance<Outdoorsman>();

        AssetDatabase.CreateAsset(meleeWeapons, skillsFolder + "/MeleeWeapons.asset");
        AssetDatabase.CreateAsset(smallGuns, skillsFolder + "/SmallGuns.asset");
        AssetDatabase.CreateAsset(bigGuns, skillsFolder + "/BigGuns.asset");
        AssetDatabase.CreateAsset(energyWeapons, skillsFolder + "/EnergyWeapons.asset");
        AssetDatabase.CreateAsset(unarmed, skillsFolder + "/Unarmed.asset");
        AssetDatabase.CreateAsset(throwing, skillsFolder + "/Throwing.asset");
        AssetDatabase.CreateAsset(firstAid, skillsFolder + "/FirstAid.asset");
        AssetDatabase.CreateAsset(doctor, skillsFolder + "/Doctor.asset");
        AssetDatabase.CreateAsset(sneak, skillsFolder + "/Sneak.asset");
        AssetDatabase.CreateAsset(lockpick, skillsFolder + "/Lockpick.asset");
        AssetDatabase.CreateAsset(steal, skillsFolder + "/Steal.asset");
        AssetDatabase.CreateAsset(traps, skillsFolder + "/Traps.asset");
        AssetDatabase.CreateAsset(science, skillsFolder + "/Science.asset");
        AssetDatabase.CreateAsset(repair, skillsFolder + "/Repair.asset");
        AssetDatabase.CreateAsset(speech, skillsFolder + "/Speech.asset");
        AssetDatabase.CreateAsset(barter, skillsFolder + "/Barter.asset");
        AssetDatabase.CreateAsset(gumbling, skillsFolder + "/Gumbling.asset");
        AssetDatabase.CreateAsset(outdoorsman, skillsFolder + "/Outdoorsman.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return new List<Skill>
        {
            meleeWeapons,
            smallGuns,
            bigGuns,
            energyWeapons,
            unarmed,
            throwing,
            firstAid,
            doctor,
            sneak,
            lockpick,
            steal,
            traps,
            science,
            repair,
            speech,
            barter,
            gumbling,
            outdoorsman
        };
    }

    private List<Trait> CreateTraits()
    {
        var fastMetabolism = ScriptableObject.CreateInstance<FastMetabolism>();
        var bruiser = ScriptableObject.CreateInstance<Bruiser>();
        var smallFrame = ScriptableObject.CreateInstance<SmallFrame>();
        var gifted = ScriptableObject.CreateInstance<Gifted>();
        var oneHander = ScriptableObject.CreateInstance<OneHander>();
        var finesse = ScriptableObject.CreateInstance<Finesse>();
        var kamikaze = ScriptableObject.CreateInstance<Kamikaze>();
        var heavyHanded = ScriptableObject.CreateInstance<HeavyHanded>();
        var fastShot = ScriptableObject.CreateInstance<FastShot>();
        var bloodyMess = ScriptableObject.CreateInstance<BloodyMess>();
        var jinxed = ScriptableObject.CreateInstance<Jinxed>();
        var goodNatured = ScriptableObject.CreateInstance<GoodNatured>();
        var chemReliant = ScriptableObject.CreateInstance<ÑhemReliant>();
        var chemResistant = ScriptableObject.CreateInstance<ÑhemResistant>();
        var sexAppeal = ScriptableObject.CreateInstance<SexAppeal>();
        var skilled = ScriptableObject.CreateInstance<Skilled>();

        AssetDatabase.CreateAsset(fastMetabolism, traitsFolder + "/FastMetabolism.asset");
        AssetDatabase.CreateAsset(bruiser, traitsFolder + "/Bruiser.asset");
        AssetDatabase.CreateAsset(smallFrame, traitsFolder + "/SmallFrame.asset");
        AssetDatabase.CreateAsset(gifted, traitsFolder + "/Gifted.asset");
        AssetDatabase.CreateAsset(oneHander, traitsFolder + "/OneHander.asset");
        AssetDatabase.CreateAsset(finesse, traitsFolder + "/Finesse.asset");
        AssetDatabase.CreateAsset(kamikaze, traitsFolder + "/Kamikaze.asset");
        AssetDatabase.CreateAsset(heavyHanded, traitsFolder + "/HeavyHanded.asset");
        AssetDatabase.CreateAsset(fastShot, traitsFolder + "/FastShot.asset");
        AssetDatabase.CreateAsset(bloodyMess, traitsFolder + "/BloodyMess.asset");
        AssetDatabase.CreateAsset(jinxed, traitsFolder + "/Jinxed.asset");
        AssetDatabase.CreateAsset(goodNatured, traitsFolder + "/GoodNatured.asset");
        AssetDatabase.CreateAsset(chemReliant, traitsFolder + "/ÑhemReliant.asset");
        AssetDatabase.CreateAsset(chemResistant, traitsFolder + "/ÑhemResistant.asset");
        AssetDatabase.CreateAsset(sexAppeal, traitsFolder + "/SexAppeal.asset");
        AssetDatabase.CreateAsset(skilled, traitsFolder + "/Skilled.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return new List<Trait>
        {
            fastMetabolism,
            bruiser,
            smallFrame,
            gifted,
            oneHander,
            finesse,
            kamikaze,
            heavyHanded,
            fastShot,
            bloodyMess,
            jinxed,
            goodNatured,
            chemReliant,
            chemResistant,
            sexAppeal,
            skilled
        };
    }
}
