using TMPro;
using UnityEngine;

public class DialogWindow : MonoBehaviour
{
    [SerializeField]
    private DialogNode node;

    [SerializeField]
    private TMP_Text lines;

    private void Start()
    {
        if (node != null && lines != null)
        {
            node.GetLines().ForEach(line => lines.text += line.value);
        }
    }
}
