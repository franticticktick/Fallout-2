using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private ScriptableObject item;

    public Image itemImage;

    [HideInInspector]
    public Transform parentAfterDrag;

    [SerializeField]
    private AudioClip startDragClip;

    [SerializeField]
    private AudioClip endDragClip;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private TMP_Text quantity;

    public UnityEvent<bool> OnEndDragItem;

    public ScriptableObject Item { get => item; }

    void Start()
    {
        if (quantity != null)
        {
            if (item != null && item is Item inventotyItem)
            {
                quantity.text = inventotyItem.GetQuantity().ToString();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PlayAudioClipOneShot(startDragClip);
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        itemImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlayAudioClipOneShot(endDragClip);
        transform.SetParent(parentAfterDrag);
        itemImage.raycastTarget = true;
        OnEndDragItem.Invoke(true);
    }

    private void PlayAudioClipOneShot(AudioClip audioClip)
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public Slot GetPatentSlot()
    {
        if (parentAfterDrag != null)
        {
            return parentAfterDrag.GetComponent<Slot>();
        }
        return null;
    }
}
