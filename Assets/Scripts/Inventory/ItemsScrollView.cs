using UnityEngine;
using UnityEngine.UI;

public class ItemsScrollView : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private Button upButton;

    [SerializeField]
    private Button downButton;

    [SerializeField]
    private float scrollStep = 0.1f;

    void Start()
    {
        upButton.onClick.AddListener(ScrollUp);
        downButton.onClick.AddListener(ScrollDown);
        UpdateButtonsInteractivity();
    }

    private void ScrollUp()
    {
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(
            scrollRect.verticalNormalizedPosition + scrollStep);

        UpdateButtonsInteractivity();
    }

    private void ScrollDown()
    {
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(
            scrollRect.verticalNormalizedPosition - scrollStep);

        UpdateButtonsInteractivity();
    }

    private void UpdateButtonsInteractivity()
    {
        upButton.interactable = scrollRect.verticalNormalizedPosition < 0.99f;
        downButton.interactable = scrollRect.verticalNormalizedPosition > 0.01f;
    }
}
