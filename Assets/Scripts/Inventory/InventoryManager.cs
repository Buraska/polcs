using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private ItemModel[] items = new ItemModel[10];
        [SerializeField] private InventoryUI _inventoryUI;




        private void Start()
        {
            var slotCount = _inventoryUI.GetChildCount();
        }


        public ItemModel GetSelectedItem()
        {
            if (!_inventoryUI.IsAnySlotSelected) return null;

            return items[_inventoryUI.SelectedId];
        }

        public void SetActive(bool value)
        {
            _inventoryUI.SetActiveBar(value);
        }
        


        public void Add(ItemModel item)
        {
            var slotInd = GetFreeSlotIndex();
            _inventoryUI.SetSlot(slotInd, item.sprite);
            items[slotInd] = item;
            _inventoryUI.SetActiveBar(true);
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
            _inventoryUI.UnsetSlot(itemIndex);

            items[itemIndex] = null;

            if (GetFreeSlotIndex() == 0)
            {
                _inventoryUI.SetActiveBar(false);
            }
            yield break;
        }

        public void SelectSlot(int slotId)
        {
            if (GameManager.Instance.GameStateManager.GameState == GameState.UsingItem) return;

            var item = items[slotId];
            if (item == null) return;

            _inventoryUI.SelectSlot(slotId);
            
            GameManager.Instance.GameStateManager.SetUsingItem();
            GameManager.Instance.StartCoroutine(SelectItemCoroutine());
        }

        private IEnumerator SelectItemCoroutine()
        {
            yield return new WaitUntil(() => GameManager.Instance.GameStateManager.GameState != GameState.UsingItem);
            _inventoryUI.Unselect();
        }

    }
}