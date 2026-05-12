using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Line : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private DialogWindow window;

    [SerializeField]
    private DialogNode nextNode;

    [SerializeField]
    private TMP_Text field;

    [SerializeField]
    private string afterDialogAction;

    private void Start()
    {
        field = GetComponent<TMP_Text>();
    }

    public void AddTextAndNode(DialogNode.KeyValuePair pair)
    {
        field.text = pair.GetText();
        nextNode = pair.GetNode();
        afterDialogAction = pair.GetActionName();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (nextNode == null)
        {
            window.EndDialog(afterDialogAction);
        }
        else
        {
            window.AddNodeAndShow(nextNode);
        }
    }

    public void ClearText()
    {
        field.text = string.Empty;
    }

    private void SetFieldColor(string color)
    {
        if (field != null)
        {
            if (ColorUtility.TryParseHtmlString(color, out Color parsedColor))
            {
                field.color = parsedColor;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetFieldColor("#E3E195");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetFieldColor("#08FF4E");
    }
}
