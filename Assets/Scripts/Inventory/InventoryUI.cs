using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Image inventoryBar;
        [SerializeField] private Color selectedColor = Color.black;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color emptyColor = new Color(56, 56, 56, 0);
        private Image[] slotImages;
        private int _selectedId = -1;
        
        public bool IsAnySlotSelected => _selectedId != -1;

        private void Start()
        {
            var slotCount = inventoryBar.transform.childCount;
            slotImages = new Image[slotCount];

            for (var i = 0; i < slotCount; i++)
            {
                slotImages[i] = inventoryBar.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            }
        }

        public void SetActive(bool value)
        {
            inventoryBar.gameObject.SetActive(value);
        }

        public void SetSlot(int index, Sprite sprite, bool visible)
        {
            var slotImage = GetSlotImage(index);
            slotImage.sprite = sprite;
            slotImage.color = visible ? normalColor : emptyColor;
        }

        public void SelectSlot(int index)
        {
            var slotImage = GetSlotImage(index);
            if (index == _selectedId)
            {
                slotImage.color = normalColor;
                _selectedId = -1;
            }
            else
            {
                slotImage.color = selectedColor;
                _selectedId = index;
            }

        }

        public Image GetSlotImage(int index)
        {
            return slotImages[index];
        }
    }
}