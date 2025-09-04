using ModestTree;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private CharacterPanel characterPanel;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip pressInvButton;

    [SerializeField]
    private AudioClip openInv;

    [SerializeField]
    private AudioClip done;

    [SerializeField]
    private Image interfaceSlot;

    [SerializeField]
    private ChosenOneController chosenOne;

    [SerializeField]
    private Image inventoryPanel;

    [SerializeField]
    private List<Slot> slots = new();

    [SerializeField]
    private ArmorSlot armorSlot;

    [SerializeField]
    private Item1Slot item1Slot;

    void Start()
    {
        InitWeaponImage();
        audioSource = GetComponent<AudioSource>();
        InitListeners();
        InitItem1Image();
    }

    private void InitItem1Image()
    {
        if (item1Slot != null)
        {
            var item = item1Slot.GetItem();
            if (item != null)
            {
                item1Slot.SetImageAction(item);
            }
        }
    }

    private void InitWeaponImage()
    {
        var weapon = chosenOne.GetCharacter().Weapon;
        if (weapon != null)
        {
            interfaceSlot.gameObject.SetActive(true);
            Sprite newSprite = Sprite.Create(weapon.Image,
                new Rect(0, 0, weapon.Image.width, weapon.Image.height),
                new Vector2(0.5f, 0.5f), 100f);
            interfaceSlot.sprite = newSprite;
        }
        else
        {
            interfaceSlot.gameObject.SetActive(false);
        }
    }

    private void InitListeners()
    {
        foreach (var slot in slots)
        {
            slot.OnDropItem.AddListener(OnDropItem);
        }
        var items = GetItems();
        foreach (var item in items)
        {
            item.OnEndDragItem.AddListener(OnEndDragItem);
        }
        if (armorSlot != null)
        {
            SlotItem item = armorSlot.GetComponentInChildren<SlotItem>();
            if (item != null)
            {
                item.OnEndDragItem.AddListener(OnEndDragItem);
            }
        }
        if (item1Slot != null)
        {
            SlotItem item = item1Slot.GetComponentInChildren<SlotItem>();
            if (item != null)
            {
                item.OnEndDragItem.AddListener(OnEndDragItem);
            }
        }
    }

    private void ShiftSlots()
    {
        var firstSlotIndex = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            SlotItem item = slot.GetComponentInChildren<SlotItem>();
            if (item == null)
            {
                firstSlotIndex = i;
                break;
            }
        }

        for (int i = firstSlotIndex + 1; i < slots.Count; i++)
        {
            var slot = slots[i];
            SlotItem item = slot.GetComponentInChildren<SlotItem>();
            if (item != null)
            {
                item.transform.SetParent(slots[i - 1].transform);
            }
            else
            {
                break;
            }
        }
    }

    public void Show()
    {
        if (audioSource != null)
        {
            if (pressInvButton != null)
            {
                PlayAudioClipOneShot(pressInvButton);
            }
            if (openInv != null)
            {
                PlayAudioClipOneShot(openInv);
            }
            if (inventoryPanel != null)
            {
                inventoryPanel.gameObject.SetActive(true);
            }
        }
    }

    public void Disable()
    {
        if (audioSource != null)
        {
            if (done != null)
            {
                PlayAudioClipOneShot(done);
            }
            if (inventoryPanel != null)
            {
                inventoryPanel.gameObject.SetActive(false);
            }
        }
    }

    private void PlayAudioClipOneShot(AudioClip audioClip)
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void OnDropItem(SlotItem slotItem)
    {
        Slot parentSlot = slotItem.GetPatentSlot();
        if (parentSlot == null)
        {
            var items = GetItems();
            if (!items.IsEmpty())
            {
                for (int i = items.Count; i > 0; i--)
                {
                    var slot = slots[i];
                    var item = items[i - 1];
                    item.transform.SetParent(slot.transform);
                }
            }

            var firstSlot = slots.First();
            slotItem.parentAfterDrag = firstSlot.transform;
        }
    }

    private List<SlotItem> GetItems()
    {
        return slots.Select(s =>
        {
            SlotItem item = s.GetComponentInChildren<SlotItem>();
            return item;
        })
        .Where(item => item != null)
        .ToList();
    }

    public void OnEndDragItem(bool dragged)
    {
        if (dragged)
        {
            ShiftSlots();
        }
    }

    public Item GetItemByType<T>() where T : Item
    {
        var items = GetItems();
        if (items != null && items.Count > 0)
        {
            return (Item)items.FirstOrDefault(item => item.Item is T)
                .Item;
        }
        return default;
    }

    public List<T> GetItemsByType<T>() where T : Item
    {
        var items = GetItems();
        if (items != null && items.Count > 0)
        {
            return items.Where(item => item.Item is T)
                .Select(item => item.Item)
                .Cast<T>()
                .ToList();
        }
        return default;
    }
}
