using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private ItemModel[] items = new ItemModel[10];
        [SerializeField] private Image inventoryBar;

    
        private int _selectedIndex = -1;
    
        private readonly Color _selectedItemColor = new Color(0, 0, 0, 255);
        private readonly Color _normalColor =  new Color(255, 255, 255, 255);

        public bool IsAnySlotSelected => _selectedIndex != -1;
        
        private Image[] _slotImages;

        private void Start()
        {
            var slotCount = inventoryBar.transform.childCount;
            _slotImages = new Image[slotCount];

            for (var i = 0; i < slotCount; i++)
            {
                _slotImages[i] = inventoryBar.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            }
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
            var slotInd = GetFreeSlotIndex();
            var slotImage = _slotImages[slotInd];
            items[slotInd] = item;
            slotImage.sprite = item.sprite;
            slotImage.color = _normalColor;
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

            yield return GameManager.Instance.StartCoroutine(CustomAnimation.FadeImage(slotImage, true, 12));
        
            slotImage.sprite = null;
            slotImage.color = new Color(56, 56, 56, 0);
            items[itemIndex] = null;

        }

        public void SelectSlot(int slotId)
        {
            if (GameManager.Instance.GameStateManager.GameState == GameState.UsingItem) return;

            var item = items[slotId];
            if (item == null)
            {
                return;
            }
        
            if (_selectedIndex != -1)
            {
                var prevSlot = _slotImages[_selectedIndex];
                prevSlot.color = _normalColor;
            }

            _selectedIndex = slotId;
            _slotImages[slotId].color = _selectedItemColor;
            GameManager.Instance.GameStateManager.SetUsingItem();
            GameManager.Instance.StartCoroutine(SelectItemCoroutine());
        }

        private IEnumerator SelectItemCoroutine()
        {
            yield return new WaitUntil(() => GameManager.Instance.GameStateManager.GameState != GameState.UsingItem);
            _slotImages[_selectedIndex].color = _normalColor;
            Unselect();
        }

        private void Unselect()
        {
            if (_selectedIndex == -1) return;
            _slotImages[_selectedIndex].color = _normalColor;
            _selectedIndex = -1;
        }

    }
}