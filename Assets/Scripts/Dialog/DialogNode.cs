using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogNode", menuName = "DialogNode", order = 1)]
public class DialogNode : ScriptableObject
{
    [SerializeField]
    private int id;

    [SerializeField]
    private string text;

    [SerializeField]
    private List<DialogNode> nodes = new();

    [SerializeField]
    private List<KeyValuePair> lines = new();

    public DialogNode GetNodeById(int id) =>
        nodes.Where(n => n.id == id).FirstOrDefault();

    public List<KeyValuePair> GetLines() => lines;

    [System.Serializable]
    public class KeyValuePair
    {
        public int key;
        public string value;
    }
}
