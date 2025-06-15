using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private ItemModel[] items;
    [SerializeField] private Image inventoryBar;

    
    private int _selectedIndex = -1;
    
    private readonly Color _selectedItemColor = new Color(0, 0, 0, 255);
    private readonly Color _normalColor =  new Color(255, 255, 255, 255);

    public bool IsAnySlotSelected => _selectedIndex != -1;

    public void Awake()
    {
        items = new ItemModel[10];
        Instance = this;
    }


    public ItemModel GetSelectedItem()
    {
        if (!IsAnySlotSelected)
        {
            return null;
        }

        return items[_selectedIndex];
    }

    public void SetActive(bool value)
    {
        inventoryBar.gameObject.SetActive(value);
    }
    
    public void Add(ItemModel item)
    {

        var slotImage = inventoryBar.transform.GetChild(GetFreeSlotIndex()).GetChild(0).GetComponent<Image>();
        items[GetFreeSlotIndex()] = item;
        slotImage.color = _normalColor;
        GameManager.Instance.StartCoroutine(Animation.FadeImage(slotImage, false, 12));
        slotImage.sprite = item.sprite;
    }

    private int GetFreeSlotIndex()
    {
        for (int i = 0; i < items.Length; i++)
        {
            var itemModel = items[i];
            if (itemModel == null)
            {
                return i;
            }
        }
        return -1;
    }

    private int GetItemIndex(ItemModel item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var itemModel = items[i];
            if (itemModel == null) continue;
            
            if (itemModel.id == item.id)
            {
                return i;
            }
        }
        return -1;
    }
    
    public IEnumerator Remove(ItemModel item)
    {
        var itemIndex = GetItemIndex(item);
        var slotImage = inventoryBar.transform.GetChild(itemIndex).GetChild(0).GetComponent<Image>();

        yield return GameManager.Instance.StartCoroutine(Animation.FadeImage(slotImage, true, 12));
        
        slotImage.sprite = null;
        slotImage.color = new Color(56, 56, 56, 0);
        items[itemIndex] = null;

    }

    public void SelectItem(int slotId)
    {
        var slot = inventoryBar.transform.GetChild(slotId).transform.GetChild(0).GetComponent<Image>();
        if (slot.sprite == null)
        {
            return;
        }
        
        if (_selectedIndex != -1)
        {
            var prevSlot = inventoryBar.transform.GetChild(_selectedIndex).transform.GetChild(0).GetComponent<Image>();
            prevSlot.color = _normalColor;
        }
        
        _selectedIndex = slotId;
        GameManager.Instance.StartCoroutine(SelectItemCoroutine(slot));
    }

    private IEnumerator SelectItemCoroutine(Image slot)
    {

        yield return null;
        slot.color = _selectedItemColor;
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse0));
        yield return new WaitWhile(() => GameManager.Instance.GameIsBusy);
        slot.color = _normalColor;
        _selectedIndex = -1;
    }

}
