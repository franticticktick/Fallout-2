using UnityEngine;
using UnityEngine.EventSystems;

public class Line : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private string text;

    [SerializeField]
    private int nodeId;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public string Text { get => text; set => text = value; }
    public string Text1 { get => text; set => text = value; }
}
