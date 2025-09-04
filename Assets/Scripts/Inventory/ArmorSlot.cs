using Armors;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmorSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private ChosenOneController chosenOne;

    private bool noItem = false;

    void Start()
    {
        SlotItem slotItem = GetComponentInChildren<SlotItem>();
        if (slotItem != null)
        {
            noItem = false;
        }
    }

    void Update()
    {
        SlotItem slotItem = GetComponentInChildren<SlotItem>();
        if (slotItem == null && !noItem)
        {
            chosenOne.ChangeArmor(null);
            noItem = true;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            SlotItem slotItem = dropped.GetComponent<SlotItem>();
            if (slotItem.Item is Armor armor)
            {
                slotItem.parentAfterDrag = transform;
                chosenOne.ChangeArmor(armor);
                noItem = false;
            }
        }
    }

    public Armor GetArmor()
    {
        SlotItem slotItem = GetComponentInChildren<SlotItem>();
        if (slotItem != null)
        {
            return (Armor)slotItem.Item;
        }
        return null;
    }
}
