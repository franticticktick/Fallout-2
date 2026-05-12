using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogNode", menuName = "DialogNode", order = 1)]
public class DialogNode : ScriptableObject
{

    [SerializeField]
    private string text;

    [SerializeField]
    private List<KeyValuePair> variants = new();

    [SerializeField]
    private Sprite[] frames;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private Sprite background;

    public string Text { get => text; }
    public AudioClip AudioClip { get => audioClip; }
    public Sprite[] Frames { get => frames; }
    public Sprite Background { get => background; }

    public List<KeyValuePair> GetLVariants() => variants;

    [System.Serializable]
    public class KeyValuePair
    {
        public string actionName;

        public string text;

        public DialogNode node;

        public DialogNode GetNode()
        {
            return node;
        }

        public string GetText()
        {
            return text;
        }

        public string GetActionName()
        {
            return actionName;
        }
    }
}
