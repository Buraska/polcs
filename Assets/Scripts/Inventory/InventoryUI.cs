using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryBar;
        [SerializeField] private Color selectedColor = Color.black;
        [SerializeField] private Color selectedSlotColor = Color.black;
        [SerializeField] private Color normalSlotColor = Color.black;
        private Color normalIconColor = Color.white;
        public int SelectedId { get; private set; } = -1;
        private Image[] iconImages;
        private Image[] slotImages;
        private Transform[] slots;

        public bool IsAnySlotSelected => SelectedId != -1;

        private void Start()
        {
            var slotCount = inventoryBar.transform.childCount;
            slots = new Transform[slotCount];
            iconImages = new Image[slotCount];
            slotImages = new Image[slotCount];

            for (var i = 0; i < slotCount; i++)
            {
                slots[i] = inventoryBar.transform.GetChild(i);
                iconImages[i] = inventoryBar.transform.GetChild(i).GetChild(0).GetComponent<Image>();
                slotImages[i] = inventoryBar.transform.GetChild(i).GetComponent<Image>();
            }
        }

        public void SetActiveBar(bool value)
        {
            inventoryBar.gameObject.SetActive(value);
        }
        public int GetChildCount()
        {
            return inventoryBar.transform.childCount;
        }
        public void SetSlot(int index, Sprite sprite)
        {
            var slotImage = GetSlotImage(index);
            var iconImage = GetIconImage(index);
            iconImage.sprite = sprite;
            iconImage.color = normalIconColor;
            slotImage.color = normalSlotColor;
            
            slots[index].gameObject.SetActive(true);
        }

        public void UnsetSlot(int index)
        {
            var slotImage = GetIconImage(index);
            slotImage.sprite = null;
            slotImage.color = normalSlotColor;
            slots[index].gameObject.SetActive(false);
        }

        public void SelectSlot(int index)
        {
            var iconImage = GetIconImage(index);
            var slotImage = GetSlotImage(index);

            if (index != SelectedId)
            {
                Unselect();
                slotImage.color = selectedSlotColor;
                iconImage.color = selectedColor;
                SelectedId = index;
            }else Unselect();
        }

        public void Unselect()
        {
            if (SelectedId == -1)
            {
                return;
            }
            var prevIconImage = GetIconImage(SelectedId);
            var prevSlotImage = GetSlotImage(SelectedId);
            prevIconImage.color = normalIconColor;
            prevSlotImage.color = normalSlotColor;
            SelectedId = -1;
        }

        public Image GetIconImage(int index)
        {
            return iconImages[index];
        }
        
        public Image GetSlotImage(int index)
        {
            return slotImages[index];
        }
    }
}