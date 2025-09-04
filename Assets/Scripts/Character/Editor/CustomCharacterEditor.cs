using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Character))]
public class CustomCharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
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

        EditorUtility.SetDirty(character);
        serializedObject.ApplyModifiedProperties();
    }
}