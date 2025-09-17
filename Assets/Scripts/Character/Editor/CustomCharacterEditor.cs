using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Character))]
public class CustomCharacterEditor : Editor
{
    [SerializeField]
    private string trait = "";

    [SerializeField]
    private string mainSkill = "";

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Enter trait:");
        trait = EditorGUILayout.TextField(trait);

        GUILayout.Label("Enter main skill:");
        mainSkill = EditorGUILayout.TextField(mainSkill);

        DrawDefaultInspector();

        Character character = (Character)target;
        if (GUILayout.Button("Increment Strenght"))
        {
            character.IncrementStrenght(1);
        }
        if (GUILayout.Button("Decrement Strenght"))
        {
            character.IncrementStrenght(-1);
        }
        if (GUILayout.Button("Increment Agility"))
        {
            character.IncrementAgility(1);
        }
        if (GUILayout.Button("Decrement Agility"))
        {
            character.IncrementAgility(-1);
        }
        if (GUILayout.Button("Increment Endurance"))
        {
            character.IncrementEndurance(1);
        }
        if (GUILayout.Button("Decrement Endurance"))
        {
            character.IncrementEndurance(-1);
        }
        if (GUILayout.Button("Increment Luck"))
        {
            character.IncrementLuck(1);
        }
        if (GUILayout.Button("Decrement Luck"))
        {
            character.IncrementLuck(-1);
        }
        if (GUILayout.Button("Increment Intelligence"))
        {
            character.IncrementIntelligence(1);
        }
        if (GUILayout.Button("Decrement Intelligence"))
        {
            character.IncrementIntelligence(-1);
        }
        if (GUILayout.Button("Increment Perception"))
        {
            character.IncrementPerception(1);
        }
        if (GUILayout.Button("Decrement Perception"))
        {
            character.IncrementPerception(-1);
        }
        if (GUILayout.Button("Increment Charisma"))
        {
            character.IncrementCharisma(1);
        }
        if (GUILayout.Button("Decrement Charisma"))
        {
            character.IncrementCharisma(-1);
        }
        if (GUILayout.Button("Increment Action Points"))
        {
            character.IncreaseActionPoints(1);
        }
        if (GUILayout.Button("Decrement Action Points"))
        {
            character.IncreaseActionPoints(-1);
        }

        if (GUILayout.Button("Init Stats"))
        {
            character.InitStats();
            character.InitSkills();
        }

        if (GUILayout.Button("Chose Trait"))
        {
            character.ChoseTrait(trait);
        }

        if (GUILayout.Button("Unchose Trait"))
        {
            character.CancelTrait(trait);
        }

        if (GUILayout.Button("Mark skill as main"))
        {
            character.MarkSkillAsMain(mainSkill);
        }

        if (GUILayout.Button("Mark skill as not main"))
        {
            character.MarkSkillAsNotMain(mainSkill);
        }

        if (GUILayout.Button("Increment Skill Points"))
        {
            character.IncrementSkillPoints();
        }

        EditorUtility.SetDirty(character);
        serializedObject.ApplyModifiedProperties();
    }
}