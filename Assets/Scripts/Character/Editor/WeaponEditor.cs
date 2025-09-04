using UnityEditor;
using UnityEngine;
using Weapons;

public class WeaponEditor : EditorWindow
{
    private string weaponFolder = "Assets";
    private bool withStore = false;

    [MenuItem("Window/WeaponEditor")]
    public static void ShowWindow()
    {
        GetWindow<WeaponEditor>("WeaponEditor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Enter Weapons folder:");
        weaponFolder = EditorGUILayout.TextField(weaponFolder);
        withStore = EditorGUILayout.Toggle("Need store", withStore);

        if (GUILayout.Button("Create New"))
        {
            Weapon weapon = ScriptableObject.CreateInstance<Weapon>();
            AssetDatabase.CreateAsset(weapon, weaponFolder + "/Weapon.asset");

            if(withStore)
            {
                WeaponStore store = ScriptableObject.CreateInstance<WeaponStore>();
                weapon.Store = store;
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }
}
