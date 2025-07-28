using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private ItemModel[] items = new ItemModel[10];
        [SerializeField] private Transform inventoryBar;
        private readonly Color _normalColor = new(255, 255, 255, 255);

        private readonly Color _selectedItemColor = new(0, 0, 0, 255);


        private int _selectedIndex = -1;

        private Transform[] _slots;

        public bool IsAnySlotSelected => _selectedIndex != -1;

        private void Start()
        {
            var slotCount = inventoryBar.childCount;
            _slots = new Transform[slotCount];

            for (var i = 0; i < slotCount; i++) _slots[i] = inventoryBar.GetChild(i);
        }


        public ItemModel GetSelectedItem()
        {
            if (!IsAnySlotSelected) return null;

            return items[_selectedIndex];
        }

        public void SetActive(bool value)
        {
            inventoryBar.gameObject.SetActive(value);
        }
        
        public void SetImageAcitve(bool value)
        {
            inventoryBar.gameObject.GetComponent<Image>().enabled = value;
        }

        private Image GetSlotImage(int ind)
        {
            return _slots[ind].GetChild(0).GetComponent<Image>();
        }

        public void Add(ItemModel item)
        {
            var slotInd = GetFreeSlotIndex();
            var slot = _slots[slotInd];
            var slotImage = slot.GetChild(0).GetComponent<Image>();
            slot.transform.gameObject.SetActive(true);
            items[slotInd] = item;
            slotImage.sprite = item.sprite;
            slotImage.color = _normalColor;
            SetImageAcitve(true);
        }

        private int GetFreeSlotIndex()
        {
            for (var i = 0; i < items.Length; i++)
            {
                var itemModel = items[i];
                if (itemModel == null) return i;
            }
            return -1;
        }

        private int GetItemIndex(ItemModel item)
        {
            for (var i = 0; i < items.Length; i++)
            {
                var itemModel = items[i];
                if (itemModel == null) continue;

                if (itemModel.id == item.id) return i;
            }

            return -1;
        }

        public IEnumerator Remove(ItemModel item)
        {
            var itemIndex = GetItemIndex(item);
            var slot = inventoryBar.GetChild(itemIndex);
            var slotImage = inventoryBar.GetChild(itemIndex).GetChild(0).GetComponent<Image>();
            yield return GameManager.Instance.StartCoroutine(CustomAnimation.FadeImage(slotImage, true, 12));
            slot.transform.gameObject.SetActive(false);

            slotImage.sprite = null;
            slotImage.color = new Color(56, 56, 56, 0);
            slotImage.enabled = false;
            items[itemIndex] = null;

            if (GetFreeSlotIndex() == 0)
            {
                SetImageAcitve(false);
            }
        }

        public void SelectSlot(int slotId)
        {
            if (GameManager.Instance.GameStateManager.GameState == GameState.UsingItem) return;

            var item = GetSlotImage(slotId);
            if (item == null) return;

            if (_selectedIndex != -1)
            {
                var prevSlot = GetSlotImage(_selectedIndex);
                prevSlot.color = _normalColor;
            }

            _selectedIndex = slotId;
            GetSlotImage(slotId).color = _selectedItemColor;
            GameManager.Instance.GameStateManager.SetUsingItem();
            GameManager.Instance.StartCoroutine(SelectItemCoroutine());
        }

        private IEnumerator SelectItemCoroutine()
        {
            yield return new WaitUntil(() => GameManager.Instance.GameStateManager.GameState != GameState.UsingItem);
            GetSlotImage(_selectedIndex).color = _normalColor;
            Unselect();
        }

        private void Unselect()
        {
            if (_selectedIndex == -1) return;
            GetSlotImage(_selectedIndex).color = _normalColor;
            _selectedIndex = -1;
        }
    }
}