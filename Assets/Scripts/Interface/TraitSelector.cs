using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TraitSelector : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text trait;

    private CharacterWindow characterWindow;

    void Start()
    {
        trait = GetComponent<TMP_Text>();
        characterWindow = GetComponentInParent<CharacterWindow>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (trait != null)
        {
            if (characterWindow != null)
            {
                characterWindow.SelectTrait(trait);
            }
        }
    }
}
