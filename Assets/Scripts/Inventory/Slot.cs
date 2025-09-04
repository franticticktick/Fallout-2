using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public UnityEvent<SlotItem> OnDropItem;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        SlotItem slotItem = dropped.GetComponent<SlotItem>();
        if (slotItem != null)
        {
            OnDropItem.Invoke(slotItem);
        }
    }
}
