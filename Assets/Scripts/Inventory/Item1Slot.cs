using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Weapons;

public class Item1Slot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private ChosenOneController chosenOne;

    [SerializeField]
    private Image imageActionSlot;

    private bool noItem = false;

    public void SetImageAction(Item item)
    {
        Sprite newSprite = Sprite.Create(item.GetImage(),
            new Rect(0, 0, item.GetImage().width,
            item.GetImage().height),
            new Vector2(0.5f, 0.5f), 100f);

        if (imageActionSlot != null)
        {
            imageActionSlot.gameObject.SetActive(true);
            imageActionSlot.sprite = newSprite;
        }
        noItem = false;
    }

    void Update()
    {
        SlotItem slotItem = GetComponentInChildren<SlotItem>();
        if (slotItem == null && !noItem)
        {
            chosenOne.ChangeWeapon(null);
            noItem = true;
            if (imageActionSlot != null)
            {
                imageActionSlot.gameObject.SetActive(false);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            SlotItem slotItem = dropped.GetComponent<SlotItem>();
            if (slotItem.Item is Weapon weapon)
            {
                slotItem.parentAfterDrag = transform;
                chosenOne.ChangeWeapon(weapon);
                noItem = false;

                if (imageActionSlot != null)
                {
                    imageActionSlot.gameObject.SetActive(true);
                    Sprite newSprite = Sprite.Create(weapon.Image,
                        new Rect(0, 0, weapon.Image.width, weapon.Image.height),
                        new Vector2(0.5f, 0.5f), 100f);

                    imageActionSlot.sprite = newSprite;
                }
            }
        }
    }

    public ActiveItem GetActiveItem()
    {
        SlotItem slotItem = GetComponentInChildren<SlotItem>();
        if (slotItem != null)
        {
            return (ActiveItem)slotItem.Item;
        }
        return null;
    }

    public Item GetItem()
    {
        SlotItem slotItem = GetComponentInChildren<SlotItem>();
        if (slotItem != null)
        {
            return (Item)slotItem.Item;
        }
        return null;
    }
}
